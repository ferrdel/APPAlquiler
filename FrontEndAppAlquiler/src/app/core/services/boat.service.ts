import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Boat } from '../models/Boat';

@Injectable({
  providedIn: 'root'
})
export class BoatService {
  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }

  createBoat( _boat: Boat ): Observable<Boat> {
    return this.httpClient.post<Boat>(`${ this.urlBase}/boats`, _boat )    
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

  getAllBoats():Observable<Boat[]>{    
    return this.httpClient.get<Boat[]>(`${ this.urlBase }/boats`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  }

  getBoatById( id: number ): Observable<Boat|undefined> {
    return this.httpClient.get<Boat>(`${ this.urlBase }/boats/${ id }`)
      .pipe(
        catchError(error => {                    
          return throwError(() => error.error);
          })        
      );
  }

  //el valor de retorno es un msj de exito
  updateBoat( _boat: Boat ): Observable<string> {
    if ( !_boat.id ) throw Error('Bote id is required');

    return this.httpClient.put<string>(`${ this.urlBase }/boats/${ _boat.id }`, _boat )    
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

  removeBoat(id: number){    
    if ( !id ) throw Error('Id is required');
    return this.httpClient.delete(`${ this.urlBase }/boats/${ id }`)
    .pipe(
      catchError(error => {          
        console.log({error});
        return throwError(() => error.error);
        })        
    );
  }

  activateBoat(id: number){    
    if ( !id ) throw Error('Id is required');

    let _boat;
    this.getBoatById(id).subscribe(
      (data) => _boat = data
    );

    return this.httpClient.patch(`${ this.urlBase }/boats/${ id }`, _boat)
    .pipe(
      catchError(error => {                  
        return throwError(() => error.error);
        })        
    );
  }
}

