import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Subscription } from 'rxjs';

@Component({
	selector: 'NavbarComponent',
	standalone: true,
	imports: [CommonModule, RouterLink, RouterLinkActive],
	templateUrl: './navbar.component.html',
	styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit {
	isAuthenticated = false;
	currentUser: { unique_name?: string, role?:string, nameid?:number } | null = null;
	private authSubscription: Subscription = new Subscription();

	constructor(private authService: AuthService,
		private router: Router
	) {}

	ngOnInit(): void {
		// Suscribirse al observable currentUser$
		this.authSubscription = this.authService.currentUser$.subscribe((user) => {
			this.isAuthenticated = !!user;
			this.currentUser = user;
		});

		// Inicializar el usuario si hay un token en localStorage
		this.authService.initializeUser();
	}

	logout(): void {
		this.authService.logout();
		this.isAuthenticated = false;
		this.currentUser = null;
		this.router.navigate(['/home']);
	}

	ngOnDestroy(): void {
		this.authSubscription.unsubscribe();
	}
}
