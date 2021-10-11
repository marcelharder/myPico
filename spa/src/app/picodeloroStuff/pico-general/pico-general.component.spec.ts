import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicoGeneralComponent } from './pico-general.component';

describe('PicoGeneralComponent', () => {
  let component: PicoGeneralComponent;
  let fixture: ComponentFixture<PicoGeneralComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicoGeneralComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicoGeneralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
