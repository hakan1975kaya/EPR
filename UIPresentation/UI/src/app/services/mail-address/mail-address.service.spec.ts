/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MailAddressService } from './mail-address.service';

describe('Service: MailAddress', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MailAddressService]
    });
  });

  it('should ...', inject([MailAddressService], (service: MailAddressService) => {
    expect(service).toBeTruthy();
  }));
});
