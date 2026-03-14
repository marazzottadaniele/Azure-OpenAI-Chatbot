import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ChatRequestDto, ChatResponseDto } from '../models/chat.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/api/chat`;

  sendMessage(dto: ChatRequestDto): Observable<ChatResponseDto> {
    return this.http.post<ChatResponseDto>(this.apiUrl, dto);
  }
}