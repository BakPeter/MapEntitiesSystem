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
  imageIsVisible: boolean = false;
  selectedMap: string = '';
  missionMapUpdateStatus: string = '';

  constructor(
    private mapEntityService: MapEntityService,
    private _sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.mapEntityService.getMapEntities().subscribe((ent) => {
      this.maps = ent.mapsNames;
    });
  }

  onMapSelected(mapName: string) {
    this.selectedMap = mapName;
    this.mapEntityService.getMapBase64(mapName).subscribe((mapDto) => {
      this.imagePath = this._sanitizer.bypassSecurityTrustResourceUrl(
        'data:image;base64,' + mapDto.mapBase64
      );
      this.imageIsVisible = true;
    });
  }

  setMissionMap(mapName: string) {
    this.mapEntityService.setMissionMap(mapName).subscribe((result) => {
      this.missionMapUpdateStatus = result.success
        ? `Mission map updated to ${mapName}`
        : result.errorMessage;
    });
  }
}
