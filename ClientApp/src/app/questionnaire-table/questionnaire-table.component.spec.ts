import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionnaireTableComponent } from './questionnaire-table.component';

describe('QuestionnaireTableComponent', () => {
  let component: QuestionnaireTableComponent;
  let fixture: ComponentFixture<QuestionnaireTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionnaireTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestionnaireTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
