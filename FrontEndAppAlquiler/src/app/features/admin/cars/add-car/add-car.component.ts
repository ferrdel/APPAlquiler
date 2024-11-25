import { Component, OnInit } from '@angular/core';
import { Car } from '../../../../core/models/car';
import { State } from '../../../../core/models/enums/state.enum';
import { Transmision } from '../../../../core/models/enums/transmision.enum';
import { Combustible } from '../../../../core/models/enums/combustible.enum';
import { Brand } from '../../../../core/models/brand';
import { Model } from '../../../../core/models/model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CilindradaAuto } from '../../../../core/models/enums/cilindrada-auto.enum';
import { forkJoin, switchMap } from 'rxjs';
import { BrandService } from '../../../../core/services/brand.service';
import { ModelService } from '../../../../core/services/model.service';
import { CarService } from '../../../../core/services/car.service';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { NgFor, NgIf } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-car',
  standalone: true,  
  imports: [ReactiveFormsModule, NgIf, NgFor, LoadingSpinnerComponent, RouterModule],
  templateUrl: './add-car.component.html',
  styleUrl: './add-car.component.css'
})
export class AddCarComponent implements OnInit{  
  public isLoading: boolean = false; //para spinner
  public showModels: boolean = false; //para mostrar cuando elija marca
  public car!: Car;

  // Convertir el enum a un array de pares clave-valor
  public estados = Object.entries(State);
  public transmisiones = Object.entries(Transmision);
  public combustibles = Object.entries(Combustible);
        
  //para listas desde BD
  public marcas: Brand[] = [];  
  public modelos: Model[] = [];
  
  miFormulario!: FormGroup;
  
  constructor(private fb: FormBuilder,
      private brandService: BrandService,
      private modelService: ModelService,
      private carService: CarService,

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
      fuel: ['', Validators.required],       
      state: ['', Validators.required],
      active: [true, Validators.required],
      price: [0, Validators.required],
      brandId: ["", Validators.required],
      modelId: ["", Validators.required], 
                        
      numberDoors: ['3', Validators.required],       
      airConditioning: [false, Validators.required],    
      transmission: [Transmision.MANUAL, Validators.required], 
      airbag: [true, Validators.required], 
      abs: [false, Validators.required], 
      sound: ['Sistema de audio', Validators.required],
      engineLiters: [CilindradaAuto._1_4, Validators.required],      


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
            this.toastr.warning("You must first add Brands and Models", 'Information');            
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
            switchMap( ({ id }) => this.carService.getCarById( id ) ),
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
              this.toastr.error(error, 'An error has occurred');                          
              return this.router.navigateByUrl('/');
            }
        );
      },
      (error) => {
        this.toastr.error(error, 'An error has occurred');                          
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
  
  get currentAuto(): Car {
    const auto = this.miFormulario.value as Car;
    return auto;
  }

  onSubmit():void {
  //VALIDAR FORM
    if (this.miFormulario.invalid){
      //marca los campos de entrada como si se accedieron a ellos
      this.miFormulario.markAllAsTouched(); 
      this.toastr.warning('Check the required fields', 'Information');           
      return;
    }
  
  //Comprobar ID, si existe id, actualiza auto
    if ( this.currentAuto.id ) {      
      this.carService.updateCar( this.currentAuto )
        .subscribe( 
          (response ) => {          
            this.toastr.success('Car successfully updated', 'Information');            
            this.router.navigate(['/cars']); // Navega a la ruta principal 
          }
          ,(error) => {                    
            this.toastr.error(error.message , 'An error has occurred');                    
          }
      );

      return;
    }

  //Agrega nuevo auto sino es edición
    console.log(this.currentAuto);
    this.carService.createCar( this.currentAuto )
      .subscribe(              
        auto => {
          // Si la operación es exitosa
          this.toastr.success('Car added successfully', 'Information');          
          this.router.navigate(['/cars']); // Navega a la ruta principal
        },
        error => {
          // Si ocurre un error
          this.toastr.error(error.message , 'An error has occurred');                  
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