import {Injectable, OnDestroy} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {Observable, Subscription, of} from 'rxjs';
import {FarmService} from '../../../shared/services/farm.service';
import {IAnimal} from '../../../types/Animal';
import {IGenericApiResponse} from '../../../types/GenericApiResponse';
import { RoutesEnums } from '../../../types/enums/RoutesEnum';
import { DataStateService } from './data.state.service';

@Injectable({
  providedIn: 'root',
})
export class AnimalsEditService implements OnDestroy {
  private editForm: FormGroup;

  private subscriptions: Subscription[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    public farmService: FarmService,
    private dataService: DataStateService
  ) {}

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  public loadItemDataByName(nameBase64: string): void {
    this.subscriptions.push(
      this.farmService
        .getByName(nameBase64)
        .subscribe((result) => this.processLoadedData(result.resultItem))
    );
  }

  public saveData(nameBase64?: string): void {
    (Object as any)
      .values(this.editForm.controls)
      .forEach((control: {markAllAsTouched: () => any}) =>
        control.markAllAsTouched()
      );
    this.editForm.markAsTouched();

    if (this.editForm.valid) {
      const payload: IAnimal = this.preparePayload();
      this.saveOrUpdate(payload, nameBase64);
    }
  }

  public buildEditForm(): FormGroup {
    this.editForm = this.formBuilder.group({
      name: ['', [Validators.required]],
    });

    return this.editForm;
  }

  public preparePayload(): IAnimal {
    const payload: IAnimal = {
      id: this.editForm.value.id,
      name: this.editForm.value.name,
    };

    return payload;
  }

  public delete(nameBase64: string): void {
    this.subscriptions.push(
      this.farmService
        .delete(nameBase64)
        .subscribe((result) => this.processDeleteData(result))
    );
  }

  private processLoadedData(result: IAnimal): void {
    this.editForm.patchValue({
      ...result,
    });
  }

  private saveOrUpdate(payload: IAnimal, nameBase64?: string): void {
    if (nameBase64 == undefined) {
      this.subscriptions.push(
        this.farmService
          .create(payload)
          .subscribe((result) => this.processSaveData(result))
      );
    } else {
      this.subscriptions.push(
        this.farmService
          .update(payload, nameBase64)
          .subscribe((result) => this.processSaveData(result))
      );
    }
  }

  private processSaveData(result: IGenericApiResponse<IAnimal>): void {
    if (result && result.error !== '') {
      this.navigateToEdit(result.resultItem.name);
    }
  }

  private processDeleteData(result: IGenericApiResponse<boolean>): void {
    if (result && result.error !== '') {
      this.navigateToView();
    }
  }

  public navigateToEdit(name: string): void {
    this.router.navigate([
      '/',
      RoutesEnums.PAGES,
      RoutesEnums.ANIMALS,
      'edit',
      btoa(name),
    ]);
  }

  public navigateToView(): void {
    this.router.navigate([
      '/',
      RoutesEnums.PAGES,
      RoutesEnums.ANIMALS,
      'view',
    ]);

    this.dataService.reload();
  }
}
