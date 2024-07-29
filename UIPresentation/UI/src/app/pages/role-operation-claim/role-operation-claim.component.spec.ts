/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RoleOperationClaimComponent } from './role-operation-claim.component';

describe('RoleOperationClaimComponent', () => {
  let component: RoleOperationClaimComponent;
  let fixture: ComponentFixture<RoleOperationClaimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoleOperationClaimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleOperationClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
