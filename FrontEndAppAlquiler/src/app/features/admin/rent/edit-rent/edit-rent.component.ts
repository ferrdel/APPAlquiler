import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../../core/services/rent.service';
import { ToastrService } from 'ngx-toastr';
import { RentDetail } from '../../../../core/models/rentDetail';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { switchMap } from 'rxjs';
import { RentState } from '../../../../core/models/enums/rent-state.enum';
import { NgFor, NgIf } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-edit-rent',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, NgFor, NgIf, LoadingSpinnerComponent],
  templateUrl: './edit-rent.component.html',
  styleUrl: './edit-rent.component.css'
})
export class EditRentComponent implements OnInit{
  private rent!: RentDetail;

  public isLoading: boolean = false; //para spinner
  public showModels: boolean = false; //para mostrar cuando cargue renta

  public isReadonly: boolean = true;  // Indica si el campo debe ser de solo lectura
  // Convertir el enum a un array de pares clave-valor
  public estados = Object.entries(RentState);  

  miFormulario!: FormGroup;

  constructor(    
    private fb: FormBuilder,    
    private toastr: ToastrService,
    private rentService: RentService,
    private router: Router, //para redirigir,
    private activatedRoute: ActivatedRoute, //para rutas
  ){
    this.miFormulario = this.fb.group({
      id: [, Validators.required],
      pickUpDate: ['', Validators.required],
      pickUpTime: ['', Validators.required],

      returnDate: ['', Validators.required],
      returnTime: ['', Validators.required],

      vehicleId: [, Validators.required],
      vehicle: ['', Validators.required],
      
      state: [, Validators.required],
      totAmount: [, Validators.required],
      
      userId: [, Validators.required],                  
      userFirstName: [, Validators.required],
      userLastName: [, Validators.required],
      userPhoneNumber: [, Validators.required]
    });
  }

  ngOnInit(): void {
    this.isLoading = true;

    this.activatedRoute.params
          .pipe(
            switchMap( ({ id }) => this.rentService.getRentById( id ) ),
          ).subscribe( 
            (data) => {

              //sino encontro dato, redirijo al home
              if ( !data ) {
                return this.router.navigateByUrl('/rents');
              }

              //reinicio form con los datos traidos del servidor
              console.log({data});
              this.miFormulario.reset( data );
              this.isLoading = false;
              this.showModels = true;              
              return;
            },
            (error) => {          
              this.toastr.error(error, 'An error has occurred');                          
              return this.router.navigateByUrl('/rents');
            }
        );            
  }

  onSubmit(){
    //VALIDAR FORM
    if (this.miFormulario.invalid){
      //marca los campos de entrada como si se accedieron a ellos
      this.miFormulario.markAllAsTouched(); 
      this.toastr.warning("Check required fields", 'Information');     
      return;
    }
    
    console.log(this.currentRent);
    
    this.rentService.updateRent( this.currentRent )
      .subscribe(              
        response => {
          // Si la operación es exitosa
          
          this.toastr.success('Rent updated successfully', 'Information');
          this.router.navigate(['/rents']);
        },
        error => {
          // Si ocurre un error
          this.toastr.error(error.message , 'An error has occurred');        
        }
    );    
  }

  get currentRent(): RentDetail {
    const rent = this.miFormulario.value as RentDetail;
    return rent;
  }
                  
  getRentById(id: number){
    this.rentService.getRentById(id).subscribe(
      (response) => {      
        //this.toastr.success('Car was successfully disabled','Information');         
        this.rent = response;
        console.log(this.rent);    
      },
      (error) => {            
        this.toastr.error(error, 'An error has occurred');            
      }
    );
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
          return 'This field is required'                        
        case 'minlength':
          return `This field requires a minimum of ${errors['minlength'].requiredLength} characters`;
        case 'min':
          return `The value of this field must be ${errors['min'].min} or greater`;
          case 'max':
            return `The value of this field must not be greater than  ${errors['max'].max}`;
      }
    }
    return null;
  }

}
