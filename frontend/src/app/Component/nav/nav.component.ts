import { Component } from '@angular/core';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';
@Component({
  selector: 'app-nav',
  imports: [RouterModule, RouterLink],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

}
