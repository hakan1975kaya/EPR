/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PaymentRequestDatailService } from './payment-request-datail.service';

describe('Service: PaymentRequestDatail', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaymentRequestDatailService]
    });
  });

  it('should ...', inject([PaymentRequestDatailService], (service: PaymentRequestDatailService) => {
    expect(service).toBeTruthy();
  }));
});
