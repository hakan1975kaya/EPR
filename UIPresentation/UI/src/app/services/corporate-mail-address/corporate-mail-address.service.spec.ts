/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CorporateMailAddressService } from './corporate-mail-address.service';

describe('Service: CorporateMailAddress', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CorporateMailAddressService]
    });
  });

  it('should ...', inject([CorporateMailAddressService], (service: CorporateMailAddressService) => {
    expect(service).toBeTruthy();
  }));
});
