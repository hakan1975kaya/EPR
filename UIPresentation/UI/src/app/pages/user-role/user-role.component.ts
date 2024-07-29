import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserRoleService } from 'src/app/services/user-role/user-role.service';
import { RoleService } from 'src/app/services/role/role.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { UserRoleSearchRequestModel } from 'src/app/models/user-role-models/user-role-serach-request-model';
import { UserRoleSearchResponseModel } from 'src/app/models/user-role-models/user-role-search-response-model';
import { UserRoleSaveRequestModel } from 'src/app/models/user-role-models/user-role-save-request-model';
import { RoleGetListResponseModel } from 'src/app/models/role-models/role-get-list-response-model';
import { UserGetListResponseModel } from 'src/app/models/user-models/user-get-list-response-model';
import { UserService } from 'src/app/services/User/User.service';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.css'],
  providers: [UserRoleService, UserService, RoleService]
})
export class UserRoleComponent implements OnInit {
  constructor(
    private userRoleService: UserRoleService,
    private userService: UserService,
    private roleService: RoleService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }

  currentUserId!: number;

  searchForm!: FormGroup;
  userRoleSearchRequestModel!: UserRoleSearchRequestModel;
  userRoleSearchResponseModels: UserRoleSearchResponseModel[] = [];

  selectedUserRoleId!: number;

  fileName = 'UserRole';

  currentPage: number = 1;
  itemsPerPage: number = 50

  userRoleForm!: FormGroup;
  displayUserRoleModal = 'none';

  userIdDefault = -1
  roleIdDefault = -1

  userGetListResponseModels!: UserGetListResponseModel[]

  roleGetListResponseModels!: RoleGetListResponseModel[]

  userRoleSaveRequestModel: UserRoleSaveRequestModel = new UserRoleSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'


  ngOnInit() {
    this.getUsers();
    this.getRoles();
    this.createSearchForm();
    this.createUserRoleForm();
    this.userRoleReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.userRoleSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.userRoleSearchRequestModel) {
        this.userRoleService
          .search(this.userRoleSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.userRoleSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.userRoleSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.userRoleSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(userRoleId: number) {
    this.userRoleService.delete(userRoleId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.userRoleSearchResponseModels);
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

  openUserRoleModal(UserRoleId?: number) {
    this.displayUserRoleModal = 'block';
    if (UserRoleId) {
      this.selectedUserRoleId = UserRoleId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(UserRoleId);
    }
    else {
      this.userRoleReset()
    }
  }

  onUserRoleModalCloseHandled() {
    this.displayUserRoleModal = 'none';
  }

  createUserRoleForm() {
    this.userRoleForm = this.formBuilder.group({
      id: [''],
      userId: ['', [Validators.required, Validators.min(0)]],
      roleId: ['', [Validators.required, Validators.min(0)]],
    });
  }

  getUsers() {
    this.userService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.userGetListResponseModels = dataResult.data
          }
        }
      }
    })
  }

  getRoles() {
    this.roleService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.roleGetListResponseModels = dataResult.data
          }
        }
      }
    })
  }

  getById(UserRoleId: number) {
    this.userRoleService.getById(UserRoleId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.userRoleForm.controls['id'].setValue(dataResult.data.id);
            this.userRoleForm.controls['userId'].setValue(dataResult.data.userId);
            this.userRoleForm.controls['roleId'].setValue(dataResult.data.roleId);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  save() {
    if (!this.selectedUserRoleId) {
      this.selectedUserRoleId = 0
    }

    if (this.userRoleForm.valid) {

      this.userRoleSaveRequestModel.userRole = Object.assign({}, this.userRoleForm.value);
      if (this.userRoleSaveRequestModel) {
        {
          this.userRoleSaveRequestModel.userRole.id = this.selectedUserRoleId
          this.userRoleSaveRequestModel.saveType = this.currentSaveType
          this.userRoleService.save(this.userRoleSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {
                this.alertifyService.success(result.message)
                this.search()
                this.userRoleReset()
                this.onUserRoleModalCloseHandled()
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

  userRoleReset() {
    this.userRoleForm.controls['id'].setValue(-1);
    this.userRoleForm.controls['userId'].setValue(-1);
    this.userRoleForm.controls['roleId'].setValue(-1);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedUserRoleId = 0
  }


  openConfirmModal(UserRoleId: number) {
    this.displayConfirmModal = 'block';
    this.selectedUserRoleId = UserRoleId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }

  deleteFromConfirm() {
    this.delete(this.selectedUserRoleId)
    this.onConfirmModalCloseHandled()
  }

}
