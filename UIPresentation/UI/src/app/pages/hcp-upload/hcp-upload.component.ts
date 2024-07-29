import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { HcpUploadService } from 'src/app/services/hcp-upload/hcp-upload.service';
import { HcpService } from 'src/app/services/hcp/hcp.service';
import { PaymentRequestService } from 'src/app/services/payment-requests/payment-request.service';
import { CorporateService } from 'src/app/services/corporate/corporate.service';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';
import { MoneyTypeEnum } from 'src/app/enums/money-type-enum.enum';
import { StatusEnum } from 'src/app/enums/status-enum.enum';

import { CorporateGetListResponseModel } from 'src/app/models/corporate-models/corporate-get-list-response-model';
import { PaymentRequestGetListByCorporateIdResponseModel } from 'src/app/models/payment-request-models/payment-request-get-list-by-corporate-id-response-model';
import { HcpUploadSearchRequestModel } from 'src/app/models/hcp-upload-models/hcp-upload-serach-request-model';
import { HcpUploadSearchResponseModel } from 'src/app/models/hcp-upload-models/hcp-upload-search-response-model';
import { HcpRequestModel } from 'src/app/models/hcp-models/hcp-request-model';

@Component({
  selector: 'app-hcp-upload',
  templateUrl: './hcp-upload.component.html',
  styleUrls: ['./hcp-upload.component.css'],
  providers: [
    CorporateService,
    PaymentRequestService,
    HcpUploadService,
    OrderPipe,
    HcpService]
})

export class HcpUploadComponent implements OnInit {

  constructor(
    private hcpUploadService: HcpUploadService,
    private hcpService: HcpService,
    private corporateService: CorporateService,
    private paymentRequestService: PaymentRequestService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private orderPipe: OrderPipe,

  ) { }

  moneyTypeEnum = MoneyTypeEnum
  statusEnum = StatusEnum

  corporateIdDefault = -1
  corporateGetListResponseModels!: CorporateGetListResponseModel[]

  requestNumberDefault = -1
  paymentRequestGetListByCorporateIdResponseModels!: PaymentRequestGetListByCorporateIdResponseModel[]

  searchForm!: FormGroup;

  hcpUploadSearchRequestModel!: HcpUploadSearchRequestModel

  hcpUploadSearchResponseModels!: HcpUploadSearchResponseModel[]

  currentPage: number = 1;
  itemsPerPage: number = 50

  selectedCorporateId!: number

  selectedRequestNumber!: string

  selectedStartDate!: Date

  selectedEndDate!: Date

  hcpRequestModel: HcpRequestModel = new HcpRequestModel()

  ngOnInit() {
    this.createSearchForm();
    this.getCorporates();
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      corporateId: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
      requestNumber: [''],
      startDate: [''],
      endDate: [''],
    });
  }

  getCorporates() {
    this.corporateService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.corporateGetListResponseModels = dataResult.data
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    })
  }

  getRequestNumbersByCorporateId(event: any) {
    this.selectedCorporateId = event.currentTarget.value
    this.paymentRequestService.getListByCorporateId(this.selectedCorporateId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.paymentRequestGetListByCorporateIdResponseModels = dataResult.data
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    })
  }

  setRequestNumber(event: any) {
    this.selectedRequestNumber = event.currentTarget.value
  }

  setStartDate(event: any) {
    this.selectedStartDate = event.currentTarget.value
  }

  setEndDate(event: any) {
    this.selectedEndDate = event.currentTarget.value
  }

  search() {
    this.hcpUploadSearchRequestModel = new HcpUploadSearchRequestModel()
    this.hcpUploadSearchRequestModel.corporateId = Number(this.selectedCorporateId)
    this.hcpUploadSearchRequestModel.requestNumber = this.selectedRequestNumber
    this.hcpUploadSearchRequestModel.startDate = this.selectedStartDate
    this.hcpUploadSearchRequestModel.endDate = this.selectedEndDate

    this.hcpUploadService.search(this.hcpUploadSearchRequestModel).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.hcpUploadSearchResponseModels = dataResult.data
          }
        }
      }
    })
  }

  clear() {
    this.searchForm.controls["corporateId"].setValue(-1)
    this.searchForm.controls["requestNumber"].setValue(-1)
    this.searchForm.controls["startDate"].setValue('')
    this.searchForm.controls["endDate"].setValue('')
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.hcpUploadSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.hcpUploadSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  downloadFileByHcpId(hcpId: string) {
    this.hcpRequestModel.fileName = hcpId
    this.hcpService.downloadFile(this.hcpRequestModel).subscribe(response => {
      let dataType = response.type;
      let binaryData = [];
      binaryData.push(response);
      let downloadLink = document.createElement('a');
      downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, {type: dataType}));
      if ( this.hcpRequestModel.fileName)
          downloadLink.setAttribute('download',  this.hcpRequestModel.fileName);
      document.body.appendChild(downloadLink);
      downloadLink.click();
    })
  }

}

