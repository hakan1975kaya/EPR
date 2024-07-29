/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WebLogService } from './web-log.service';

describe('Service: WebLog', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WebLogService]
    });
  });

  it('should ...', inject([WebLogService], (service: WebLogService) => {
    expect(service).toBeTruthy();
  }));
});
