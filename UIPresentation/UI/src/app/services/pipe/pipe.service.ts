import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { throwError } from "rxjs";

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { AlertifyService } from '../alertify/alertify.service';

import { environment } from 'src/environments/environment';

import { WebLogAddRequestModel } from 'src/app/models/web-log-models/web-log-add-request-model';

@Injectable()
export class PipeService {

  constructor(private httpClient: HttpClient, private alertifyService: AlertifyService) { }

  handleError(error: HttpErrorResponse) {
    let errorMessage: string = "";
    if (error) {
      errorMessage = JSON.stringify(error)
      if (error.error) {
        errorMessage = JSON.stringify(error.error)
        if (error.error.message) {
          errorMessage = error.error.message;
        }
      }
    }

    this.alertifyService.error(errorMessage)

    this.saveLog(AuditEnum.Error, JSON.stringify(error))

    return throwError(errorMessage);
  }

  saveLog(audit: AuditEnum, detail: string) {
    let webLogAddRequestModel: WebLogAddRequestModel = new WebLogAddRequestModel()
    webLogAddRequestModel.date = new Date()
    webLogAddRequestModel.audit = audit
    webLogAddRequestModel.detail = detail
    this.httpClient.post<WebLogAddRequestModel>(environment.path + "WebLogs/add", webLogAddRequestModel).subscribe(result => {
    })
  }
}