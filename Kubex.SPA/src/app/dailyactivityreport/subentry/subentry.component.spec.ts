/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SubentryComponent } from './subentry.component';

describe('SubentryComponent', () => {
  let component: SubentryComponent;
  let fixture: ComponentFixture<SubentryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubentryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubentryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
