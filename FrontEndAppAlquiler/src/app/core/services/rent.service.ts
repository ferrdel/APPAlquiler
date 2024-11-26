import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Rent } from '../models/rent';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RentService {

  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  createRent( rent: Rent ): Observable<void> {
    console.log("rent en service: " + rent);
    return this.httpClient.post<void>(`${ this.urlBase}/rents`, rent )    
      .pipe(
        catchError(error => {      
          console.error(error)   ;
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

  updateRent( rent: Rent ): Observable<string> {
    if ( !rent.id ) throw Error('Rent id is required');

    return this.httpClient.put<string>(`${ this.urlBase }/rents/${ rent.id }`, rent )    
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

  myRents(userId: number):Observable<Rent[]>{    
    return this.httpClient.get<Rent[]>(`${ this.urlBase }/rents/UserRents/${userId}`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  }

  
}
