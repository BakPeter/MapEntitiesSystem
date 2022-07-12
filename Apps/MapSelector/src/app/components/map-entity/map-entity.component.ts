import { Component, OnInit } from '@angular/core';
import { MapEntityService } from '../../services/map-entity.service';
import { MapPoint } from '../../models/MapPoint';

@Component({
  selector: 'app-map-entity',
  templateUrl: './map-entity.component.html',
  styleUrls: ['./map-entity.component.css'],
})
export class MapEntityComponent implements OnInit {
  addedEntityStatus: string = '';
  missionMapName: string = '';
  points: Array<MapPoint> = [];

  constructor(private mapEntityService: MapEntityService) {}

  ngOnInit(): void {
    this.mapEntityService.getMissionMap().subscribe((dto) => {
      if (dto.success) {
        this.missionMapName = dto.mapName;
      }
    });
  }

  addEntity(title: string, lat: string, lon: string) {
    this.mapEntityService
      .addMapEntity(title, Number.parseFloat(lat), Number.parseFloat(lon))
      .subscribe((result) => {
        if (result.success) {
          this.points.push({
            title: title,
            lat: Number.parseFloat(lat),
            lon: Number.parseFloat(lon),
          });
        }
      });
    return false;
  }
}
