/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OperationClaimComponent } from './operation-claim.component';

describe('OperationClaimComponent', () => {
  let component: OperationClaimComponent;
  let fixture: ComponentFixture<OperationClaimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OperationClaimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperationClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
