/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CorporateService } from './corporate.service';

describe('Service: Corporate', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CorporateService]
    });
  });

  it('should ...', inject([CorporateService], (service: CorporateService) => {
    expect(service).toBeTruthy();
  }));
});
