import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorviewComponent } from './errorview.component';

describe('ErrorviewComponent', () => {
  let component: ErrorviewComponent;
  let fixture: ComponentFixture<ErrorviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ErrorviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrorviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
