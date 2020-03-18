import {Component, OnInit, ViewChild} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {CustomersViewModel} from "../../../models/CustomersViewModel"; // TODO : FIX RELATIVE PATHS
import {CustomerRepositoryService} from "../../../services/customer/customer-repository.service";
import {first} from "rxjs/operators";
import {MatDialog} from "@angular/material/dialog";
import {EditCustomerComponent} from "../edit-customer-modal/edit-customer.component";
import {MatSort} from "@angular/material/sort";
import {MatTableDataSource} from "@angular/material/table";
import {CustomerViewModel} from "../../../models/CustomerViewModel";

@Component({
  selector: 'app-customer-table',
  templateUrl: './customer-table.component.html',
  styleUrls: ['./customer-table.component.scss']
})
export class CustomerTableComponent implements OnInit {

  customerViewData$ = new BehaviorSubject(new CustomersViewModel());
  tableColumns: string[] = ['customerId', 'name', 'groupid', 'numberoforders','address'];
  tableData: any;
  dataSource: MatTableDataSource<CustomerViewModel>;
  customerGroupId: any;
  numberoforders: any;
  streetname: any;
  zipcode: any;
  customerName: any;

  @ViewChild(MatSort, {static: true}) sort: MatSort;
  
  constructor(private CustomerService: CustomerRepositoryService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getAllCustomers();
  }
  
  getAllCustomers(){
    this.CustomerService.getCustomers().pipe(first()).subscribe((val: CustomerViewModel[]) => {
      debugger;
      //this.dataSource = val.customers;
       this.dataSource = new MatTableDataSource(val);
       this.dataSource.sort = this.sort;
      
    });
  }

  
}
