/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PicoMapComponent } from './picoMap.component';

describe('PicoMapComponent', () => {
  let component: PicoMapComponent;
  let fixture: ComponentFixture<PicoMapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicoMapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicoMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
