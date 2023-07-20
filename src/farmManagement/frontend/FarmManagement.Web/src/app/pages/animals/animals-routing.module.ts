import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import { AnimalsComponent } from './animals.component';
import { AnimalsEditComponent } from './animals-edit/animals-edit.component';
import { AnimalsViewComponent } from './animals-view/animals-view.component';

const routes: Routes = [
  {
    path: '',
    component: AnimalsComponent,
    children: [
      {
        path: 'view',
        component: AnimalsViewComponent,
      },
      {
        path: 'edit/:id',
        component: AnimalsEditComponent,
      },
      {
        path: 'add',
        component: AnimalsEditComponent,
      },
      {
        path: '**',
        component: AnimalsViewComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AnimalsRoutingModule {}

export const routedComponents = [
  AnimalsComponent,
  AnimalsEditComponent,
  AnimalsViewComponent,
];
