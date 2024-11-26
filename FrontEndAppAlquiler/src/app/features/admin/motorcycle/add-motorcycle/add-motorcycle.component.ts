import { NgIf, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, switchMap } from 'rxjs';
import { State } from '../../../../core/models/enums/state.enum';
import { Motorcycle } from '../../../../core/models/motorcycle';
import { BrandService } from '../../../../core/services/brand.service';
import { ModelService } from '../../../../core/services/model.service';
import { MotorcycleService } from '../../../../core/services/motorcycle.service';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { Brand } from '../../../../core/models/brand';
import { Model } from '../../../../core/models/model';
import { TypeMotorcycle } from '../../../../core/models/enums/type-motorcycle';

@Component({
  selector: 'app-add-motorcycle',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, NgFor, LoadingSpinnerComponent, RouterModule],
  templateUrl: './add-motorcycle.component.html',
  styleUrl: './add-motorcycle.component.css'
})
export class AddMotorcycleComponent implements OnInit {
  public isLoading: boolean = false; //para spinner
  public showModels: boolean = false; //para mostrar cuando elija marca
  public moto!: Motorcycle;

   // Convertir el enum a un array de pares clave-valor
   public estados = Object.entries(State);
   public typeMoto = Object.entries(TypeMotorcycle);

   //para listas desde BD
  public marcas: Brand[] = [];  
  public modelos: Model[] = [];
  
  miFormulario!: FormGroup; 
   
  constructor(private fb: FormBuilder,
    private brandService: BrandService,
    private modelService: ModelService,
    private motoService: MotorcycleService,

    private activatedRoute: ActivatedRoute, //para rutas
    private router: Router, //para redirigir
    private toastr: ToastrService //para mensajes
) {    
  // Crear un FormGroup con varios FormControls
  this.miFormulario = this.fb.group({      
    id: [],
    description: ['Nueva descripcion', Validators.required], 
    gasolineConsumption: [1, Validators.required], 
    luggageCapacity: ['1', Validators.required], 
    passengerCapacity: ['1', Validators.required],
    fuel: ['2', Validators.required],
    brandId: ["", Validators.required],
    modelId: ["", Validators.required],      
    state: ['', Validators.required], 
    active: [true, Validators.required], 
    abs: [false, Validators.required],  
    cilindrada: [0, Validators.required],  
    price: [0, Validators.required],
    
    typeMotorcycle: ["", Validators.required], 
    

    //agregado para imagen
    image:['']
  });
}

ngOnInit() {        
  // Cargar marcas y modelos antes de asignar los valores del formulario
  this.isLoading = true;
  this.CargaMarcasModelos();
} 

 
CargaMarcasModelos(){    
  forkJoin({      
    brands: this.brandService.getBrands(),      
  }).subscribe(      
    ({ brands}) => {        
      //filtro por marcas que tengan modelos asociados           
      this.marcas = brands.filter(brand=> brand.models && brand.models.length > 0);      
      
      //Sino hay marcas redirijo al listado
      if(this.marcas.length < 1)
        {
          this.toastr.warning("Primero debe agregar Marcas y Modelos", 'Información');
          this.router.navigateByUrl('/');
          return;
        }

      //AGREGA NUEVO -> comprobar que ruta NO es para editar
      if ( !this.router.url.includes('edit') ) {
        this.isLoading = false;
        return;
      }        

      //EDITA -> Si la ruta es para editar, busco el dato por id
      this.activatedRoute.params
        .pipe(
          switchMap( ({ id }) => this.motoService.getMotoById( id ) ),
        ).subscribe( 
          (myCar) => {

            //sino encontro dato, redirijo al home
            if ( !myCar ) {
              return this.router.navigateByUrl('/');
            }

            //reinicio form con los datos traidos del servidor
            console.log({myCar});
            this.miFormulario.reset( myCar );
            this.isLoading = false;
            this.showModels = true;

            //filtrar modelos por marca del vehiculo
            this.filtrarModelosPorMarca(myCar.brandId);

            return;
          },
          (error) => {          
            this.toastr.error(error, 'Se ha producido un error');            
            return this.router.navigateByUrl('/');
          }
      );
    },
    (error) => {
      this.toastr.error(error, 'Se ha producido un error');                  
    }
  );
}


// Obtener el valor seleccionado de marca
get selectedOption() {
  return this.miFormulario.get('brandId')?.value;
}

// Método para filtrar por ID
public findMarcaById(id: number): Brand | undefined {        
  let marcaElegida = this.marcas.find(mar => mar.id == id);        
  return marcaElegida;
}
//evento de cambiar valor en select marcas
onOptionChange(){    
  let idMarcaSeleccionada = this.selectedOption;
  //muestra modelos si eligio una marca
  if(idMarcaSeleccionada){
    this.filtrarModelosPorMarca(idMarcaSeleccionada);
    this.showModels = true;
  }else{
    this.showModels = false;
  }
  
}

filtrarModelosPorMarca(idMarca: number){
  let marcaSeleccionada = this.findMarcaById(idMarca);
  this.modelos = marcaSeleccionada!.models;

  // Verificar si el modelo actual (modelId) está en los modelos filtrados
  const modeloSeleccionado = this.modelos.find(modelo => modelo.id === this.miFormulario.value.modelId);

  if (modeloSeleccionado) {
    // Si el modelo está en la lista de modelos filtrados, asignamos el valor al formulario
    this.miFormulario.get('modelId')?.setValue(modeloSeleccionado.id);
  } else {
    // Si el modelo no está en los modelos filtrados, asignamos el valor por defecto (o null)      
    this.miFormulario.get('modelId')?.setValue('');
  }
}


get selectedOptionType() {
  return this.miFormulario.get('typeId')?.value;
}



// Método para filtrar por ID
public findMotoById(id: number): Brand | undefined {        
  let marcaElegida = this.marcas.find(mar => mar.id == id);        
  return marcaElegida;
}


get currentMoto(): Motorcycle {
  const auto = this.miFormulario.value as Motorcycle;
  return auto;
}


onSubmit():void {
  console.log(this.miFormulario.value);

  //VALIDAR FORM
    if (this.miFormulario.invalid){
      //marca los campos de entrada como si se accedieron a ellos
      this.miFormulario.markAllAsTouched(); 
      this.toastr.warning("Verifique los campos requeridos", 'Información');     
      return;
    }
  
  //Comprobar ID, si existe id, actualiza auto
    if ( this.currentMoto.id ) {      
      this.motoService.updateMoto( this.currentMoto )
        .subscribe( 
          (response ) => {          
            this.toastr.success('Se actualizó con éxito', `Información`);
            this.router.navigate(['/']); // Navega a la ruta principal 
          }
          ,(error) => {                    
            this.toastr.error(error.message , 'Se ha producido un error');        
          }
      );

      return;
    }

  //Agrega nuevo auto sino es edición
    console.log(this.currentMoto);
    this.motoService.createMoto( this.currentMoto )
      .subscribe(              
        auto => {
          // Si la operación es exitosa
          this.toastr.success('Se agregó Motocicleta con éxito', 'Información');
          this.router.navigate(['/']); // Navega a la ruta principal
        },
        error => {
          // Si ocurre un error
          this.toastr.error(error.message , 'Se ha producido un error');        
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
