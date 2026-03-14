import { HistoryMessage } from './history-message.model';

export interface ChatRequest {
  message: string;
}

export interface ChatRequestDto {
  request: ChatRequest;
  history: HistoryMessage[];
}

export interface ChatResponseDto {
  content: string;
  timestamp: string;
  wordCount: number;
}