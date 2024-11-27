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

  removeFromSessionStorage(key:string){
    try {
      // Verificar si la clave existe antes de eliminar
      if (sessionStorage.getItem(key) !== null) {
        sessionStorage.removeItem(key);
        console.log(`El item con la clave '${key}' ha sido eliminado.`);
      } else {
        console.log(`No se encontró el item con la clave '${key}' en sessionStorage.`);
      }
    } catch (e) {
      console.error('Error al intentar eliminar el item de sessionStorage:', e);
    }
  }

  getFromLocalStorage(key: string) {	
    try {	
        // Obtener el dato desde sessionStorage	
        const storedData = localStorage.getItem(key);	
	
        // Si no existe el dato, devolver null o un valor por defecto	
        if (storedData === null) {	
            console.log("No se encontró el dato en localStorage.");	
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
