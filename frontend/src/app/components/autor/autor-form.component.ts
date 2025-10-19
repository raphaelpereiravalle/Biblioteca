import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AutorService } from '../../services/autor.service';


@Component({
  selector: 'app-autor-form',
  templateUrl: './autor-form.component.html'
})
export class AutorFormComponent implements OnInit {
  id?: number;

  form = this.fb.group({
    nome: ['', [Validators.required, Validators.maxLength(100)]]
  });

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private toast: ToastrService, private service: AutorService) { }

  ngOnInit(): void {

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

  voltar(){ this.router.navigate(['/autor']); }
}
