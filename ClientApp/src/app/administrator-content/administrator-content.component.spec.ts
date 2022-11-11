import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorContentComponent } from './administrator-content.component';

describe('AdministratorContentComponent', () => {
  let component: AdministratorContentComponent;
  let fixture: ComponentFixture<AdministratorContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministratorContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
