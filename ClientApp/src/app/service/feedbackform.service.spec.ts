import { TestBed } from '@angular/core/testing';

import { FeedbackformService } from './feedbackform.service';

describe('FeedbackformService', () => {
  let service: FeedbackformService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FeedbackformService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
