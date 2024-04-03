import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CatalogueClient } from '@catalogclient/itemcatalogue.api.client';
import { CatalogComponent } from './catalog/catalog/catalog.component';
import { ItemComponent } from './catalog/item/item.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
@NgModule({
  declarations: [AppComponent, CatalogComponent, ItemComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatTableModule,
    CommonModule,
    RouterOutlet,
    RouterModule,
    MatCardModule,
    MatSidenavModule,
    MatButtonModule,
  ],
  providers: [CatalogueClient, provideAnimationsAsync()], // Add the missing module to the providers array
  bootstrap: [AppComponent],
})
export class AppModule {}
