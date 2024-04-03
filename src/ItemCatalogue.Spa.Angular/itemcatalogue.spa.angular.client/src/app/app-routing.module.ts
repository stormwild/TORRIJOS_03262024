import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemComponent } from '@catalog/item/item.component';
import { CatalogComponent } from '@catalog/catalog/catalog.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/catalog',
    pathMatch: 'full',
  },
  {
    path: 'catalog',
    component: CatalogComponent,
    title: 'Catalogue',
  },
  {
    path: 'item',
    component: ItemComponent,
    title: 'Item Details',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
