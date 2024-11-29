import { Combustible } from "./enums/combustible.enum";
import { State } from "./enums/state.enum";
import { Transmision } from "./enums/transmision.enum";
import { Vehicle } from "./vehicle";

export class Bike extends Vehicle{
    constructor(
        id: number,
        description: string,
        gasolineConsumption: string,//cantidad de litros de nafta
        luggageCapacity: string,//capacidad de equipaje(En Litros)
        passengerCapacity: number, //Cantiadad de pasajeros        
        fuel: Combustible, //tipo combustible (ver bien el tipo de dato)
        state: State, //Alquilado,Disponible,EnMantenimiento
        active: boolean, //Baja logica
        price: number,        

        //RelationShips
        modelId: number,        
        brandId: number,

        image: string,
        model: string,
        brand: string,
        
        public whell: number,
        public frameSize: number,
        public numberSpeeds: number      
        
    ){
        super(
            id,
            description,
            gasolineConsumption,
            luggageCapacity,
            passengerCapacity,            
            fuel,
            state,
            active,
            price,                            
            modelId,            
            brandId,

            image,
            brand,
            model
        );
    }
}
