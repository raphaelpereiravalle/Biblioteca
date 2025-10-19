import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment'
import { ApiResponse } from '../models/api-response.model'
import { PagedResult } from '../models/paged-result.model'
import { Autor } from '../models/autor.model'

@Injectable({ providedIn: 'root' })
export class AutorService {
  private apiUrl = `${environment.apiUrl}/autor`

  constructor(private http: HttpClient) {}

  getAll(pageNumber = 1, pageSize = 10, search = ''): Observable<ApiResponse<PagedResult<Autor>>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize)
      .set('search', search || '')
    return this.http.get<ApiResponse<PagedResult<Autor>>>(`${this.apiUrl}/listar-autores`, { params })
  }

  getById(id: number): Observable<ApiResponse<Autor>> {
    return this.http.get<ApiResponse<Autor>>(`${this.apiUrl}/obter-autor/${id}`)
  }

  create(payload: Partial<Autor>): Observable<ApiResponse<Autor>> {
    return this.http.post<ApiResponse<Autor>>(`${this.apiUrl}/manter-autor`, payload)
  }

  update(id: number, payload: Partial<Autor>): Observable<ApiResponse<Autor>> {
    return this.http.put<ApiResponse<Autor>>(`${this.apiUrl}/editar-autor/${id}`, payload)
  }

  delete(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}/deletar-autor/${id}`)
  }
}
