import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { DataResult } from 'src/app/models/result-models/data-result';

import { TandemPaymentTransferRequestExternalModel } from 'src/app/models/tandem-models/tandem-payment-transfer-request-external-model';
import { TandemPaymentTransferResponseExternalModel } from 'src/app/models/tandem-models/tandem-payment-transfer-response-external-model';
import { TandemPaymentUpdateRequestExternalModel } from 'src/app/models/tandem-models/tandem-payment-update-request-external-model';
import { TandemPaymentUpdateResponseExternalModel } from 'src/app/models/tandem-models/tandem-payment-update-response-external-model';
import { TandemPaymentInquiryRequestExternalModel } from 'src/app/models/tandem-models/tandem-payment-inquiry-request-external-model';
import { TandemPaymentInquiryResponseExternalModel } from 'src/app/models/tandem-models/tandem-payment-inquiry-response-external-model';
import { TandemCorporateDefineRequestExternalModel } from 'src/app/models/tandem-models/tandem-corporate-define-request-external-model';
import { TandemCorporateDefineResponseExternalModel } from 'src/app/models/tandem-models/tandem-corporate-define-response-external-model';

@Injectable()
export class TandemService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  paymentTransfer(tandemPaymentTransferRequestExternalModel: TandemPaymentTransferRequestExternalModel): Observable<DataResult<TandemPaymentTransferResponseExternalModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<TandemPaymentTransferResponseExternalModel>>(environment.path + "Tandems/paymentTransfer", tandemPaymentTransferRequestExternalModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  paymentUpdate(tandemPaymentUpdateRequesExternalModel: TandemPaymentUpdateRequestExternalModel): Observable<DataResult<TandemPaymentUpdateResponseExternalModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<TandemPaymentUpdateResponseExternalModel>>(environment.path + "Tandems/paymentUpdate", tandemPaymentUpdateRequesExternalModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  paymentInquiry(tandemPaymentInquiryRequestExternalModel: TandemPaymentInquiryRequestExternalModel): Observable<DataResult<TandemPaymentInquiryResponseExternalModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<TandemPaymentInquiryResponseExternalModel>>(environment.path + "Tandems/paymentInquiry", tandemPaymentInquiryRequestExternalModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  corporateDefine(tandemCorporateDefineRequestExternalModel: TandemCorporateDefineRequestExternalModel): Observable<DataResult<TandemCorporateDefineResponseExternalModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<TandemCorporateDefineResponseExternalModel>>(environment.path + "Tandems/corporateDefine", tandemCorporateDefineRequestExternalModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
