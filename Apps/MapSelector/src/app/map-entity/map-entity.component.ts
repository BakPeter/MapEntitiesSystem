import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map-entity',
  templateUrl: './map-entity.component.html',
  styleUrls: ['./map-entity.component.css'],
})
export class MapEntityComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  addEntity() {
    console.log('entity added');
    return false;
  }
}
