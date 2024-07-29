import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UUID } from 'angular2-uuid';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { TandemService } from 'src/app/services/tandem/tandem.service';
import { CorporateService } from 'src/app/services/corporate/corporate.service';
import { PaymentRequestSummaryService } from 'src/app/services/payment-request-summary/payment-request-summary.service';
import { HcpService } from 'src/app/services/hcp/hcp.service';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';
import { StatusEnum } from 'src/app/enums/status-enum.enum';

import { PaymentRequestSummarySearchRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-serach-request-model';
import { PaymentRequestSummarySearchResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-search-response-model';
import { PaymentRequestSummarySaveRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-save-request-model';
import { TandemPaymentInquiryRequestExternalModel } from 'src/app/models/tandem-models/tandem-payment-inquiry-request-external-model';
import { TandemPaymentInquiryResponseExternalModel } from 'src/app/models/tandem-models/tandem-payment-inquiry-response-external-model';
import { PaymentRequestSummary } from 'src/app/models/payment-request-summary-models/payment-request-summary';
import { RequestListExternal } from 'src/app/models/tandem-models/request-list-external';
import { TandemPaymentUpdateRequestExternalModel } from 'src/app/models/tandem-models/tandem-payment-update-request-external-model';
import { UserGetByRegistrationNumberResponseModel } from 'src/app/models/user-models/user-get-by-user-name-response-model';
import { SummaryExternal } from 'src/app/models/tandem-models/summary-external'
import { PaymentRequestSummaryGetByTodayResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-get-by-today-response-model';
import { CorporateGetByCorporateCodeResponseModel } from 'src/app/models/corporate-models/corporate-get-by-corporate-code-response-model';
import { HcpFileRequestModel } from 'src/app/models/payment-request-summary-models/hcp-file-request-model';


@Component({
  selector: 'app-payment-request-summary',
  templateUrl: './payment-request-summary.component.html',
  styleUrls: ['./payment-request-summary.component.css'],
  providers: [
    PaymentRequestSummaryService,
    CorporateService,
    TandemService,
    HcpService
  ]
})

export class PaymentRequestSummaryComponent implements OnInit {
  constructor(
    private paymentRequestSummaryService: PaymentRequestSummaryService,
    private corporateService: CorporateService,
    private tandemService: TandemService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe,
    private hcpService: HcpService
  ) { }

  userGetByRegistrationNumberResponseModel: UserGetByRegistrationNumberResponseModel = new UserGetByRegistrationNumberResponseModel()

  currentUserId!: number;

  searchForm!: FormGroup;

  paymentRequestSummarySearchRequestModel!: PaymentRequestSummarySearchRequestModel;

  paymentRequestSummarySearchResponseModels: PaymentRequestSummarySearchResponseModel[] = [];

  paymentRequestSummaryGetByTodayResponseModels: PaymentRequestSummaryGetByTodayResponseModel[] = [];

  selectedStatus!: StatusEnum

  statusDefault = -1

  corporateIdDefault = -1

  selectedCorporate: CorporateGetByCorporateCodeResponseModel = new CorporateGetByCorporateCodeResponseModel()

  fileName = 'PaymentRequestSummary';

  currentPagePaymentRequestSummary: number = 1;

  itemsPerPagePaymentRequestSummary: number = 50

  paymentRequestSummaryForm!: FormGroup;

  displaypaymentRequestSummaryModal = 'none';

  currentPageTandem: number = 1;

  itemsPerPageTandem: number = 50

  tandemPaymentInquiryRequestExternalModel: TandemPaymentInquiryRequestExternalModel = new TandemPaymentInquiryRequestExternalModel()

  tandemPaymentInquiryResponseExternalModel: TandemPaymentInquiryResponseExternalModel = new TandemPaymentInquiryResponseExternalModel()

  selectedSummaryExternals: SummaryExternal[] = []

  tandemPaymentUpdateRequestExternalModel: TandemPaymentUpdateRequestExternalModel = new TandemPaymentUpdateRequestExternalModel()

  paymentRequestSummarySaveRequestModel: PaymentRequestSummarySaveRequestModel = new PaymentRequestSummarySaveRequestModel();

  saveTypeEnum = SaveTypeEnum

  currentSaveType!: SaveTypeEnum

  statusEnum = StatusEnum

  formData!: FormData;
  isSendData: boolean = false

  hcpFileRequestModel!: HcpFileRequestModel
  hcpFileRequestModels: HcpFileRequestModel[] = []

  ngOnInit() {
    this.createSearchForm();
    this.createPaymentRequestSummaryForm();
    this.paymentRequestSummaryReset()
    this.getByToday();
    this.userGetByRegistrationNumberResponseModel = this.authService.getCurrentUser();
  }

  getByToday() {
    this.paymentRequestSummaryGetByTodayResponseModels = []
    this.paymentRequestSummarySearchResponseModels = []
    this.paymentRequestSummaryService.getByToday().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.paymentRequestSummaryGetByTodayResponseModels = dataResult.data

            if (this.paymentRequestSummaryGetByTodayResponseModels) {
              if (this.paymentRequestSummaryGetByTodayResponseModels.length > 0) {
                this.paymentRequestSummaryGetByTodayResponseModels.forEach(paymentRequestSummary => {
                  this.paymentRequestSummarySearchResponseModels.push(paymentRequestSummary)
                })
              }
            }
          }
        }
      }
    })
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.paymentRequestSummarySearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.paymentRequestSummarySearchRequestModel) {
        this.paymentRequestSummaryService
          .search(this.paymentRequestSummarySearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.paymentRequestSummarySearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.paymentRequestSummarySearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.paymentRequestSummarySearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(paymentRequestSummaryId: number) {
    this.paymentRequestSummaryService.delete(paymentRequestSummaryId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.paymentRequestSummarySearchResponseModels);
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

  openPaymentRequestSummaryModal(corporateCode?: number) {
    this.displaypaymentRequestSummaryModal = 'block';
    if (corporateCode) {
      this.currentSaveType = SaveTypeEnum.Add
      this.getByCorporateCode(corporateCode);
    }
    else {
      this.paymentRequestSummaryReset()
    }
  }

  getByCorporateCode(corporateCode: number) {
    this.corporateService.getByCorporateCode(corporateCode).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.selectedCorporate = dataResult.data
            this.getFromTandenData()
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  onPaymentRequestSummaryModalCloseHandled() {
    this.displaypaymentRequestSummaryModal = 'none';
  }

  createPaymentRequestSummaryForm() {
    this.paymentRequestSummaryForm = this.formBuilder.group({
      corporateId: ['', Validators.required],
      fileId: ['']
    });
  }

  onFileSelect(event: any) {
    this.isSendData = false
    if (event.target.files.length > 0) {

      this.isSendData = true
      this.formData = new FormData()

      this.hcpFileRequestModels = []

      for (let i = 0; i < event.target.files.length; i++) {
        this.hcpFileRequestModel = new HcpFileRequestModel()

        let hcpId = UUID.UUID()
        let explanation = event.target.files[i].name
        let extension = event.target.files[i].name.split('.').pop()

        this.hcpFileRequestModel.hcpId = hcpId
        this.hcpFileRequestModel.explanation = explanation
        this.hcpFileRequestModel.extension = extension

        this.formData.append('file', event.target.files[i], hcpId + "." + extension)
        this.formData.append('fileName', event.target.files[i], hcpId + "." + extension);

        this.hcpFileRequestModel.formData = this.formData

        this.hcpFileRequestModels.push(this.hcpFileRequestModel)
      }
    }
  }

  getFromTandenData() {
    if (this.tandemPaymentInquiryRequestExternalModel) {
      this.tandemPaymentInquiryRequestExternalModel.status = this.selectedStatus
      this.tandemPaymentInquiryRequestExternalModel.corporateCode = this.selectedCorporate.corporateCode
      this.tandemService.paymentInquiry(this.tandemPaymentInquiryRequestExternalModel).subscribe(dataResult => {
        if (dataResult) {
          if (dataResult.success) {
            if (dataResult.data) {
              if (dataResult.data.responseCode == "00") {
                this.tandemPaymentInquiryResponseExternalModel = dataResult.data
              }
              else {
                this.alertifyService.error("Tandem Sonuç Kodu: " + dataResult.data.responseCode + " Tandem Somuç Mesajı: " + dataResult.data.responseMessage)
              }
            }
          }
          else {
            this.alertifyService.error(dataResult.message)
          }
        }
      })
    }

  }

  setTandemUpdate(summaryExternal: SummaryExternal, event: any) {

    this.tandemPaymentInquiryResponseExternalModel.summaryExternals.forEach(summaryReset => {
      summaryReset.isChecked = false
    })

    this.selectedSummaryExternals.forEach(selectSummaryReset => {
      let indexSelectSummaryReset = this.selectedSummaryExternals.indexOf(selectSummaryReset)
      this.selectedSummaryExternals.splice(indexSelectSummaryReset, 1)
    })

    if (event.currentTarget.checked == true) {

      this.selectedSummaryExternals.push(summaryExternal)

      let indexSummary = this.tandemPaymentInquiryResponseExternalModel.summaryExternals.indexOf(summaryExternal)
      this.tandemPaymentInquiryResponseExternalModel.summaryExternals.forEach(summary => {
        let indexItemSummary = this.tandemPaymentInquiryResponseExternalModel.summaryExternals.indexOf(summary)
        if (indexSummary == indexItemSummary) {
          summary.isChecked = true
        }
        else {
          summary.isChecked = false
        }
      })

    }
  }

  tandemUpdate(status: StatusEnum) {
    if (this.paymentRequestSummaryForm.valid) {
      if (this.selectedSummaryExternals) {
        if (this.selectedSummaryExternals.length > 0) {

          this.tandemPaymentUpdateRequestExternalModel.requestListExternals = []

          this.selectedSummaryExternals.forEach(paymentRequest => {

            let requestListExternal: RequestListExternal = new RequestListExternal()

            requestListExternal.corporateCode = paymentRequest.corporateCode
            requestListExternal.registrationNumber = this.userGetByRegistrationNumberResponseModel.registrationNumber
            requestListExternal.requestNumber = paymentRequest.requestNumber
            requestListExternal.status = status

            this.tandemPaymentUpdateRequestExternalModel.requestListExternals.push(requestListExternal)
          })

          if (this.tandemPaymentUpdateRequestExternalModel) {
            if (this.tandemPaymentUpdateRequestExternalModel.requestListExternals) {
              if (this.tandemPaymentUpdateRequestExternalModel.requestListExternals.length > 0) {
                this.tandemService.paymentUpdate(this.tandemPaymentUpdateRequestExternalModel).subscribe(dataResult => {
                  if (dataResult) {
                    if (dataResult.success) {
                      if (dataResult.data) {
                        if (dataResult.data.responseCode == "00") {
                          this.paymentRequestSummarySave(status)
                        }
                        else {
                          this.alertifyService.error("Tandem Sonuç: " + dataResult.data.responseCode + " Tandem Mesaj: " + dataResult.data.responseMessage)
                        }
                      }
                    }
                  }
                })
              }
            }
          }
        }
      }
    }
  }

  paymentRequestSummarySave(status: StatusEnum) {
    if (this.paymentRequestSummaryForm.valid) {
      if (this.selectedSummaryExternals) {
        if (this.selectedSummaryExternals.length > 0) {

          this.paymentRequestSummarySaveRequestModel.paymentRequestSummaries = []

          this.selectedSummaryExternals.forEach(paymentRequest => {

            let paymentRequestSummary = new PaymentRequestSummary()

            paymentRequestSummary.id = 0
            paymentRequestSummary.paymentRequestId = 0
            paymentRequestSummary.isActive = true
            paymentRequestSummary.status = status
            paymentRequestSummary.amount = paymentRequest.amount
            paymentRequestSummary.quantity = paymentRequest.quantity
            paymentRequestSummary.userId = this.userGetByRegistrationNumberResponseModel.id
            paymentRequestSummary.systemEnteredDate = paymentRequest.systemEnteredDate
            paymentRequestSummary.systemEnteredRegistrationNumber = paymentRequest.systemEnteredRegistrationNumber
            paymentRequestSummary.systemEnteredTime = paymentRequest.systemEnteredTime
            paymentRequestSummary.uploadDate = paymentRequest.uploadDate

            this.paymentRequestSummarySaveRequestModel.paymentRequestSummaries.push(paymentRequestSummary)

            this.paymentRequestSummarySaveRequestModel.saveType = this.currentSaveType

            this.paymentRequestSummarySaveRequestModel.requestNumber = paymentRequest.requestNumber

            this.paymentRequestSummarySaveRequestModel.hcpFileRequests = this.hcpFileRequestModels

            this.paymentRequestSummaryService.save(this.paymentRequestSummarySaveRequestModel).subscribe(result => {
              if (result) {
                if (result.success) {
                  if (this.isSendData) {
                    this.hcpFileRequestModels.forEach(hcpFile => {
                      this.hcpService.uploadFile(hcpFile.formData).subscribe(resultfile => {
                        if (resultfile) {
                          this.alertifyService.success(resultfile)
                        }
                        else {
                          this.alertifyService.error(resultfile)
                        }
                      })
                    })
                  }
                  this.alertifyService.success(result.message)
                  this.paymentRequestSummaryReset()
                  this.onPaymentRequestSummaryModalCloseHandled()
                  this.getByToday()
                }
                else {
                  this.alertifyService.error(result.message)
                }
              }
            })
          })
        }
      }
    }
  }

  paymentRequestSummaryReset() {
    this.paymentRequestSummaryForm.controls['corporateId'].setValue(-1);
    this.hcpFileRequestModels = []
    this.tandemPaymentInquiryResponseExternalModel.summaryExternals = []
    this.formData = new FormData()
    this.paymentRequestSummaryForm.controls['fileId'].setValue([])
  }


}
