import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { PaymentRequestService } from 'src/app/services/payment-requests/payment-request.service';
import { CorporateService } from 'src/app/services/corporate/corporate.service';
import { TandemService } from 'src/app/services/tandem/tandem.service';
import { PaymentRequestDetailService } from 'src/app/services/payment-request-datail/payment-request-datail.service';
import { SftpService } from 'src/app/services/sftp/sftp.service';
import { SftpDownloadService } from 'src/app/services/sftp-download/sftp-download.service';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { MoneyTypeEnum } from 'src/app/enums/money-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';
import { ComissionTypeEnum } from 'src/app/enums/comission-type-enum.enum';
import { StatusEnum } from 'src/app/enums/status-enum.enum';

import { PaymentRequestGetListResponseModel } from 'src/app/models/payment-request-models/payment-request-get-list-response-model';
import { PaymentRequestSaveRequestModel } from 'src/app/models/payment-request-models/payment-request-save-request-model';
import { PaymentRequestSearchRequestModel } from 'src/app/models/payment-request-models/payment-request-serach-request-model';
import { PaymentRequestSearchResponseModel } from 'src/app/models/payment-request-models/payment-request-search-response-model';
import { CorporateGetListResponseModel } from 'src/app/models/corporate-models/corporate-get-list-response-model';
import { PaymentRequestDetailGetByIdResponseModel } from 'src/app/models/payment-request-datail-models/payment-request-detail-get-by-id-response-model';
import { PaymentRequestDetail } from 'src/app/models/payment-request-datail-models/payment-request-detail';
import { TandemPaymentTransferRequestExternalModel } from 'src/app/models/tandem-models/tandem-payment-transfer-request-external-model';
import { PaymentExternal } from 'src/app/models/tandem-models/payment-external';
import { UserGetByRegistrationNumberResponseModel } from 'src/app/models/user-models/user-get-by-user-name-response-model';
import { PaymentRequestGetByTodayResponseModel } from 'src/app/models/payment-request-models/payment-request-get-by-today-response-model';
import { SftpDownloadFileRequestModel } from 'src/app/models/sftp-models/sftp-download-file-request-model';
import { PaymentRequestDownloadRequestModel } from 'src/app/models/payment-request-models/payment-request-download-request-model';
import { PaymentRequestDownloadResponseModel } from 'src/app/models/payment-request-models/payment-request-download-response-model';
import { PaymentRequest } from 'src/app/models/payment-request-models/payment-request';

@Component({
  selector: 'app-payment-request',
  templateUrl: './payment-request.component.html',
  styleUrls: ['./payment-request.component.css'],
  providers: [PaymentRequestService,
    CorporateService,
    PaymentRequestDetailService,
    TandemService,
    SftpService,
    SftpDownloadService]
})

export class PaymentRequestComponent implements OnInit {
  constructor(
    private paymentRequestService: PaymentRequestService,
    private paymentRequestDetailService: PaymentRequestDetailService,
    private corporateService: CorporateService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private tandemService: TandemService,
    private orderPipe: OrderPipe,
    private sftpService: SftpService,
    private sftpDownloadService: SftpDownloadService
  ) { }

  userGetByRegistrationNumberResponseModel: UserGetByRegistrationNumberResponseModel = new UserGetByRegistrationNumberResponseModel()

  paymentRequestGetByTodayResponseModels!: PaymentRequestGetByTodayResponseModel[]

  searchForm!: FormGroup;
  paymentRequestSearchRequestModel!: PaymentRequestSearchRequestModel;
  paymentRequestSearchResponseModels: PaymentRequestSearchResponseModel[] = [];

  selectedPaymentRequestId!: number;

  uploadedPaymentRequestNumber!: string;

  uploadedPaymentRequestdate!: Date

  fileName = 'PaymentRequest';

  currentPagePaymentRequest: number = 1;
  itemsPerPagePaymentRequest: number = 50

  paymentRequestForm!: FormGroup;

  displayPaymentRequestModal = 'none';

  corporateGetListResponseModels!: CorporateGetListResponseModel[]

  selectedCorporate: CorporateGetListResponseModel = new CorporateGetListResponseModel()

  corporateIdDefault = -1

  fileDefault = -1

  statusEnum = StatusEnum

  moneyTypeEnum = MoneyTypeEnum

  currentPagePaymentRequestDetail: number = 1;

  itemsPerPagePaymentRequestDetail: number = 50

