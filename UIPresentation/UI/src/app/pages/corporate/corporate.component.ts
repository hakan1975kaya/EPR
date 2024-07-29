import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CorporateService } from 'src/app/services/corporate/corporate.service';
import { TandemService } from 'src/app/services/tandem/tandem.service';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { MoneyTypeEnum } from 'src/app/enums/money-type-enum.enum';
import { ComissionTypeEnum } from 'src/app/enums/comission-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { CorporateSearchRequestModel } from 'src/app/models/corporate-models/corporate-serach-request-model';
import { CorporateSearchResponseModel } from 'src/app/models/corporate-models/corporate-search-response-model';
import { CorporateGetListResponseModel } from 'src/app/models/corporate-models/corporate-get-list-response-model';
import { CorporateSaveRequestModel } from 'src/app/models/corporate-models/corporate-save-request-model';
import { TandemCorporateDefineRequestExternalModel } from 'src/app/models/tandem-models/tandem-corporate-define-request-external-model';
import { TandemCorporateDefineResponseExternalModel } from 'src/app/models/tandem-models/tandem-corporate-define-response-external-model';
import { Corporate } from 'src/app/models/corporate-models/corporate';
import { CorporateExternal } from 'src/app/models/tandem-models/corporate-external';

@Component({
  selector: 'app-corporate',
  templateUrl: './corporate.component.html',
  styleUrls: ['./corporate.component.css'],
  providers: [CorporateService, TandemService]
})

export class CorporateComponent implements OnInit {
  constructor(
    private corporateService: CorporateService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe,
    private tandemService: TandemService
  ) { }

  currentUserId!: number;

  searchForm!: FormGroup;
  corporateSearchRequestModel!: CorporateSearchRequestModel;
  corporateSearchResponseModels: CorporateSearchResponseModel[] = [];

  moneyTypeEnum = MoneyTypeEnum
  selectedMoneyType!: MoneyTypeEnum
  moneyTypeDefault = -1

  comissionTypeEnum = ComissionTypeEnum
  selectedComissionType!: ComissionTypeEnum
  comissionTypeDefault = -1

  comissionMoneyTypeEnum = MoneyTypeEnum
  selectedComissionMoneyType!: MoneyTypeEnum
  comissionMoneyTypeDefault = -1

  selectedCorporateId!: number;

  fileName = 'Corporate';

  currentPage: number = 1;
  itemsPerPage: number = 50

  corporateForm!: FormGroup;
  displayCorporateModal = 'none';

  corporateGetListResponseModels!: CorporateGetListResponseModel[]

  corporateSaveRequestModel: CorporateSaveRequestModel = new CorporateSaveRequestModel();

  corporate: Corporate = new Corporate()
  corporateExternal: CorporateExternal = new CorporateExternal()

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  selectedComission:number=0

  displayConfirmModal = 'none'

  maxCorporateCode = 0;

  tandemCorporateDefineRequestExternalModel: TandemCorporateDefineRequestExternalModel = new TandemCorporateDefineRequestExternalModel()
  tandemCorporateDefineResponseExternalModel!: TandemCorporateDefineResponseExternalModel

