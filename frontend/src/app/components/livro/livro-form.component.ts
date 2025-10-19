import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LivroService } from '../../services/livro.service';
import { AssuntoService } from '../../services/assunto.service';

@Component({
  selector: 'app-livro-form',
  templateUrl: './livro-form.component.html'
})
export class LivroFormComponent implements OnInit {
  id?: number;
  assuntos:any[] = [];
  form = this.fb.group({
    idAssunto: [null],
    titulo: ['', [Validators.required, Validators.maxLength(200)]],
    editora: [''],
    edicao: [''],
    anoPublicacao: [null]
  });

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private toast: ToastrService, private service: LivroService, private assuntoService: AssuntoService) { }

  ngOnInit(): void {
    this.assuntoService.getAll(1,100).subscribe({ next: res => this.assuntos = res.data.items, error: _ => {} });
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

  voltar(){ this.router.navigate(['/livro']); }
}
