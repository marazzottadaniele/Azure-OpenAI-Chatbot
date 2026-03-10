import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {
  messages: { text: string; isUser: boolean }[] = [];
  userMessage = '';

  constructor(private api: ApiService) {}

  sendMessage() {
    if (!this.userMessage.trim()) return;

    this.messages.push({ text: this.userMessage, isUser: true });
    const msg = this.userMessage;
    this.userMessage = '';

    this.api.chat(msg).subscribe(response => {
      this.messages.push({ text: response.content, isUser: false });
    }, error => {
      this.messages.push({ text: 'Error: ' + error.error.error, isUser: false });
    });
  }
}
