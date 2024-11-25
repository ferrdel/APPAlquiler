import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageManageService {

  constructor() { }

  //Guardar un objeto en sessionStorage
	
  saveToSessionStorage(key: string, data: any) {	
    try {	
        // Convertir el objeto a una cadena JSON y guardarlo en sessionStorage	
        sessionStorage.setItem(key, JSON.stringify(data));	
    } catch (error) {	
        console.error("Error guardando en sessionStorage:", error);	
    }	
}	
	
//Obtener un objeto desde sessionStorage
	
  getFromSessionStorage(key: string) {	
    try {	
        // Obtener el dato desde sessionStorage	
        const storedData = sessionStorage.getItem(key);	
	
        // Si no existe el dato, devolver null o un valor por defecto	
        if (storedData === null) {	
            console.log("No se encontró el dato en sessionStorage.");	
            return null;	
        }	
	
        // Intentar convertir la cadena JSON a un objeto	
        return JSON.parse(storedData);	
    } catch (error) {	
        // Manejo de errores si el contenido no es un JSON válido	
        console.error("Error al obtener o parsear el dato de sessionStorage:", error);	
        return null;	
    }	
}	

}
