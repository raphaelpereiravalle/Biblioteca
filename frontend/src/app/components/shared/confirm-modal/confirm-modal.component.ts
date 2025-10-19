import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ConfirmModalService, ConfirmRequest } from './confirm-modal.service';

declare const bootstrap: any;

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html'
})
export class ConfirmModalComponent implements OnInit, OnDestroy {
  title = 'Confirmar ação';
  message = 'Deseja continuar?';
  detail?: string;
  private sub?: Subscription;
  private modal?: any;
  private resolver?: (v: boolean) => void;

  constructor(private confirmService: ConfirmModalService) {}

  ngOnInit(): void {
    const el = document.getElementById('confirmModal');
    if (el) this.modal = new bootstrap.Modal(el, { backdrop: 'static' });
    this.sub = this.confirmService.requests$.subscribe((req: ConfirmRequest) => {
      this.title = req.title || 'Confirmação';
      this.message = req.message || 'Deseja continuar?';
      this.detail = req.detail;
      this.resolver = req.resolver;
      this.modal?.show();
    });
  }

  confirm() { if (this.resolver) this.resolver(true); this.modal?.hide(); }
  ngOnDestroy(): void { this.sub?.unsubscribe(); }
}
