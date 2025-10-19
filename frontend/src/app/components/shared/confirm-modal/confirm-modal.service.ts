import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface ConfirmRequest {
  title?: string;
  message?: string;
  detail?: string;
  resolver: (v: boolean) => void;
}

@Injectable({ providedIn: 'root' })
export class ConfirmModalService {
  private requests = new Subject<ConfirmRequest>();
  public requests$ = this.requests.asObservable();

  confirm(title: string, message: string, detail?: string): Promise<boolean> {
    return new Promise(resolve => {
      this.requests.next({ title, message, detail, resolver: resolve });
    });
  }
}
