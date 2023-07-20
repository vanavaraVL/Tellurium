import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataStateService {
  private reloadData: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  public readonly reloadData$: Observable<boolean> =
    this.reloadData.asObservable();

  public reload(): void {
    return this.reloadData.next(true);
  }
}
