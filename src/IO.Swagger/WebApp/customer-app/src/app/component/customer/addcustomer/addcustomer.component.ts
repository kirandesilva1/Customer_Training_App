import {Component, EventEmitter, Inject, Input, OnInit, Optional, Output, SkipSelf} from '@angular/core';
import {CustomerViewModel} from "../../../models/CustomerViewModel";
import {CustomerformService} from "../../../services/form/customerform.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormGroup} from "@angular/forms";
import {first} from "rxjs/operators";
import {CustomerRepositoryService} from "../../../services/customer/customer-repository.service";
import {GetCustomerService} from "../services/http/get-customer.service";
import {CustomersViewModel} from "../../../models/CustomersViewModel";

@Component({
  selector: 'app-addcustomer',
  templateUrl: './addcustomer.component.html',
  styleUrls: ['./addcustomer.component.scss']
})
export class AddcustomerComponent implements OnInit {

  @Input()
  customer: CustomerViewModel;
  
  @Output('refreshRecords')
  refreshCustomerTable : EventEmitter<any> = new EventEmitter<any>();

  isDialogRef: boolean;
  enableDeletion: boolean;

  submittedEvent = false;
  customerForm: FormGroup;

  constructor(private customerFormService: CustomerformService, private customerRepositoryService: CustomerRepositoryService
      , private customerGetService: GetCustomerService) {
  }
  
  
  ngOnInit() {
    this.customerFormService.init(); // Initialize Customer form service for usage
    this.customerForm = this.customerFormService.form;

    if (this.customer && this.customer.customerId) {
      this.customerForm = this.customerFormService.loadCustomer(this.customer);
      this.enableDeletion = true;
    } else {
      this.enableDeletion = false;
    }
  }

  saveEvent() {
    this.submittedEvent = true;
    const value = this.customerForm.value as CustomerViewModel; // CONVERTS VIEW MODEL 
    console.log('value' + value);
    this.customerRepositoryService.createCustomer(value).pipe(first()).subscribe((val: string) => {
      this.refreshCustomerTable.emit(null);
      this.customerForm.reset();
      alert('Save Successful');
    });
  }
  
  
}

