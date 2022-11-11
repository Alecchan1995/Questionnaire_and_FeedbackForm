import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemFeedbackFormComponent } from './system-feedback-form.component';

describe('SystemFeedbackFormComponent', () => {
  let component: SystemFeedbackFormComponent;
  let fixture: ComponentFixture<SystemFeedbackFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SystemFeedbackFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemFeedbackFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
