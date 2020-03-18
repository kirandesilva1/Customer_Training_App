import {Component, Input, OnInit} from '@angular/core';
import {OrderItemViewModel} from "../../../models/OrderItemViewModel";
import {OrdersViewModel} from "../../../models/OrdersViewModel";
import {OrderViewModel} from "../../../models/OrderViewModel";

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.scss']
})
export class OrderItemsComponent implements OnInit {

  @Input()
  items: OrderViewModel;
  
  constructor() { }

  ngOnInit() {
    console.log(this.items);
  }

}
