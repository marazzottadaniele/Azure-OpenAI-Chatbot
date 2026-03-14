import { Component, inject, OnInit, signal } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { SettingsService } from '../../core/services/settings';
import { ChatbotSettings } from '../../core/models/settings.model';

@Component({
  selector: 'app-settings',
  imports: [ReactiveFormsModule],
  templateUrl: './settings.html',
  styleUrl: './settings.scss',
})
export class Settings implements OnInit {
  private readonly settingsService = inject(SettingsService);

  protected readonly isLoading = signal(false);
  protected readonly isSaving = signal(false);
  protected readonly errorMessage = signal<string | null>(null);
  protected readonly successMessage = signal<string | null>(null);

  protected readonly form = new FormGroup({
    voiceTone: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    context: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    maxWords: new FormControl(100, { nonNullable: true, validators: [Validators.required, Validators.min(1), Validators.max(1000)] }),
    systemMessage: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.settingsService.getSettings().subscribe({
      next: (settings) => {
        this.form.patchValue(settings);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.errorMessage.set('Failed to load settings.');
        this.isLoading.set(false);
        console.error(err);
      }
    });
  }

  protected onSubmit(): void {
    if (this.form.invalid) return;

    this.isSaving.set(true);
    this.successMessage.set(null);
    this.errorMessage.set(null);

    const settings = this.form.getRawValue() as ChatbotSettings;

    this.settingsService.updateSettings(settings).subscribe({
      next: () => {
        this.successMessage.set('Settings saved successfully.');
        this.isSaving.set(false);
      },
      error: (err) => {
        this.errorMessage.set('Failed to save settings.');
        this.isSaving.set(false);
        console.error(err);
      }
    });
  }
}