import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';
import { Motorcycle } from '../../../../core/models/motorcycle';
import { MotorcycleService } from '../../../../core/services/motorcycle.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-motorcycle',
  standalone: true,
  imports: [ CommonModule, RouterModule, LoadingSpinnerComponent],
  templateUrl: './list-motorcycle.component.html',
  styleUrl: './list-motorcycle.component.css'
})
export class ListMotorcycleComponent {

  public idEliminar?: number;

  public isLoading:boolean = true; //para spinner
  
  //para vincular la lista con la vista
  public motorcycleList: Motorcycle[] = [];
  
  constructor(
    private motorcycleService: MotorcycleService,
    private toastr: ToastrService, //para mensajes  
  ){}
  
  //Agregado para cargar tareas cuando se instancia el componente
  ngOnInit(): void {  
    this.getAll();
  }
  
  getAll(){
    this.motorcycleService.getAllMotos().subscribe(    
      (data:Motorcycle[]) => {
        this.motorcycleList = data;   
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
    this.motorcycleService.removeMoto(this.idEliminar!).subscribe(
      (response) => {      
        this.toastr.success('Motorcycle was successfully disabled','Information');         
        this.getAll();
      },
      (error) => {            
        this.toastr.error(error, 'An error has occurred');            
      }
    );
  }  
  
  onEnable(){
    this.motorcycleService.activateMoto(this.idEliminar!).subscribe(
      () => {      
        this.toastr.success('Motorcycle was successfully enabled','Information');          
        this.getAll();
      },
      (error) => {            
        this.toastr.error(error, 'An error has occurred');            
      }
    );
  }  
}
