import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'
import { ApiResponse } from '../models/api-response.model'
import { PagedResult } from '../models/paged-result.model'
import { Livro } from '../models/livro.model'

@Injectable({ providedIn: 'root' })
export class LivroService {
  private apiUrl = `${environment.apiUrl}/livro`

  constructor(private http: HttpClient) {}

  getAll(pageNumber = 1, pageSize = 10, search = ''): Observable<ApiResponse<PagedResult<Livro>>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize)
      .set('search', search || '')
    return this.http.get<ApiResponse<PagedResult<Livro>>>(`${this.apiUrl}/listar-livros`, { params })
  }

  getById(id: number): Observable<ApiResponse<Livro>> {
    return this.http.get<ApiResponse<Livro>>(`${this.apiUrl}/obter-livro/${id}`)
  }

  create(payload: Partial<Livro>): Observable<ApiResponse<Livro>> {
    return this.http.post<ApiResponse<Livro>>(`${this.apiUrl}/manter-livro`, payload)
  }

  update(id: number, payload: Partial<Livro>): Observable<ApiResponse<Livro>> {
    return this.http.put<ApiResponse<Livro>>(`${this.apiUrl}/editar-livro/${id}`, payload)
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/deletar-livro/${id}`)
  }
}
