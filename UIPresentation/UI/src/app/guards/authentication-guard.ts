import { ActivatedRouteSnapshot, CanActivate, Route, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Injectable } from "@angular/core";

import { AuthService } from "../services/auth/auth.service";

@Injectable()
export class AuthenticationGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router,
  ) { }

  isLogin: boolean = false

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    this.isLogin = this.authService.isLogIn()
    if (this.isLogin) {
      return true
    }
    else
    {
      this.router.navigateByUrl("login")
      return false
    }

  }

}
