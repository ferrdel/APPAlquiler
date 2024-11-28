import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';
import { DateRent } from '../../../../core/models/dateRent';

@Component({
  selector: 'app-date-rent',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './date-rent.component.html',
  styleUrl: './date-rent.component.css'
})
export class DateRentComponent {

  miFormulario!: FormGroup;
  
  constructor(private fb: FormBuilder,
      
      private activatedRoute: ActivatedRoute, //para rutas
      private router: Router, //para redirigir
      private toastr: ToastrService, //para mensajes
      private sessionStorage: SessionStorageManageService  //para administrar sessionStorage
  ) {    
    // Crear un FormGroup con varios FormControls
    this.miFormulario = this.fb.group({            
      pickUpDate: [new Date().toISOString().split('T')[0], Validators.required],      
      pickUpTime: ['09:00', Validators.required],      

      returnDate: [this.getReturnDate(), Validators.required],            
      returnTime: ['09:00', Validators.required],      
    });
  }

  onSubmit(){
    //VALIDAR FORM
    if (this.miFormulario.invalid){
      //marca los campos de entrada como si se accedieron a ellos
      this.miFormulario.markAllAsTouched(); 
      this.toastr.warning("Verifique los campos requeridos", 'Información');     
      return;
    }

    console.log(this.miFormulario.value);

    // La fecha en formato string
    const pickUpDate = this.miFormulario.get('pickUpDate')?.value;  //"2024-11-12";
    const pickUpTime = this.miFormulario.get('pickUpTime')?.value;  //"10:00";
    const returnDate = this.miFormulario.get('returnDate')?.value;  //"2024-11-12";
    const returnTime = this.miFormulario.get('returnTime')?.value;  //"10:00";

    const rentDate: DateRent = { 
      pickUpDate: pickUpDate,
      pickUpTime: `${pickUpTime}:00`, //guardo como "10:00:00" formato solicitado
      returnDate: returnDate,
      returnTime: `${returnTime}:00`,
    }

    this.sessionStorage.saveToSessionStorage('dateRent', rentDate);
    this.router.navigate(['/rent/vehicle']); 
  }

  getReturnDate(){
    // Crear un objeto Date con la fecha y hora actuales
    const now = new Date();

    const currentDate = new Date(now);

    // Agregar un día a la fecha
    currentDate.setDate(currentDate.getDate() + 1);

    // Formatear la nueva fecha en el formato "YYYY-MM-DD"
    const nextDayString = currentDate.toISOString().split('T')[0];
    return nextDayString;
  }
  

hasErrorsAndTouched (field: string): boolean | null{        
  //Al usar !!, estás convirtiendo el valor en un booleano. this.myForm.controls[field].errors se convertirá en true si hay errores y false si no.
  let hasErrors: boolean = !!this.miFormulario.controls[field].errors;    
  let wasTouched: boolean = this.miFormulario.controls[field].touched;
          
  return hasErrors && wasTouched;
}

getFieldError( field: string): string | null {
  if( !this.miFormulario.controls[field]) return null;

  const errors = this.miFormulario.controls[field].errors || {};

  for( const key of Object.keys(errors)) {
    //console.log(`${field} - error: ${key}`);
    switch (key) {
      case 'required':
        return 'Este campo es requerido'                        
      case 'minlength':
        return `Este campo requiere mínimo de ${errors['minlength'].requiredLength} caracteres`;
      case 'min':
        return `El valor de éste campo debe de ser ${errors['min'].min} o mayor`;
        case 'max':
          return `El valor de éste campo no debe de ser mayor a ${errors['max'].max}`;
    }
  }
  return null;
}

}
