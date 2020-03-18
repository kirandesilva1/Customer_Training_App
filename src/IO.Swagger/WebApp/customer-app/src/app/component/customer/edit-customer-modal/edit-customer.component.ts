import {Component, EventEmitter, Inject, Input, OnInit, Optional, Output, SkipSelf} from '@angular/core';
import {MatDialog, MAT_DIALOG_DATA,MatDialogRef} from "@angular/material/dialog";
import {FormGroup} from "@angular/forms";
import {CustomerViewModel} from "../../../models/CustomerViewModel";
import {CustomerFormService} from "../services/form/customer-form-service";
import {CustomerRepositoryService} from "../../../services/customer/customer-repository.service";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.scss']
})
export class EditCustomerComponent implements OnInit {
  customerForm: FormGroup;
  customerAddress: FormGroup;
  customer: CustomerViewModel;

  @Output('refreshRecords')
  refreshCustomerTable : EventEmitter<any> = new EventEmitter<any>();
  
  constructor(private customerFormService : CustomerFormService, private customerRepositoryService : CustomerRepositoryService,
              @Optional() @SkipSelf() private dialogRef: MatDialogRef<EditCustomerComponent>,
              @Optional() @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.customerForm = this.customerFormService.init(this.data.customerObject);
    this.customer = this.data.customerObject;    
  }
  
  saveCustomer() {
    const value = this.customerForm.getRawValue();
    
    // SAVE DATA
    this.customerRepositoryService.createCustomer(value).pipe(first()).subscribe((val: string) => {
      this.refreshCustomerTable.emit(null);
      this.close();
    });
    
  }

  close(){
    this.dialogRef.close({update: true});
  }
  
}
