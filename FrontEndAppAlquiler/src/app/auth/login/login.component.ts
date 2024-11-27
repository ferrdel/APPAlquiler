import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FooterComponent } from "../../shared/components/footer/footer.component";
import { ToastrService } from 'ngx-toastr';

@Component({
	selector: 'login',
	standalone: true,
	imports: [ReactiveFormsModule, CommonModule, FooterComponent],
	templateUrl: './login.component.html',
	styleUrl: './login.component.css',
})
export class LoginComponent {
	loginForm: FormGroup = new FormGroup({});
	errorMessage: string | null = null;

	constructor(
		private fb: FormBuilder,
		private authService: AuthService,
		private router: Router,
		private route: ActivatedRoute,
		private toastr: ToastrService
	) {}

	ngOnInit() {
		this.loginForm = this.fb.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required, Validators.minLength(6)]],
		});
	}

	onLogin(): void {
		console.log('Form Submitted');
		if (this.loginForm.valid) {
			console.log('Form Valid');
			this.authService.login(this.loginForm.value).subscribe(
				() => {
					this.toastr.success('Login Success')
					console.log('Login Success');
					this.router.navigate([
						this.route.snapshot.queryParams['returnUrl'] || '/home',
					]);
					this.errorMessage = null; // Limpia el error si el inicio es exitoso
				},
				(error) => {
					console.error('Login Error: ', error);
					this.errorMessage = error?.error || 'Invalid email or password';
				}
			);
		}
	}
}
