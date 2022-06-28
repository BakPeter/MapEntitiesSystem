import { Component, OnInit } from '@angular/core';
import { MapEntityService } from '../services/map-entity.service';

@Component({
  selector: 'app-map-entity',
  templateUrl: './map-entity.component.html',
  styleUrls: ['./map-entity.component.css'],
})
export class MapEntityComponent implements OnInit {
  addedEntityStatus: string = '';

  constructor(private mapEntityService: MapEntityService) {}

  ngOnInit(): void {}

  addEntity(title: string, lat: string, lon: string) {
    this.mapEntityService
      .addMapEntity(title, Number.parseFloat(lat), Number.parseFloat(lon))
      .subscribe((result) => {
        this.addedEntityStatus = result.success
          ? `${title} added at ${lat},${lon}`
          : result.errorMessage;
      });
    return false;
  }
}
