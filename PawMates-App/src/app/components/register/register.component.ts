import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterModel } from 'src/app/model/registerModel';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerModel : RegisterModel;


  constructor(private authService: AuthenticationService, private apiService: ApiService, private router: Router) {
    this.registerModel = {
      userName: '',
      password: '',
    
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      profileImageURL: '',
      description: '',
      city: '',
      state: '',
      postalCode: '',
    };

   }
  ngOnInit(): void {
    // if user is already logged in, navigate to home page
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    }
  }

  register() {
    this.apiService.register(this.registerModel).subscribe(response => {
      this.authService.setToken(response.token);
      // Route to Create Pet (Need to make sure user cannot exit without having a pet)
      this.router.navigate(['/']);
      

    }, error => {
      console.error('Register failed');
      
    });
    
  }
}
