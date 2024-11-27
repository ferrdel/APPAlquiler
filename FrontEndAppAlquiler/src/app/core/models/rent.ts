import { RentState } from "./enums/rent-state.enum";
import { TypeVehicle } from "./enums/type-vehicle.enum";

export class Rent {
    constructor(
        public id:number,
        public pickUpDate: string,
        public pickUpTime: string,
        public returnDate: string,
        public returnTime: string,
        public state: RentState,
        public userId: number,
        public vehicle: TypeVehicle,
        public vehicleId: number,

        public totAmount: number
    ){}
}
