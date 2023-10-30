import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  isLoggedin: boolean = false;
  constructor(private authService: AuthenticationService, private router: Router) { }
  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event.constructor.name === "NavigationEnd") {
        this.isLoggedin = this.authService.isAuthenticated();
      }
    })
    
  }

  logout() {
    this.authService.clearToken();
    this.router.navigate(['/login']);
    this.isLoggedin = this.authService.isAuthenticated();
  }

}
