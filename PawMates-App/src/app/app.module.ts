import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MdbAccordionModule } from 'mdb-angular-ui-kit/accordion';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { MdbCollapseModule } from 'mdb-angular-ui-kit/collapse';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';
import { MdbModalModule } from 'mdb-angular-ui-kit/modal';
import { MdbPopoverModule } from 'mdb-angular-ui-kit/popover';
import { MdbRadioModule } from 'mdb-angular-ui-kit/radio';
import { MdbRangeModule } from 'mdb-angular-ui-kit/range';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { MdbScrollspyModule } from 'mdb-angular-ui-kit/scrollspy';
import { MdbTabsModule } from 'mdb-angular-ui-kit/tabs';
import { MdbTooltipModule } from 'mdb-angular-ui-kit/tooltip';
import { MdbValidationModule } from 'mdb-angular-ui-kit/validation';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@danielmoncada/angular-datetime-picker';

import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PlaydatesComponent } from './components/playdate/playdates/playdates.component';
import { PlaydatesPlaydateComponent } from './components/playdate/playdates-playdate/playdates-playdate.component';
import { PlaydatesDetailComponent } from './components/playdate/playdates-detail/playdates-detail.component';
import { PlaydatesFormComponent } from './components/playdate/playdates-form/playdates-form.component';
import { PetsComponent } from './components/pet/pets/pets.component';
import { PetsPetComponent } from './components/pet/pets-pet/pets-pet.component';
import { PetparentDetailComponent } from './components/petparent/petparent-detail/petparent-detail.component';
import { PetparentPetDetailComponent } from './components/petparent/petparent-pet-detail/petparent-pet-detail.component';
import { NavComponent } from './components/nav/nav.component';
import { PetDetailsComponent } from './components/pet/pet-details/pet-details.component';
import { PetFormComponent } from './components/pet/pet-form/pet-form.component';
import { UnknownBreedPipe } from './pipes/unknown-breed.pipe';
import { PetparentProfileComponent } from './components/petparent/petparent-profile/petparent-profile/petparent-profile.component';
import { FormatDatePipe } from './pipes/format-date.pipe';
import { FormatTimePipe } from './pipes/format-time.pipe';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    PlaydatesComponent,
    PlaydatesPlaydateComponent,
    PlaydatesDetailComponent,
    PlaydatesFormComponent,
    PetsComponent,
    PetsPetComponent,
    PetparentDetailComponent,
    PetparentPetDetailComponent,
    NavComponent,
    PetDetailsComponent,
    PetFormComponent,
    UnknownBreedPipe,
    PetparentProfileComponent,
    FormatDatePipe,
    FormatTimePipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MdbAccordionModule,
    MdbCarouselModule,
    MdbCheckboxModule,
    MdbCollapseModule,
    MdbDropdownModule,
    MdbFormsModule,
    MdbModalModule,
    MdbPopoverModule,
    MdbRadioModule,
    MdbRangeModule,
    MdbRippleModule,
    MdbScrollspyModule,
    MdbTabsModule,
    MdbTooltipModule,
    MdbValidationModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgMultiSelectDropDownModule.forRoot(),
    NgbModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
