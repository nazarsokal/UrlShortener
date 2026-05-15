import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-authentication',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent {
  isLoginModeActive = true;
  userCredentials = {
    username: '',
    password: ''
  };

  validationErrorMessages: string[] = [];

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  toggleAuthenticationMode(): void {
    this.isLoginModeActive = !this.isLoginModeActive;
    this.validationErrorMessages = [];
    this.cdr.detectChanges();
  }

  onSubmitCredentials(): void {
    this.validationErrorMessages = [];

    if (this.isLoginModeActive) {
      this.authenticationService.loginUser(this.userCredentials).subscribe({
        next: (response) => {
          this.router.navigate(['/dashboard']);
        },
        error: (httpErrorResponse: HttpErrorResponse) => {
          this.extractAndSetErrorMessages(httpErrorResponse);
        }
      });
    } else {
      this.authenticationService.registerUser(this.userCredentials).subscribe({
        next: (response) => {
          this.isLoginModeActive = true;
        },
        error: (httpErrorResponse: HttpErrorResponse) => {
          this.extractAndSetErrorMessages(httpErrorResponse);
        }
      });
    }
  }

  private extractAndSetErrorMessages(httpErrorResponse: HttpErrorResponse): void {
    const err = httpErrorResponse.error;

    const errorObj = typeof err === 'string' ? JSON.parse(err) : err;

    if (httpErrorResponse.status === 409) {
      this.validationErrorMessages = ['This username is already taken. Please choose a different one or switch to Login.'];
      this.cdr.detectChanges();
      return;
    }

    if (errorObj && errorObj.Errors && Array.isArray(errorObj.Errors)) {
      this.validationErrorMessages = errorObj.Errors.map((e: any) => e.error);
    } else if (errorObj && errorObj.Detail) {
      this.validationErrorMessages = errorObj.Detail.split('\n').filter((msg: string) => msg.trim() !== '');
    } else {
      this.validationErrorMessages = ['An unexpected error occurred.'];
    }

    console.log('MAPPED ARRAY:', this.validationErrorMessages);

    this.cdr.detectChanges();
  }
}
