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
    title: 'Catalogue App',
  },
  {
    path: 'item/:id',
    component: ItemComponent,
    title: 'Item Details - Catalogue App',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
