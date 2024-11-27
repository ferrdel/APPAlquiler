import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Car } from '../../../../core/models/car';
import { Router } from '@angular/router';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';

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
    this.sessionStorage.saveToSessionStorage('carRent', this.car);
    this.router.navigate(['/rent/resume']); 
  }
}
