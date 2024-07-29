/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HcpService } from './hcp.service';

describe('Service: Hcp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HcpService]
    });
  });

  it('should ...', inject([HcpService], (service: HcpService) => {
    expect(service).toBeTruthy();
  }));
});