  ngOnInit() {
    this.getList()
    this.createSearchForm();
    this.createCorporateForm();
    this.corporateReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.corporateSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.corporateSearchRequestModel) {
        this.corporateService
          .search(this.corporateSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.corporateSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.corporateSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.corporateSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(id: number) {
    this.corporateService.delete(id).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.corporateSearchResponseModels);
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

  openCorporateModal(corporateId?: number) {
    this.displayCorporateModal = 'block';
    if (corporateId) {
      this.selectedCorporateId = corporateId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(corporateId);
    }
    else {
      this.corporateReset()
    }
  }

  onCorporateModalCloseHandled() {
    this.displayCorporateModal = 'none';
  }

  createCorporateForm() {
    this.corporateForm = this.formBuilder.group({
      id: [''],
      corporateCode: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
      corporateName: ['', [Validators.required, Validators.minLength(3)]],
      corporateAccountNo: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
      moneyType: ['', [Validators.required]],
      prefix: ['', [Validators.required, Validators.minLength(1)]],
      comissionType: ['', Validators.required],
      comissionMoneyType: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
      comissionPercentage: ['', [Validators.required, Validators.min(0), Validators.max(100), Validators.pattern("^[0-9.-]*$")]],
      comissionQuantity: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9.-]*$")]],
      comissionAccountNo: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
    });
  }

  getList() {
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

  getById(corporateId: number) {
    this.corporateService.getById(corporateId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.corporateForm.controls['id'].setValue(dataResult.data.id);
            this.corporateForm.controls['corporateCode'].setValue(dataResult.data.corporateCode);
            this.corporateForm.controls['corporateName'].setValue(dataResult.data.corporateName);
            this.corporateForm.controls['corporateAccountNo'].setValue(dataResult.data.corporateAccountNo);
            this.corporateForm.controls['moneyType'].setValue(dataResult.data.moneyType);
            this.corporateForm.controls['prefix'].setValue(dataResult.data.prefix);
            this.corporateForm.controls['comissionType'].setValue(dataResult.data.comissionType);
            this.corporateForm.controls['comissionMoneyType'].setValue(dataResult.data.comissionMoneyType);

            if (this.selectedComissionType == ComissionTypeEnum.Percentage) {
              this.corporateForm.controls['comissionPercentage'].setValue(dataResult.data.comission);
            }

            if (this.selectedComissionType == ComissionTypeEnum.Quantity) {
              this.corporateForm.controls['comissionQuantity'].setValue(dataResult.data.comission);
            }

            this.corporateForm.controls['comissionAccountNo'].setValue(dataResult.data.comissionAccountNo);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  setMoneyType(event: any) {
    this.selectedMoneyType = event.currentTarget.value
    this.selectedComissionMoneyType = event.currentTarget.value
    this.corporateForm.controls['comissionMoneyType'].setValue(event.currentTarget.value)

    if (event.currentTarget.value == this.moneyTypeEnum.TL) {
      this.corporateForm.controls['comissionAccountNo'].setValue(17969249);
    }

    if (event.currentTarget.value == this.moneyTypeEnum.USD) {
      this.corporateForm.controls['comissionAccountNo'].setValue(60031638);
    }

    if (event.currentTarget.value == this.moneyTypeEnum.EUR) {
      this.corporateForm.controls['comissionAccountNo'].setValue(70009342);
    }
  }

  setComissionType(event: any) {
    this.selectedComissionType = event.currentTarget.value
    if (this.selectedComissionType == ComissionTypeEnum.Percentage) {
      this.corporateForm.controls['comissionQuantity'].setValue(0);
      this.corporateForm.controls['comissionPercentage'].setValue("");
    }
    if (this.selectedComissionType == ComissionTypeEnum.Quantity) {
      this.corporateForm.controls['comissionPercentage'].setValue(0);
      this.corporateForm.controls['comissionQuantity'].setValue("");
    }
  }

  setComissionMoneyType(event: any) {
    this.selectedComissionMoneyType = event.currentTarget.value
    this.selectedMoneyType = event.currentTarget.value
    this.corporateForm.controls['moneyType'].setValue(event.currentTarget.value);

    if (event.currentTarget.value == this.moneyTypeEnum.TL) {
      this.corporateForm.controls['comissionAccountNo'].setValue(17969249);
    }

    if (event.currentTarget.value == this.moneyTypeEnum.USD) {
      this.corporateForm.controls['comissionAccountNo'].setValue(60031638);
    }

    if (event.currentTarget.value == this.moneyTypeEnum.EUR) {
      this.corporateForm.controls['comissionAccountNo'].setValue(70009342);
    }

  }

  save() {
    if (!this.selectedCorporateId) {
      this.selectedCorporateId = 0
    }

    if (this.corporateForm.valid) {
      this.corporate = Object.assign({}, this.corporateForm.value);
      if (this.corporate) {

        if (this.selectedComissionType == ComissionTypeEnum.Percentage) {
          this.selectedComission = this.corporateForm.controls['comissionPercentage'].value
        }
        if (this.selectedComissionType == ComissionTypeEnum.Quantity) {
          this.selectedComission= this.corporateForm.controls['comissionQuantity'].value
        }

        if (this.currentSaveType == this.saveTypeEnum.Add) {

          this.corporateExternal.corporateAccountNo = Number(this.corporate.comissionAccountNo)
          this.corporateExternal.corporateCode = Number(this.corporate.corporateCode)
          this.corporateExternal.corporateName = this.corporate.corporateName
          this.corporateExternal.moneyType = Number(this.corporate.moneyType)
          this.corporateExternal.prefix = this.corporate.prefix

          if (this.corporateExternal) {

            this.tandemCorporateDefineRequestExternalModel.corporateExternal = this.corporateExternal
            this.tandemService.corporateDefine(this.tandemCorporateDefineRequestExternalModel).subscribe(dataResult => {
              if (dataResult) {
                if (dataResult.success) {
                  if (dataResult.data) {
                    if (dataResult.data.responseCode == "00") {

                      this.corporateSaveRequestModel.corporate = this.corporate

                      this.corporateSaveRequestModel.corporate.id = this.selectedCorporateId
                      this.corporateSaveRequestModel.corporate.comissionMoneyType = Number(this.corporateSaveRequestModel.corporate.comissionMoneyType)
                      this.corporateSaveRequestModel.corporate.comissionType = Number(this.corporateSaveRequestModel.corporate.comissionType)
                      this.corporateSaveRequestModel.corporate.moneyType = Number(this.corporateSaveRequestModel.corporate.moneyType)
                      this.corporateSaveRequestModel.corporate.comission=this.selectedComission

                      this.corporateSaveRequestModel.saveType = this.currentSaveType
                      this.corporateService.save(this.corporateSaveRequestModel).subscribe(result => {
                        if (result) {
                          if (result.success) {
                            this.alertifyService.success(result.message)
                            this.search()
                            this.corporateReset()
                            this.onCorporateModalCloseHandled()
                          }
                          else {
                            this.alertifyService.error(result.message)
                          }
                        }
                      })
                    }
                    else {
                      this.alertifyService.error(" Tandem Sonuç: " + dataResult.data.responseCode + " --> " + dataResult.data.responseMessage)
                    }
                  }
                }
              }
            })
          }
        }
        else {
          this.corporateSaveRequestModel.corporate = this.corporate

          this.corporateSaveRequestModel.corporate.id = this.selectedCorporateId
          this.corporateSaveRequestModel.corporate.comissionMoneyType = Number(this.corporateSaveRequestModel.corporate.comissionMoneyType)
          this.corporateSaveRequestModel.corporate.comissionType = Number(this.corporateSaveRequestModel.corporate.comissionType)
          this.corporateSaveRequestModel.corporate.moneyType = Number(this.corporateSaveRequestModel.corporate.moneyType)
          this.corporateSaveRequestModel.corporate.comission=this.selectedComission

          this.corporateSaveRequestModel.saveType = this.currentSaveType
          this.corporateService.save(this.corporateSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {
                this.alertifyService.success(result.message)
                this.search()
                this.corporateReset()
                this.onCorporateModalCloseHandled()
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

  getMaxCorporateCode(): number {
    let max = 0
    if (this.corporateGetListResponseModels) {
      if (this.corporateGetListResponseModels.length > 0) {
        this.corporateGetListResponseModels.forEach(corporate => {
          if (corporate.corporateCode > max) {
            max = corporate.corporateCode
          }
        })
      }
    }
    return max
  }

  corporateReset() {
    this.maxCorporateCode = this.getMaxCorporateCode();
    this.corporateForm.controls['id'].setValue(0);
    this.corporateForm.controls['corporateCode'].setValue(this.maxCorporateCode + 1);
    this.corporateForm.controls['corporateName'].setValue('');
    this.corporateForm.controls['corporateAccountNo'].setValue(0);
    this.corporateForm.controls['moneyType'].setValue(-1);
    this.corporateForm.controls['prefix'].setValue('');

    this.corporateForm.controls['comissionType'].setValue(-1);
    this.corporateForm.controls['comissionMoneyType'].setValue(-1);

    if (this.selectedComissionType == ComissionTypeEnum.Percentage) {
      this.corporateForm.controls['comissionPercentage'].setValue(0);
    }

    if (this.selectedComissionType == ComissionTypeEnum.Quantity) {
      this.corporateForm.controls['comissionQuantity'].setValue(0);
    }

    this.corporateForm.controls['comissionAccountNo'].setValue(0);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedCorporateId = 0
  }

  openConfirmModal(corporateId: number) {
    this.displayConfirmModal = 'block';
    this.selectedCorporateId = corporateId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }
  deleteFromConfirm() {
    this.delete(this.selectedCorporateId)
    this.onConfirmModalCloseHandled()
  }

}
