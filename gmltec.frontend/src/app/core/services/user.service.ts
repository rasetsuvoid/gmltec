import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {PersonDto} from '../models/person-dto';
import {HttpResponse} from '../models/http-response';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {PaginationFilterRequest} from '../models/pagination-filter-request';

@Injectable({
  providedIn: 'root'
})
export class UserService{

  private baseUrl = environment.apiUrl;

  constructor(protected http: HttpClient) {}

  getPersons(filter: PaginationFilterRequest): Observable<HttpResponse<PersonDto[]>> {
    return this.http.post<HttpResponse<PersonDto[]>>(`${this.baseUrl}/Person/GetPerson`, filter);
  }

  getPersonById(id: number): Observable<HttpResponse<PersonDto>> {
    return this.http.get<HttpResponse<PersonDto>>(`${this.baseUrl}/Person/GetPersonById/${id}`);
  }

  deletePerson(id: number): Observable<HttpResponse<boolean>> {
    return this.http.delete<HttpResponse<boolean>>(`${this.baseUrl}/Person/DeletePerson/${id}`);
  }

  createPerson(person: PersonDto): Observable<HttpResponse<PersonDto>> {
    return this.http.post<HttpResponse<PersonDto>>(`${this.baseUrl}/Person/CreatePerson`, person);
  }

  updatePerson(id: number, person: PersonDto): Observable<HttpResponse<PersonDto>> {
    return this.http.put<HttpResponse<PersonDto>>(`${this.baseUrl}/Person/UpdatePerson/${id}`, person);
  }

}
