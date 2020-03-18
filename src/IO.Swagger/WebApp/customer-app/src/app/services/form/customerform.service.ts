import { Injectable } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CustomerViewModel} from "../../models/CustomerViewModel";

@Injectable({
  providedIn: 'root'
})
export class CustomerformService {
  form: FormGroup;

  constructor(private formBuilder:FormBuilder) { }
  
  init():void{
    this.form = this.loadCustomer();
  }

  loadCustomer(customer: CustomerViewModel = new CustomerViewModel()): FormGroup{
      return this.formBuilder.group({
        name: ['', Validators.required],
        groupid: ['',Validators.required],
        numberoforders: ['', Validators.required],
        address: this.formBuilder.group(
            {streetname: ['', Validators.required],
                          zipcode: ['', Validators.required]})
        });
  }
}
