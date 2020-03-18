import { Component, OnInit } from '@angular/core';
import {OrderRepositoryService} from "../../../services/order/order-repository.service";
import {first} from "rxjs/operators";
import {OrdersViewModel} from "../../../models/OrdersViewModel";

@Component({
  selector: 'app-order-table',
  templateUrl: './order-table.component.html',
  styleUrls: ['./order-table.component.scss']
})
export class OrderTableComponent implements OnInit {

  tableColumns: string[] = ['orderId', 'customer' , 'description', 'shipAddress', 'orderItems' ];
  dataSource = [];
  
  constructor(private OrderService: OrderRepositoryService) { }

  ngOnInit() {
    this.getAllOrders();
  }
  
  getAllOrders(){
    this.OrderService.getOrders()
        .pipe(first()).subscribe((val: OrdersViewModel) => {
      this.dataSource.push(val);
      console.log(this.dataSource);
    });
    
  }

}
