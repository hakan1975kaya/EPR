import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { OperationClaimService } from 'src/app/services/operation-claim/operation-claim.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { OperationClaimSearchRequestModel } from 'src/app/models/operation-claim-models/operation-claim-serach-request-model';
import { OperationClaimSearchResponseModel } from 'src/app/models/operation-claim-models/operation-claim-search-response-model';
import { OperationClaimSaveRequestModel } from 'src/app/models/operation-claim-models/operation-claim-save-request-model';
import { OperationClaimGetListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-get-list-response-model';

@Component({
  selector: 'app-operation-claim',
  templateUrl: './operation-claim.component.html',
  styleUrls: ['./operation-claim.component.css'],
  providers: [OperationClaimService]
})
export class OperationClaimComponent implements OnInit {
  constructor(
    private operationClaimService: OperationClaimService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }
  currentUserId!: number;

  searchForm!: FormGroup;
  operationClaimSearchRequestModel!: OperationClaimSearchRequestModel;
  operationClaimSearchResponseModels: OperationClaimSearchResponseModel[] = [];

  selectedOperationClaimId!: number;

  fileName = 'OperationClaim';

  currentPage: number = 1;
  itemsPerPage: number = 50

  operationClaimForm!: FormGroup;
  displayOperationClaimModal = 'none';

  linkedOperationClaimIdDefault = -1

  operationClaimGetListResponseModels!: OperationClaimGetListResponseModel[]

  operationClaimSaveRequestModel: OperationClaimSaveRequestModel = new OperationClaimSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'

  ngOnInit() {
    this.getOperationClaims();
    this.createSearchForm();
    this.createOperationClaimForm();
    this.operationClaimReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.operationClaimSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.operationClaimSearchRequestModel) {
        this.operationClaimService
          .search(this.operationClaimSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.operationClaimSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.operationClaimSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.operationClaimSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(operationClaimId: number) {
    this.operationClaimService.delete(operationClaimId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.operationClaimSearchResponseModels);
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

  openOperationClaimModal(operationClaimId?: number) {
    this.displayOperationClaimModal = 'block';
    if (operationClaimId) {
      this.selectedOperationClaimId = operationClaimId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(operationClaimId);
    }
    else {
      this.operationClaimReset()
    }
  }

  onOperationClaimModalCloseHandled() {
    this.displayOperationClaimModal = 'none';
  }

  createOperationClaimForm() {
    this.operationClaimForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(3)]],
      linkedOperationClaimId: ['', [Validators.required, Validators.min(0)]],
    });
  }

  getOperationClaims() {
    this.operationClaimService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimGetListResponseModels = dataResult.data
          }
        }
      }
    })
  }

  getById(operationClaimId: number) {
    this.operationClaimService.getById(operationClaimId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimForm.controls['id'].setValue(dataResult.data.id);
            this.operationClaimForm.controls['name'].setValue(dataResult.data.name);
            this.operationClaimForm.controls['linkedOperationClaimId'].setValue(dataResult.data.linkedOperationClaimId);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  save() {
    if (!this.selectedOperationClaimId) {
      this.selectedOperationClaimId = 0
    }

    if (this.operationClaimForm.valid) {

      this.operationClaimSaveRequestModel.operationClaim = Object.assign({}, this.operationClaimForm.value);

      if (this.operationClaimSaveRequestModel) {
        {
          this.operationClaimSaveRequestModel.operationClaim.id = this.selectedOperationClaimId
          this.operationClaimSaveRequestModel.saveType = this.currentSaveType
          this.operationClaimService.save(this.operationClaimSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {

                this.alertifyService.success(result.message)
                this.search()
                this.operationClaimReset()
                this.onOperationClaimModalCloseHandled()
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

  operationClaimReset() {
    this.operationClaimForm.controls['id'].setValue('0');
    this.operationClaimForm.controls['name'].setValue('');
    this.operationClaimForm.controls['linkedOperationClaimId'].setValue('-1');
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedOperationClaimId = 0
  }

  openConfirmModal(operationClaimId: number) {
    this.displayConfirmModal = 'block';
    this.selectedOperationClaimId = operationClaimId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }

  deleteFromConfirm() {
    this.delete(this.selectedOperationClaimId)
    this.onConfirmModalCloseHandled()
  }

}
