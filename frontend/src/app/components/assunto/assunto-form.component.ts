import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AssuntoService } from '../../services/assunto.service';


@Component({
  selector: 'app-assunto-form',
  templateUrl: './assunto-form.component.html'
})
export class AssuntoFormComponent implements OnInit {
  id?: number;

  form = this.fb.group({
    descricao: ['', [Validators.required, Validators.maxLength(150)]]
  });

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private toast: ToastrService, private service: AssuntoService) { }

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

  voltar(){ this.router.navigate(['/assunto']); }
}
