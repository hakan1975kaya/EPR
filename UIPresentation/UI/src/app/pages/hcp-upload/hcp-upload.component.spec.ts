/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HcpUploadComponent } from './hcp-upload.component';

describe('HcpUploadComponent', () => {
  let component: HcpUploadComponent;
  let fixture: ComponentFixture<HcpUploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HcpUploadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HcpUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
