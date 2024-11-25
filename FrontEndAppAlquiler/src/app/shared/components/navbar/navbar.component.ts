import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
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
	currentUser: { unique_name?: string } | null = null;
	private authSubscription: Subscription = new Subscription();

	constructor(private authService: AuthService) {}

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
	}

	ngOnDestroy(): void {
		this.authSubscription.unsubscribe();
	}
}
