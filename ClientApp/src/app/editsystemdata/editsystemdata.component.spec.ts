import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditsystemdataComponent } from './editsystemdata.component';

describe('EditsystemdataComponent', () => {
  let component: EditsystemdataComponent;
  let fixture: ComponentFixture<EditsystemdataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditsystemdataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditsystemdataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
