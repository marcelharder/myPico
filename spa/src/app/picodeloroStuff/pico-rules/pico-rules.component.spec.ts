import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicoRulesComponent } from './pico-rules.component';

describe('PicoRulesComponent', () => {
  let component: PicoRulesComponent;
  let fixture: ComponentFixture<PicoRulesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicoRulesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicoRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
