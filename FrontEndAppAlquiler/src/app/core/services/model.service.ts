import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Model } from '../models/model';

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  getModels():Observable<Model[]>{
    return this.httpClient.get<Model[]>(`${ this.urlBase}/models`);
  }
}
