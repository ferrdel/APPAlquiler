import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	private baseUrl = 'https://localhost:7169/api/Account';

	private currentUserSubject = new BehaviorSubject<any>(null);
	currentUser$ = this.currentUserSubject.asObservable();

	constructor(private http: HttpClient) {}

	login(credentials: { email: string; password: string }): Observable<any> {
		return this.http.post(`${this.baseUrl}/login`, credentials).pipe(
			tap((response: any) => {
				localStorage.setItem('authToken', response.token);
				const decodedToken: any = this.decodeToken(response.token);
				const userRole = decodedToken.role;
				localStorage.setItem('roles', userRole);
				this.currentUserSubject.next(decodedToken);
			}),
			catchError((error) => {
				console.error('Error: ', error);
				return throwError(() => {
					new Error(error.message);
				});
			})
		);
	}

	logout() {
		localStorage.removeItem('authToken');
		localStorage.removeItem('roles');
		this.currentUserSubject.next(null);
	}

	getToken() {
		return localStorage.getItem('authToken');
	}

	isLoggeIn() {
		return !!this.getToken();
	}

	getRoles() {
		let roles = localStorage.getItem('roles');
		return roles || '';
	}

	initializeUser() {
		const token = this.getToken();
		if (token) {
			const decodeToken = this.decodeToken(token);
			this.currentUserSubject.next(decodeToken);
		}
	}

	private decodeToken(token: string) {
		try {
			return jwtDecode(token);
		} catch (error) {
			console.error('Error decodificando el token: ', error);
			return null;
		}
	}

	register(data: any): Observable<any> {
		return this.http.post(`${this.baseUrl}/register`, data).pipe(
			catchError((error) => {
				console.error('Error: ', error);
				return throwError(() => error);
			})
		);
	}
}
