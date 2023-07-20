import {Component, OnInit, OnDestroy} from '@angular/core';
import {IAnimal} from '../../../types/Animal';
import {Observable, Subscription, of} from 'rxjs';
import {Router} from '@angular/router';
import {RoutesEnums} from '../../../types/enums/RoutesEnum';
import {AnimalsEditService} from '../services/animals.edit.service';
import {FarmService} from '../../../shared/services/farm.service';
import {DataStateService} from '../services/data.state.service';

@Component({
  selector: 'app-animals-view',
  templateUrl: './animals-view.component.html',
})
export class AnimalsViewComponent implements OnInit, OnDestroy {
  public animals: IAnimal[] = [];

  private subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private farmService: FarmService,
    private animalEditService: AnimalsEditService,
    private dataService: DataStateService
  ) {}

  ngOnInit(): void {
    this.loadData();

    this.subscriptions.push(
      this.dataService.reloadData$.subscribe((needReload) => {
        if (needReload) {
          this.loadData();
        }
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  public add(): void {
    this.router.navigate(['/', RoutesEnums.PAGES, RoutesEnums.ANIMALS, 'add']);
  }

  public edit(index: number): void {
    this.router.navigate([
      '/',
      RoutesEnums.PAGES,
      RoutesEnums.ANIMALS,
      'edit',
      btoa(this.animals[index].name),
    ]);
  }

  public delete(index: number): void {
    this.animalEditService.delete(btoa(this.animals[index].name));
  }

  private processLoadedData(result: IAnimal[]) {
    this.animals = result;
  }

  private loadData(): void {
    this.subscriptions.push(
      this.farmService
        .getAll()
        .subscribe((result) => this.processLoadedData(result.resultItem))
    );
  }
}
