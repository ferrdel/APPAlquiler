import { Combustible } from "./enums/combustible.enum";
import { State } from "./enums/state.enum";
import { Vehicle } from "./vehicle";

export class Motorcycle extends Vehicle{
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
        public typeMotorcycleId:number,
    
        public abs: boolean,
        public cilindrada: number        
        
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
