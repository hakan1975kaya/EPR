/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RoleOperationClaimService } from './role-operation-claim.service';

describe('Service: RoleOperationClaim', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RoleOperationClaimService]
    });
  });

  it('should ...', inject([RoleOperationClaimService], (service: RoleOperationClaimService) => {
    expect(service).toBeTruthy();
  }));
});
