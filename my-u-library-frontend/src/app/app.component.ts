import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';


@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet],
    template: `
    <!-- The Layout Shell -->

    <main class="container mx-auto p-4">
      <!-- The Active Route -->
      <router-outlet /> 
    </main>
  `
})
export class AppComponent {

}