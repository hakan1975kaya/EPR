import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Router } from '@angular/router';

import { AlertifyService } from '../alertify/alertify.service';
import { JwtHelperService } from '@auth0/angular-jwt'
import { PipeService } from '../pipe/pipe.service';
import { Observable, catchError, tap } from 'rxjs';

import { environment } from '../../../environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { DataResult } from '../../models/result-models/data-result';

import { LoginRequestModel } from 'src/app/models/auth-models/login-request-model';
import { AccessTokenModel } from 'src/app/models/auth-models/access-token-model';
import { RegisterRequestModel } from 'src/app/models/auth-models/register-request-model';
import { OperationClaimGetListByUserIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-get-list-by-user-id-response-model';
import { MenuListGetByUserIdResponseModel } from 'src/app/models/menu-models/menu-list-get-by-user-id-response-model';
import { UserGetByRegistrationNumberResponseModel } from 'src/app/models/user-models/user-get-by-user-name-response-model';


@Injectable()
export class AuthService {

  constructor(
    private httpClient: HttpClient,
    private alertifyService: AlertifyService,
    private router: Router,
    private pipeService: PipeService,
  ) { }

  @Output() activateApp: EventEmitter<void> = new EventEmitter<void>();
  jwtHelperService: JwtHelperService = new JwtHelperService()

  menuListGetByUserId(userId: number):Observable<DataResult<MenuListGetByUserIdResponseModel[]>> {
  return  this.httpClient.get<DataResult<MenuListGetByUserIdResponseModel[]>>(environment.path + "Auths/menuListGetByUserId?userId=" + userId)
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  setCurrentUser(registrationNumber:number) {
    this.httpClient.get<DataResult<UserGetByRegistrationNumberResponseModel>>(environment.path + "Auths/userGetByRegistrationNumber?registrationNumber=" + registrationNumber)
      .pipe(
        tap((result) => {
          this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result));
        }),
        catchError((error) => this.pipeService.handleError(error))
      )
      .subscribe(dataResult => {
        if (dataResult) {
          if (dataResult.success) {
            if (dataResult.data) {
              localStorage.setItem("currentUser", JSON.stringify(dataResult.data))

              this.setOperationClaims(dataResult.data.id)
            }
          }
        }
      })
  }

  getCurrentUser(): UserGetByRegistrationNumberResponseModel {
    let currentuser = localStorage.getItem("currentUser") == null ? "{}" : localStorage.getItem("currentUser")?.toString()

    return JSON.parse(currentuser == undefined ? "{}" : currentuser)
  }

  setOperationClaims(userId: number) {
    this.httpClient.get<DataResult<OperationClaimGetListByUserIdResponseModel[]>>(environment.path + "Auths/operationClaimGetListByUserId?userId=" + userId)
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
      .subscribe(dataResult => {
        if (dataResult) {
          if (dataResult.success) {
            if (dataResult.data) {
              localStorage.setItem("operationClaims", JSON.stringify(dataResult.data))
              this.activateApp.emit()
            }
          }
        }
      })
  }

  getOperationClaims() {
    return localStorage.getItem("operationClaims")
  }

  login(loginRequestModel: LoginRequestModel) {
    this.httpClient.post<DataResult<AccessTokenModel>>(environment.path + "Auths/login", loginRequestModel)
      .pipe(
        tap((result) => {
          this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result));
        }),
        catchError((error) => this.pipeService.handleError(error))
      )
      .subscribe(dataResult => {
        if (dataResult) {
          if (dataResult.success) {
            if (dataResult.data) {
              if (dataResult.data.token) {
                localStorage.setItem("token", dataResult.data.token)

                this.setCurrentUser(loginRequestModel.registrationNumber)

                this.router.navigateByUrl('dashboard')
              }
            }
          }
          else {
            this.alertifyService.error(dataResult.message)
          }
        }
      })
  }

  register(registerRequestModel: RegisterRequestModel) {
    this.httpClient.post<DataResult<AccessTokenModel>>(environment.path + "Auths/register", registerRequestModel)
      .pipe(
        tap((result) => {
          this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result));
        }),
        catchError((error) => this.pipeService.handleError(error))
      )
      .subscribe(dataResult => {
        if (dataResult) {
          if (dataResult.success) {
            if (dataResult.data) {
              if (dataResult.data.token) {

                localStorage.setItem("token", dataResult.data.token)

                this.setCurrentUser(registerRequestModel.registrationNumber)

                this.router.navigateByUrl('dashboard')
              }
            }
          }
          else {
            this.alertifyService.error(dataResult.message)
          }
        }
      })
  }

  isLogIn() {
    return !this.jwtHelperService.isTokenExpired(this.getToken())
  }

  getToken() {
    return localStorage.getItem("token")
  }

  decodeToken() {
    let token = this.getToken()?.toString()
    return this.jwtHelperService.decodeToken(token == undefined ? "" : token.toString())
  }

  logOut() {
    localStorage.removeItem("token")
  }





}
