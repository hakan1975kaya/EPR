import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';

import { MenuAddRequestModel } from 'src/app/models/menu-models/menu-add-request-model';
import { MenuGetByIdResponseModel } from 'src/app/models/menu-models/menu-get-by-id-response-model';
import { MenuGetListResponseModel } from 'src/app/models/menu-models/menu-get-list-response-model';
import { MenuUpdateRequestModel } from 'src/app/models/menu-models/menu-update-request-model';
import { MenuParentListGetByUserIdResponseModel } from 'src/app/models/menu-models/menu-parent-list-get-by-user-id-response-model';
import { MenuChildListGetByUserIdResponseModel } from 'src/app/models/menu-models/menu-child-list-get-by-user-id-response-model ';
import { MenuSearchRequestModel } from 'src/app/models/menu-models/menu-serach-request-model';
import { MenuSearchResponseModel } from 'src/app/models/menu-models/menu-search-response-model';
import { MenuSaveRequestModel } from 'src/app/models/menu-models/menu-save-request-model';

@Injectable()
export class MenuService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(menuAddRequestModel: MenuAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Menus/add", menuAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<MenuGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MenuGetByIdResponseModel>>(environment.path + "Menus/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<MenuGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MenuGetListResponseModel[]>>(environment.path + "Menus/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(menuUpdateRequestModel: MenuUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "Menus/update", menuUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))

  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "Menus/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  menuParentListGetByUserId(userId:number): Observable<DataResult<MenuParentListGetByUserIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MenuParentListGetByUserIdResponseModel[]>>(environment.path + "Menus/menuParentListGetByUserId?userId="+userId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  menuChildListGetByUserId(userId:number): Observable<DataResult<MenuChildListGetByUserIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MenuChildListGetByUserIdResponseModel[]>>(environment.path + "Menus/menuChildListGetByUserId?userId="+userId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  search(menuSerachRequestModel:MenuSearchRequestModel): Observable<DataResult<MenuSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<MenuSearchResponseModel[]>>(environment.path + "Menus/search",menuSerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(menuSaveRequestModel:MenuSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Menus/save", menuSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }
}
