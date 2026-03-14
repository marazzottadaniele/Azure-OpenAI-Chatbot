import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChatbotSettings } from '../models/settings.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/api/settings`;

  getSettings(): Observable<ChatbotSettings> {
    return this.http.get<ChatbotSettings>(this.apiUrl);
  }

  updateSettings(settings: ChatbotSettings): Observable<ChatbotSettings> {
    return this.http.put<ChatbotSettings>(this.apiUrl, settings);
  }
}