import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';

import { OperationClaimAddRequestModel } from 'src/app/models/operation-claim-models/operation-claim-add-request-model';
import { OperationClaimGetByIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-get-by-id-response-model';
import { OperationClaimGetListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-get-list-response-model';
import { OperationClaimUpdateRequestModel } from 'src/app/models/operation-claim-models/operation-claim-update-request-model';
import { OperationClaimGetListByUserIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-get-list-by-user-id-response-model';
import { OperationClaimSearchRequestModel } from 'src/app/models/operation-claim-models/operation-claim-serach-request-model';
import { OperationClaimSearchResponseModel } from 'src/app/models/operation-claim-models/operation-claim-search-response-model';
import { OperationClaimSaveRequestModel } from 'src/app/models/operation-claim-models/operation-claim-save-request-model';
import { OperationClaimParentListGetByUserIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-get-by-user-id-response-model';
import { OperationClaimChildListGetByUserIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-child-list-get-by-user-id-response-model ';
import { OperationClaimParentListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-response-model';
import { OperationClaimChildListResponseModel } from 'src/app/models/operation-claim-models/operation-claim-child-list-response-model ';
import { OperationClaimParentListGetByMenuIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-get-by-menu-id-response-model';
import { OperationClaimChildListGetByMenuIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-child-list-get-by-menu-id-response-model ';
import { OperationClaimParentListGetByRoleIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-parent-list-get-by-role-id-response-model';
import { OperationClaimChildListGetByRoleIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-child-list-get-by-role-id-response-model ';

@Injectable()
export class OperationClaimService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(operationClaimAddRequestModel: OperationClaimAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "OperationClaims/add", operationClaimAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<OperationClaimGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimGetByIdResponseModel>>(environment.path + "OperationClaims/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<OperationClaimGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimGetListResponseModel[]>>(environment.path + "OperationClaims/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(operationClaimUpdateRequestModel: OperationClaimUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "OperationClaims/update", operationClaimUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "OperationClaims/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getListByUserId(userId:number): Observable<DataResult<OperationClaimGetListByUserIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimGetListByUserIdResponseModel[]>>(environment.path + "OperationClaims/getListByUserId?userId=userId"+userId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimParentList(): Observable<DataResult<OperationClaimParentListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimParentListResponseModel[]>>(environment.path + "OperationClaims/OperationClaimParentList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimChildList(): Observable<DataResult<OperationClaimChildListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimChildListResponseModel[]>>(environment.path + "OperationClaims/OperationClaimChildList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimParentListGetByUserId(userId:number): Observable<DataResult<OperationClaimParentListGetByUserIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimParentListGetByUserIdResponseModel[]>>(environment.path + "OperationClaims/OperationClaimParentListGetByUserId?userId="+userId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimChildListGetByUserId(userId:number): Observable<DataResult<OperationClaimChildListGetByUserIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimChildListGetByUserIdResponseModel[]>>(environment.path + "OperationClaims/OperationClaimChildListGetByUserId?userId="+userId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimParentListGetByRoleId(roleId:number): Observable<DataResult<OperationClaimParentListGetByRoleIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimParentListGetByRoleIdResponseModel[]>>(environment.path + "OperationClaims/OperationClaimParentListGetByRoleId?RoleId="+roleId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimChildListGetByRoleId(roleId:number): Observable<DataResult<OperationClaimChildListGetByRoleIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimChildListGetByRoleIdResponseModel[]>>(environment.path + "OperationClaims/OperationClaimChildListGetByRoleId?RoleId="+roleId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }


  operationClaimParentListGetByMenuId(menuId:number): Observable<DataResult<OperationClaimParentListGetByMenuIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimParentListGetByMenuIdResponseModel[]>>(environment.path + "OperationClaims/OperationClaimParentListGetByMenuId?menuId="+menuId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  operationClaimChildListGetByMenuId(menuId:number): Observable<DataResult<OperationClaimChildListGetByMenuIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<OperationClaimChildListGetByMenuIdResponseModel[]>>(environment.path + "OperationClaims/OperationClaimChildListGetByMenuId?menuId="+menuId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }


  search(operationClaimSerachRequestModel:OperationClaimSearchRequestModel): Observable<DataResult<OperationClaimSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<OperationClaimSearchResponseModel[]>>(environment.path + "OperationClaims/search",operationClaimSerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(operationClaimSaveRequestModel:OperationClaimSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "OperationClaims/save", operationClaimSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
