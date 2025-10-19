import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'
import { ApiResponse } from '../models/api-response.model'
import { PagedResult } from '../models/paged-result.model'
import { Assunto } from '../models/assunto.model'

@Injectable({ providedIn: 'root' })
export class AssuntoService {
  private apiUrl = `${environment.apiUrl}/assunto`

  constructor(private http: HttpClient) {}

  getAll(pageNumber = 1, pageSize = 10, search = ''): Observable<ApiResponse<PagedResult<Assunto>>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize)
      .set('search', search || '')
    return this.http.get<ApiResponse<PagedResult<Assunto>>>(`${this.apiUrl}/listar-assuntos`, { params })
  }

  getById(id: number): Observable<ApiResponse<Assunto>> {
    return this.http.get<ApiResponse<Assunto>>(`${this.apiUrl}/obter-assunto/${id}`)
  }

  create(payload: Partial<Assunto>): Observable<ApiResponse<Assunto>> {
    return this.http.post<ApiResponse<Assunto>>(`${this.apiUrl}/manter-assunto`, payload)
  }

  update(id: number, payload: Partial<Assunto>): Observable<ApiResponse<Assunto>> {
    return this.http.put<ApiResponse<Assunto>>(`${this.apiUrl}/editar-assunto/${id}`, payload)
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/deletar-assunto/${id}`)
  }
}
