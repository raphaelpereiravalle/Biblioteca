import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ApiResponseInterceptor } from './interceptors/api-response.interceptor';

import { AutorListComponent } from './components/autor/autor-list.component';
import { AutorFormComponent } from './components/autor/autor-form.component';
import { AssuntoListComponent } from './components/assunto/assunto-list.component';
import { AssuntoFormComponent } from './components/assunto/assunto-form.component';
import { LivroListComponent } from './components/livro/livro-list.component';
import { LivroFormComponent } from './components/livro/livro-form.component';
import { LivroValorListComponent } from './components/livro-valor/livro-valor-list.component';
import { LivroValorFormComponent } from './components/livro-valor/livro-valor-form.component';
import { ConfirmModalComponent } from './components/shared/confirm-modal/confirm-modal.component';
import { RelatoriosComponent } from './components/relatorios/relatorios.component';

@NgModule({
  declarations: [
    AppComponent,
    ConfirmModalComponent,
    AutorListComponent, AutorFormComponent,
    AssuntoListComponent, AssuntoFormComponent,
    LivroListComponent, LivroFormComponent,
    LivroValorListComponent, LivroValorFormComponent,
    RelatoriosComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule, FormsModule,
    BrowserAnimationsModule, ToastrModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ApiResponseInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
