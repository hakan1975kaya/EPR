import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MailAddressService } from 'src/app/services/mail-address/mail-address.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { MailAddressSearchRequestModel } from 'src/app/models/mail-address-models/mail-address-serach-request-model';
import { MailAddressSearchResponseModel } from 'src/app/models/mail-address-models/mail-address-search-response-model';
import { MailAddressSaveRequestModel } from 'src/app/models/mail-address-models/mail-address-save-request-model';

@Component({
  selector: 'app-mail-address',
  templateUrl: './mail-address.component.html',
  styleUrls: ['./mail-address.component.css'],
  providers: [MailAddressService]
})
export class MailAddressComponent implements OnInit {
  constructor(
    private mailAddressService: MailAddressService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }
  currentUserId!: number;

  searchForm!: FormGroup;
  mailAddressSearchRequestModel!: MailAddressSearchRequestModel;
  mailAddressSearchResponseModels: MailAddressSearchResponseModel[] = [];

  selectedMailAddressId!: number;

  selectedIsCC!: boolean
  selectedIsPtt!: boolean

  fileName = 'MailAddress';

  currentPage: number = 1;
  itemsPerPage: number = 50

  mailAddressForm!: FormGroup;
  displayMailAddressModal = 'none';

  isCCDefault = -1
  isPttDefault = -1

  mailAddressSaveRequestModel: MailAddressSaveRequestModel = new MailAddressSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'

  ngOnInit() {
    this.createSearchForm();
    this.createMailAddressForm();
    this.MailAddressReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.mailAddressSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.mailAddressSearchRequestModel) {
        this.mailAddressService
          .search(this.mailAddressSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.mailAddressSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.mailAddressSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.mailAddressSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(mailAddressId: number) {
    this.mailAddressService.delete(mailAddressId).subscribe((result) => {
      if (result) {
        if (result.success) {
          this.alertifyService.success(result.message);
          this.search()
        } else {
          this.alertifyService.error(result.message);
        }
      }
    });
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.mailAddressSearchResponseModels);
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

  openMailAddressModal(MailAddressId?: number) {
    this.displayMailAddressModal = 'block';
    if (MailAddressId) {
      this.selectedMailAddressId = MailAddressId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(MailAddressId);
    }
    else {
      this.MailAddressReset()
    }
  }

  onMailAddressModalCloseHandled() {
    this.displayMailAddressModal = 'none';
  }

  createMailAddressForm() {
    this.mailAddressForm = this.formBuilder.group({
      id: [''],
      address: ['', [Validators.required, Validators.minLength(3)]],
      isCC: ['', [Validators.required, Validators.min(0)]],
      isPtt: ['', [Validators.required, Validators.min(0)]],
    });
  }

  getById(MailAddressId: number) {
    this.mailAddressService.getById(MailAddressId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.mailAddressForm.controls['id'].setValue(dataResult.data.id);
            this.mailAddressForm.controls['address'].setValue(dataResult.data.address);
            this.mailAddressForm.controls['isCC'].setValue(dataResult.data.isCC);
            this.mailAddressForm.controls['isPtt'].setValue(dataResult.data.isPtt);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  setIsCC(event: any) {
    if (event.target.value == "false") {
      this.selectedIsCC = false;
    }
    if (event.target.value == "true") {
      this.selectedIsCC = true
    }
  }

  setIsPtt(event: any) {
    if (event.target.value == "false") {
      this.selectedIsPtt = false;
    }
    if (event.target.value == "true") {
      this.selectedIsPtt = true
    }
  }

  save() {
    if (!this.selectedMailAddressId) {
      this.selectedMailAddressId = 0
    }

    if (this.mailAddressForm.valid) {

      this.mailAddressSaveRequestModel.mailAddress = Object.assign({}, this.mailAddressForm.value);
      if (this.mailAddressSaveRequestModel) {
        {
          this.mailAddressSaveRequestModel.mailAddress.id = this.selectedMailAddressId
          this.mailAddressSaveRequestModel.mailAddress.isCC = this.selectedIsCC
          this.mailAddressSaveRequestModel.mailAddress.isPtt = this.selectedIsPtt
          this.mailAddressSaveRequestModel.saveType = this.currentSaveType
          this.mailAddressService.save(this.mailAddressSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {
                this.alertifyService.success(result.message)
                this.search()
                this.MailAddressReset()
                this.onMailAddressModalCloseHandled()
              }
              else {
                this.alertifyService.error(result.message)
              }
            }
          })
        }
      }
    }
  }

  MailAddressReset() {
    this.mailAddressForm.controls['id'].setValue(-1);
    this.mailAddressForm.controls['address'].setValue('');
    this.mailAddressForm.controls['isCC'].setValue(-1);
    this.mailAddressForm.controls['isPtt'].setValue(-1);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedMailAddressId = 0
  }


  openConfirmModal(MailAddressId: number) {
    this.displayConfirmModal = 'block';
    this.selectedMailAddressId = MailAddressId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }

  deleteFromConfirm() {
    this.delete(this.selectedMailAddressId)
    this.onConfirmModalCloseHandled()
  }

}
