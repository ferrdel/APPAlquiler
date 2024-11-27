import { Injectable } from '@angular/core';
import {
	CanActivateChild,
	Router,
	ActivatedRouteSnapshot,
	RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
	providedIn: 'root',
})
export class roleGuard implements CanActivateChild {
	constructor(private authService: AuthService, private router: Router) {}

	canActivateChild(
		childRoute: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): boolean {
		const expectedRole = childRoute.data['expectedRole'];
		const userRole = this.authService.getRoles();

		if (userRole !== expectedRole) {
			this.router.navigate(['/home']);
			return false;
		}
		return true;
	}

  
}
