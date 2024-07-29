/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HcpUploadService } from './hcp-upload.service';

describe('Service: HcpUpload', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HcpUploadService]
    });
  });

  it('should ...', inject([HcpUploadService], (service: HcpUploadService) => {
    expect(service).toBeTruthy();
  }));
});
