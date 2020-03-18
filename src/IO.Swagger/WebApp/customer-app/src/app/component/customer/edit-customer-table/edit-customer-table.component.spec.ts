import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCustomerTableComponent } from './edit-customer-table.component';

describe('EditCustomerTableComponent', () => {
  let component: EditCustomerTableComponent;
  let fixture: ComponentFixture<EditCustomerTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCustomerTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCustomerTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
