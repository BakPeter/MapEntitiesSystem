import { Component, OnInit } from '@angular/core';
import { MapEntityService } from '../../services/map-entity.service';

@Component({
  selector: 'app-map-repository',
  templateUrl: './map-repository.component.html',
  styleUrls: ['./map-repository.component.css'],
})
export class MapRepositoryComponent implements OnInit {
  maps: string[] = [];
  selectedMap: string = '';
  missionMapName: string = '';

  constructor(private mapEntityService: MapEntityService) {}

  ngOnInit(): void {
    this.mapEntityService.getMapEntities().subscribe((ent) => {
      this.maps = ent.mapsNames;
    });

    this.mapEntityService.getMissionMap().subscribe((dto) => {
      if (dto.success) {
        this.setMapName(dto.mapName);
        this.setMissionMapName(dto.mapName);
      }
    });
  }

  setMapName(mapName: string) {
    this.selectedMap = mapName;
  }

  setMissionMapName(missionMapName: string) {
    this.missionMapName = missionMapName;
  }

  onMapSelected(mapName: string) {
    this.selectedMap = mapName;
    this.mapEntityService.getMapBase64(mapName).subscribe((dto) => {
      this.setMapName(mapName);
    });
  }

  setMissionMap(missionMapName: string) {
    this.mapEntityService.setMissionMap(missionMapName).subscribe((result) => {
      if (result.success) {
        this.setMissionMapName(missionMapName);
      }
    });
  }

  isMissionMap(missionMap: string): boolean {
    return missionMap != null && missionMap == this.missionMapName;
  }
}
