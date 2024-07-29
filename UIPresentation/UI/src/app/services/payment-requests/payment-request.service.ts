import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';
import { PaymentRequestAddRequestModel } from 'src/app/models/payment-request-models/payment-request-add-request-model';
import { PaymentRequestGetByIdResponseModel } from 'src/app/models/payment-request-models/payment-request-get-by-id-response-model';
import { PaymentRequestGetListResponseModel } from 'src/app/models/payment-request-models/payment-request-get-list-response-model';
import { PaymentRequestUpdateRequestModel } from 'src/app/models/payment-request-models/payment-request-update-request-model';
import { PaymentRequestSearchRequestModel } from 'src/app/models/payment-request-models/payment-request-serach-request-model';
import { PaymentRequestSearchResponseModel } from 'src/app/models/payment-request-models/payment-request-search-response-model';
import { PaymentRequestSaveRequestModel } from 'src/app/models/payment-request-models/payment-request-save-request-model';
import { PaymentRequestGetByTodayResponseModel } from 'src/app/models/payment-request-models/payment-request-get-by-today-response-model';
import { PaymentRequestDownloadRequestModel } from 'src/app/models/payment-request-models/payment-request-download-request-model';
import { PaymentRequestDownloadResponseModel } from 'src/app/models/payment-request-models/payment-request-download-response-model';
import { PaymentRequestGetByRequestNumberResponseModel } from 'src/app/models/payment-request-models/payment-request-get-by-request-number-response-model';
import { PaymentRequestGetListByCorporateIdResponseModel } from 'src/app/models/payment-request-models/payment-request-get-list-by-corporate-id-response-model';

@Injectable()
export class PaymentRequestService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(paymentRequestAddRequestModel: PaymentRequestAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "PaymentRequests/add", paymentRequestAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<PaymentRequestGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestGetByIdResponseModel>>(environment.path + "PaymentRequests/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<PaymentRequestGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestGetListResponseModel[]>>(environment.path + "PaymentRequests/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(paymentRequestUpdateRequestModel: PaymentRequestUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "PaymentRequests/update", paymentRequestUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "PaymentRequests/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  search(paymentRequestSerachRequestModel:PaymentRequestSearchRequestModel): Observable<DataResult<PaymentRequestSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<PaymentRequestSearchResponseModel[]>>(environment.path + "PaymentRequests/search",paymentRequestSerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(paymentRequestSaveRequestModel:PaymentRequestSaveRequestModel ): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "PaymentRequests/save", paymentRequestSaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getByToday(): Observable<DataResult<PaymentRequestGetByTodayResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestGetByTodayResponseModel[]>>(environment.path + "PaymentRequests/getByToday", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  paymentRequestDownload(paymentRequestDownloadRequestDto:PaymentRequestDownloadRequestModel): Observable<DataResult<PaymentRequestDownloadResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<PaymentRequestDownloadResponseModel>>(environment.path + "PaymentRequests/paymentRequestDownload",paymentRequestDownloadRequestDto, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getByRequestNumber(requestNumber: string): Observable<DataResult<PaymentRequestGetByRequestNumberResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestGetByRequestNumberResponseModel>>(environment.path + "PaymentRequests/getByRequestNumber?requestNumber=" + requestNumber, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getListByCorporateId(corporateId:number): Observable<DataResult<PaymentRequestGetListByCorporateIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestGetListByCorporateIdResponseModel[]>>(environment.path + "PaymentRequests/getListByCorporateId?corporateId="+corporateId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }
}
