import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { forkJoin } from 'rxjs';
import { Pet } from 'src/app/model/pet';
import { Location } from 'src/app/model/location';
import { EventType } from 'src/app/model/eventtype';
import { PlayDate } from 'src/app/model/playdate';

@Component({
  selector: 'app-playdates-form',
  templateUrl: './playdates-form.component.html',
  styleUrls: ['./playdates-form.component.scss', ],
})
export class PlaydatesFormComponent implements OnInit{

  host: {fullName: string, id: number};
  // Host Pets
  hostPets: Pet[] = [];
  hostPetList: any[] = [];
  selectedHostPets: any[] = [];
  hostPetDropdownSettings: IDropdownSettings = {};

  // Location
  locations: Location[] = [];
  selectedLocation: {id: number | undefined, name: string} = {id: 0, name: ''};
  location: Location; // Set up at submit()
  newLocation: Location;
  locationOptions: {id: number | undefined, name: string}[] = [];
  isNewLocation: boolean = false;

  // Event
  events: EventType[] = [];
  selectedEvent: {id: number | undefined, name: string} = {id: 0, name: ''};
  event: EventType;
  newEvent: EventType;
  eventOptions: {id: number | undefined, name: string}[] = [];
  isNewEvent: boolean = false;

  // Local Pets
  localPets: Pet[] = [];
  localPetList: any[] = [];
  selectedLocalPets: any[] = [];
  localPetDropdownSettings: IDropdownSettings = {};

  startTime: Date = new Date();
  endTime: Date = new Date();

  playDate: PlayDate;

  constructor(private authService: AuthenticationService, private apiService: ApiService, private router: Router) {
    this.host = { fullName: '', id: 0}

    this.playDate = {
      id: 0,
      petParentId: 0,
      locationId: 0,
      eventTypeId: 0,
      startTime: new Date(),
      endTime: new Date()
    }

    this.location = {
      id: 0,
      name: '',
      street1: '',
      city: '',
      state: '',
      postalCode: '',
      petTypeId: 0,

    };
    this.newLocation = {
      id: 0,
      name: '',
      street1: '',
      city: '',
      state: '',
      postalCode: '',
      petTypeId: 0,

    };
    this.event = {
      id: 0,
      name: '',
      description: '',
      restrictionTypeId: 0,
    }
    this.newEvent = {
      id: 0,
      name: '',
      description: '',
      restrictionTypeId: 0,
    }
  }

  ngOnInit() {
    // Get Host Name from Token
    this.host.id = parseInt(this.authService.getDecodedToken().PetParentId, 10);
    if (this.host.id) {
      this.apiService.getParentById(+this.host.id)
      .subscribe({
        next: (response) => {
          this.host.fullName = response.firstName + ' ' + response.lastName;
        }
      });

      // Host Pets and Get Pets excluding host pets
      forkJoin({
        hostPets: this.apiService.getPetsByParent(+this.host.id),
        allPets: this.apiService.getPets()
      }).subscribe(result => {
        this.hostPets = result.hostPets;
        this.hostPetList = this.hostPets.map(p => ({pet_id: p.id, pet_name: p.name}));
        this.localPets = result.allPets.filter((pet : Pet) => pet.parentId != this.host.id);
        this.localPetList = this.localPets.map(p => ({pet_id: p.id, pet_name: p.name}));
      });
     
    };

    this.hostPetDropdownSettings = {
      singleSelection: false,
      idField: 'pet_id',
      textField: 'pet_name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
    };

    this.localPetDropdownSettings = {
      singleSelection: false,
      idField: 'pet_id',
      textField: 'pet_name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
    };

    // Get Locations
    this.apiService.getLocations().subscribe(locations => {
      this.locations = locations;
      this.locationOptions = this.locations.map((l : Location) => ({id: l.id, name: l.name}));
      this.locationOptions.push({ id: 0, name: "Create New Location"});

    });
    
    // Get Events
    this.apiService.getEvents().subscribe(events => {
      this.events = events;
      this.eventOptions = this.events.map((e : EventType) => ({id: e.id, name: e.name}));
      this.eventOptions.push({ id: 0, name: "Create New Event"});
    });
    
  
    // Get Pet Types for Location
    // Get Restriction for Event
    
}

onSubmit() {
  // Host ID
  this.playDate.petParentId = this.host.id;

  // Location ID
  if (this.location.id) {
    this.playDate.locationId = this.location.id;
  } else {
    // Create Location
  }

  // Event ID
  if (this.event.id) {
    this.playDate.eventTypeId = this.event.id;
  } else {
    // Create Event
  }
  // TODO Validate Location and Event Pet Restriction match

  // Start and End Time
  this.playDate.startTime = this.startTime;
  this.playDate.endTime = this.endTime;
  // Validate that start is not after end and no more than 24 hrs long
  
  // Require one Host Pet
  // Post the Play Date
  this.apiService.addPlayDate(this.playDate).subscribe(response => {

  }, error => {
      console.error('Create Play Date failed');
  });
  // TODO Make sure the host cannot make a play date that conflict with their own playdates

  // Pets IDs - Loop through each id and add it to the submitted play date
  // Host Pets
  this.selectedHostPets.forEach(element => {
    // Add Pet to Play Date if successfully created
    //this.playDate.pets.push(element.pet_id)
  });
  // Local Pets
  this.selectedLocalPets.forEach(element => {
    //this.playDate.pets.push(element.pet_id)
  });
  console.log(this.playDate);
  
  // Route to Play Date Details
}

onItemSelect(item: any) {
    //console.log('onItemSelect', item);
}
onSelectAll(items: any) {
    //console.log('onSelectAll', items);
}

onLocationSelect(location: any) {
  if (location == "Create New Location") {
    this.isNewLocation = true;
  } else {
    this.isNewLocation = false;
  }
  var locationId = this.locations.find(l => l.name == location);
  if (locationId) {
    this.location = locationId;
  } else {
    this.location = this.newLocation;
  }
}

onEventSelect(event: any) {
  if (event == "Create New Event") {
    this.isNewEvent = true;
  } else {
    this.isNewEvent = false;
    
  }
  var eventId = this.events.find(e => e.name == event);
  if (eventId) {
    this.event = eventId;
  } else {
    this.event = this.newEvent;
  }
}
}
