import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  parent: Parent = {
    id: 0,
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    imageUrl: ''
  };
  isLoggedin: boolean = false;
  constructor(private authService: AuthenticationService, private router: Router) { }
  ngOnInit(): void {
    this.isLoggedin = this.authService.isAuthenticated();
  }

  logout() {
    this.authService.clearToken();
    this.router.navigate(['/login']);
    this.isLoggedin = this.authService.isAuthenticated();
  }

}
