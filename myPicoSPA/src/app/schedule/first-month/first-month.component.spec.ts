import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstMonthComponent } from './first-month.component';

describe('FirstMonthComponent', () => {
  let component: FirstMonthComponent;
  let fixture: ComponentFixture<FirstMonthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FirstMonthComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstMonthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
