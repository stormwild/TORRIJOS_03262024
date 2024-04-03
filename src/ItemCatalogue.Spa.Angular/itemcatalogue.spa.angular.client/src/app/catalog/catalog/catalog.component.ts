import { Component, OnInit } from '@angular/core';
import {
  CatalogueClient,
  CatalogueItems,
} from '@catalogclient/itemcatalogue.api.client';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.css',
})
export class CatalogComponent implements OnInit {
  public catalogue: CatalogueItems | undefined;

  constructor(private client: CatalogueClient) {}

  ngOnInit() {
    this.getCatalogue();
  }

  getCatalogue() {
    this.client
      .getCatalogueItemsList('88bd0000-f588-04d9-3c54-08dc4e40d836')
      .subscribe((catalogue) => {
        console.log('catalogue', catalogue);
        this.catalogue = catalogue;
      });
  }
}
