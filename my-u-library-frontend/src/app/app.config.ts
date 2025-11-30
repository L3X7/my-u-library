// src/app/app.config.ts
import { ApplicationConfig, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    // 1. The Core: Enable Zoneless (Signals drive the app)
    provideZonelessChangeDetection(),

    // 2. The Router: Enable modern View Transitions (native browser animations)
    provideRouter(routes, withViewTransitions()),

    // 3. The Data: Use fetch API instead of XHR
    provideHttpClient(withFetch())
  ]
};