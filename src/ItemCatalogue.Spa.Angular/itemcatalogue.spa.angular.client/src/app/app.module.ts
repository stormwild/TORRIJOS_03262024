import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CatalogueClient } from '@catalogclient/itemcatalogue.api.client';
import { CatalogComponent } from './catalog/catalog/catalog.component';
import { ItemComponent } from './catalog/item/item.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

@NgModule({
  declarations: [AppComponent, CatalogComponent, ItemComponent],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule],
  providers: [CatalogueClient, provideAnimationsAsync()], // Add the missing module to the providers array
  bootstrap: [AppComponent],
})
export class AppModule {}
