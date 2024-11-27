import { State } from "./enums/state.enum";


export class Vehicle {
    constructor(        
        public id: number, 
        public description: string,
        public gasolineConsumption: string,//cantidad de litros de nafta
        public luggageCapacity: string,//capacidad de equipaje(En Litros)
        public passengerCapacity: number, //Cantiadad de pasajeros
        //definicion combustible
        public fuel: string, //tipo combustible (ver bien el tipo de dato)
        public state: State, //Alquilado,Disponible,EnMantenimiento
        public active: boolean, //Baja logica
        public price: number,
        

        //RelationShips
        public modelId: number,
        //definicion de la marca
        public brandId: number,

        public image: string,
        public brand: string,
        public model: string,
    ){}
}
