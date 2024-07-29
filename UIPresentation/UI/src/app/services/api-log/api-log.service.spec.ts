/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ApiLogService } from './api-log.service';

describe('Service: ApiLog', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApiLogService]
    });
  });

  it('should ...', inject([ApiLogService], (service: ApiLogService) => {
    expect(service).toBeTruthy();
  }));
});
