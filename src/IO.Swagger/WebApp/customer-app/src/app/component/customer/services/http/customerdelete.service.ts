import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ApiService} from "../../../../services/api/api.service";

@Injectable({
  providedIn: 'root'
})

export class CustomerdeleteService {

  constructor(private http: HttpClient, private apiUrl: ApiService) { }

  public deleteCustomer(customerId:string){
    return this.http.delete(`${this.apiUrl.apiAddress}/${customerId}`);
  }
  
}
