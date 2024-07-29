import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { AlertifyService } from '../../services/alertify/alertify.service';

import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';
import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';

import { OperationClaimService } from 'src/app/services/operation-claim/operation-claim.service';
import { RoleOperationClaimService } from 'src/app/services/role-operation-claim/role-operation-claim.service';
import { RoleService } from 'src/app/services/role/role.service';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { OperationClaim } from 'src/app/models/operation-claim-models/operation-claim';
import { OperationClaimParentListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-response-model';
import { OperationClaimChildListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-child-list-response-model ';
import { RoleOperationClaimSaveRequestModel } from 'src/app/models/role-operation-claim-models/role-operation-claim-save-request-model';
import { Role } from 'src/app/models/role-models/role';
import { RoleSearchResponseModel } from 'src/app/models/role-models/role-search-response-model';
import { RoleSearchRequestModel } from 'src/app/models/role-models/role-serach-request-model';
import { OperationClaimParentListGetByRoleIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-get-by-role-id-response-model';
import { OperationClaimChildListGetByRoleIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-child-list-get-by-role-id-response-model ';

@Component({
  selector: 'app-role-operation-claim',
  templateUrl: './role-operation-claim.component.html',
  styleUrls: ['./role-operation-claim.component.css'],
  providers: [RoleService, OperationClaimService, RoleOperationClaimService],
})
export class RoleOperationClaimComponent implements OnInit {
  constructor(
    private roleService: RoleService,
    private RoleOperationClaimService: RoleOperationClaimService,
    private operationClaimService: OperationClaimService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }

  currentUserId!: number;

  searchForm!: FormGroup;

  roleSearchRequestModel!: RoleSearchRequestModel;
  roleSearchResponseModels: RoleSearchResponseModel[] = [];

  currentPage: number = 1;
  itemsPerPage: number = 50

  fileName = 'RoleOperationClaim';

  selectedRoleId!: number

  roleOperationClaimForm!: FormGroup;
  displayRoleOperationClaimModal = 'none';

  operationClaimParentListResponseModels: OperationClaimParentListResponseModel[] = []
  operationClaimChildListResponseModels: OperationClaimChildListResponseModel[] = []

  operationClaimParentListGetByRoleIdResponseModels: OperationClaimParentListGetByRoleIdResponseModel[] = []
  operationClaimChildListGetByRoleIdResponseModels: OperationClaimChildListGetByRoleIdResponseModel[] = []
  selectedOperationClaims: OperationClaim[] = []

  roleOperationClaimSaveRequestModel: RoleOperationClaimSaveRequestModel = new RoleOperationClaimSaveRequestModel();

  saveTypeEnumRoleOperationClaim = SaveTypeEnum
  currentSaveTypeRoleOperationClaim!: SaveTypeEnum

  ngOnInit() {

    this.createSearchForm();

    this.createRoleOperationClaimForm();

    this.getParentOperationClaim()

    this.getChildOperationClaim()

    this.roleOperationClaimReset()

    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {

      this.roleSearchRequestModel = Object.assign({}, this.searchForm.value);

      if (this.roleSearchRequestModel) {
        this.roleService.search(this.roleSearchRequestModel).subscribe((dataResult) => {
          if (dataResult) {
            if (dataResult.success) {
              if (dataResult.data) {
                this.roleSearchResponseModels = dataResult.data;
              }
            }
          }
        });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.roleSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.roleSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  exportToExcel() {
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.roleSearchResponseModels);
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

  openRoleOperationClaimModal(roleId?: number) {
    this.displayRoleOperationClaimModal = 'block';
    if (roleId) {
      this.currentSaveTypeRoleOperationClaim = SaveTypeEnum.Update
      this.selectedRoleId = roleId

      this.getByRoleId(roleId)
    }
    else {
      this.currentSaveTypeRoleOperationClaim = SaveTypeEnum.Add
      this.roleOperationClaimReset()
    }
  }

  getByRoleId(roleId: number) {
    this.selectedOperationClaims = []

    this.operationClaimParentListResponseModels.forEach(operationClaim => {
      operationClaim.isChecked = false;
    })

    this.operationClaimChildListResponseModels.forEach(operationClaim => {
      operationClaim.isChecked = false;
    })

    this.operationClaimService.operationClaimParentListGetByRoleId(roleId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimParentListGetByRoleIdResponseModels = dataResult.data

            this.operationClaimParentListResponseModels.forEach(operationClaim => {
              this.operationClaimParentListGetByRoleIdResponseModels.forEach(checkedOperationClaim => {
                if (operationClaim.id == checkedOperationClaim.id) {
                  operationClaim.isChecked = true
                  this.selectedOperationClaims.push(operationClaim)
                }
              })
            })

          }
        }
      }
    })

    this.operationClaimService.operationClaimChildListGetByRoleId(roleId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimChildListGetByRoleIdResponseModels = dataResult.data

            this.operationClaimChildListResponseModels.forEach(operationClaim => {
              this.operationClaimChildListGetByRoleIdResponseModels.forEach(checkedOperationClaim => {
                if (operationClaim.id == checkedOperationClaim.id) {
                  operationClaim.isChecked = true
                  this.selectedOperationClaims.push(operationClaim)
                }
              })
            })

          }
        }
      }
    })

  }

  onRoleOperationClaimModalCloseHandled() {
    this.displayRoleOperationClaimModal = 'none';
  }


  createRoleOperationClaimForm() {
    this.roleOperationClaimForm = this.formBuilder.group({
      id: [''],
      name: ['']
    });
  }

  getParentOperationClaim() {
    this.operationClaimService.operationClaimParentList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimParentListResponseModels = dataResult.data
          }
        }
        else {
          this.alertifyService.error(dataResult.message)
        }
      }
    })
  }

  getChildOperationClaim() {
    this.operationClaimService.operationClaimChildList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimChildListResponseModels = dataResult.data
          }
        }
        else {
          this.alertifyService.error(dataResult.message)
        }
      }
    })
  }

  setParentOperationClaimCheck(operationClaimId: number, event: any) {
    if (event.currentTarget.checked) {

      this.operationClaimParentListResponseModels.forEach(operationClaim => {
        if (operationClaim.id == operationClaimId) {
          this.selectedOperationClaims.push(operationClaim)
        }
      })

      this.operationClaimChildListResponseModels.forEach(operationClaim => {
        if (operationClaim.linkedOperationClaimId == operationClaimId) {
          operationClaim.isChecked = true
          this.selectedOperationClaims.push(operationClaim)
        }
      });

    }
    else {

      const index = this.selectedOperationClaims.map(x => x.id).indexOf(operationClaimId);
      if (index !== -1) {
        this.selectedOperationClaims.splice(index, 1);
      }

      this.operationClaimChildListResponseModels.forEach(operationClaim => {
        if (operationClaim.linkedOperationClaimId == operationClaimId) {
          operationClaim.isChecked = false

          const index = this.selectedOperationClaims.map(x => x.id).indexOf(operationClaim.id);
          if (index !== -1) {
            this.selectedOperationClaims.splice(index, 1);
          }
        }
      });
    }

  }

  setClientOperationClaimCheck(operationClaimId: number, event: any) {
    if (event.currentTarget.checked) {

      this.operationClaimChildListResponseModels.forEach(operationClaim => {
        if (operationClaim.id == operationClaimId) {
          this.selectedOperationClaims.push(operationClaim)
        }
      })
    }
    else {
      const index = this.selectedOperationClaims.map(x => x.id).indexOf(operationClaimId);
      if (index !== -1) {
        this.selectedOperationClaims.splice(index, 1);
      }
    }
  }

  roleOperationClaimSave() {
    if (!this.selectedRoleId) {
      this.selectedRoleId = 0
    }

    this.roleService.getById(this.selectedRoleId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.roleOperationClaimSaveRequestModel.role = new Role()
            this.roleOperationClaimSaveRequestModel.role = dataResult.data

            this.roleOperationClaimSaveRequestModel.operationClaims = []
            this.roleOperationClaimSaveRequestModel.operationClaims = this.selectedOperationClaims
            this.roleOperationClaimSaveRequestModel.saveType = this.currentSaveTypeRoleOperationClaim

            this.RoleOperationClaimService.save(this.roleOperationClaimSaveRequestModel).subscribe(result => {
              if (result) {
                if (result.success) {

                  this.alertifyService.success(result.message)
                  this.search()
                  this.roleOperationClaimReset()
                  this.onRoleOperationClaimModalCloseHandled()
                }
                else {
                  this.alertifyService.error(result.message)
                }
              }
            })
          }
        }
      }
    })

  }

  roleOperationClaimReset() {
    this.selectedOperationClaims = []
    this.currentUserId = 0
    this.currentSaveTypeRoleOperationClaim = this.saveTypeEnumRoleOperationClaim.Add
  }

  roleOperationClaimCancel() {
    this.roleOperationClaimReset()
    this.displayRoleOperationClaimModal = 'none';
  }


}
