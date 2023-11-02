import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{
  username: string = '';
  password: string = '';

  constructor(private authService: AuthenticationService, private apiService: ApiService, private router: Router) { }
  ngOnInit(): void {
    // if user is already logged in, navigate to home page
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    }
  }

  login() {
    this.apiService.login(this.username, this.password).subscribe(response => {
      this.authService.setToken(response.token);
      // navigate to home page
      this.router.navigate(['/']);
      location.reload();

    }, error => {
      console.error('Login failed');
      this.router.navigate(['/login']);
      // Login Failed should leave a message
    });
  }

  loginDemoUser(){
    this.apiService.login("jchen2", "pb60}mRl%kJgz0J").subscribe(response => {
      this.authService.setToken(response.token);
      // navigate to home page
      this.router.navigate(['/']);


    }, error => {
      console.error('Login failed');
      this.router.navigate(['/login']);
    });
  }
}
