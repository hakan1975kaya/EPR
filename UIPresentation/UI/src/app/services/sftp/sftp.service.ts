import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from 'src/environments/environment';

import { AuditEnum } from 'src/app/enums/audit-enum.enum';

import { DataResult } from 'src/app/models/result-models/data-result';
import { Result } from 'src/app/models/result-models/result';

import { SftpUploadFileRequestModel } from 'src/app/models/sftp-models/sftp-upload-file-request-model';
import { SftpDownloadFileRequestModel } from 'src/app/models/sftp-models/sftp-download-file-request-model';
import { SftpDownloadFileResponseModel } from 'src/app/models/sftp-models/sftp-download-file-response-model';

@Injectable()
export class SftpService {

  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  sftpDownload(sftpDownloadFileRequestModel: SftpDownloadFileRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Sftps/sftpDownload", sftpDownloadFileRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  uploadFile(sftpUploadFileRequestModel: SftpUploadFileRequestModel): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Sftps/uploadFile", sftpUploadFileRequestModel, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  deleteFile(filePath: string): Observable<Result> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<Result>(environment.path + "Sftps/deleteFile", filePath, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getDirectory(prefix: string): Observable<DataResult<string>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<string>>(environment.path + "Sftps/getDirectory?prefix="+prefix, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  getFileNames(directory: string): Observable<DataResult<string[]>> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.get<DataResult<string[]>>(environment.path + "Sftps/getFileNames?directory="+directory, { headers: httpHeaders })
      .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }


}
