import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map-upload',
  templateUrl: './map-upload.component.html',
  styleUrls: ['./map-upload.component.css'],
})
export class MapUploadComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  onFileSelected(event: Event) {}

  uploadMap() {
    console.log(1111);
  }
}
