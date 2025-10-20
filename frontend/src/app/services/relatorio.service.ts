import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'

@Injectable({ providedIn: 'root' })
export class RelatorioService {
  private apiUrl = `${environment.apiUrl}/relatorio`

  constructor(private http: HttpClient) {}

  getRelatorioLivros(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/relatorio-livros`, { responseType: 'blob' })
  }

  getRelatorioAutores(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/relatorio-autores`, { responseType: 'blob' })
  }

  getRelatorioLivroValores() {
    return this.http.get(`${this.apiUrl}/relatorio-livro-valores`, { responseType: 'blob' })
  }
}
