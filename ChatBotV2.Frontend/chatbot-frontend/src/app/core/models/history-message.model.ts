export type HistoryRole = 'System' | 'User' | 'Assistant';

export interface HistoryMessage {
  role: HistoryRole;
  message: string;
  timestamp: string;
}