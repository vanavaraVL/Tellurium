import {HttpClient} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {IAppConfig} from '../../types/AppConfig';
import {APP_CONFIG} from '../../../injection-tokens';
import {IAnimal } from '../../types/Animal';
import {IGenericApiResponse} from '../../types/GenericApiResponse';
import {ApiEndpointsEnums} from '../../types/enums/ApiEndpointsEnum';
import {ApiHelper} from './api.helper';

@Injectable({
  providedIn: 'root',
})
export class FarmService {
  constructor(
    private http: HttpClient,
    private apiHelper: ApiHelper,
    @Inject(APP_CONFIG) private appConfig: IAppConfig
  ) {}

  public getByName(name: string): Observable<IGenericApiResponse<IAnimal>> {
    return this.http
      .get<IGenericApiResponse<IAnimal>>(
        `${this.appConfig.apiUrl}${ApiEndpointsEnums.FARMER}/${name}`
      )
      .pipe(catchError((error) => this.apiHelper.handleError(error)));
  }

  public getAll(): Observable<IGenericApiResponse<IAnimal[]>> {
    return this.http
      .get<IGenericApiResponse<IAnimal[]>>(
        `${this.appConfig.apiUrl}${ApiEndpointsEnums.FARMER}`
      )
      .pipe(catchError((error) => this.apiHelper.handleError(error)));
  }

  public create(
    payload: IAnimal
  ): Observable<IGenericApiResponse<IAnimal>> {
    return this.http
      .post<IGenericApiResponse<IAnimal>>(
        `${this.appConfig.apiUrl}${ApiEndpointsEnums.FARMER}/`,
        payload
      )
      .pipe(catchError((error) => this.apiHelper.handleError(error)));
  }

  public update(
    payload: IAnimal,
    name: string
  ): Observable<IGenericApiResponse<IAnimal>> {
    return this.http
      .put<IGenericApiResponse<IAnimal>>(
        `${this.appConfig.apiUrl}${ApiEndpointsEnums.FARMER}/${name}`,
        payload
      )
      .pipe(catchError((error) => this.apiHelper.handleError(error)));
  }

  public delete(name: string): Observable<IGenericApiResponse<boolean>> {
    return this.http
      .delete<IGenericApiResponse<boolean>>(
        `${this.appConfig.apiUrl}${ApiEndpointsEnums.FARMER}/${name}`
      )
      .pipe(catchError((error) => this.apiHelper.handleError(error)));
  }
}
