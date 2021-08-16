import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicoMapsComponent } from './pico-maps.component';

describe('PicoMapsComponent', () => {
  let component: PicoMapsComponent;
  let fixture: ComponentFixture<PicoMapsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicoMapsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicoMapsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