  paymentRequestGetListResponseModels!: PaymentRequestGetListResponseModel[]

  paymentRequestDetailGetByIdResponseModels!: PaymentRequestDetailGetByIdResponseModel[]

  waitingSftpFiles: string[] = []

  selectedSftpFileName!: string

  sftpDownloadFileRequestModel!: SftpDownloadFileRequestModel

  paymentRequestDownloadRequestModel!: PaymentRequestDownloadRequestModel

  paymentRequestDownloadResponseModel!: PaymentRequestDownloadResponseModel

  tandemPaymentTransferRequestExternalModel: TandemPaymentTransferRequestExternalModel = new TandemPaymentTransferRequestExternalModel()

  paymentRequestSaveRequestModel: PaymentRequestSaveRequestModel = new PaymentRequestSaveRequestModel();

  paymentRequest:PaymentRequest=new PaymentRequest()

  saveTypeEnum = SaveTypeEnum

  currentSaveType!: SaveTypeEnum

  showSaveButton!: boolean

  totalAmount: number = 0

  paymentRequestDetailCommisyon: PaymentRequestDetail = new PaymentRequestDetail()

  isShowFiledId!: boolean

  displayConfirmModal = 'none'

  editable:boolean=true

  ngOnInit() {

    this.createSearchForm();
    this.createPaymentRequestForm();
    this.paymentRequestReset()
    this.getCorporates()
    this.getByToday()
    this.userGetByRegistrationNumberResponseModel = this.authService.getCurrentUser();
    if (this.userGetByRegistrationNumberResponseModel) {
      if (this.userGetByRegistrationNumberResponseModel.registrationNumber) {
        this.paymentRequestForm.controls['registrationNumber'].setValue(this.userGetByRegistrationNumberResponseModel.registrationNumber);
      }
    }
  }

