import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import {MatListModule} from '@angular/material/list';

import { MapEntityComponent } from './map-entity/map-entity.component';
import { RouterModule, Routes } from '@angular/router';
import { MapRepositoryComponent } from './map-repository/map-repository.component';

const appRoutes: Routes = [
  { path: '', component: MapEntityComponent },
  { path: 'repository', component: MapRepositoryComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    MapEntityComponent,
    MapRepositoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatInputModule,
    MatButtonModule,
    MatChipsModule,
    HttpClientModule,
    MatListModule,
    RouterModule.forRoot(appRoutes, { enableTracing: false }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
