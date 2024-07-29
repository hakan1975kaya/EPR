import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';

import { CorporateMailAddressAddRequestModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-add-request-model';
import { CorporateMailAddressGetByIdResponseModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-get-by-id-response-model';
import { CorporateMailAddressGetListResponseModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-get-list-response-model';
import { CorporateMailAddressUpdateRequestModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-update-request-model';
import { CorporateMailAddressSaveRequestModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-save-request-model';
import { CorporateMailAddressSearchResponseModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-search-response-model';
import { CorporateMailAddressSearchRequestModel } from 'src/app/models/corporate-mail-address-models/corporate-mail-address-serach-request-model';

@Injectable()
export class CorporateMailAddressService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(corporateMailAddressAddRequestModel: CorporateMailAddressAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "CorporateMailAddresses/add", corporateMailAddressAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<CorporateMailAddressGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<CorporateMailAddressGetByIdResponseModel>>(environment.path + "CorporateMailAddresses/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<CorporateMailAddressGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<CorporateMailAddressGetListResponseModel[]>>(environment.path + "CorporateMailAddresses/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(corporateMailAddressUpdateRequestModel: CorporateMailAddressUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "CorporateMailAddresses/update", corporateMailAddressUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "CorporateMailAddresses/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  search(corporateMailAddressSerachRequestModel:CorporateMailAddressSearchRequestModel): Observable<DataResult<CorporateMailAddressSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<CorporateMailAddressSearchResponseModel[]>>(environment.path + "CorporateMailAddresses/search",corporateMailAddressSerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(corporateMailAddressSaveRequestModel:CorporateMailAddressSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "CorporateMailAddresses/save", corporateMailAddressSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
