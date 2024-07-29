import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ApiLogService } from 'src/app/services/api-log/api-log.service';

import { TranslatePipe } from 'src/app/pipes/translate/translate.pipe';
import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { ApiLogSearchRequestModel } from 'src/app/models/api-log-models/api-log-serach-request-model';
import { ApiLogSearchResponseModel } from 'src/app/models/api-log-models/api-log-search-response-model';
import { ApiLogGetByIdResponseModel } from 'src/app/models/api-log-models/api-log-get-by-id-response-model';

@Component({
  selector: 'app-api-log',
  templateUrl: './api-log.component.html',
  styleUrls: ['./api-log.component.css'],
  providers: [ApiLogService, TranslatePipe],
})
export class ApiLogComponent implements OnInit {
  constructor(
    private apiLogService: ApiLogService,
    private formBuilder: FormBuilder,
    private orderPipe: OrderPipe,
    private translatePipe: TranslatePipe
  ) { }
  searchForm!: FormGroup;

  apiLogSearchRequestModel!: ApiLogSearchRequestModel;
  apiLogSearchResponseModels: ApiLogSearchResponseModel[] = [];

  currentPage: number = 1;
  itemsPerPage: number = 50

  fileName = 'ApiLog';

  auditEnum = AuditEnum
  auditDefault = 0

  selectedApiLogId!: number
  selectedAudit: AuditEnum = this.auditEnum.None

  apiLogGetByIdResponseModel!: ApiLogGetByIdResponseModel

  apiLogForm!: FormGroup;
  displayApiLogModal = 'none';


  ngOnInit() {
    this.createSearchForm();
    this.createApiLogForm();
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', Validators.required],
      audit: [''],
    });
  }

  search() {
    if (this.searchForm.valid) {

      this.apiLogSearchRequestModel = Object.assign({}, this.searchForm.value);
      this.apiLogSearchRequestModel.audit = Number(this.selectedAudit)
      if (this.apiLogSearchRequestModel) {
        this.apiLogService.search(this.apiLogSearchRequestModel).subscribe((dataResult) => {
          if (dataResult) {
            if (dataResult.success) {
              if (dataResult.data) {
                this.apiLogSearchResponseModels = dataResult.data;
              }
            }
          }
        });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.apiLogSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.apiLogSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }


  setAudit(event: any) {
    this.selectedAudit = event.currentTarget.value
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.apiLogSearchResponseModels);
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

  openApiLogModal(apiLogId: number) {
    this.displayApiLogModal = 'block';
    if (apiLogId) {
      this.selectedApiLogId = apiLogId

      this.getByApiLogId(apiLogId)
    }
  }

  getByApiLogId(apiLogId: number) {

    this.apiLogService.getById(apiLogId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.apiLogGetByIdResponseModel = dataResult.data
            if (this.apiLogGetByIdResponseModel) {
              this.apiLogForm.controls['id'].setValue(this.apiLogGetByIdResponseModel.id)
              this.apiLogForm.controls['detail'].setValue(this.apiLogGetByIdResponseModel.detail)
              this.apiLogForm.controls['date'].setValue(this.apiLogGetByIdResponseModel.date)
              this.apiLogForm.controls['audit'].setValue(this.translatePipe.transform(this.apiLogGetByIdResponseModel.audit))
            }
          }
        }
      }
    })
  }

  onApiLogModalCloseHandled() {
    this.displayApiLogModal = 'none';
  }


  createApiLogForm() {
    this.apiLogForm = this.formBuilder.group({
      id: [''],
      detail: [''],
      date: [''],
      audit: [''],
    });
  }


  ApiLogReset() {
    this.selectedApiLogId = 0
  }

  ApiLogCancel() {
    this.ApiLogReset()
    this.displayApiLogModal = 'none';
  }


}
