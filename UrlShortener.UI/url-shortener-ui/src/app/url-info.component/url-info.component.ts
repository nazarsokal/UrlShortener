import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { UrlManagementService, UrlDetailsRecord } from '../dashboard.component/url-management.service';

@Component({
  selector: 'app-url-info',
  standalone: true,
  imports: [CommonModule, RouterModule, DatePipe],
  templateUrl: './url-info.component.html',
  styleUrls: ['./url-info.component.css']
})
export class UrlInfoComponent implements OnInit {
  currentUsername: string = '';
  urlDetails: UrlDetailsRecord | null = null;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private urlManagementService: UrlManagementService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const savedUserData = localStorage.getItem('currentUser');
    if (savedUserData) {
      const parsedUser = JSON.parse(savedUserData);
      this.currentUsername = parsedUser.username || parsedUser.userName || 'Unknown User';
    }

    const urlIdentifier = this.route.snapshot.paramMap.get('id');

    if (urlIdentifier) {
      this.loadUrlDetails(urlIdentifier);
    } else {
      this.errorMessage = 'No URL ID provided.';
      this.cdr.detectChanges();
    }
  }

  loadUrlDetails(urlIdentifier: string): void {
    this.urlManagementService.fetchUrlDetails(urlIdentifier).subscribe({
      next: (details) => {
        this.urlDetails = details;

        this.cdr.detectChanges();
      },
      error: () => {
        this.errorMessage = 'Failed to load URL details. It may not exist.';

        this.cdr.detectChanges();
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}
