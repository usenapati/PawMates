import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AppComponent } from './app.component';
import { PetparentDetailComponent } from './components/petparent/petparent-detail/petparent-detail.component';
import { PetsComponent } from './components/pet/pets/pets.component';
import { PlaydatesComponent } from './components/playdate/playdates/playdates.component';

const routes: Routes = [
  {path: '', component: LoginComponent }, // Change it to a separate component
  {path: 'login', component: LoginComponent },
  {path: 'register', component: RegisterComponent },

  {path: 'profile', component: PetparentDetailComponent },

  {path: 'pets', component: PetsComponent },

  {path: 'events', component: PlaydatesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
