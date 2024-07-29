import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";

import { AuthService } from "../services/auth/auth.service";

import { MenuListGetByUserIdResponseModel } from "../models/menu-models/menu-list-get-by-user-id-response-model";

@Injectable()
export class AuthorizationGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }
    menuListGetByUserIdResponseModel!: MenuListGetByUserIdResponseModel[]

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let isAuthorization = false
        let currentUserId = this.authService.getCurrentUser().id;
        if (currentUserId) {
            this.menuListGetByUserId(currentUserId)
            if (this.menuListGetByUserIdResponseModel) {
                if (this.menuListGetByUserIdResponseModel.length > 0) {
                    this.menuListGetByUserIdResponseModel.forEach(menu => {
                        if (menu.path == route.url[0].path) {
                            isAuthorization = true
                        }
                    })
                }
            }
        }
        return isAuthorization
    }

    menuListGetByUserId(userId: number) {
        this.authService.menuListGetByUserId(userId).subscribe(dataResult => {
            if (dataResult) {
                if (dataResult.success) {
                    if (dataResult.data) {
                        this.menuListGetByUserIdResponseModel = dataResult.data
                    }
                }
            }
        })
    }


}
