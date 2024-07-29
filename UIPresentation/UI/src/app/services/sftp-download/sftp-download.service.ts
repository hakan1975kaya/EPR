import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { Result } from 'src/app/models/result-models/result';
import { DataResult } from 'src/app/models/result-models/data-result';
import { SftpDownloadAddRequestModel } from 'src/app/models/sftp-download-models/sftp-download-add-Request-model';
import { SftpDownloadGetByIdResponseModel } from 'src/app/models/sftp-download-models/sftp-download-get-by-id-response-model';
import { SftpDownloadGetListResponseModel } from 'src/app/models/sftp-download-models/sftp-download-get-list-response-model';
import { SftpDownloadUpdateRequestModel } from 'src/app/models/sftp-download-models/sftp-download-update-request-model';
import { SftpDownloadSearchRequestModel } from 'src/app/models/sftp-download-models/sftp-download-serach-request-model';
import { SftpDownloadSearchResponseModel } from 'src/app/models/sftp-download-models/sftp-download-search-response-model';
import { SftpDownloadGetBySftpFileNameResponseModel } from 'src/app/models/sftp-download-models/sftp-download-get-by-sftp-file-name-response-model';


@Injectable()
export class SftpDownloadService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  add(sftpDownloadAddRequestModel: SftpDownloadAddRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "SftpDownloads/add", sftpDownloadAddRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getById(id: number): Observable<DataResult<SftpDownloadGetByIdResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<SftpDownloadGetByIdResponseModel>>(environment.path + "SftpDownloads/getById?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getList(): Observable<DataResult<SftpDownloadGetListResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<SftpDownloadGetListResponseModel[]>>(environment.path + "SftpDownloads/getList", { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  update(sftpDownloadUpdateRequestModel: SftpDownloadUpdateRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.put<Result>(environment.path + "SftpDownloads/update", sftpDownloadUpdateRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  delete(id: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.delete<Result>(environment.path + "SftpDownloads/delete?id=" + id, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  search(sftpDownloadSearchRequestModel:SftpDownloadSearchRequestModel): Observable<DataResult<SftpDownloadSearchResponseModel[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<DataResult<SftpDownloadSearchResponseModel[]>>(environment.path + "SftpDownloads/search",sftpDownloadSearchRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getBySftpFileName(sftpFileName: string): Observable<DataResult<SftpDownloadGetBySftpFileNameResponseModel>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<SftpDownloadGetBySftpFileNameResponseModel>>(environment.path + "SftpDownloads/getBySftpFileName?sftpFileName=" + sftpFileName, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  openToPaymentRequest(paymentRequestId: number): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<Result>(environment.path + "SftpDownloads/openToPaymentRequest?paymentRequestId="+paymentRequestId, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
