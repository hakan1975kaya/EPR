import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';

import { MenuOperationClaimAddRequestModel } from 'src/app/models/menu-operation-claim-models/menu-operation-claim-add-request-model';
import { MenuOperationClaimGetByIdResponseModel } from 'src/app/models/menu-operation-claim-models/mmenu-operation-claim-get-by-id-response-model';
import { MenuOperationClaimGetListResponseModel } from 'src/app/models/menu-operation-claim-models/menu-operation-claim-get-list-response-model';
import { MenuOperationClaimUpdateRequestModel } from 'src/app/models/menu-operation-claim-models/menu-operation-claim-update-request-model';
import { MenuOperationClaimSaveRequestModel } from 'src/app/models/menu-operation-claim-models/menu-operation-claim-save-request-model';

@Injectable()
export class MenuOperationClaimService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(menuOperationClaimAddRequestModel: MenuOperationClaimAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "MenuOperationClaims/add", menuOperationClaimAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<MenuOperationClaimGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MenuOperationClaimGetByIdResponseModel>>(environment.path + "MenuOperationClaims/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<MenuOperationClaimGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MenuOperationClaimGetListResponseModel[]>>(environment.path + "MenuOperationClaims/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(menuOperationClaimUpdateRequestModel: MenuOperationClaimUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "MenuOperationClaims/update", menuOperationClaimUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))

  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "MenuOperationClaims/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(menuOperationClaimSaveRequestModel:MenuOperationClaimSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "MenuOperationClaims/save", menuOperationClaimSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
