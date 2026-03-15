import { Component, inject, signal, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../../core/services/chat';
import { HistoryMessage } from '../../core/models/history-message.model';
import { ChatResponseDto } from '../../core/models/chat.model';
import { ChatBubble } from './chat-bubble/chat-bubble';

@Component({
  selector: 'app-chat',
  imports: [FormsModule, ChatBubble],
  templateUrl: './chat.html',
  styleUrl: './chat.scss',
})
export class Chat {
  private readonly chatService = inject(ChatService);

  protected readonly messages = signal<HistoryMessage[]>([]);
  protected readonly inputMessage = signal('');
  protected readonly isLoading = signal(false);
  protected readonly errorMessage = signal<string | null>(null);

  protected readonly canSend = computed(() =>
    this.inputMessage().trim().length > 0 && !this.isLoading()
  );

protected sendMessage(): void {
  const text = this.inputMessage().trim();
  if (!text) return;

  const userMessage: HistoryMessage = {
    role: 'User',
    message: text,
    timestamp: new Date().toISOString()
  };

  const currentHistory = this.messages();

  this.messages.update(msgs => [...msgs, userMessage]);
  this.inputMessage.set('');
  this.isLoading.set(true);
  this.errorMessage.set(null);

  this.chatService.sendMessage({
    request: { message: text },
    history: currentHistory
  }).subscribe({
    next: (response: ChatResponseDto) => {
      const assistantMessage: HistoryMessage = {
        role: 'Assistant',
        message: response.content,
        timestamp: response.timestamp
      };
      this.messages.update(msgs => [...msgs, assistantMessage]);
      this.isLoading.set(false);
    },
    error: (err) => {
      this.errorMessage.set('Failed to send message.');
      this.isLoading.set(false);
      console.error(err);
    }
  });
}
}