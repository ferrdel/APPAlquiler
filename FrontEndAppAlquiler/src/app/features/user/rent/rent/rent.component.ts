import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { Route, Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Car } from '../../../../core/models/car';
import { DateRent } from '../../../../core/models/dateRent';
import { SessionStorageManageService } from '../../../../core/services/session-storage-manage.service';
import { RentService } from '../../../../core/services/rent.service';
import { TypeVehicle } from '../../../../core/models/enums/type-vehicle.enum';
import { RentState } from '../../../../core/models/enums/rent-state.enum';
import { Rent } from '../../../../core/models/rent';


@Component({
  selector: 'app-rent',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './rent.component.html',
  styleUrl: './rent.component.css'
})
export class RentComponent implements OnInit{
  //public userId!:number;
  public car!: Car;
  public dateRent?: DateRent;
  public daysRent!: number;
  public priceRent!: number;

  miFormulario!: FormGroup;

  constructor(
    private sessionManage: SessionStorageManageService,
    private fb: FormBuilder,    
    private toastr: ToastrService,
    private rentService: RentService,
    private router: Router, //para redirigir
  ){
    
  }

  ngOnInit(): void {
    this.dateRent = this.sessionManage.getFromSessionStorage('dateRent');    
    if(!this.dateRent){
      this.router.navigate(['/rent/date']);
      this.toastr.warning("Select a date and time", 'Warning');  
      return;
    }
    
    this.car = this.sessionManage.getFromSessionStorage('carRent');
    if(!this.car){
      this.router.navigate(['/rent/car']);
      this.toastr.warning("Select a vehicle", 'Warning');  
      return;
    }
          
    //this.userId = this.sessionManage.getFromLocalStorage('userId');
    this.daysRent = this.calcularDiferenciaEnDias(this.dateRent.pickUpDate, this.dateRent.returnDate);
    this.priceRent = this.car.price * this.daysRent;

    this.miFormulario = this.fb.group({
      id: [,],
      pickUpDate: [this.dateRent.pickUpDate, Validators.required],
      pickUpTime: [this.dateRent.pickUpTime, Validators.required],

      returnDate: [this.dateRent.returnDate, Validators.required],
      returnTime: [this.dateRent.returnTime, Validators.required],

      vehicle: [TypeVehicle.car, Validators.required],
      vehicleId: [this.car.id, Validators.required],
      state: [RentState.pending, Validators.required],
      //userId: [this.userId, Validators.required]
    });
  }

  onSubmit(){
    //VALIDAR FORM
    if (this.miFormulario.invalid){
      //marca los campos de entrada como si se accedieron a ellos
      this.miFormulario.markAllAsTouched(); 
      this.toastr.warning("Check required fields", 'Information');     
      return;
    }
    
    console.log("Rent a guardar: " + this.currentRent);
    this.rentService.createRent( this.currentRent )
      .subscribe(              
        response => {
          // Si la operación es exitosa
          this.sessionManage.removeFromSessionStorage('carRent');
          this.sessionManage.removeFromSessionStorage('dateRent');

          this.toastr.success('Rent added successfully', 'Information');
          this.router.navigate(['/rent']); // Navega a la ruta principal
        },
        error => {
          // Si ocurre un error
          this.toastr.error(error.message , 'An error has occurred');        
        }
    );
  }

  get currentRent(): Rent {
    const rent = this.miFormulario.value as Rent;
    return rent;
  }

  // Función que calcula la diferencia en días entre dos fechas string
  calcularDiferenciaEnDias(pickupDate: string, returnDate: string): number {
    // Convertir las fechas string a objetos Date
    const fechaDate1 = new Date(pickupDate);
    const fechaDate2 = new Date(returnDate);

    // Calcular la diferencia en milisegundos
    const diferenciaEnMilisegundos = fechaDate2.getTime() - fechaDate1.getTime();

    // Convertir la diferencia de milisegundos a días
    const diferenciaEnDias = diferenciaEnMilisegundos / (1000 * 3600 * 24);

    return Math.abs(Math.round(diferenciaEnDias)); // Redondeamos y devolvemos el valor absoluto
  }
}
