import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { VehicleResume } from '../../../../core/models/vehicleResume';
import { Rent } from '../../../../core/models/rent';
import { RentService } from '../../../../core/services/rent.service';
import { CarService } from '../../../../core/services/car.service';
import { RouterModule } from '@angular/router';
import { MotorcycleService } from '../../../../core/services/motorcycle.service';

@Component({
  selector: 'app-user-rents',
  standalone: true,
  imports: [NgIf, NgFor, LoadingSpinnerComponent, RouterModule],
  templateUrl: './user-rents.component.html',
  styleUrl: './user-rents.component.css'
})
export class UserRentsComponent {
  public vehiculoId!: number;
  public typeVehicle!: string;
  public loadingVehicle: boolean = false;

  public vehicleResume?: VehicleResume = { id:0, brand: '', model: '', image: '', description: ''};

  public isLoading:boolean = true; //para spinner en lista
  
  //para vincular la lista con la vista
  public rentList: Rent[] = [];
  
  constructor(
    private rentService: RentService,
    private toastr: ToastrService, //para mensajes      
    private carService: CarService,
    private motorcycleService: MotorcycleService
  ){}
  
  //Agregado para cargar tareas cuando se instancia el componente
  ngOnInit(): void {      
    this.getMyRents();    
  }


  getMyRents(){    
      this.rentService.myRents().subscribe(    
      (data:Rent[]) => {        
        this.rentList = data;   
        console.log(this.rentList);
        this.isLoading = false;          
      },
      (error) => {                  
        this.toastr.error(error, 'An error has occurred');    
      }
    );
  }
  
  //al llamar al boton que dispara el modal, asigna el id del vehiculo
  callModal(vehiculoId:number, typeVehicle: string){
    this.vehiculoId = vehiculoId;
    this.typeVehicle = typeVehicle;

    switch (typeVehicle) {
      case 'car':
        this.carService.getCarById(vehiculoId).subscribe(
          (response) => {                  
            this.vehicleResume = response;                        
          },
          (error) => {            
            this.toastr.error(error, 'An error has occurred');    
          }
        );
        
        this.loadingVehicle = true;
        break;

        case 'motorcycle':
          this.motorcycleService.getMotoById(vehiculoId).subscribe(
            (response) => {                  
              this.vehicleResume = response;                        
            },
            (error) => {            
              this.toastr.error(error, 'An error has occurred');    
            }
          );
          
          this.loadingVehicle = true;
          break;
    
      default:
        console.log('no se encontró tipo de vehiculo');
        break;
    }
    
  }  
}
