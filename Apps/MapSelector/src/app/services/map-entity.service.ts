import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MapEntity } from '../models/MapEntity';
import { MapDto } from '../models/MapDto';
import { ResultModel } from '../models/ResultModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class MapEntityService {
  private mapsEntityApiUrl: string = 'http://localhost:55555/maps';
  private missionMapApiUrl: string = 'http://localhost:55555/maps/missionmap';
  private mapEntityApiUrl: string = 'http://localhost:55555/entity';

  constructor(private http: HttpClient) {}

  getMapEntities(): Observable<MapEntity> {
    return this.http.get<MapEntity>(this.mapsEntityApiUrl);
  }

  getMapBase64(mapName: string): Observable<MapDto> {
    const mapDtoUrl: string = `${this.mapsEntityApiUrl}/${mapName}`;
    return this.http.get<MapDto>(mapDtoUrl);
  }

  setMissionMap(mapName: string): Observable<ResultModel> {
    const formData: FormData = new FormData();
    formData.append('mapName', mapName);
    return this.http.post<ResultModel>(this.missionMapApiUrl, formData);
  }

  addMapEntity(
    title: string,
    lat: number,
    lon: number
  ): Observable<ResultModel> {
    const formData: FormData = new FormData();
    formData.append('Title', title);
    formData.append('Lat', lat.toString());
    formData.append('Lon', lon.toString());
    return this.http.post<ResultModel>(this.mapEntityApiUrl, formData);
  }
}
