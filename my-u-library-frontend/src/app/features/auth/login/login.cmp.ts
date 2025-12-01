import { Component, inject, signal } from "@angular/core";
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/core/auth/auth.service";

@Component({
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './login.cmp.html',
    styleUrl: './login.cmp.scss'
})
export class LoginPage {
    private fb = inject(FormBuilder);
    private authService = inject(AuthService);
    private router = inject(Router);

    isLoading = signal(false);
    errorMessage = signal<string | null>(null);

    loginForm = this.fb.nonNullable.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
    });

    async onSubmit() {}
}