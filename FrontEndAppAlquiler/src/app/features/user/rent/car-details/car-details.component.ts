import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Car } from '../../../../core/models/car';
import { Router } from '@angular/router';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';
import { VehicleStorage } from '../../../../core/models/vehicleStorage';
import { TypeVehicle } from '../../../../core/models/enums/type-vehicle.enum';
import { Vehicle } from '../../../../core/models/vehicle';

@Component({
  selector: 'car-details',
  standalone: true,
  imports: [],
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.css'
})
export class CarDetailsComponent {
  @Input()
  public car!: Car;

  @Output()
  public carDetailEvent:EventEmitter<Car> = new EventEmitter();
  

  constructor( 
    private router: Router, //para redirigir    
    private sessionStorage: SessionStorageManageService
  ){}

  public emitAutomovil(carDetail: Car):void{    
    this.carDetailEvent.emit(carDetail);
  }

  public selectCar(){
    let vehicle: VehicleStorage = { typeVehicle: TypeVehicle.car, vehicle: this.car as Vehicle};    
    this.sessionStorage.saveToSessionStorage('vehicleRent', vehicle);    
    this.router.navigate(['/rent/resume']); 
  }
}
