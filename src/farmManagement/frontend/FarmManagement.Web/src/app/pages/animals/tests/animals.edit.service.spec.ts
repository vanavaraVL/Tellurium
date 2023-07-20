import {TestBed} from '@angular/core/testing';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Observable, of} from 'rxjs';
import {AnimalsEditService} from '../services/animals.edit.service';
import {ConfigureTestBed as configureTestBed} from './testBedConfiguration';
import {IAnimal} from '../../../types/Animal';
import {AnimalMock} from './mock/animals.mock';

describe('AnimalsEditService tests', () => {
  let service: AnimalsEditService;
  let mockData: IAnimal;

  let form: FormGroup;
  let spy: any,
    spyUpd: any,
    spyRoute: any,
    spyRouteView: any,
    spyCreate: any,
    spyDelete: any;

  beforeEach(() => {
    configureTestBed();
    service = TestBed.inject(AnimalsEditService);
    mockData = AnimalMock;

    form = service.buildEditForm();
    spy = spyOn(service, 'preparePayload').and.callThrough();
    spyUpd = spyOn(service.farmService, 'update').and.callThrough();
    spyCreate = spyOn(service.farmService, 'create').and.callThrough();
    spyDelete = spyOn(service.farmService, 'delete').and.callThrough();
    spyRoute = spyOn(service, 'navigateToEdit').and.stub();
    spyRoute = spyOn(service, 'navigateToView').and.stub();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();

    TestBed.resetTestingModule();
  });

  it('buildEditForm() should return a form of correct structure and set it as a state', () => {
    expect(form).not.toBeUndefined();
    expect(form.controls['name'].validator).not.toBeNull();

    TestBed.resetTestingModule();
  });

  it('loadItemDataById() should load AND patch data to the form', () => {
    service.loadItemDataByName('any');

    expect(form.controls['name'].value).toEqual(mockData.name);

    TestBed.resetTestingModule();
  });

  it('saveData() should UPDATE', () => {
    service.loadItemDataByName('any');

    expect(form.valid);

    service.saveData('any');

    expect(spy).toHaveBeenCalled();
    expect(spyUpd).toHaveBeenCalled();

    TestBed.resetTestingModule();
  });

  it('saveData() should CREATE', () => {
    service.loadItemDataByName('any');

    expect(form.valid);

    service.saveData();

    expect(spy).toHaveBeenCalled();
    expect(spyCreate).toHaveBeenCalled();

    TestBed.resetTestingModule();
  });

  it('saveData() should not save form when invalid', () => {
    service.loadItemDataByName('any');

    form.setErrors({incorrect: true});
    expect(form.valid).toBeFalse();

    service.saveData('any');
    expect(spy).not.toHaveBeenCalled();

    TestBed.resetTestingModule();
  });

  it('delete() should DELETE', () => {
    service.delete('any');

    expect(spyDelete).toHaveBeenCalled();

    TestBed.resetTestingModule();
  });
});
