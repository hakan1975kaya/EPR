import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserService } from 'src/app/services/User/User.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { UserSearchResponseModel } from 'src/app/models/user-models/user-search-response-model';
import { UserSearchRequestModel } from 'src/app/models/user-models/user-serach-request-model';
import { UserSaveRequestModel } from 'src/app/models/user-models/user-save-request-model';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }
  currentUserId!: number;

  searchForm!: FormGroup;
  userSearchRequestModel!: UserSearchRequestModel;
  userSearchResponseModels: UserSearchResponseModel[] = [];

  selectedUserId!: number;

  fileName = 'User';

  currentPage: number = 1;
  itemsPerPage: number = 50

  userForm!: FormGroup;
  displayUserModal = 'none';

  isPasswordSeted: boolean = false

  userSaveRequestModel: UserSaveRequestModel = new UserSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'


  ngOnInit() {
    this.createSearchForm();
    this.createUserForm();
    this.userReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.userSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.userSearchRequestModel) {
        this.userService
          .search(this.userSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.userSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.userSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.userSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(userId: number) {
    this.userService.delete(userId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.userSearchResponseModels);
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

  openUserModal(userId?: number) {
    this.displayUserModal = 'block';
    if (userId) {
      this.selectedUserId = userId
      this.isPasswordSeted = false
      this.userForm.controls['passwordIsSet'].setValue(false);
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(userId);
    }
    else {
      this.userReset()
    }
  }

  onUserModalCloseHandled() {
    this.displayUserModal = 'none';
  }

  createUserForm() {
    this.userForm = this.formBuilder.group({
      id: [''],
      registrationNumber: ['', [Validators.required, Validators.min(3)]],
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      password: [''],
      passwordIsSet: ['']
    });
  }

  getById(userId: number) {
    this.userService.getById(userId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.userForm.controls['id'].setValue(dataResult.data.id);
            this.userForm.controls['registrationNumber'].setValue(dataResult.data.registrationNumber);
            this.userForm.controls['firstName'].setValue(dataResult.data.firstName);
            this.userForm.controls['lastName'].setValue(dataResult.data.lastName);
            this.userForm.controls['password'].setValue(dataResult.data.password);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  setIsPasswordSeted(event: any) {
    this.isPasswordSeted = event.currentTarget.checked
  }

  save() {
    if (!this.selectedUserId) {
      this.selectedUserId = 0
    }

    if (this.userForm.valid) {

      this.userSaveRequestModel.user = Object.assign({}, this.userForm.value);

      if (this.userSaveRequestModel) {
        {
          this.userSaveRequestModel.user.id = this.selectedUserId
          this.userSaveRequestModel.isPasswordSeted = this.isPasswordSeted

          if (!this.isPasswordSeted) {
            this.userSaveRequestModel.user.password = ""
          }

          this.userSaveRequestModel.saveType = this.currentSaveType

          this.userService.save(this.userSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {

                this.alertifyService.success(result.message)
                this.search()
                this.userReset()
                this.onUserModalCloseHandled()
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

  userReset() {
    this.userForm.controls['id'].setValue('0');
    this.userForm.controls['registrationNumber'].setValue(0);
    this.userForm.controls['firstName'].setValue('');
    this.userForm.controls['lastName'].setValue('');
    this.userForm.controls['password'].setValue('');
    this.userForm.controls['passwordIsSet'].setValue(false);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedUserId = 0
    this.isPasswordSeted = false
  }


  openConfirmModal(userId: number) {
    this.displayConfirmModal = 'block';
    this.selectedUserId = userId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }
  deleteFromConfirm() {
    this.delete(this.selectedUserId)
    this.onConfirmModalCloseHandled()
  }

}
