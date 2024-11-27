import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { NavbarComponent } from '../../shared/components/navbar/navbar.component';
import { FooterComponent } from '../../shared/components/footer/footer.component';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-register',
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		NavbarComponent,
		FooterComponent,
	],
	templateUrl: './register.component.html',
	styleUrl: './register.component.css',
})
export class RegisterComponent {
	registerForm: FormGroup;
	errorMessage: string | null = null;

	constructor(
		private fb: FormBuilder,
		private authService: AuthService,
		private router: Router
	) {
		this.registerForm = this.fb.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required, Validators.minLength(6)]],
			firstName: ['', Validators.required],
			lastName: ['', Validators.required],
			dni: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
			phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
			address: ['', Validators.required],
			city: ['', Validators.required],
			country: ['', Validators.required],
			gender: ['', Validators.required],
		});
	}

	onRegister(): void {
		if (this.registerForm.valid) {
			this.authService.register(this.registerForm.value).subscribe({
				next: (response) => {
					console.log('Registration successful', response);
					this.errorMessage = null;
					this.router.navigate(['/login']);
				},
				error: (error) => {
					if (error.error && error.error.length > 0) {
						this.errorMessage = error.error[0].description; // Captura el mensaje específico
					} else {
						this.errorMessage = 'Registration failed: ' + error.message;
					}
				},
			});
		} else {
			console.error('Form is invalid');
			this.errorMessage = 'Please fill out all required fields.';
		}
	}
}
