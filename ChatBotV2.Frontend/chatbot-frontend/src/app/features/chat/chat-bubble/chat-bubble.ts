import { Component, input } from '@angular/core';
import { HistoryMessage } from '../../../core/models/history-message.model';

@Component({
  selector: 'app-chat-bubble',
  imports: [],
  templateUrl: './chat-bubble.html',
  styleUrl: './chat-bubble.scss',
})
export class ChatBubble {
  readonly message = input.required<HistoryMessage>();
}
