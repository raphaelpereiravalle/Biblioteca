import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'
import { ApiResponse } from '../models/api-response.model'
import { PagedResult } from '../models/paged-result.model'
import { LivroValor } from '../models/livro-valor.model'

@Injectable({ providedIn: 'root' })
export class LivroValorService {
  private apiUrl = `${environment.apiUrl}/LivroValor`

  constructor(private http: HttpClient) {}

  getAll(pageNumber = 1, pageSize = 10, search = ''): Observable<ApiResponse<PagedResult<LivroValor>>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize)
      .set('search', search || '')
    return this.http.get<ApiResponse<PagedResult<LivroValor>>>(`${this.apiUrl}/listar-livros-valores`, { params })
  }

  getById(id: number): Observable<ApiResponse<LivroValor>> {
    return this.http.get<ApiResponse<LivroValor>>(`${this.apiUrl}/obter-livro-valor/${id}`)
  }

  create(payload: Partial<LivroValor>): Observable<ApiResponse<LivroValor>> {
    return this.http.post<ApiResponse<LivroValor>>(`${this.apiUrl}/manter-livro-valor`, payload)
  }

  update(id: number, payload: Partial<LivroValor>): Observable<ApiResponse<LivroValor>> {
    return this.http.put<ApiResponse<LivroValor>>(`${this.apiUrl}/editar-livro-valor/${id}`, payload)
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/deletar-livro-valor/${id}`)
  }
}
