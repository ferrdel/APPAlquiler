import { Component } from '@angular/core';
import { Car } from '../../../../core/models/car';
import { CarService } from '../../../../core/services/car.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-car',
  standalone: true,
  imports: [ CommonModule, RouterModule, LoadingSpinnerComponent ],
  templateUrl: './list-car.component.html',
  styleUrl: './list-car.component.css'
})
export class ListCarComponent {
  public idEliminar?: number;

  public isLoading:boolean = true; //para spinner
  
  //para vincular la lista con la vista
  public carList: Car[] = [];
  
  constructor(
    private carService: CarService,
    private toastr: ToastrService, //para mensajes  
  ){}
  
  //Agregado para cargar tareas cuando se instancia el componente
  ngOnInit(): void {  
    this.getAll();
  }
  
  getAll(){
    this.carService.getAll().subscribe(    
      (data:Car[]) => {
        this.carList = data;   
        this.isLoading = false;          
      },
      (error) => {                  
        this.toastr.error(error, 'An error has occurred');            
      }
    );
  }
  
  //al llamar al boton que dispara el modal, asigna el id del vehiculo a eliminar
  callModal(id:number){
    this.idEliminar = id;
  }
  
  onDelete(){
    this.carService.removeCar(this.idEliminar!).subscribe(
      (response) => {      
        this.toastr.success('Car was successfully disabled','Information');         
        this.getAll();
      },
      (error) => {            
        this.toastr.error(error, 'An error has occurred');            
      }
    );
  }  
  
  onEnable(){
    this.carService.activateCar(this.idEliminar!).subscribe(
      () => {      
        this.toastr.success('Car was successfully enabled','Information');          
        this.getAll();
      },
      (error) => {            
        this.toastr.error(error, 'An error has occurred');            
      }
    );
  }  
}
