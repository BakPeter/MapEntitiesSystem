import { Component, OnInit } from '@angular/core';
import { MapEntityService } from '../services/map-entity.service';
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

  constructor(
    private mapEntityService: MapEntityService,
    private _sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.mapEntityService.getMapEntities().subscribe((ent) => {
      this.maps = ent.mapsNames;
      console.log(
        `${
          this.maps.length
        } map entities loaded at ${new Date().toLocaleTimeString()}`
      );
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
    console.log(mapName);
  }  
}
