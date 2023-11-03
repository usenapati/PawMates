import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ApiService } from 'src/app/services/api.service';

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
    imageUrl: '',
    description: '',
    city: '',
    state: '',
    postalCode: ''
  };
  isLoggedin: boolean = false;
  constructor(private authService: AuthenticationService, private router: Router, private apiService : ApiService) { }
  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event.constructor.name === "NavigationEnd") {
        this.isLoggedin = this.authService.isAuthenticated();
        if(this.isLoggedin){
          var parentId = this.authService.getDecodedToken().PetParentId;
          this.apiService.getParentById(+parentId)
          .subscribe({
            next: (response) => {
              this.parent = response;
            }
          });
        }
      }
    })
  }

  logout() {
    this.authService.clearToken();
    this.router.navigate(['/login']);
    this.isLoggedin = this.authService.isAuthenticated();
  }

  handleParentImageError(){
    this.parent.imageUrl = "../../assets/for-parent-without-image.png"
  }
}
