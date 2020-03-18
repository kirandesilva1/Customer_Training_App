import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  // TO DO NEED TO CHANGE THIS TO USE CONFIG FILE
  public apiAddress:string = 'http://localhost:5000/v1/customer'; 
  public orderApiAddress: string = 'http://localhost:5000/v1/order';
  constructor() { }
}
