import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ShortenedUrlRecord {
  id: string;
  urlOriginal: string;
  urlShortened: string;
  dateCreated: string;
}

export interface UrlDetailsRecord {
  id: string;
  urlOriginal: string;
  dateCreated: string;
  description: string;
  createdByUser: string;
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
    return this.httpClient.get<ShortenedUrlRecord[]>(`${this.baseApiEndpoint}/createdBy/${userIdentifier}`);
  }

  shortenUrl(originalUrl: string, description: string, userId: string): Observable<any> {
    return this.httpClient.post(`${this.baseApiEndpoint}/shorten`, {
      originalUrl: originalUrl,
      description: description,
      userId: userId
    });
  }

  fetchUrlDetails(urlId: string): Observable<UrlDetailsRecord> {
    return this.httpClient.get<UrlDetailsRecord>(`${this.baseApiEndpoint}/${urlId}`);
  }

  deleteShortenedUrlRecord(urlIdentifier: string): Observable<any> {
    return this.httpClient.delete(`${this.baseApiEndpoint}/${urlIdentifier}`);
  }
}
