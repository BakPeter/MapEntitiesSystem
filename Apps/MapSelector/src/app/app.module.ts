import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app-root/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';

import { MapEntityComponent } from './components/map-entity/map-entity.component';
import { RouterModule, Routes } from '@angular/router';
import { MapRepositoryComponent } from './components/map-repository/map-repository.component';
import { MapViewComponent } from './components/map-view/map-view.component';

const appRoutes: Routes = [
  { path: '', component: MapEntityComponent },
  { path: 'repository', component: MapRepositoryComponent },
];

@NgModule({
  declarations: [AppComponent, MapEntityComponent, MapRepositoryComponent, MapViewComponent],
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
    MatIconModule,
    MatSelectModule,
    RouterModule.forRoot(appRoutes, { enableTracing: false }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
