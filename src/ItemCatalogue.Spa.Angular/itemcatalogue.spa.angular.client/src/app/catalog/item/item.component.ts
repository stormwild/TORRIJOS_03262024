import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  CatalogueClient,
  CatalogueItem,
} from '@catalogclient/itemcatalogue.api.client';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrl: './item.component.css',
})
export class ItemComponent implements OnInit {
  id: string = '';
  paramsSub: any;
  item: CatalogueItem | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private client: CatalogueClient
  ) {}

  ngOnInit(): void {
    this.paramsSub = this.activatedRoute.params.subscribe(
      (params) => (this.id = params['id'])
    );
    this.getItem(this.id);
  }

  getItem(id: string) {
    this.client
      .getItemById('88bd0000-f588-04d9-3c54-08dc4e40d836', id)
      .subscribe((item) => {
        console.log('item', item);
        this.item = item;
      });
  }

  ngOnDestroy() {
    this.paramsSub.unsubscribe();
  }
}
