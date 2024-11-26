import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ListCarComponent } from './features/admin/cars/list-car/list-car.component';
import { AddCarComponent } from './features/admin/cars/add-car/add-car.component';
import { AddBikeComponent } from './features/admin/bikes/add-bike/add-bike.component';
import { AddBoatsComponent } from './features/admin/boats/add-boats/add-boats.component';
import { AddMotorcycleComponent } from './features/admin/motorcycle/add-motorcycle/add-motorcycle.component';

export const routes: Routes = [
    {
        path: 'home', component: HomeComponent
    },
    {
        path: '', redirectTo: '/home', pathMatch: 'full'
    },
    {
        path: 'login', component: LoginComponent
    },
    {
        path: 'signup', component: RegisterComponent
    },

    {
        path: 'cars', children: [
            { path:'', component: ListCarComponent },
            { path:'add', component: AddCarComponent },
            { path:'edit/:id', component: AddCarComponent },            
        ]        
    },
    {
        path: 'bikes', children: [
            { path:'', component: ListCarComponent },
            { path:'addBike', component: AddBikeComponent },
            { path:'editBike/:id', component: AddBikeComponent },            
        ]        
    },

    {
        path: 'boats', children: [
            { path:'', component: ListCarComponent },
            { path:'addBoat', component: AddBoatsComponent },
            { path:'editBoat/:id', component: AddBoatsComponent },            
        ]        
    },
    {
        path: 'motorcycles', children: [
            { path:'', component: ListCarComponent },
            { path:'addMoto', component: AddMotorcycleComponent },
            { path:'editMoto/:id', component: AddMotorcycleComponent },            
        ]        
    },
];

