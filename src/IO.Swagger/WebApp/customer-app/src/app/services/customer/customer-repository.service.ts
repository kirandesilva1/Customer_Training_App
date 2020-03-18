import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ApiService} from "../api/api.service";
import {map} from "rxjs/operators";
import {CustomersViewModel} from "../../models/CustomersViewModel";
import {Observable} from "rxjs";
import {CustomerViewModel} from "../../models/CustomerViewModel";

@Injectable({
  providedIn: 'root'
})
export class CustomerRepositoryService {
  
  constructor(private http: HttpClient, private apiUrl: ApiService) { }

  public getCustomers() {
    return this.http.get(`${this.apiUrl.apiAddress}`);
  }

  public getCustomerInfo(customerId) {
    return this.http.get(`${this.apiUrl.apiAddress}/${customerId}`);
  }

  public updateCustomer(body){
    return this.http.put(`${this.apiUrl.apiAddress}`, body, this.generateHeaders());
  }
  
  public createCustomer(body) {
    return this.http.post(`${this.apiUrl.apiAddress}`, body, this.generateHeaders());
  }
  
  public deleteCustomer(){
    return this.http.delete(`${this.apiUrl.apiAddress}`);
  }
  
  private generateHeaders() {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    }
  }
}
