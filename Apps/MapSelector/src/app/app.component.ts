import { Component } from '@angular/core';
import { menuItems } from './menu-items';
import { MenuItem } from './MenuItem';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  navLinks: MenuItem[] = menuItems;
}
