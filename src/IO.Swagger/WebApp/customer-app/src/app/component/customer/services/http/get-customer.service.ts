import { Injectable } from '@angular/core';
import {first} from "rxjs/operators";
import {CustomersViewModel} from "../../../../models/CustomersViewModel";
import {HttpClient} from "@angular/common/http";
import {ApiService} from "../../../../services/api/api.service";

@Injectable({
  providedIn: 'root'
})
export class GetCustomerService {

  constructor(private http: HttpClient, private apiUrl: ApiService) { }
 
  getCustomers(){
    return this.http.get(`${this.apiUrl.apiAddress}`);
  }
}
