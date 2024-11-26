import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ListCarComponent } from './features/admin/cars/list-car/list-car.component';
import { AddCarComponent } from './features/admin/cars/add-car/add-car.component';

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
];

