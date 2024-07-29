import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';
import { PaymentRequestDetailAddRequestModel } from 'src/app/models/payment-request-datail-models/payment-request-detail-add-request-model';
import { PaymentRequestDetailGetByIdResponseModel } from 'src/app/models/payment-request-datail-models/payment-request-detail-get-by-id-response-model';
import { PaymentRequestDetailGetListResponseModel } from 'src/app/models/payment-request-datail-models/payment-request-detail-get-list-response-model';
import { PaymentRequestDetailUpdateRequestModel } from 'src/app/models/payment-request-datail-models/payment-request-detail-update-request-model';
import { PaymentRequestDetailGetListByPaymentRequestIdResponseModel } from 'src/app/models/payment-request-datail-models/payment-request-detail-get-list-by-payment-request-id-response-model';




@Injectable()
export class PaymentRequestDetailService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(paymentRequestDetailAddRequestModel: PaymentRequestDetailAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "PaymentRequestDetails/add", paymentRequestDetailAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<PaymentRequestDetailGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestDetailGetByIdResponseModel>>(environment.path + "PaymentRequestDetails/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<PaymentRequestDetailGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestDetailGetListResponseModel[]>>(environment.path + "PaymentRequestDetails/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(aymentRequestDetailUpdateRequestModel: PaymentRequestDetailUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "PaymentRequestDetails/update", aymentRequestDetailUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))

  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "PaymentRequestDetails/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getListByPaymentRequestId(paymentRequestId:number): Observable<DataResult<PaymentRequestDetailGetListByPaymentRequestIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestDetailGetListByPaymentRequestIdResponseModel[]>>(environment.path + "PaymentRequestDetails/getListByPaymentRequestId?PaymentRequestId="+paymentRequestId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }
}
