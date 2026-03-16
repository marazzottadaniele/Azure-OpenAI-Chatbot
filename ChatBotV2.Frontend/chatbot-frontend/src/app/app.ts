import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { SettingsService } from './core/services/settings';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private readonly settingsService = inject(SettingsService);

  protected readonly apiStatus = signal('Connecting');

  ngOnInit(): void {
    this.checkStatusOfApi();
  }

  private checkStatusOfApi(): void {

    this.settingsService.getSettings().subscribe({
      next: () => {
        this.apiStatus.set('Connected');
      },
      error: (err) => {
        this.apiStatus.set('Disconnected');
        console.error(err);
      }
    });
  }}