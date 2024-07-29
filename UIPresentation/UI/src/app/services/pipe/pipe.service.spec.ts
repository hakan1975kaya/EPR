/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PipeService } from './pipe.service';

describe('Service: Pipe', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PipeService]
    });
  });

  it('should ...', inject([PipeService], (service: PipeService) => {
    expect(service).toBeTruthy();
  }));
});
