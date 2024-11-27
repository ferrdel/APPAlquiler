import { RentState } from "./enums/rent-state.enum";
import { TypeVehicle } from "./enums/type-vehicle.enum";
import { Rent } from "./rent";

export class RentDetail extends Rent {
    constructor(
        id:number,
        pickUpDate: string,
        pickUpTime: string,
        returnDate: string,
        returnTime: string,
        state: RentState,
        userId: number,
        vehicle: TypeVehicle,
        vehicleId: number,

        totAmount: number,

        public userFirstName: string,
        public userLastName: string,
        public userDNI: number,
        public userEmail: string,
        public userPhoneNumber: string,
        public userAddress: string,
        public userCity: string,
        public userCountry: string,        
    ){
        super(
            id,
            pickUpDate,
            pickUpTime,
            returnDate,
            returnTime,
            state,
            userId,
            vehicle,
            vehicleId,
            totAmount
        );
    }
}
