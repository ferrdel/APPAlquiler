import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { Motorcycle } from '../../../../core/models/motorcycle';
import { MotorcycleService } from '../../../../core/services/motorcycle.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';
import { State } from '../../../../core/models/enums/state.enum';
import { MotorcycleDetailsComponent } from '../motorcycle-details/motorcycle-details.component';
import { VehicleStorage } from '../../../../core/models/vehicleStorage';
import { TypeVehicle } from '../../../../core/models/enums/type-vehicle.enum';
import { Vehicle } from '../../../../core/models/vehicle';

@Component({
  selector: 'app-motorcycles-list',
  standalone: true,
  imports: [ NgFor, NgIf, LoadingSpinnerComponent, MotorcycleDetailsComponent ],
  templateUrl: './motorcycles-list.component.html',
  styleUrl: './motorcycles-list.component.css'
})
export class MotorcyclesListComponent {
  public isLoading:boolean = true; //para spinner

  //para vincular la lista con la vista
  public motorcycleList: Motorcycle[] = [];
  
  //para mostrar detalles en modal
  public motoDetail?: Motorcycle;
  private vehicleStorage!: VehicleStorage;
  
  constructor(
    private motoService: MotorcycleService,
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
    this.motoService.getAllMotos().subscribe(    
      (data:Motorcycle[]) => {
        //filtro por estado activo y disponible      
        this.motorcycleList = data.filter( moto => moto.active && moto.state.toUpperCase() == State.disponible.toUpperCase());   
              
        this.isLoading = false;                            
      },
      (error) => {                  
        this.toastr.error(error, 'Se ha producido un error');    
      }
    );
  }
  
  //al llamar al boton que dispara el modal, asigna el id del vehiculo a eliminar
  callModal(moto: Motorcycle){
    this.motoDetail = moto;
  }
  
  //metodo para usar el dato recibido desde componente hijo
  getMotoDetail(motoDetail: Motorcycle):void{
    console.log('llego al componente padre');
    this.motoDetail = motoDetail;  
    console.log(this.motoDetail);
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
  
  public selectMoto(){
      let vehicle: VehicleStorage = { typeVehicle: TypeVehicle.motorcycle, vehicle: this.motoDetail as Vehicle};
      console.log(vehicle);
      this.storageManage.saveToSessionStorage('vehicleRent', vehicle);      
      this.router.navigate(['/rent/resume']);   
  }
}
