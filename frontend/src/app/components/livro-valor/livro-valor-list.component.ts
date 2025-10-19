import { Component, OnInit } from '@angular/core';
import { LivroValorService } from '../../services/livro-valor.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ConfirmModalService } from '../shared/confirm-modal/confirm-modal.service';

@Component({
  selector: 'app-livro-valor-list',
  templateUrl: './livro-valor-list.component.html'
})
export class LivroValorListComponent implements OnInit {
  itens: any[] = [];
  pesquisa = '';
  pageNumber=1; pageSize=10; totalPages=1; totalCount=0;

  constructor(private service: LivroValorService, private toast: ToastrService, private router: Router, private modal: ConfirmModalService) { }

  ngOnInit(): void { this.carregar(); }

  carregar() {
    this.service.getAll(this.pageNumber, this.pageSize, this.pesquisa).subscribe({
      next: res => { this.itens = res.data.items; this.totalPages = res.data.totalPages; this.totalCount = res.data.totalCount; },
      error: _ => this.toast.error('Falha ao carregar lista.')
    });
  }
  limpar() { this.pesquisa=''; this.pageNumber=1; this.carregar(); }
  novo() { this.router.navigate(['/livro-valor','novo']); }
  editar(id:number) { this.router.navigate(['/livro-valor', id]); }

  async confirmarExcluir(id:number) {
    const ok = await this.modal.confirm('Excluir registro?', 'Esta ação não pode ser desfeita.');
    if (!ok) return;
    this.service.delete(id).subscribe({
      next: _ => this.carregar(),
      error: _ => this.toast.error('Falha ao excluir.')
    });
  }
  paginaAnterior(){ if(this.pageNumber>1){ this.pageNumber--; this.carregar(); } }
  proximaPagina(){ if(this.pageNumber<this.totalPages){ this.pageNumber++; this.carregar(); } }
}
