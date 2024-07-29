import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';

import { PaymentRequestSummaryAddRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-add-request-model';
import { PaymentRequestSummaryGetByIdResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-get-by-id-response-model';
import { PaymentRequestSummaryGetListResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-get-list-response-model';
import { PaymentRequestSummaryUpdateRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-update-request-model';
import { PaymentRequestSummarySearchRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-serach-request-model';
import { PaymentRequestSummarySearchResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-search-response-model';
import { PaymentRequestSummarySaveRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-save-request-model';
import { PaymentRequestSummaryGetListByPaymentRequestIdResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-get-list-by-payment-request-id-response-model';
import { PaymentRequestSummaryAmountByCorporateIdYearResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-amount-by-corporate-id-year-response-model';
import { PaymentRequestSummaryAmountByCorporateIdYearRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-amount-by-corporate-id-year-request-model';
import { PaymentRequestSummaryGetByTodayResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-get-by-today-response-model';
import { PaymentRequestSummaryTotalOutgoingPaymentResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-total-outgoing-payment-response-model';

@Injectable()
export class PaymentRequestSummaryService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(paymentRequestSummaryAddRequestModel: PaymentRequestSummaryAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "PaymentRequestSummarys/add", paymentRequestSummaryAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<PaymentRequestSummaryGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestSummaryGetByIdResponseModel>>(environment.path + "PaymentRequestSummarys/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<PaymentRequestSummaryGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestSummaryGetListResponseModel[]>>(environment.path + "PaymentRequestSummarys/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(paymentRequestSummaryUpdateRequestModel: PaymentRequestSummaryUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "PaymentRequestSummarys/update", paymentRequestSummaryUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))

  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "PaymentRequestSummarys/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }


  search(paymentRequestSummarySerachRequestModel: PaymentRequestSummarySearchRequestModel): Observable<DataResult<PaymentRequestSummarySearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<PaymentRequestSummarySearchResponseModel[]>>(environment.path + "PaymentRequestSummaries/search", paymentRequestSummarySerachRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  save(paymentRequestSummarySaveRequestModel: PaymentRequestSummarySaveRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "PaymentRequestSummaries/save", paymentRequestSummarySaveRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getListByPaymentRequestId(paymentRequestId: number): Observable<DataResult<PaymentRequestSummaryGetListByPaymentRequestIdResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestSummaryGetListByPaymentRequestIdResponseModel[]>>(environment.path + "PaymentRequestSummaries/getListByPaymentRequestId?paymentRequestId=" + paymentRequestId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  amountByCorporateIdYear(paymentRequestSummaryAmountByCorporateIdYearRequestModel: PaymentRequestSummaryAmountByCorporateIdYearRequestModel): Observable<DataResult<PaymentRequestSummaryAmountByCorporateIdYearResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<PaymentRequestSummaryAmountByCorporateIdYearResponseModel[]>>(environment.path + "PaymentRequestSummaries/amountByCorporateIdYear", paymentRequestSummaryAmountByCorporateIdYearRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getByToday(): Observable<DataResult<PaymentRequestSummaryGetByTodayResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestSummaryGetByTodayResponseModel[]>>(environment.path + "PaymentRequestSummaries/getByToday",  { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  totalOutgoingPayment(): Observable<DataResult<PaymentRequestSummaryTotalOutgoingPaymentResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<PaymentRequestSummaryTotalOutgoingPaymentResponseModel[]>>(environment.path + "PaymentRequestSummaries/totalOutgoingPayment", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
