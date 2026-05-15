import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { UrlManagementService, ShortenedUrlRecord } from './url-management.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule, DatePipe, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  currentUsername: string = '';
  currentUserIdentifier: string = '';

  shortenedUrlRecords: ShortenedUrlRecord[] = [];
  isDisplayingAllRecords: boolean = true;

  isShortenModalOpen: boolean = false;
  newOriginalUrl: string = '';
  shortenErrorMessage: string = '';

  constructor(
    private urlManagementService: UrlManagementService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const savedUserData = localStorage.getItem('currentUser');

    if (savedUserData) {
      const parsedUser = JSON.parse(savedUserData);

      this.currentUsername = parsedUser.username || parsedUser.userName || 'Unknown User';
      this.currentUserIdentifier = parsedUser.id || parsedUser.userId || '';

      this.loadAllUrlRecords();
    } else {
      this.router.navigate(['/login']);
    }
  }

  loadAllUrlRecords(): void {
    this.isDisplayingAllRecords = true;
    this.urlManagementService.fetchAllShortenedUrlRecords().subscribe({
      next: (records) => {
        this.shortenedUrlRecords = records;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  loadUserSpecificUrlRecords(): void {
    this.isDisplayingAllRecords = false;
    this.urlManagementService.fetchShortenedUrlRecordsByUserIdentifier(this.currentUserIdentifier).subscribe({
      next: (records) => {
        this.shortenedUrlRecords = records;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  viewRecordInformation(recordIdentifier: string): void {
    console.log(recordIdentifier);
  }

  openShortenModal(): void {
    this.isShortenModalOpen = true;
    this.newOriginalUrl = '';
    this.shortenErrorMessage = '';
  }

  closeShortenModal(): void {
    this.isShortenModalOpen = false;
  }

  submitShortenUrl(): void {
    this.shortenErrorMessage = '';

    if (!this.newOriginalUrl) {
      this.shortenErrorMessage = 'Please enter a URL.';
      return;
    }

    this.urlManagementService.shortenUrl(this.newOriginalUrl, '', this.currentUserIdentifier).subscribe({
      next: () => {
        this.closeShortenModal();
        if (this.isDisplayingAllRecords) {
          this.loadAllUrlRecords();
        } else {
          this.loadUserSpecificUrlRecords();
        }
      },
      error: (httpResponse: HttpErrorResponse) => {
        if (httpResponse.status === 409) {
          this.shortenErrorMessage = 'This URL has already been shortened.';
        } else {
          this.shortenErrorMessage = 'An unexpected error occurred.';
        }
        this.cdr.detectChanges();
      }
    });
  }
}
