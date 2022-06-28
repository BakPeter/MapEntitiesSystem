import { Component } from '@angular/core';
import { menuItems } from '../../models/menu-items';
import { MenuItem } from '../../models/MenuItem';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  navLinks: MenuItem[] = menuItems;
}
