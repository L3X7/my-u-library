import { Routes } from '@angular/router';

export const routes: Routes = [
//   // 1. Default Route: Redirect to the main domain (e.g., Products)
//   {
//     path: '',
//     redirectTo: 'products',
//     pathMatch: 'full'
//   },

//   // 2. Lazy Load a "Domain" (A feature with its own child routes)
//   // This downloads the 'products' code ONLY when the user visits /products
//   {
//     path: 'products',
//     loadChildren: () => import('./features/products/products.routes')
//       .then(m => m.productRoutes)
//   },

//   // 3. Lazy Load a "Page" (A feature that is just one component)
//   // Useful for simple pages like Cart or About
//   {
//     path: 'cart',
//     loadComponent: () => import('./features/cart/cart-page.cmp')
//       .then(c => c.CartPage)
//   },

//   // 4. Wildcard Route (404 handling)
//   {
//     path: '**',
//     loadComponent: () => import('./ui/pages/not-found/not-found.cmp')
//       .then(c => c.NotFoundPage)
//   }
];