import { Component } from '@angular/core'
import { RelatorioService } from '../../services/relatorio.service'
import { ToastrService } from 'ngx-toastr'
import { downloadBlob } from '../../services/download.util'

@Component({
  selector: 'app-relatorios',
  templateUrl: './relatorios.component.html',
})
export class RelatoriosComponent {
  loading = false
  constructor(private service: RelatorioService, private toast: ToastrService) {}

  baixarLivros() {
    this.loading = true
    this.service.getRelatorioLivros().subscribe({
      next: (blob) => {
        downloadBlob(blob, 'relatorio-livros.pdf')
        this.toast.success('Relatório de livros gerado.')
        this.loading = false
      },
      error: (_) => {
        this.toast.error('Falha ao gerar relatório de livros.')
        this.loading = false
      },
    })
  }

  baixarRelatorioValores() {
    this.loading = true
    this.service.getRelatorioLivroValores().subscribe({
      next: (blob) => {
        downloadBlob(blob, 'relatorio-livro-valores.pdf')
        this.toast.success('Relatório de valores gerado.')
        this.loading = false
      },
      error: (_) => {
        this.toast.error('Falha ao gerar relatório de valores.')
        this.loading = false
      },
    })
  }

  baixarAutores() {
    this.loading = true
    this.service.getRelatorioAutores().subscribe({
      next: (blob) => {
        downloadBlob(blob, 'relatorio-autores.pdf')
        this.toast.success('Relatório de autores gerado.')
        this.loading = false
      },
      error: (_) => {
        this.toast.error('Falha ao gerar relatório de autores.')
        this.loading = false
      },
    })
  }
}
