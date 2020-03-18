import {Component, Input, NgModule, OnInit, Output} from '@angular/core';
import {first} from "rxjs/operators";
import {CustomersViewModel} from "../../../models/CustomersViewModel";
import {CustomerRepositoryService} from "../../../services/customer/customer-repository.service";
import {MatDialog} from "@angular/material/dialog";
import {EditCustomerComponent} from "../edit-customer-modal/edit-customer.component";
import {CustomerViewModel} from "../../../models/CustomerViewModel";
import {CustomerdeleteService} from "../services/http/customerdelete.service";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-edit-customer-table',
  templateUrl: './edit-customer-table.component.html',
  styleUrls: ['./edit-customer-table.component.scss']
})
export class EditCustomerTableComponent implements OnInit {

  @Input()
  UpdateData = new BehaviorSubject('');
  
  @Output()
  editEmitter = new BehaviorSubject('');
  
  customers: CustomerViewModel[];
  
  constructor(private CustomerService: CustomerRepositoryService, private dialog: MatDialog,
              private CustomerDeleteService: CustomerdeleteService) { }
  
  ngOnInit() {
    this.getAllCustomers();
    
    this.UpdateData.pipe().subscribe(value => {
      this.getAllCustomers();
    });
    
  }

  getAllCustomers(){
    this.CustomerService.getCustomers()
        .pipe(first()).subscribe((val: CustomerViewModel[]) => {
      this.customers = val;
    });
  }

  // TODO: How to test Open Edit Modal
  // TODO: Fix Open Edit Modal appearing in table
  openCustomerEditModal(customer:CustomerViewModel) {
    const dialog = this.dialog.open(EditCustomerComponent, {
      height: '250px',
      width: '50%',
      data: {customerObject: customer}
    });

    dialog.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.editEmitter.next('');
    });
  }
  
  deleteCustomer(customer:CustomerViewModel) {
      this.CustomerDeleteService.deleteCustomer(customer.customerId).pipe(first()).subscribe(response =>{
        alert('Customer deleted successfully!');
        this.getAllCustomers();
      }, error => {
          confirm('Unable to delete customer, It has an order attached to it!');
      });
  }
    
}
