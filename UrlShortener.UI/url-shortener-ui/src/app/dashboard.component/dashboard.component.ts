import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { UrlManagementService, ShortenedUrlRecord } from './url-management.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule, DatePipe],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  currentUsername: string = '';
  currentUserIdentifier: string = '';

  shortenedUrlRecords: ShortenedUrlRecord[] = [];
  isDisplayingAllRecords: boolean = true;

  constructor(
    private urlManagementService: UrlManagementService,
    private router: Router,
    private cdr: ChangeDetectorRef // 1. Inject the change detector
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

        // 2. Force the HTML to update immediately
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

        // 3. Force the HTML to update immediately
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
}
