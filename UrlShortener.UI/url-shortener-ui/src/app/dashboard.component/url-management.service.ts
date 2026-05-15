import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ShortenedUrlRecord {
  id: string;
  urlOriginal: string;
  urlShortened: string;
  dateCreated: string;
}

@Injectable({
  providedIn: 'root'
})
export class UrlManagementService {
  private baseApiEndpoint = 'http://localhost:5017/api/Urls';

  constructor(private httpClient: HttpClient) { }

  fetchAllShortenedUrlRecords(): Observable<ShortenedUrlRecord[]> {
    return this.httpClient.get<ShortenedUrlRecord[]>(this.baseApiEndpoint);
  }

  fetchShortenedUrlRecordsByUserIdentifier(userIdentifier: string): Observable<ShortenedUrlRecord[]> {
    return this.httpClient.get<ShortenedUrlRecord[]>(`${this.baseApiEndpoint}/${userIdentifier}`);
  }
}
