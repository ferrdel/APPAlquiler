import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Bike } from '../models/bike';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BikeService {

  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  createBike( bike: Bike ): Observable<Bike> {
    console.log(bike);
    return this.httpClient.post<Bike>(`${ this.urlBase}/bikes`, bike )    
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

  getAllBikes():Observable<Bike[]>{    
    return this.httpClient.get<Bike[]>(`${ this.urlBase }/bikes`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  }

  getBikeById( id: number ): Observable<Bike|undefined> {
    return this.httpClient.get<Bike>(`${ this.urlBase }/bikes/${ id }`)
      .pipe(
        catchError(error => {                    
          return throwError(() => error.error);
          })        
      );
  }

  //el valor de retorno es un msj de exito
  updateBike( _bike: Bike ): Observable<string> {
    if ( !_bike.id ) throw Error('Bike id is required');

    return this.httpClient.put<string>(`${ this.urlBase }/bikes/${ _bike.id }`, _bike )    
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

  removeBike(id: number){    
    if ( !id ) throw Error('Id is required');
    return this.httpClient.delete(`${ this.urlBase }/bikes/${ id }`)
    .pipe(
      catchError(error => {          
        console.log({error});
        return throwError(() => error.error);
        })        
    );
  }

  activateBike(id: number){    
    if ( !id ) throw Error('Id is required');

    let bike;
    this.getBikeById(id).subscribe(
      (data) => bike = data
    );

    return this.httpClient.patch(`${ this.urlBase }/bikes/${ id }`, bike)
    .pipe(
      catchError(error => {                  
        return throwError(() => error.error);
        })        
    );
  }
  
}
