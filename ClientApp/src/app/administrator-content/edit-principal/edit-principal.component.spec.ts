import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPrincipalComponent } from './edit-principal.component';

describe('EditPrincipalComponent', () => {
  let component: EditPrincipalComponent;
  let fixture: ComponentFixture<EditPrincipalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPrincipalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPrincipalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
