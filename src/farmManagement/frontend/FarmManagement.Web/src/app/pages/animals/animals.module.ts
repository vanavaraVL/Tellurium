import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {
  AnimalsRoutingModule,
  routedComponents,
} from './animals-routing.module';
import { AnimalsComponent } from './animals.component';
import {SharedModule} from '../../shared/shared.module';

@NgModule({
  declarations: [...routedComponents],
  imports: [CommonModule, AnimalsRoutingModule, SharedModule],
  exports: [AnimalsComponent, SharedModule],
})
export class AnimalsModule {}
