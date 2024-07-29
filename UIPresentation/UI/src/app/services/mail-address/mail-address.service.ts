import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';
import { MailAddressAddRequestModel } from 'src/app/models/mail-address-models/mail-address-add-request-model';
import { MailAddressGetByIdResponseModel } from 'src/app/models/mail-address-models/mail-address-get-by-id-response-model';
import { MailAddressGetListResponseModel } from 'src/app/models/mail-address-models/mail-address-get-list-response-model';
import { MailAddressUpdateRequestModel } from 'src/app/models/mail-address-models/mail-address-update-request-model';
import { MailAddressSearchRequestModel } from 'src/app/models/mail-address-models/mail-address-serach-request-model';
import { MailAddressSearchResponseModel } from 'src/app/models/mail-address-models/mail-address-search-response-model';
import { MailAddressSaveRequestModel } from 'src/app/models/mail-address-models/mail-address-save-request-model';
import { MailAddressGetListPttResposeModel } from 'src/app/models/mail-address-models/mail-address-get-list-ptt-respose-model';
import { MailAddressGetListNotPttResposeModel } from 'src/app/models/mail-address-models/mail-address-get-list-not-ptt-respose-model';

@Injectable()
export class MailAddressService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(mailAddressAddRequestModel: MailAddressAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "MailAddresses/add", mailAddressAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<MailAddressGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MailAddressGetByIdResponseModel>>(environment.path + "MailAddresses/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<MailAddressGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MailAddressGetListResponseModel[]>>(environment.path + "MailAddresses/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(mailAddressUpdateRequestModel: MailAddressUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "MailAddresses/update", mailAddressUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "MailAddresses/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  search(mailAddressSerachRequestModel:MailAddressSearchRequestModel): Observable<DataResult<MailAddressSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<MailAddressSearchResponseModel[]>>(environment.path + "MailAddresses/search",mailAddressSerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(mailAddressSaveRequestModel:MailAddressSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "MailAddresses/save", mailAddressSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  
  getListPtt(): Observable<DataResult<MailAddressGetListPttResposeModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MailAddressGetListPttResposeModel[]>>(environment.path + "MailAddresses/getListPtt", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getListNotPtt(): Observable<DataResult<MailAddressGetListNotPttResposeModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<MailAddressGetListNotPttResposeModel[]>>(environment.path + "MailAddresses/getListNotPtt", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
