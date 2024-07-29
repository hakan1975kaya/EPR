import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { MenuService } from 'src/app/services/menu/menu.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OrderPipe } from 'src/app/pipes/order/order.pipe';

import { MenuSearchRequestModel } from 'src/app/models/menu-models/menu-serach-request-model';
import { MenuSearchResponseModel } from 'src/app/models/menu-models/menu-search-response-model';
import { MenuSaveRequestModel } from 'src/app/models/menu-models/menu-save-request-model';
import { MenuGetListResponseModel } from 'src/app/models/menu-models/menu-get-list-response-model';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
  providers: [MenuService]
})
export class MenuComponent implements OnInit {
  constructor(
    private menuService: MenuService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }
  currentUserId!: number;

  searchForm!: FormGroup;
  menuSearchRequestModel!: MenuSearchRequestModel;
  menuSearchResponseModels: MenuSearchResponseModel[] = [];

  selectedMenuId!: number;

  fileName = 'Menu';

  currentPage: number = 1;
  itemsPerPage: number = 50

  menuForm!: FormGroup;
  displayMenuModal = 'none';

  linkedMenuIdDefault = -1
  menuOrderDefault = -1

  menuGetListResponseModels!: MenuGetListResponseModel[]

  menuSaveRequestModel: MenuSaveRequestModel = new MenuSaveRequestModel();

  saveTypeEnum = SaveTypeEnum
  currentSaveType!: SaveTypeEnum

  displayConfirmModal = 'none'


  ngOnInit() {
    this.getMenus();
    this.createSearchForm();
    this.createMenuForm();
    this.MenuReset()
    this.currentUserId = this.authService.getCurrentUser().id;
  }

  createSearchForm() {
    this.searchForm = this.formBuilder.group({
      filter: ['', [Validators.required, Validators.minLength(1)]],
    });
  }

  search() {
    if (this.searchForm.valid) {
      this.menuSearchRequestModel = Object.assign({}, this.searchForm.value);
      if (this.menuSearchRequestModel) {
        this.menuService
          .search(this.menuSearchRequestModel)
          .subscribe((dataResult) => {
            if (dataResult) {
              if (dataResult.success) {
                if (dataResult.data) {
                  this.menuSearchResponseModels = dataResult.data;
                }
              }
            }
          });
      }
    }
  }

  orderAsc(columnName: string) {
    this.orderPipe.transform(this.menuSearchResponseModels, columnName, OrderTypeEnum.Asc)
  }

  orderDesc(columnName: string) {
    this.orderPipe.transform(this.menuSearchResponseModels, columnName, OrderTypeEnum.Desc)
  }

  clear() {
    this.searchForm.controls['filter'].setValue('');
  }

  delete(menuId: number) {
    this.menuService.delete(menuId).subscribe((result) => {
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
    const ws: xlsx.WorkSheet = xlsx.utils.json_to_sheet(this.menuSearchResponseModels);
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

  openMenuModal(menuId?: number) {
    this.displayMenuModal = 'block';
    if (menuId) {
      this.selectedMenuId = menuId
      this.currentSaveType = SaveTypeEnum.Update
      this.getById(menuId);
    }
    else {
      this.MenuReset()
    }
  }

  onMenuModalCloseHandled() {
    this.displayMenuModal = 'none';
  }

  createMenuForm() {
    this.menuForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(3)]],
      linkedMenuId: ['', [Validators.required, Validators.min(0)]],
      path: ['', [Validators.required, Validators.minLength(3)]],
      menuOrder: ['', [Validators.required, Validators.min(0)]],
    });
  }

  getMenus() {
    this.menuService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.menuGetListResponseModels = dataResult.data
          }
        }
      }
    })
  }

  getById(menuId: number) {
    this.menuService.getById(menuId).subscribe((dataResult) => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.menuForm.controls['id'].setValue(dataResult.data.id);
            this.menuForm.controls['name'].setValue(dataResult.data.name);
            this.menuForm.controls['description'].setValue(dataResult.data.description);
            this.menuForm.controls['linkedMenuId'].setValue(dataResult.data.linkedMenuId);
            this.menuForm.controls['path'].setValue(dataResult.data.path);
            this.menuForm.controls['menuOrder'].setValue(dataResult.data.menuOrder);
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    });
  }

  save() {
    if (!this.selectedMenuId) {
      this.selectedMenuId = 0
    }

    if (this.menuForm.valid) {

      this.menuSaveRequestModel.menu = Object.assign({}, this.menuForm.value);

      if (this.menuSaveRequestModel) {
        {
          this.menuSaveRequestModel.menu.id = this.selectedMenuId
          this.menuSaveRequestModel.saveType = this.currentSaveType
          this.menuService.save(this.menuSaveRequestModel).subscribe(result => {
            if (result) {
              if (result.success) {

                this.alertifyService.success(result.message)
                this.search()
                this.MenuReset()
                this.onMenuModalCloseHandled()
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

  MenuReset() {
    this.menuForm.controls['id'].setValue(0);
    this.menuForm.controls['name'].setValue('');
    this.menuForm.controls['description'].setValue('');
    this.menuForm.controls['linkedMenuId'].setValue(-1);
    this.menuForm.controls['path'].setValue('');
    this.menuForm.controls['menuOrder'].setValue(-1);
    this.currentSaveType = this.saveTypeEnum.Add
    this.selectedMenuId = 0
  }


  openConfirmModal(menuId: number) {
    this.displayConfirmModal = 'block';
    this.selectedMenuId = menuId
  }

  onConfirmModalCloseHandled() {
    this.displayConfirmModal = 'none';
  }
  deleteFromConfirm() {
    this.delete(this.selectedMenuId)
    this.onConfirmModalCloseHandled()
  }

}
