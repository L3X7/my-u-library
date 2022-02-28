import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { SecurityService } from '../services/security.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(public router: Router, private autService: SecurityService) { }

    canActivate(): boolean {
        if (!this.autService.isAuthenticated()) {
            this.router.navigate(['/security/login']);
            return false;
        }

        return true;
    }
}