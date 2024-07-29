/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PaymentRequestSummaryService } from './payment-request-summary.service';

describe('Service: PaymentRequestSummary', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaymentRequestSummaryService]
    });
  });

  it('should ...', inject([PaymentRequestSummaryService], (service: PaymentRequestSummaryService) => {
    expect(service).toBeTruthy();
  }));
});
