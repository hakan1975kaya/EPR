import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap } from 'rxjs';

import { AuthService } from '../auth/auth.service';
import { PipeService } from '../pipe/pipe.service';

import { environment } from '../../../environments/environment';
import { AuditEnum } from 'src/app/enums/audit-enum.enum';
import { HcpRequestModel } from 'src/app/models/hcp-models/hcp-request-model';


@Injectable({
  providedIn: 'root'
})
export class HcpService {
  constructor(private httpClient: HttpClient, private authService: AuthService, private pipeService: PipeService) { }

  getFile(hcpRequestModel: HcpRequestModel): Observable<File> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<File>(environment.path + "Hcps/getFile", hcpRequestModel, { headers: httpHeaders })
    .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  downloadFile(hcpRequestModel: HcpRequestModel): Observable<any> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    return this.httpClient.post<any>(environment.path + "Hcps/downloadFile", hcpRequestModel, { headers: httpHeaders,responseType: 'blob' as 'json' })
    .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

  uploadFile(formData: FormData): Observable<any> {
    const httpHeaders: HttpHeaders = new HttpHeaders().set('Authorization', 'Bearer ' + this.authService.getToken())
    httpHeaders.append( 'Content-Type', 'multipart/form-data');
    return this.httpClient.post<any>(environment.path + "Hcps/uploadFile", formData, { headers: httpHeaders })
    .pipe(tap(result => this.pipeService.saveLog(AuditEnum.Info, JSON.stringify(result))), catchError(error => this.pipeService.handleError(error)))
  }

}
