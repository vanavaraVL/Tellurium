import {TestBed} from '@angular/core/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {FormBuilder} from '@angular/forms';
import {RouterTestingModule} from '@angular/router/testing';
import {Observable, of} from 'rxjs';
import {IGenericApiResponse} from '../../../types/GenericApiResponse';
import {IAnimal} from '../../../types/Animal';
import {AnimalMock} from './mock/animals.mock';
import {FarmService} from '../../../shared/services/farm.service';

class FarmServiceMock {
  private response: IGenericApiResponse<IAnimal> = {
    resultItem: AnimalMock,
    error: 'no error',
  };

  private responseDelete: IGenericApiResponse<boolean> = {
    resultItem: true,
    error: 'no error',
  };

  public getByName(name: string): Observable<IGenericApiResponse<IAnimal>> {
    return of(this.response);
  }
  public update(
    payload: object,
    name: string
  ): Observable<IGenericApiResponse<IAnimal>> {
    return of(this.response);
  }
  public create(payload: object): Observable<IGenericApiResponse<IAnimal>> {
    return of(this.response);
  }

  public delete(name: string): Observable<IGenericApiResponse<boolean>> {
    return of(this.responseDelete);
  }
}

export const ConfigureTestBed = () => {
  TestBed.configureTestingModule({
    imports: [HttpClientTestingModule, RouterTestingModule],
    providers: [
      FormBuilder,
      { provide: FarmService, useClass: FarmServiceMock},
    ],
  });
};
