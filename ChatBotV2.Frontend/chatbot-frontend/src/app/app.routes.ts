import { Routes } from '@angular/router';
import { Settings } from './features/settings/settings';
import { Chat } from './features/chat/chat';

export const routes: Routes = [
  { path: 'settings', component: Settings },
  { path: 'chat', component: Chat },
  { path: '', redirectTo: 'settings', pathMatch: 'full' },
  { path: '**', redirectTo: 'settings' }
];