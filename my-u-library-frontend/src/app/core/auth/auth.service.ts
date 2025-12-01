import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { LoginRequest } from './models/login-request.interface';
import { User } from './models/user.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  private _currentUser = signal<User | null>(this.loadFromStorage());
  readonly currentUser = this._currentUser.asReadonly();

  async login(credentials: LoginRequest): Promise<void> {
    try {
      const user = await firstValueFrom(
        this.http.post<User>('', credentials)
      );
      this.updateState(user);
      this.router.navigate(['/']);

    } catch (error) {

    }
  }

  private updateState(user: User | null): void {
    this._currentUser.set(user);
    if (user) {
      localStorage.setItem('auth_user', JSON.stringify(user));
    } else {
      localStorage.removeItem('auth_user');
    }
  }

  private loadFromStorage(): User | null {
    const data = localStorage.getItem('auth_user');
    return data ? JSON.parse(data) : null;
  }
}
