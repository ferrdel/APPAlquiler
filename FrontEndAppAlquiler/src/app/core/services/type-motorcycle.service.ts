import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TypeMotorcycle } from '../models/typeMotorcycle';

@Injectable({
  providedIn: 'root'
})
export class TypeMotorcycleService {

  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  getTypeMoto():Observable<TypeMotorcycle[]>{
    return this.httpClient.get<TypeMotorcycle[]>(`${ this.urlBase}/typeMotorcycles`);
  }
}

