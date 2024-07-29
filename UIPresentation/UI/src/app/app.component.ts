import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './services/auth/auth.service';
import { AlertifyService } from './services/alertify/alertify.service';

import { UserGetByRegistrationNumberResponseModel } from './models/user-models/user-get-by-user-name-response-model';
import { MenuChildListGetByUserIdResponseModel } from './models/menu-models/menu-child-list-get-by-user-id-response-model ';
import { MenuParentListGetByUserIdResponseModel } from './models/menu-models/menu-parent-list-get-by-user-id-response-model';
import { MenuService } from './services/menu/menu.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private menuService: MenuService,
    private alertifyService: AlertifyService,
    private router: Router) { }

  title = 'PTT Bütçe';
  isLogin!: boolean
  currentUserId!: number
  userGetByRegistrationNumberResponseModel!: UserGetByRegistrationNumberResponseModel

  menuChildListGetByUserIdResponseModels !: MenuChildListGetByUserIdResponseModel[]
  menuParentListGetByUserIdResponseModels !: MenuParentListGetByUserIdResponseModel[]

  selectedMenuId!: number

  childToParentData!:string

  activateApp() {
    this.ngOnInit()
  }

  ngOnInit(): void {
    this.isLogin = this.authService.isLogIn()
    if (this.isLogin) {
      this.userGetByRegistrationNumberResponseModel = this.authService.getCurrentUser()
      if (this.userGetByRegistrationNumberResponseModel) {
        if (this.userGetByRegistrationNumberResponseModel.id) {
          this.currentUserId = this.userGetByRegistrationNumberResponseModel.id
          if (this.currentUserId) {
            this.getParentMenuByUserId(this.currentUserId)
          }
        }
      }
    }
  }

  logOut() {
    this.authService.logOut()
    if (!this.authService.getToken()) {
      this.router.navigateByUrl('login')
      this.isLogin = false
    }
  }

  getParentMenuByUserId(userId: number) {
    this.menuService.menuParentListGetByUserId(userId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.menuParentListGetByUserIdResponseModels = dataResult.data
          }
        }
        else {
          this.alertifyService.error(dataResult.message)
        }
      }
    })
  }

  getChildMenuByUserId(userId: number) {
    this.menuService.menuChildListGetByUserId(userId).subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.menuChildListGetByUserIdResponseModels = dataResult.data
          }
        }
        else {
          this.alertifyService.error(dataResult.message)
        }
      }
    })
  }

  setMenuId(menuId: number) {
    this.selectedMenuId = menuId
  }


}

