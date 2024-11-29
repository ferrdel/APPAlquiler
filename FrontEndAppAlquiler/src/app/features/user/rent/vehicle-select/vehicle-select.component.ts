import { Component } from '@angular/core';
import { CarListComponent } from '../car-list/car-list.component';
import { FormsModule, NgModel } from '@angular/forms';
import { TypeVehicle } from '../../../../core/models/enums/type-vehicle.enum';
import { NgIf, NgSwitch, NgSwitchCase, NgSwitchDefault } from '@angular/common';
import { MotorcyclesListComponent } from '../motorcycles-list/motorcycles-list.component';

@Component({
  selector: 'app-vehicle-select',
  standalone: true,
  imports: [CarListComponent, MotorcyclesListComponent, FormsModule, NgSwitch, NgSwitchCase, NgSwitchDefault],
  templateUrl: './vehicle-select.component.html',
  styleUrl: './vehicle-select.component.css'
})
export class VehicleSelectComponent {
  typeVehicle = TypeVehicle;
  selectedTypeVehicle: TypeVehicle = TypeVehicle.car;
}
