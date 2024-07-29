import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { WebLogService } from 'src/app/services/web-log/web-log.service';

import { TranslatePipe } from 'src/app/pipes/translate/translate.pipe';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { WebLogSearchRequestModel } from 'src/app/models/web-log-models/web-log-serach-request-model';
import { WebLogSearchResponseModel } from 'src/app/models/web-log-models/web-log-search-response-model';
import { WebLogGetByIdResponseModel } from 'src/app/models/web-log-models/web-log-get-by-id-response-model';
import { OrderPipe } from 'src/app/pipes/order/order.pipe';

@Component({
  selector: 'app-web-log',
  templateUrl: './web-log.component.html',
  styleUrls: ['./web-log.component.css'],
  providers: [WebLogService, TranslatePipe],
})
export class WebLogComponent implements OnInit {
  constructor(
    private webLogService: WebLogService,
    private formBuilder: FormBuilder,
    private orderPipe: OrderPipe,
    private translatePipe: TranslatePipe
  ) { }
  searchForm!: FormGroup;

  webLogSearchRequestModel!: WebLogSearchRequestModel;
  webLogSearchResponseModels: WebLogSearchResponseModel[] = [];

  currentPage: number = 1;
  itemsPerPage: number = 50

  fileName = 'WebLog';

  auditEnum = AuditEnum
  auditDefault = this.auditEnum.None

  selectedWebLogId!: number
  selectedAudit: AuditEnum = this.auditEnum.None

  webLogGetByIdResponseModel!: WebLogGetByIdResponseModel

  webLogForm!: FormGroup;
  displayWebLogModal = 'none';


  ngOnInit() {
    this.createSearchForm();
    this.createWebLogForm();
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', Validators.required],
      audit: [''],
    });
  }

  search() {
    if (this.searchForm.valid) {

      this.webLogSearchRequestModel = Object.assign({}, this.searchForm.value);
      this.webLogSearchRequestModel.audit = Number(this.selectedAudit)
      if (this.webLogSearchRequestModel) {
        this.webLogService.search(this.webLogSearchRequestModel).subscribe((dataResult) => {
          if (dataResult) {
            if (dataResult.success) {
              if (dataResult.data) {
                this.webLogSearchResponseModels = dataResult.data;
              }
            }
          }
        });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.webLogSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.webLogSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  setAudit(event: any) {
    this.selectedAudit = event.currentTarget.value
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.webLogSearchResponseModels);
    const wb: xlsx.WorkBook = xlsx.utils.book_new();
    xlsx.utils.book_append_sheet(wb, ws, this.fileName);
    xlsx.writeFile(wb, this.fileName + '.xlsx');
  }

  exportToPdf() {
    let data: any = document.getElementById('divSerach');
    html2canvas(data).then((canvas) => {
      let fileWidth = 208;
      let fileHeight = (canvas.height * fileWidth) / canvas.width;
      const fileUri = canvas.toDataURL('image/png');
      let pdf = new jsPDF('p', 'mm', 'a4');
      let position = 0;
      pdf.addImage(fileUri, 'Png', 0, position, fileWidth, fileHeight);
      pdf.save(this.fileName + '.pdf');
    });
  }

  openWebLogModal(webLogId: number) {
    this.displayWebLogModal = 'block';
    if (webLogId) {
      this.selectedWebLogId = webLogId

      this.getByWebLogId(webLogId)
    }
  }

  getByWebLogId(webLogId: number) {

    this.webLogService.getById(webLogId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.webLogGetByIdResponseModel = dataResult.data
            if (this.webLogGetByIdResponseModel) {
              this.webLogForm.controls['id'].setValue(this.webLogGetByIdResponseModel.id)
              this.webLogForm.controls['detail'].setValue(this.webLogGetByIdResponseModel.detail)
              this.webLogForm.controls['date'].setValue(this.webLogGetByIdResponseModel.date)
              this.webLogForm.controls['audit'].setValue(this.translatePipe.transform(AuditEnum[this.webLogGetByIdResponseModel.audit]))
            }
          }
        }
      }
    })
  }

  onWebLogModalCloseHandled() {
    this.displayWebLogModal = 'none';
  }


  createWebLogForm() {
    this.webLogForm = this.formBuilder.group({
      id: [''],
      detail: [''],
      date: [''],
      audit: [''],
    });
  }

  WebLogReset() {
    this.selectedWebLogId = 0
  }

  WebLogCancel() {
    this.WebLogReset()
    this.displayWebLogModal = 'none';
  }


}
