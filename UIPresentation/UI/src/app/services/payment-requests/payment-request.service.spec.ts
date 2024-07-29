/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PaymentRequestService } from './payment-request.service';

describe('Service: PaymentRequest', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaymentRequestService]
    });
  });

  it('should ...', inject([PaymentRequestService], (service: PaymentRequestService) => {
    expect(service).toBeTruthy();
  }));
});
