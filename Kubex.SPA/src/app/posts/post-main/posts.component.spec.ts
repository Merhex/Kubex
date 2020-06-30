/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PostMainComponent } from './posts.component';

describe('PostsComponent', () => {
  let component: PostMainComponent;
  let fixture: ComponentFixture<PostMainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostMainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
