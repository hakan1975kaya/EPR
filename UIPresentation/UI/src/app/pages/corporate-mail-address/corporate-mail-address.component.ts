import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CorporateMailAddressService } from 'src/app/services/corporate-mail-address/corporate-mail-address.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { CorporateMailAddressSaveRequestModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-save-request-model';
import { CorporateMailAddressSearchRequestModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-serach-request-model';
import { CorporateMailAddressSearchResponseModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-search-response-model';
import { CorporateService } from 'src/app/services/corporate/corporate.service';
import { CorporateGetListResponseModel } from 'src/app/models/corporate-models/corporate-get-list-response-model';
import { MailAddressGetListResponseModel } from 'src/app/models/mail-address-models/mail-address-get-list-response-model';
import { MailAddressService } from 'src/app/services/mail-address/mail-address.service';
import { MailAddressGetListNotPttResposeModel } from 'src/app/models/mail-address-models/mail-address-get-list-not-ptt-respose-model';

@Component({
  selector: 'app-corporate-mail-address',
  templateUrl: './corporate-mail-address.component.html',
  styleUrls: ['./corporate-mail-address.component.css'],
  providers: [CorporateMailAddressService, CorporateService,MailAddressService]
})
export class CorporateMailAddressComponent implements OnInit {
  constructor(
    private corporateMailAddressService: CorporateMailAddressService,
    private corporateService: CorporateService,
    private mailAddressService:MailAddressService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }
  currentUserId!: number;

  searchForm!: FormGroup;
  corporateMailAddressSearchRequestModel!: CorporateMailAddressSearchRequestModel;
  corporateMailAddressSearchResponseModels: CorporateMailAddressSearchResponseModel[] = [];

  selectedCorporateMailAddressId!: number;

  fileName = 'CorporateMailAddress';

  currentPage: number = 1;
  itemsPerPage: number = 50

  corporateMailAddressForm!: FormGroup;
  displayCorporateMailAddressModal = 'none';

  corporateIdDefault = -1
  mailAddressIdDefault =-1

  corporateGetListResponseModels!: CorporateGetListResponseModel[]

  mailAddressGetListNotPttResposeModels!:MailAddressGetListNotPttResposeModel[]

  corporateMailAddressSaveRequestModel: CorporateMailAddressSaveRequestModel = new CorporateMailAddressSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'


  ngOnInit() {
    this.getCorporates();
    this.getListNotPtt();
    this.createSearchForm();
    this.createCorporateMailAddressForm();
    this.CorporateMailAddressReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.corporateMailAddressSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.corporateMailAddressSearchRequestModel) {
        this.corporateMailAddressService
          .search(this.corporateMailAddressSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.corporateMailAddressSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.corporateMailAddressSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.corporateMailAddressSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(corporateMailAddressId: number) {
    this.corporateMailAddressService.delete(corporateMailAddressId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.corporateMailAddressSearchResponseModels);
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

  openCorporateMailAddressModal(corporateMailAddressId?: number) {
    this.displayCorporateMailAddressModal = 'block';
    if (corporateMailAddressId) {
      this.selectedCorporateMailAddressId = corporateMailAddressId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(corporateMailAddressId);
    }
    else {
      this.CorporateMailAddressReset()
    }
  }

  onCorporateMailAddressModalCloseHandled() {
    this.displayCorporateMailAddressModal = 'none';
  }

  createCorporateMailAddressForm() {
    this.corporateMailAddressForm = this.formBuilder.group({
      id: [''],
      corporateId: ['', [Validators.required, Validators.min(0)]],
      mailAddressId: ['', [Validators.required, Validators.min(0)]],
    });
  }

  getCorporates() {
    this.corporateService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.corporateGetListResponseModels = dataResult.data
          }
        }
      }
    })
  }

  getListNotPtt() {
    this.mailAddressService.getListNotPtt().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.mailAddressGetListNotPttResposeModels = dataResult.data
          }
        }
      }
    })
  }

  getById(corporateMailAddressId: number) {
    this.corporateMailAddressService.getById(corporateMailAddressId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.corporateMailAddressForm.controls['id'].setValue(dataResult.data.id);
            this.corporateMailAddressForm.controls['corporateId'].setValue(dataResult.data.corporateId);
            this.corporateMailAddressForm.controls['mailAddressId'].setValue(dataResult.data.mailAddressId);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  save() {
    if (!this.selectedCorporateMailAddressId) {
      this.selectedCorporateMailAddressId = 0
    }

    if (this.corporateMailAddressForm.valid) {

      this.corporateMailAddressSaveRequestModel.corporateMailAddress = Object.assign({}, this.corporateMailAddressForm.value);
      if (this.corporateMailAddressSaveRequestModel) {
        {
          this.corporateMailAddressSaveRequestModel.corporateMailAddress.id = this.selectedCorporateMailAddressId
          this.corporateMailAddressSaveRequestModel.saveType = this.currentSaveType
          this.corporateMailAddressService.save(this.corporateMailAddressSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {
                this.alertifyService.success(result.message)
                this.search()
                this.CorporateMailAddressReset()
                this.onCorporateMailAddressModalCloseHandled()
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

  CorporateMailAddressReset() {
    this.corporateMailAddressForm.controls['id'].setValue(-1);
    this.corporateMailAddressForm.controls['corporateId'].setValue(-1);
    this.corporateMailAddressForm.controls['mailAddressId'].setValue(-1);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedCorporateMailAddressId = 0
  }


  openConfirmModal(corporateMailAddressId: number) {
    this.displayConfirmModal = 'block';
    this.selectedCorporateMailAddressId = corporateMailAddressId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }

  deleteFromConfirm() {
    this.delete(this.selectedCorporateMailAddressId)
    this.onConfirmModalCloseHandled()
  }

}
