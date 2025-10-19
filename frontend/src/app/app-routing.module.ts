import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { AutorListComponent } from './components/autor/autor-list.component'
import { AutorFormComponent } from './components/autor/autor-form.component'
import { AssuntoListComponent } from './components/assunto/assunto-list.component'
import { AssuntoFormComponent } from './components/assunto/assunto-form.component'
import { LivroListComponent } from './components/livro/livro-list.component'
import { LivroFormComponent } from './components/livro/livro-form.component'
import { LivroValorListComponent } from './components/livro-valor/livro-valor-list.component'
import { LivroValorFormComponent } from './components/livro-valor/livro-valor-form.component'
import { RelatoriosComponent } from './components/relatorios/relatorios.component'

const routes: Routes = [
  { path: '', redirectTo: 'autor', pathMatch: 'full' },
  { path: 'autor', component: AutorListComponent },
  { path: 'autor/novo', component: AutorFormComponent },
  { path: 'autor/:id', component: AutorFormComponent },
  { path: 'assunto', component: AssuntoListComponent },
  { path: 'assunto/novo', component: AssuntoFormComponent },
  { path: 'assunto/:id', component: AssuntoFormComponent },
  { path: 'livro', component: LivroListComponent },
  { path: 'livro/novo', component: LivroFormComponent },
  { path: 'livro/:id', component: LivroFormComponent },
  { path: 'livro-valor', component: LivroValorListComponent },
  { path: 'livro-valor/novo', component: LivroValorFormComponent },
  { path: 'livro-valor/:id', component: LivroValorFormComponent },
  { path: 'relatorios', component: RelatoriosComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
