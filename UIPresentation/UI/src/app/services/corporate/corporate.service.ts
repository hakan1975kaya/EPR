import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';
import { CorporateAddRequestModel } from 'src/app/models/corporate-models/corporate-add-request-model';
import { CorporateGetByIdResponseModel } from 'src/app/models/corporate-models/corporate-get-by-id-ResponseModel';
import { CorporateGetListResponseModel } from 'src/app/models/corporate-models/corporate-get-list-response-model';
import { CorporateUpdateRequestModel } from 'src/app/models/corporate-models/corporate-update-request-model';
import { CorporateSearchRequestModel } from 'src/app/models/corporate-models/corporate-serach-request-model';
import { CorporateSearchResponseModel } from 'src/app/models/corporate-models/corporate-search-response-model';
import { CorporateSaveRequestModel } from 'src/app/models/corporate-models/corporate-save-request-model';
import { CorporateGetListPrefixAvailableResponseModel } from 'src/app/models/corporate-models/corporate-get-list-prefix-available-response-model';
import { CorporateGetByCorporateCodeResponseModel } from 'src/app/models/corporate-models/corporate-get-by-corporate-code-response-model';


@Injectable()
export class CorporateService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(corporateAddRequestModel: CorporateAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Corporates/add", corporateAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<CorporateGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<CorporateGetByIdResponseModel>>(environment.path + "Corporates/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<CorporateGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<CorporateGetListResponseModel[]>>(environment.path + "Corporates/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(corporateUpdateRequestModel: CorporateUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "Corporates/update", corporateUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))

  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "Corporates/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  search(corporateSerachRequestModel:CorporateSearchRequestModel): Observable<DataResult<CorporateSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<CorporateSearchResponseModel[]>>(environment.path + "Corporates/search",corporateSerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(corporateSaveRequestModel:CorporateSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Corporates/save", corporateSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getListPrefixAvailable(): Observable<DataResult<CorporateGetListPrefixAvailableResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<CorporateGetListPrefixAvailableResponseModel[]>>(environment.path + "Corporates/getListPrefixAvailable", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getByCorporateCode(corporateCode: number): Observable<DataResult<CorporateGetByCorporateCodeResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<CorporateGetByCorporateCodeResponseModel>>(environment.path + "Corporates/getByCorporateCode?corporateCode=" + corporateCode, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
