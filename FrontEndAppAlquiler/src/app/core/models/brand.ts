import { Model } from "./model";

export class Brand {
    constructor(public id:number, public name: string, public active: boolean, public models: Model[]){}
}