/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SftpDownloadService } from './sftp-download.service';

describe('Service: SftpDownload', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SftpDownloadService]
    });
  });

  it('should ...', inject([SftpDownloadService], (service: SftpDownloadService) => {
    expect(service).toBeTruthy();
  }));
});
