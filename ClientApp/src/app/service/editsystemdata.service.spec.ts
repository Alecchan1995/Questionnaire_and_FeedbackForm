import { TestBed } from '@angular/core/testing';

import { EditsystemdataService } from './editsystemdata.service';

describe('EditsystemdataService', () => {
  let service: EditsystemdataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditsystemdataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
