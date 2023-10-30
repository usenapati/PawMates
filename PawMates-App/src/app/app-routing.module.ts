import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PetDetailsComponent } from './components/pet/pet-details/pet-details.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PetparentDetailComponent } from './components/petparent/petparent-detail/petparent-detail.component';
import { PetsComponent } from './components/pet/pets/pets.component';
import { PlaydatesComponent } from './components/playdate/playdates/playdates.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent }, // Change it to a separate component
  

  {path: 'register', component: RegisterComponent },
  {path: 'login', component: LoginComponent },
  {path: 'profile', component: PetparentDetailComponent  },

  {path: 'pets', component: PetsComponent, canActivate: [AuthGuard] },
  {path: 'pets/:id', component: PetDetailsComponent},
  {path: 'events', component: PlaydatesComponent, canActivate: [AuthGuard]  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
