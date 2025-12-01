import { Routes } from "@angular/router";
import { LoginPage } from "./login/login.cmp";
import { RegisterPage } from "./register/register.cmp";

export const authRotes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginPage },
    { path: 'register', component: RegisterPage },
];