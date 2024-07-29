/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WebLogComponent } from './web-log.component';

describe('WebLogComponent', () => {
  let component: WebLogComponent;
  let fixture: ComponentFixture<WebLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WebLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
