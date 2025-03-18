import { Component } from '@angular/core';
import { NavComponent } from '../nav/nav.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [NavComponent,RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
