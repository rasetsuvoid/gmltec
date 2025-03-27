import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {HttpResponse} from '../models/http-response';
import {DocumentTypeDto} from '../models/document-type-dto';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DocumentTypeService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getDocumentTypes(): Observable<HttpResponse<DocumentTypeDto[]>> {
    return this.http.get<HttpResponse<DocumentTypeDto[]>>(`${this.baseUrl}/DocumentType/GetDocumentType`);
  }

}
