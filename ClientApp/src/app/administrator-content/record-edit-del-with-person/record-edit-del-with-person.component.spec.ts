import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordEditDelWithPersonComponent } from './record-edit-del-with-person.component';

describe('RecordEditDelWithPersonComponent', () => {
  let component: RecordEditDelWithPersonComponent;
  let fixture: ComponentFixture<RecordEditDelWithPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecordEditDelWithPersonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordEditDelWithPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
