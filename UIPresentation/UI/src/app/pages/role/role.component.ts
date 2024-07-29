import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { RoleService } from 'src/app/services/role/role.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';
import { RoleSearchRequestModel } from 'src/app/models/role-models/role-serach-request-model';
import { RoleSearchResponseModel } from 'src/app/models/role-models/role-search-response-model';
import { RoleGetListResponseModel } from 'src/app/models/role-models/role-get-list-response-model';
import { RoleSaveRequestModel } from 'src/app/models/role-models/role-save-request-model';


@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css'],
  providers: [RoleService]
})
export class RoleComponent implements OnInit {
  constructor(
    private roleService: RoleService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }
  currentUserId!: number;

  searchForm!: FormGroup;
  roleSearchRequestModel!: RoleSearchRequestModel;
  roleSearchResponseModels: RoleSearchResponseModel[] = [];

  selectedRoleId!: number;

  fileName = 'Role';

  currentPage: number = 1;
  itemsPerPage: number = 50

  roleForm!: FormGroup;
  displayRoleModal = 'none';

  linkedRoleIdDefault = -1

  roleGetListResponseModels!: RoleGetListResponseModel[]

  roleSaveRequestModel: RoleSaveRequestModel = new RoleSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'

  ngOnInit() {
    this.getRoles();
    this.createSearchForm();
    this.createRoleForm();
    this.RoleReset()
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
        this.roleService
          .search(this.roleSearchRequestModel)
          .subscribe((dataResult) => {
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

  delete(RoleId: number) {
    this.roleService.delete(RoleId).subscribe((result) => {
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

  openRoleModal(RoleId?: number) {
    this.displayRoleModal = 'block';
    if (RoleId) {
      this.selectedRoleId = RoleId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(RoleId);
    }
    else {
      this.RoleReset()
    }
  }

  onRoleModalCloseHandled() {
    this.displayRoleModal = 'none';
  }

  createRoleForm() {
    this.roleForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(3)]],
    });
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

  getById(RoleId: number) {
    this.roleService.getById(RoleId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.roleForm.controls['id'].setValue(dataResult.data.id);
            this.roleForm.controls['name'].setValue(dataResult.data.name);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  save() {
    if (!this.selectedRoleId) {
      this.selectedRoleId = 0
    }

    if (this.roleForm.valid) {

      this.roleSaveRequestModel.role = Object.assign({}, this.roleForm.value);

      if (this.roleSaveRequestModel) {
        {
          this.roleSaveRequestModel.role.id = this.selectedRoleId
          this.roleSaveRequestModel.saveType = this.currentSaveType
          this.roleService.save(this.roleSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {

                this.alertifyService.success(result.message)
                this.search()
                this.RoleReset()
                this.onRoleModalCloseHandled()
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

  RoleReset() {
    this.roleForm.controls['id'].setValue('0');
    this.roleForm.controls['name'].setValue('');
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedRoleId = 0
  }

  openConfirmModal(RoleId: number) {
    this.displayConfirmModal = 'block';
    this.selectedRoleId = RoleId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }

  deleteFromConfirm() {
    this.delete(this.selectedRoleId)
    this.onConfirmModalCloseHandled()
  }

}
