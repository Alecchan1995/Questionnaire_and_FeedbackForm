import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionnaireHeadComponent } from './questionnaire-head.component';

describe('QuestionnaireHeadComponent', () => {
  let component: QuestionnaireHeadComponent;
  let fixture: ComponentFixture<QuestionnaireHeadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionnaireHeadComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionnaireHeadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
