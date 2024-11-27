import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ListCarComponent } from './features/admin/cars/list-car/list-car.component';
import { AddCarComponent } from './features/admin/cars/add-car/add-car.component';
import { UserRentsComponent } from './features/user/rent/user-rents/user-rents.component';
import { DateRentComponent } from './features/user/rent/date-rent/date-rent.component';
import { CarListComponent } from './features/user/rent/car-list/car-list.component';
import { RentComponent } from './features/user/rent/rent/rent.component';
import { roleGuard } from './core/guards/role.guard';
import { ListRentComponent } from './features/admin/rent/list-rent/list-rent.component';
import { EditRentComponent } from './features/admin/rent/edit-rent/edit-rent.component';

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
        path: 'cars',
        canActivateChild: [roleGuard],
        data: {expectedRole: 'ADMIN'},
        children: [
            { path:'', component: ListCarComponent },
            { path:'add', component: AddCarComponent },
            { path:'edit/:id', component: AddCarComponent },            
        ]        
    },

    {
        path: 'rents',
        canActivateChild: [roleGuard],
        data: {expectedRole: 'ADMIN'},
        children: [
            { path:'', component: ListRentComponent },            
            { path:'edit/:id', component: EditRentComponent },            
        ]        
    },
    
    {
        path: 'rent', 
        canActivateChild: [roleGuard],
        data: {expectedRole: 'USER'},
        children: [
            { path:'', component: UserRentsComponent },
            { path:'date', component: DateRentComponent },
            { path:'car', component: CarListComponent },
            { path:'resume', component: RentComponent },            
        ]        
    }
];

