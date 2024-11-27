import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Rent } from '../models/rent';
import { catchError, Observable, throwError } from 'rxjs';
import { RentDetail } from '../models/rentDetail';

@Injectable({
  providedIn: 'root'
})
export class RentService {

  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  createRent( rent: Rent ): Observable<void> {
    console.log("rent en service: " + rent);

    // Recuperar el token del localStorage o sessionStorage
    const token = localStorage.getItem('authToken');

    // Crear el encabezado con el token
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`  // El valor del token debe ir en formato "Bearer <token>"
    });

    return this.httpClient.post<void>(`${ this.urlBase}/rents`, rent, { headers } )    
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

  updateRent( rent: RentDetail ): Observable<string> {
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
  
    myRents():Observable<Rent[]>{    
    // Recuperar el token del localStorage o sessionStorage
    const token = localStorage.getItem('authToken');

    // Crear el encabezado con el token
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`  // El valor del token debe ir en formato "Bearer <token>"
    });

    // Realizar la solicitud GET pasando el encabezado
        
    return this.httpClient.get<Rent[]>(`${ this.urlBase }/rents/MyRents`, { headers })
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  }    

  getAllRents():Observable<RentDetail[]>{                
    return this.httpClient.get<RentDetail[]>(`${ this.urlBase }/rents`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  } 

  getRentById(id:number):Observable<RentDetail>{                
    return this.httpClient.get<RentDetail>(`${ this.urlBase }/rents/${id}`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  } 

  
}
