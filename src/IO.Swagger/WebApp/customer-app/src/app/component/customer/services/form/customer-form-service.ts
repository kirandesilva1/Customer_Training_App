import {Injectable, OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {CustomerViewModel} from "../../../../models/CustomerViewModel";

@Injectable({
  providedIn: 'root'
})
export class CustomerFormService {
  form: FormGroup;
  
  constructor(private formBuilder: FormBuilder) { }
  
  init(event: CustomerViewModel):FormGroup{
    this.form = this.loadEvent(event);
    return this.form;
  }
  
  loadEvent(customer: CustomerViewModel): FormGroup{
      
    return this.formBuilder.group({
      ...customer,
            // id: [customer.customerId],
            customerName: [customer.name, Validators.required],
            address: 
            this.formBuilder.group({streetName:  [customer.address.streetname, Validators.required],
                                                 zipCode : [customer.address.zipcode, Validators.required]}),
            numberOfOrders: [customer.numberoforders]
            });
    
  }
  
}
