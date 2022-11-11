import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivateDataTableComponent } from './private-data-table.component';

describe('PrivateDataTableComponent', () => {
  let component: PrivateDataTableComponent;
  let fixture: ComponentFixture<PrivateDataTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrivateDataTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivateDataTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
