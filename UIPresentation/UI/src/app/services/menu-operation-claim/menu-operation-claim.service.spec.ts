/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MenuOperationClaimService } from './menu-operation-claim.service';

describe('Service: MenuOperationClaim', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MenuOperationClaimService]
    });
  });

  it('should ...', inject([MenuOperationClaimService], (service: MenuOperationClaimService) => {
    expect(service).toBeTruthy();
  }));
});
