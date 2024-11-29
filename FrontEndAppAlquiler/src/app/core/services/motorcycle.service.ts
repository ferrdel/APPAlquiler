import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Motorcycle } from '../models/motorcycle';

@Injectable({
  providedIn: 'root'
})
export class MotorcycleService {
  private urlBase:string =' https://localhost:7169/api';

  constructor(private httpClient:HttpClient) { }
  createMoto( moto: Motorcycle ): Observable<Motorcycle> {
    console.log(moto);
    return this.httpClient.post<Motorcycle>(`${ this.urlBase}/motorcycles`, moto )    
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

  getAllMotos():Observable<Motorcycle[]>{    
    return this.httpClient.get<Motorcycle[]>(`${ this.urlBase }/motorcycles`)
      .pipe(        
        catchError(error => {                    
          return throwError(() => error.error);
        })        
      );
  }

  getMotoById( id: number ): Observable<Motorcycle|undefined> {
    return this.httpClient.get<Motorcycle>(`${ this.urlBase }/motorcycles/${ id }`)
      .pipe(
        catchError(error => {                    
          return throwError(() => error.error);
          })        
      );
  }

  //el valor de retorno es un msj de exito
  updateMoto( moto: Motorcycle ): Observable<string> {
    if ( !moto.id ) throw Error('Moto id is required');

    return this.httpClient.put<string>(`${ this.urlBase }/motorcycles/${ moto.id }`, moto )    
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

  removeMoto(id: number){    
    if ( !id ) throw Error('Id is required');
    return this.httpClient.delete(`${ this.urlBase }/motorcycles/${ id }`)
    .pipe(
      catchError(error => {          
        console.log({error});
        return throwError(() => error.error);
        })        
    );
  }

  activateMoto(id: number){    
    if ( !id ) throw Error('Id is required');

    let _moto;
    this.getMotoById(id).subscribe(
      (data) => _moto = data
    );

    return this.httpClient.patch(`${ this.urlBase }/motorcycles/${ id }`, _moto)
    .pipe(
      catchError(error => {                  
        return throwError(() => error.error);
        })        
    );
  }
  
}