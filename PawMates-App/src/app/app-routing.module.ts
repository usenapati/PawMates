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
import { PlaydatesDetailComponent } from './components/playdate/playdates-detail/playdates-detail.component';
import { PlaydatesFormComponent } from './components/playdate/playdates-form/playdates-form.component';
import { PetparentProfileComponent } from './components/petparent/petparent-profile/petparent-profile/petparent-profile.component';
import { PetparentPetDetailComponent } from './components/petparent/petparent-pet-detail/petparent-pet-detail.component';
import { PetFormComponent } from './components/pet/pet-form/pet-form.component';

const routes: Routes = [
  {path: '', component: HomeComponent }, // Change it to a separate component


  {path: 'register', component: RegisterComponent },
  {path: 'login', component: LoginComponent },
  {path: 'profile', component: PetparentDetailComponent, canActivate: [AuthGuard] },
  {path: 'pets/:id/parents/:id', component: PetparentProfileComponent, canActivate: [AuthGuard] },
  {path: 'playdates/host/:id', component: PetparentProfileComponent, canActivate: [AuthGuard] },
  {path: 'profile/pets/add', component: PetFormComponent, canActivate: [AuthGuard]},
  {path: 'profile/pets/:id/edit', component: PetparentPetDetailComponent, canActivate: [AuthGuard]},


  {path: 'pets', component: PetsComponent, canActivate: [AuthGuard] },
  {path: 'pets/:id', component: PetDetailsComponent, canActivate: [AuthGuard]},

  {path: 'playdates', component: PlaydatesComponent, canActivate: [AuthGuard]  },
  {path: 'playdates/:id', component: PlaydatesDetailComponent, canActivate: [AuthGuard]  },
  {path: 'events', component: PlaydatesFormComponent, canActivate: [AuthGuard]  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
