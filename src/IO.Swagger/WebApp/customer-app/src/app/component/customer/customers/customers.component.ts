import {Component, Input, OnInit} from '@angular/core';
import {CustomerViewModel} from "../../../models/CustomerViewModel";
import {EditCustomerComponent} from "../edit-customer-modal/edit-customer.component";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit {
  
  @Input()
  openEmitter;
  
  updateCustomerTable = new BehaviorSubject('');
  
  constructor() { }

  ngOnInit() {
  }
  
  updateTable($event: any) {
    this.updateCustomerTable.next("1");
  }
  
}
