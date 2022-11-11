import { TestBed } from '@angular/core/testing';

import { AutoHelpSeviceManagerService } from './auto-help-sevice-manager.service';

describe('AutoHelpSeviceManagerService', () => {
  let service: AutoHelpSeviceManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutoHelpSeviceManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
