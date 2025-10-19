import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LivroValorService } from '../../services/livro-valor.service';
import { LivroService } from '../../services/livro.service';

@Component({
  selector: 'app-livro-valor-form',
  templateUrl: './livro-valor-form.component.html'
})
export class LivroValorFormComponent implements OnInit {
  id?: number;
  livros:any[] = [];
  form = this.fb.group({
    idLivro: [null, [Validators.required, Validators.min(1)]],
    tipoVenda: ['', [Validators.required]],
    valor: [null, [Validators.required, Validators.min(0.01)]]
  });

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private toast: ToastrService, private service: LivroValorService, private livroService: LivroService) { }

  ngOnInit(): void {
    this.livroService.getAll(1,100).subscribe({ next: res => this.livros = res.data.items, error: _ => {} });
    this.id = Number(this.route.snapshot.paramMap.get('id')) || undefined;
    if (this.id) {
      this.service.getById(this.id).subscribe({
        next: res => this.form.patchValue(res.data),
        error: _ => this.toast.error('Falha ao carregar registro')
      });
    }
  }

  salvar() {
    const payload = this.form.value;
    if (this.id) {
      this.service.update(this.id, payload).subscribe({
        next: _ => { this.toast.success('Registro atualizado!'); this.voltar(); },
        error: _ => this.toast.error('Falha ao atualizar')
      });
    } else {
      this.service.create(payload).subscribe({
        next: _ => { this.toast.success('Registro criado!'); this.voltar(); },
        error: _ => this.toast.error('Falha ao criar')
      });
    }
  }

  voltar(){ this.router.navigate(['/livro-valor']); }
}
