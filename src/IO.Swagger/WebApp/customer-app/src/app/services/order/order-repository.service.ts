import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ApiService} from "../api/api.service";

@Injectable({
  providedIn: 'root'
})
export class OrderRepositoryService {

  constructor(private http: HttpClient, private apiUrl: ApiService) { }

  public getOrders() {
    return this.http.get(`${this.apiUrl.orderApiAddress}`);
  }
  
  private generateHeaders() {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json',
        'Accept': 'application/json'
      })
    }
  }
  
}
