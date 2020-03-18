import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material';
import {MatInputModule} from "@angular/material/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule} from "@angular/material/dialog";
import {MatTabsModule} from "@angular/material/tabs";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CustomersComponent} from "./component/customer/customers/customers.component";
import {AddcustomerComponent} from "./component/customer/addcustomer/addcustomer.component";
import {CustomerTableComponent} from "./component/customer/customer-table/customer-table.component";
import {OrderTableComponent} from "./component/order/order-table/order-table.component";
import {OrderItemsComponent} from "./component/order/order-items/order-items.component";
import {EditCustomerComponent} from "./component/customer/edit-customer-modal/edit-customer.component";
import {EditCustomerTableComponent} from "./component/customer/edit-customer-table/edit-customer-table.component";
import {CommonModule} from "@angular/common";
import {MatSortModule} from "@angular/material/sort";
import {MatIconModule} from "@angular/material/icon";
import { LoginComponent } from './component/login/login.component';


@NgModule({
    declarations: [
        AppComponent,
        CustomersComponent,
        AddcustomerComponent,
        CustomerTableComponent,
        CustomersComponent,
        OrderTableComponent,
        OrderItemsComponent,
        EditCustomerComponent,
        EditCustomerTableComponent,
        LoginComponent
    ],
    entryComponents:[EditCustomerComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        MatTableModule,
        MatInputModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatDialogModule,
        MatTabsModule,
        BrowserAnimationsModule,
        FormsModule,
        CommonModule,
        MatSortModule,
        MatIconModule
    ],
  providers: [ {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
