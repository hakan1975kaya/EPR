/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TandemService } from './tandem.service';

describe('Service: Tandem', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TandemService]
    });
  });

  it('should ...', inject([TandemService], (service: TandemService) => {
    expect(service).toBeTruthy();
  }));
});
