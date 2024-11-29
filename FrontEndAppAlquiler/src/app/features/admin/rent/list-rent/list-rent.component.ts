import { Component } from '@angular/core';
import { RentService } from '../../../../core/services/rent.service';
import { ToastrService } from 'ngx-toastr';
import { RentDetail } from '../../../../core/models/rentDetail';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { NgFor, NgIf,NgClass } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RentState } from '../../../../core/models/enums/rent-state.enum';

@Component({
  selector: 'app-list-rent',
  standalone: true,
  imports: [LoadingSpinnerComponent, NgIf, NgFor, RouterModule,NgClass],
  templateUrl: './list-rent.component.html',
  styleUrl: './list-rent.component.css'
})
export class ListRentComponent {
  RentState = RentState;
  public idRent?: number;

  public isLoading:boolean = true; //para spinner
  
  //para vincular la lista con la vista
  public rentList: RentDetail[] = [];
  
  constructor(
    private rentService: RentService,
    private toastr: ToastrService, //para mensajes  
  ){}
  
  //Agregado para cargar tareas cuando se instancia el componente
  ngOnInit(): void {  
    this.getAll();
  }
  
  getAll(){
    this.rentService.getAllRents().subscribe(    
      (data:RentDetail[]) => {
        this.rentList = data;   
        this.isLoading = false;          
      },
      (error) => {                  
        this.toastr.error(error, 'An error has occurred');            
      }
    );
  }
  
  //al llamar al boton que dispara el modal, asigna el id del vehiculo a eliminar
  callModal(id:number){
    this.idRent = id;
  }
  
  /*
  onEdit(){
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
    */ 
}
