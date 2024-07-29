/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { OperationClaimService } from './operation-claim.service';

describe('Service: OperationClaim', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OperationClaimService]
    });
  });

  it('should ...', inject([OperationClaimService], (service: OperationClaimService) => {
    expect(service).toBeTruthy();
  }));
});
