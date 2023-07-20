import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {NavPagesComponent} from '../nav-pages/nav-pages.component';
import {RoutesEnums} from '../types/enums/RoutesEnum';
import {NotFoundComponent} from '../shared/components/not-found/not-found.component';
import { AnimalsModule } from './animals/animals.module';

const routes: Routes = [
  {
    path: '',
    component: NavPagesComponent,
    children: [
      {
        path: '404',
        component: NotFoundComponent,
      },
      {
        path: RoutesEnums.ABOUT,
        loadChildren: () =>
          import('./about/about.module').then((m) => m.AboutModule),
      },
      {
        path: RoutesEnums.ANIMALS,
        loadChildren: () =>
          import('./animals/animals.module').then((m) => AnimalsModule),
      },
      {
        path: '',
        redirectTo: '/pages/about',
        pathMatch: 'full',
      },
      {
        path: '**',
        component: NotFoundComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
