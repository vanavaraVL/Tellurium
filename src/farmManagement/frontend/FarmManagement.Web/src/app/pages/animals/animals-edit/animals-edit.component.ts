import {Component, OnInit} from '@angular/core';
import {FormGroup, AbstractControl} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import { AnimalsEditService } from '../services/animals.edit.service';

@Component({
  selector: 'app-animals-edit',
  templateUrl: './animals-edit.component.html',
})
export class AnimalsEditComponent implements OnInit {
  public editForm: FormGroup;
  public editState: string;

  public sex: number[] = [0, 1];
  private itemNameBase64: string | undefined = undefined;

  constructor(
    private route: ActivatedRoute,
    private animalsEditService: AnimalsEditService
  ) {
    this.editForm = this.animalsEditService.buildEditForm();
  }

  ngOnInit(): void {
    this.itemNameBase64 = this.route.snapshot.params['id'] || undefined;
    this.editState = this.itemNameBase64 ? 'Edit' : 'Add new';

    if (this.itemNameBase64) {
      this.animalsEditService.loadItemDataByName(this.itemNameBase64);
    }
  }

  public saveData(): void {
    this.animalsEditService.saveData(this.itemNameBase64);
  }

  public validate(controlName: string): string {
    if (!this.editForm.get(controlName)?.touched) {
      return '';
    }
    if (this.editForm.hasError('required', [controlName])) {
      return `<b>The field ${controlName}</b> is required`;
    }

    return '';
  }
}
