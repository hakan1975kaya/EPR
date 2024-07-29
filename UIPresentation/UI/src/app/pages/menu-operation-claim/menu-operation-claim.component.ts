import { Component, OnInit } from '@angular/core';
import * as xlsx from 'xlsx';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { AlertifyService } from '../../services/alertify/alertify.service';

import { OperationClaimService } from 'src/app/services/operation-claim/operation-claim.service';
import { MenuService } from 'src/app/services/menu/menu.service';

import { SaveTypeEnum } from 'src/app/enums/save-type-enum.enum';
import { OrderPipe } from 'src/app/pipes/order/order.pipe';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

import { OperationClaim } from 'src/app/models/operation-claim-models/operation-claim';
import { OperationClaimParentListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-response-model';
import { MenuOperationClaimService } from 'src/app/services/menu-operation-claim/menu-operation-claim.service';
import { MenuSearchRequestModel } from 'src/app/models/menu-models/menu-serach-request-model';
import { MenuSearchResponseModel } from 'src/app/models/menu-models/menu-search-response-model';
import { MenuOperationClaimSaveRequestModel } from 'src/app/models/menu-operation-claim-models/menu-operation-claim-save-request-model';
import { Menu } from 'src/app/models/menu-models/menu';
import { OperationClaimParentListGetByMenuIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-get-by-menu-id-response-model';

@Component({
  selector: 'app-menu-operation-claim',
  templateUrl: './menu-operation-claim.component.html',
  styleUrls: ['./menu-operation-claim.component.css'],
  providers: [MenuService, OperationClaimService, MenuOperationClaimService],
})
export class MenuOperationClaimComponent implements OnInit {
  constructor(
    private menuService: MenuService,
    private menuOperationClaimService: MenuOperationClaimService,
    private operationClaimService: OperationClaimService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private orderPipe: OrderPipe
  ) { }

  currentMenuId!: number;

  searchForm!: FormGroup;

  menuSearchRequestModel!: MenuSearchRequestModel;
  menuSearchResponseModels: MenuSearchResponseModel[] = [];

  currentPage: number = 1;
  itemsPerPage: number = 50

  fileName = 'Menu';

  selectedMenuId!: number

  menuOperationClaimForm!: FormGroup;
  displayMenuOperationClaimModal = 'none';

  operationClaimParentListResponseModels: OperationClaimParentListResponseModel[] = []

  operationClaimParentListGetByMenuIdResponseModels: OperationClaimParentListGetByMenuIdResponseModel[] = []

  selectedOperationClaims: OperationClaim[] = []

  menuOperationClaimSaveRequestModel: MenuOperationClaimSaveRequestModel = new MenuOperationClaimSaveRequestModel();

  saveTypeEnumMenuOperationClaim = SaveTypeEnum
  currentSaveTypeMenuOperationClaim!: SaveTypeEnum

  ngOnInit() {

    this.createSearchForm();

    this.createMenuOperationClaimForm();

    this.getParentOperationClaim()

    this.menuOperationClaimReset()

    this.currentMenuId = this.authService.getCurrentUser().id;
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
        this.menuService.search(this.menuSearchRequestModel).subscribe((dataResult) => {
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

  openMenuOperationClaimModal(menuId?: number) {
    this.displayMenuOperationClaimModal = 'block';
    if (menuId) {
      this.currentSaveTypeMenuOperationClaim = SaveTypeEnum.Update
      this.selectedMenuId = menuId

      this.getByMenuId(menuId)
    }
    else {
      this.currentSaveTypeMenuOperationClaim = SaveTypeEnum.Add
      this.menuOperationClaimReset()
    }
  }

  getByMenuId(menuId: number) {
    this.selectedOperationClaims = []

    this.operationClaimParentListResponseModels.forEach(operationClaim => {
      operationClaim.isChecked = false;
    })

    this.operationClaimService.operationClaimParentListGetByMenuId(menuId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.operationClaimParentListGetByMenuIdResponseModels = dataResult.data
            this.operationClaimParentListResponseModels.forEach(operationClaim => {
              this.operationClaimParentListGetByMenuIdResponseModels.forEach(checkedOperationClaim => {
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

  onMenuOperationClaimModalCloseHandled() {
    this.displayMenuOperationClaimModal = 'none';
  }


  createMenuOperationClaimForm() {
    this.menuOperationClaimForm = this.formBuilder.group({
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

  setParentOperationClaimCheck(operationClaimId: number, event: any) {
    if (event.currentTarget.checked) {

      this.operationClaimParentListResponseModels.forEach(operationClaim => {
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

  menuOperationClaimSave() {
    if (!this.selectedMenuId) {
      this.selectedMenuId = 0
    }

    this.menuService.getById(this.selectedMenuId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.menuOperationClaimSaveRequestModel.menu = new Menu()
            this.menuOperationClaimSaveRequestModel.menu = dataResult.data

            this.menuOperationClaimSaveRequestModel.operationClaims = []
            this.menuOperationClaimSaveRequestModel.operationClaims = this.selectedOperationClaims
            this.menuOperationClaimSaveRequestModel.saveType = this.currentSaveTypeMenuOperationClaim

            this.menuOperationClaimService.save(this.menuOperationClaimSaveRequestModel).subscribe(result => {
              if (result) {
                if (result.success) {

                  this.alertifyService.success(result.message)
                  this.search()
                  this.menuOperationClaimReset()
                  this.onMenuOperationClaimModalCloseHandled()
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

  menuOperationClaimReset() {
    this.selectedOperationClaims = []
    this.currentMenuId = 0
    this.currentSaveTypeMenuOperationClaim = this.saveTypeEnumMenuOperationClaim.Add
  }

  menuOperationClaimCancel() {
    this.menuOperationClaimReset()
    this.displayMenuOperationClaimModal = 'none';
  }


}
