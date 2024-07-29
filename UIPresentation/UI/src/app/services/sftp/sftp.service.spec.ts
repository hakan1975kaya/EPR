/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SftpService } from './sftp.service';

describe('Service: Sftp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SftpService]
    });
  });

  it('should ...', inject([SftpService], (service: SftpService) => {
    expect(service).toBeTruthy();
  }));
});
