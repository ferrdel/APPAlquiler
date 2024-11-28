import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Motorcycle } from '../../../../core/models/motorcycle';
import { Router } from '@angular/router';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';
import { VehicleStorage } from '../../../../core/models/vehicleStorage';
import { TypeVehicle } from '../../../../core/models/enums/type-vehicle.enum';
import { Vehicle } from '../../../../core/models/vehicle';

@Component({
  selector: 'app-motorcycle-details',
  standalone: true,
  imports: [],
  templateUrl: './motorcycle-details.component.html',
  styleUrl: './motorcycle-details.component.css'
})
export class MotorcycleDetailsComponent {
  @Input()
  public moto!: Motorcycle;

  @Output()
  public motoDetailEvent:EventEmitter<Motorcycle> = new EventEmitter();
  

  constructor( 
    private router: Router, //para redirigir    
    private sessionStorage: SessionStorageManageService
  ){}

  public emitMotorcycle(motoDetail: Motorcycle):void{    
    this.motoDetailEvent.emit(motoDetail);
  }

  public selectMotorcycle(){
    let vehicle: VehicleStorage = { typeVehicle: TypeVehicle.motorcycle, vehicle: this.moto as Vehicle};    
    this.sessionStorage.saveToSessionStorage('vehicleRent', vehicle);
    this.router.navigate(['/rent/resume']); 
  }
}
