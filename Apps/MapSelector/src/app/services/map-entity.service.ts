import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MapEntity } from '../MapEntity';
import { MapDto } from '../MapDto';
import { MissionMapDto } from '../MissionMapDto';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class MapEntityService {
  private mapsEntityApiUrl: string = 'http://localhost:55555/maps';
  private missionMapApiUrl: string = 'http://localhost:55555/maps/missionmap';

  constructor(private http: HttpClient) {}

  getMapEntities(): Observable<MapEntity> {
    return this.http.get<MapEntity>(this.mapsEntityApiUrl);
  }

  getMapBase64(mapName: string): Observable<MapDto> {
    const mapDtoUrl: string = `${this.mapsEntityApiUrl}/${mapName}`;
    return this.http.get<MapDto>(mapDtoUrl);
  }

  setMissionMap(mapName: string): Observable<MissionMapDto> {
    const formData: FormData = new FormData();
    formData.append('mapName', mapName);
    return this.http.post<MissionMapDto>(this.missionMapApiUrl, formData);
  }
}
