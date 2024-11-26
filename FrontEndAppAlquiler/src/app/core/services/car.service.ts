import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap, throwError } from 'rxjs';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  createCar( car: Car ): Observable<Car> {
    console.log(car);
    return this.httpClient.post<Car>(`${ this.urlBase}/cars`, car )    
      .pipe(
        catchError(error => {         
          if(error.error.errors){
            let errorMessageValidation = '';
            // Recorremos los errores y los mostramos como un string
            for (const field in error.error.errors) {      
                errorMessageValidation = error.error.errors[field].join('\n'); // Unimos los mensajes con coma                      
            }            
             // En vez de lanzar un nuevo Error, pasamos un objeto con el mensaje
            return throwError(() => ({ message: errorMessageValidation }));
          }
          
          return throwError( () => ({ message: error.error}) );       

          }
        )
      );
  }

  getAll():Observable<Car[]>{    
    return this.httpClient.get<Car[]>(`${ this.urlBase }/cars`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  }

  getCarById( id: number ): Observable<Car|undefined> {
    return this.httpClient.get<Car>(`${ this.urlBase }/cars/${ id }`)
      .pipe(
        catchError(error => {                    
          return throwError(() => error.error);
          })        
      );
  }

  //el valor de retorno es un msj de exito
  updateCar( car: Car ): Observable<string> {
    if ( !car.id ) throw Error('Car id is required');

    return this.httpClient.put<string>(`${ this.urlBase }/cars/${ car.id }`, car )    
    .pipe(
      catchError(error => {          
        if(error.error.errors){
          let errorMessageValidation = '';
          // Recorremos los errores y los mostramos como un string
          for (const field in error.error.errors) {      
              errorMessageValidation = error.error.errors[field].join('\n'); // Unimos los mensajes con coma                      
          }            
           // En vez de lanzar un nuevo Error, pasamos un objeto con el mensaje
          return throwError(() => ({ message: errorMessageValidation }));
        }
        
        return throwError( () => ({ message: error.error}) );    
      })        
    );
  }

  removeCar(id: number){    
    if ( !id ) throw Error('Id is required');
    return this.httpClient.delete(`${ this.urlBase }/cars/${ id }`)
    .pipe(
      catchError(error => {          
        console.log({error});
        return throwError(() => error.error);
        })        
    );
  }

  activateCar(id: number){    
    if ( !id ) throw Error('Id is required');

    let auto;
    this.getCarById(id).subscribe(
      (data) => auto = data
    );

    return this.httpClient.patch(`${ this.urlBase }/cars/${ id }`, auto)
    .pipe(
      catchError(error => {                  
        return throwError(() => error.error);
        })        
    );
  }
}
