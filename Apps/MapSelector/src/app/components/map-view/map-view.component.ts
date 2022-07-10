import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { MapEntityService } from '../../services/map-entity.service';

@Component({
  selector: 'app-map-view',
  templateUrl: './map-view.component.html',
  styleUrls: ['./map-view.component.css'],
})
export class MapViewComponent implements OnInit {
  @Input() mapName: string = '';
  imagePath: SafeResourceUrl = '';

  constructor(
    private mapEntityService: MapEntityService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.mapEntityService.getMapBase64(this.mapName).subscribe((dto) => {
      if (dto.success) {
        this.imagePath = this.sanitizer.bypassSecurityTrustResourceUrl(
          'data:image;base64,' + dto.mapBase64
        );
      }
    });
  }
}
