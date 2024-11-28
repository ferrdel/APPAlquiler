import { TypeVehicle } from "./enums/type-vehicle.enum";
import { Vehicle } from './vehicle';

export class VehicleStorage {
    constructor(        
        public typeVehicle: TypeVehicle, 
        public vehicle: Vehicle
    ){}
}
