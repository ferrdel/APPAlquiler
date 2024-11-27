import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { CarDetailsComponent } from '../car-details/car-details.component';
import { Car } from '../../../../core/models/car';
import { CarService } from '../../../../core/services/car.service';
import { State } from '../../../../core/models/enums/state.enum';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';

@Component({
  selector: 'app-car-list',
  standalone: true,
  imports: [NgFor, CarDetailsComponent, NgIf, LoadingSpinnerComponent],
  templateUrl: './car-list.component.html',
  styleUrl: './car-list.component.css'
})
export class CarListComponent {
  public isLoading:boolean = true; //para spinner

//para vincular la lista con la vista
public automovilList: Car[] = [];

//para mostrar detalles en modal
public autoDetail?: Car;

constructor(
  private carService: CarService,
  private toastr: ToastrService, //para mensajes
  private router: Router, //para redirigir      
  private storageManage: SessionStorageManageService
){}

//Agregado para cargar tareas cuando se instancia el componente
ngOnInit(): void {  
  let dateRent = this.storageManage.getFromSessionStorage('dateRent');    
    if(!dateRent){
      this.router.navigate(['/rent/date']);
      this.toastr.warning("Seleccione una fecha", 'Advertencia');  
      return;
    }
  this.getAll();
}

getAll(){
  this.carService.getAll().subscribe(    
    (data:Car[]) => {
      //filtro por estado activo y disponible      
      this.automovilList = data.filter( car => car.active && car.state.toUpperCase() == State.disponible.toUpperCase());   
            
      this.isLoading = false;          
            
      //Sino hay marcas redirijo al listado
      if(this.automovilList.length < 1)
        {
          this.toastr.warning("Primero debe agregar autos", 'Información');
          //this.router.navigateByUrl('/');
          return;
        }

    },
    (error) => {                  
      this.toastr.error(error, 'Se ha producido un error');    
    }
  );
}

//al llamar al boton que dispara el modal, asigna el id del vehiculo a eliminar
callModal(automovil: Car){
  this.autoDetail = automovil;
}

//metodo para usar el dato recibido desde componente hijo
getCarDetail(carDetail: Car):void{
  console.log('llego al componente padre');
  this.autoDetail = carDetail;  
  console.log(this.autoDetail);
}

// Función que convierte un string en su correspondiente valor del enum
getEstadoTexto(estado: string | undefined): string {
  switch(estado?.toLowerCase()) {
    case 'disponible':
      return State.disponible;  // 'Disponible'
    case 'enmantenimiento':
      return State.enmantenimiento;  // 'En Mantenimiento'
    case 'alquilado':
      return State.alquilado;  // 'Alquilado'
    default:
      return 'Estado incorrecto';  // Si no se encuentra el string
  }
}

public selectCar(){
    this.storageManage.saveToSessionStorage('carRent', this.autoDetail);
    this.router.navigate(['/rent/resume']);   
}

}
