import { Component, OnInit } from '@angular/core';
import { MapEntityService } from '../../services/map-entity.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-map-repository',
  templateUrl: './map-repository.component.html',
  styleUrls: ['./map-repository.component.css'],
})
export class MapRepositoryComponent implements OnInit {
  maps: string[] = [];
  imagePath: SafeResourceUrl = '';
  selectedMap: string = '';
  missionMapName: string = '';

  constructor(
    private mapEntityService: MapEntityService,
    private _sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.mapEntityService.getMapEntities().subscribe((ent) => {
      this.maps = ent.mapsNames;
    });

    this.mapEntityService.getMissionMap().subscribe((dto) => {
      if (dto.success) {
        this.setMapName(dto.mapName);
        this.setMissionMapName(dto.mapName);
        this.setMapImage(dto.mapBase64);
      }
    });
  }

  setMapName(mapName: string) {
    this.selectedMap = mapName;
  }

  setMissionMapName(missionMapName: string) {
    this.missionMapName = missionMapName;
  }

  setMapImage(imageBase64: string) {
    this.imagePath = this._sanitizer.bypassSecurityTrustResourceUrl(
      'data:image;base64,' + imageBase64
    );
  }

  onMapSelected(mapName: string) {
    this.selectedMap = mapName;
    this.mapEntityService.getMapBase64(mapName).subscribe((dto) => {
      this.setMapName(mapName);
      this.setMapImage(dto.mapBase64);
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