  getByToday() {
    this.paymentRequestGetByTodayResponseModels = []
    this.paymentRequestSearchResponseModels = []
    this.paymentRequestService.getByToday().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.paymentRequestGetByTodayResponseModels = dataResult.data

            if (this.paymentRequestGetByTodayResponseModels) {
              if (this.paymentRequestGetByTodayResponseModels.length > 0) {
                this.paymentRequestGetByTodayResponseModels.forEach(paymentRequest => {
                  this.paymentRequestSearchResponseModels.push(paymentRequest)
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
      this.paymentRequestSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.paymentRequestSearchRequestModel) {
        this.paymentRequestService
          .search(this.paymentRequestSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.paymentRequestSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.paymentRequestSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.paymentRequestSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(paymentRequestId: number) {
    this.paymentRequestService.delete(paymentRequestId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.paymentRequestSearchResponseModels);
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

  openPaymentRequestModal(paymentRequestId?: number) {
    this.displayPaymentRequestModal = 'block';
    this.showSaveButton = true;
    if (paymentRequestId) {
      this.selectedPaymentRequestId = paymentRequestId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(paymentRequestId);
      this.editable=false
    }
    else {
      this.paymentRequestReset()
    }
  }

  onPaymentRequestModalCloseHandled() {
    this.displayPaymentRequestModal = 'none';
  }

  createPaymentRequestForm() {
    this.paymentRequestForm = this.formBuilder.group({
      id: [''],
      corporateId: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
      moneyType: [''],
      fileId: ['', Validators.required],
      requestNumber: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  getById(paymentRequestId: number) {
    this.showSaveButton = false
    this.isShowFiledId = false
    this.paymentRequestService.getById(paymentRequestId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.paymentRequestForm.controls['id'].setValue(dataResult.data.id);
            this.paymentRequestForm.controls['requestNumber'].setValue(dataResult.data.requestNumber);
            this.paymentRequestForm.controls['corporateId'].setValue(dataResult.data.corporateId);
            this.paymentRequestForm.controls['moneyType'].setValue(MoneyTypeEnum[dataResult.data.moneyType]);
            this.paymentRequestForm.controls['fileId'].setValue(dataResult.data.fileId);
            this.getDetailByPaymentRequestId(paymentRequestId)
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  getDetailByPaymentRequestId(paymentRequestId: number) {
    this.paymentRequestDetailService.getListByPaymentRequestId(paymentRequestId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.paymentRequestDetailGetByIdResponseModels = dataResult.data

            if (this.paymentRequestDetailGetByIdResponseModels) {
              if (this.paymentRequestDetailGetByIdResponseModels.length > 0) {
                this.totalAmount = 0

                this.paymentRequestDetailGetByIdResponseModels.forEach(paymentRequestDetail => {
                  this.totalAmount += paymentRequestDetail.paymentAmount
                })
              }
            }
          }
        }
        else {
          this.alertifyService.error(dataResult.message);
        }
      }
    })
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

  setCorporate(event: any) {
    if (this.corporateGetListResponseModels) {
      if (this.corporateGetListResponseModels.length > 0) {
        this.corporateGetListResponseModels.forEach(corporate => {
          if (corporate.id == event.currentTarget.value) {
            this.selectedCorporate = corporate
            if (this.selectedCorporate) {
              this.resetFileId()
              this.getWaitingSftpFiles()
            }
            else {
              this.alertifyService.error("Kurum Seçilemedi Tekrar Deneyiniz")
              this.resetFileId()
            }
          }
        })
      }
    }
  }

  resetFileId() {
    this.fileDefault = -1
    this.paymentRequestForm.controls['requestNumber'].setValue('');
    this.waitingSftpFiles = []
  }

  getWaitingSftpFiles() {
    if (this.selectedCorporate) {
      if (this.selectedCorporate.prefix) {
        this.sftpService.getDirectory(this.selectedCorporate.prefix).subscribe(dataResultDirectory => {
          if (dataResultDirectory) {
            if (dataResultDirectory.success) {
              if (dataResultDirectory.data) {
                let directory = dataResultDirectory.data
                if (directory) {
                  this.sftpService.getFileNames(directory).subscribe(dataResultFileNames => {
                    if (dataResultFileNames) {
                      if (dataResultFileNames.success) {
                        if (dataResultFileNames.data) {
                          this.waitingSftpFiles = dataResultFileNames.data
                        }
                      }
                      else {
                        this.alertifyService.error(dataResultFileNames.message)
                        this.resetFileId()
                      }
                    }
                  })
                }
              }
            }
          }
        })
      }
    }
  }

  setFile(event: any) {
    this.selectedSftpFileName = event.target.value
    if (this.selectedSftpFileName) {
      this.sftpFileDownload()
    }

  }

  sftpFileDownload() {
    this.sftpDownloadFileRequestModel = new SftpDownloadFileRequestModel();
    this.sftpDownloadFileRequestModel.sftpFileName = this.selectedSftpFileName
    this.sftpDownloadFileRequestModel.prefix = this.selectedCorporate.prefix
    if (this.sftpDownloadFileRequestModel) {
      if (this.sftpDownloadFileRequestModel.sftpFileName) {
        if (this.sftpDownloadFileRequestModel.prefix) {
          this.sftpService.sftpDownload(this.sftpDownloadFileRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {

                this.paymentRequestDownloadRequestModel = new PaymentRequestDownloadRequestModel()
                this.paymentRequestDownloadRequestModel.sftpFileName = this.selectedSftpFileName
                this.paymentRequestDownloadRequestModel.corporateId = this.selectedCorporate.id

                if (this.paymentRequestDownloadRequestModel) {
                  this.paymentRequestService.paymentRequestDownload(this.paymentRequestDownloadRequestModel).subscribe(dataResult => {
                    if (dataResult) {
                      if (dataResult.success) {
                        if (dataResult.data) {
                          if (dataResult.data.paymentRequestDetails) {
                            if (dataResult.data.paymentRequestDetails.length > 0) {

                              this.paymentRequestDownloadResponseModel = dataResult.data
                              this.paymentRequestDetailGetByIdResponseModels = dataResult.data.paymentRequestDetails

                              if (this.paymentRequestDownloadResponseModel) {
                                if (this.paymentRequestDownloadResponseModel.paymentRequest) {
                                  if (this.paymentRequestDownloadResponseModel.paymentRequest.requestNumber) {

                                    this.uploadedPaymentRequestNumber = this.paymentRequestDownloadResponseModel.paymentRequest.requestNumber

                                    if (this.uploadedPaymentRequestNumber) {

                                      this.paymentRequestForm.controls['requestNumber'].setValue(this.uploadedPaymentRequestNumber);

                                      this.totalAmount = 0

                                      if (this.paymentRequestDownloadResponseModel.paymentRequestDetails) {
                                        if (this.paymentRequestDownloadResponseModel.paymentRequestDetails.length > 0) {

                                          this.paymentRequestDownloadResponseModel.paymentRequestDetails.forEach(paymentRequestDetail => {
                                            this.totalAmount += paymentRequestDetail.paymentAmount
                                          })

                                          if (this.selectedCorporate) {
                                            if (this.selectedCorporate.comissionType) {
                                              if (this.selectedCorporate.comissionAccountNo) {
                                                this.paymentRequestDetailCommisyon.referenceNumber = this.uploadedPaymentRequestNumber + "-Com"
                                                this.paymentRequestDetailCommisyon.accountNumber = this.selectedCorporate.comissionAccountNo
                                                this.paymentRequestDetailCommisyon.customerNumber = 0
                                                this.paymentRequestDetailCommisyon.phoneNumber = ""
                                                this.paymentRequestDetailCommisyon.firstName = ""
                                                this.paymentRequestDetailCommisyon.lastName = ""
                                                this.paymentRequestDetailCommisyon.cardDepositDate = dataResult.data.paymentRequestDetails[0].cardDepositDate

                                                if (this.selectedCorporate.comissionType == ComissionTypeEnum.Percentage) {
                                                  this.paymentRequestDetailCommisyon.paymentAmount = (0.01) * (this.selectedCorporate.comission * this.totalAmount)
                                                }
                                                if (this.selectedCorporate.comissionType == ComissionTypeEnum.Quantity) {
                                                  this.paymentRequestDetailCommisyon.paymentAmount = this.selectedCorporate.comission * (this.paymentRequestDownloadResponseModel.paymentRequestDetails.length)
                                                }
                                                this.paymentRequestDetailCommisyon.explanation = "Komisyon Ödeme Tutarı"

                                                this.paymentRequestDownloadResponseModel.paymentRequestDetails.push(this.paymentRequestDetailCommisyon)

                                                this.paymentRequestDetailGetByIdResponseModels = this.paymentRequestDownloadResponseModel.paymentRequestDetails
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  })
                }
              }
              else {
                this.alertifyService.error(result.message)
              }
            }
          })
        }
        else {
          this.alertifyService.error("prefix Seçilemedi Tekrar Deneyiniz")
        }
      }
      else {
        this.alertifyService.error("sftpFileName Verisi Eksik Tekrar Deneyiniz")
      }
    }
    else {
      this.alertifyService.error("sftpDownloadFileRequestModel Verisi Eklenemedi Tekrar Deneyiniz")
    }
  }

  sentTandemAndSave() {
    if (!this.selectedPaymentRequestId) {
      this.selectedPaymentRequestId = 0
    }

    if (this.paymentRequestForm.valid) {

      this.paymentRequest= Object.assign({}, this.paymentRequestForm.value);
      if (this.paymentRequest) {

        this.sftpDownloadService.getBySftpFileName(this.paymentRequestForm.controls['fileId'].value).subscribe(sftpDownloadDataResult => {
          if (sftpDownloadDataResult) {
            if (sftpDownloadDataResult.success) {
              if (sftpDownloadDataResult.data) {
                if (sftpDownloadDataResult.data.id > 0) {

                  this.tandemPaymentTransferRequestExternalModel.corporateCode = this.selectedCorporate.corporateCode
                  this.tandemPaymentTransferRequestExternalModel.moneyType = this.selectedCorporate.moneyType
                  this.tandemPaymentTransferRequestExternalModel.registrationNumber = this.userGetByRegistrationNumberResponseModel.registrationNumber
                  this.tandemPaymentTransferRequestExternalModel.requestNumber = this.paymentRequest.requestNumber

                  this.tandemPaymentTransferRequestExternalModel.paymentExternals = []

                  this.paymentRequestDownloadResponseModel.paymentRequestDetails.forEach(requestDetail => {
                    let paymentExternal = new PaymentExternal()
                    paymentExternal.accountNumber = requestDetail.accountNumber
                    paymentExternal.cardDepositDate = requestDetail.cardDepositDate
                    paymentExternal.customerNumber = requestDetail.customerNumber
                    paymentExternal.explanation = requestDetail.explanation
                    paymentExternal.firstName = requestDetail.firstName
                    paymentExternal.lastName = requestDetail.lastName
                    paymentExternal.paymentAmount = requestDetail.paymentAmount
                    paymentExternal.phoneNumber = requestDetail.phoneNumber
                    paymentExternal.referenceNumber = requestDetail.referenceNumber
                    this.tandemPaymentTransferRequestExternalModel.paymentExternals.push(paymentExternal)
                  })

                  this.tandemService.paymentTransfer(this.tandemPaymentTransferRequestExternalModel).subscribe(dataResultTransferRequestExternal => {
                    if (dataResultTransferRequestExternal) {
                      if (dataResultTransferRequestExternal.success) {
                        if (dataResultTransferRequestExternal.data) {
                          if (dataResultTransferRequestExternal.data.responseCode == "00") {

                            this.paymentRequestSaveRequestModel.paymentRequest = this.paymentRequest

                            this.paymentRequestSaveRequestModel.paymentRequest.fileId = sftpDownloadDataResult.data.id
                            this.paymentRequestSaveRequestModel.paymentRequest.id = this.selectedPaymentRequestId
                            this.paymentRequestSaveRequestModel.paymentRequest.corporateId = this.selectedCorporate.id
                            this.paymentRequestSaveRequestModel.paymentRequest.moneyType = this.selectedCorporate.moneyType

                            this.paymentRequestSaveRequestModel.paymentRequestDetails = []

                            this.paymentRequestDownloadResponseModel.paymentRequestDetails.forEach(paymentRequestDetail => {
                              let savePaymentRequestDetail = new PaymentRequestDetail()
                              savePaymentRequestDetail.accountNumber = paymentRequestDetail.accountNumber
                              savePaymentRequestDetail.cardDepositDate = paymentRequestDetail.cardDepositDate
                              savePaymentRequestDetail.customerNumber = paymentRequestDetail.customerNumber
                              savePaymentRequestDetail.explanation = paymentRequestDetail.explanation
                              savePaymentRequestDetail.firstName = paymentRequestDetail.firstName
                              savePaymentRequestDetail.lastName = paymentRequestDetail.lastName
                              savePaymentRequestDetail.paymentAmount = paymentRequestDetail.paymentAmount
                              savePaymentRequestDetail.phoneNumber = paymentRequestDetail.phoneNumber
                              savePaymentRequestDetail.referenceNumber = paymentRequestDetail.referenceNumber
                              this.paymentRequestSaveRequestModel.paymentRequestDetails.push(savePaymentRequestDetail)
                            })

                            this.paymentRequestSaveRequestModel.saveType = this.currentSaveType
                            this.paymentRequestService.save(this.paymentRequestSaveRequestModel).subscribe(result => {
                              if (result) {
                                if (result.success) {
                                  this.alertifyService.success(" EPR Sonuç: " + result.message + " Tandem Sonuç: " + dataResultTransferRequestExternal.data.responseCode + " --> " + dataResultTransferRequestExternal.data.responseMessage)
                                  this.paymentRequestReset()
                                  this.onPaymentRequestModalCloseHandled()
                                  this.getByToday()
                                }
                                else {
                                  this.alertifyService.error(result.message)
                                }
                              }
                            })
                          }
                          else {
                            this.alertifyService.error(" Tandem Sonuç: " + dataResultTransferRequestExternal.data.responseCode + " --> " + dataResultTransferRequestExternal.data.responseMessage)
                          }
                        }
                      }
                      else {
                        this.alertifyService.error(dataResultTransferRequestExternal.message)
                      }
                    }
                  })
                }
              }
            }
            else {
              this.alertifyService.error(sftpDownloadDataResult.message)
            }
          }
        })
      }
      else {
        this.alertifyService.error("Form da bir hata var")
      }
    }
  }

  paymentRequestReset() {
    this.paymentRequestForm.controls['id'].setValue(0);
    this.paymentRequestForm.controls['requestNumber'].setValue('');
    this.paymentRequestForm.controls['corporateId'].setValue(-1);
    this.paymentRequestForm.controls['fileId'].setValue(-1);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedPaymentRequestId = 0
    this.paymentRequestDetailGetByIdResponseModels = []
    this.totalAmount = 0
    this.isShowFiledId = true
    this.editable=true
  }

  openConfirmModal(paymentRequestId: number) {
    this.displayConfirmModal = 'block';
    this.selectedPaymentRequestId = paymentRequestId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }

  openToPaymentRequestFromConfirm() {
    this.sftpDownloadService.openToPaymentRequest(this.selectedPaymentRequestId).subscribe(result => {
      if (result) {
        if (result.success) {
          this.alertifyService.success(result.message)
        }
        else {
          this.alertifyService.error(result.message)
        }
      }
    })
    this.onConfirmModalCloseHandled()
  }

}
