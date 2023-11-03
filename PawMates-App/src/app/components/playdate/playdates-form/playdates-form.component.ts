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
import { PetType } from 'src/app/model/petType';
import { RestrictionType } from 'src/app/model/restrictionType';

@Component({
  selector: 'app-playdates-form',
  templateUrl: './playdates-form.component.html',
  styleUrls: ['./playdates-form.component.css', ],
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

  // PetTypes
  petTypes: PetType[] = [];
  selectedPetType: {id: number | undefined, name: string} = {id: 0, name: ''};
  petType: PetType;
  petTypeOptions: {id: number | undefined, name: string}[] = [];
  
  // RestrictionTypes
  restrictionTypes: RestrictionType[] = [];
  selectedRestriction: {id: number | undefined, name: string} = {id: 0, name: ''};
  restrictionType: RestrictionType;
  restrictionTypeOptions: {id: number | undefined, name: string}[] = [];


  // Local Pets
  localPets: Pet[] = [];
  localPetList: any[] = [];
  selectedLocalPets: any[] = [];
  localPetDropdownSettings: IDropdownSettings = {};

  startTime: Date = new Date;
  endTime: Date = new Date;

  playDate: PlayDate;

  constructor(private authService: AuthenticationService, private apiService: ApiService, private router: Router) {
    this.host = { fullName: '', id: 0}

    this.playDate = {
      id: 0,
      petParentId: 0,
      locationId: 0,
      eventTypeId: 0,
      startTime: new Date(),
      endTime: new Date(),
      hostPets: [],
      invitedPets: []
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
      petTypeId: 0
    }
    this.newEvent = {
      id: 0,
      name: '',
      description: '',
      restrictionTypeId: 0,
      petTypeId: 0,
    }

    this.petType = {
      id: 0,
      species: ''
    }

    this.restrictionType = {
      id: 0,
      name: ''
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
    this.apiService.getPetTypes().subscribe(petTypes => {
      this.petTypes = petTypes;
      this.petTypeOptions = this.petTypes.map((p : PetType) => ({id: p.id, name: p.species}));
    })

    // Get Restriction for Event
    this.apiService.getRestrictionTypes().subscribe(restrictionTypes => {
      this.restrictionTypes = restrictionTypes;
      this.restrictionTypeOptions = this.restrictionTypes.map((r : RestrictionType) => ({id: r.id, name: r.name}));
      this.restrictionTypeOptions.push({ id: 0, name: "No Restrictions"});
    })
    
}
convertUTCDateToLocalDate(date: Date) {
  var newDate = new Date(date.getTime()+date.getTimezoneOffset()*60*1000);

  var offset = date.getTimezoneOffset() / 60;
  var hours = date.getHours();

  newDate.setHours(hours - offset);

  return newDate;   
}

  onSubmit() {
    // Host ID
    this.playDate.petParentId = this.host.id;

    // Start and End Time
    this.playDate.startTime = this.convertUTCDateToLocalDate(new Date(this.startTime));
    this.playDate.endTime = this.convertUTCDateToLocalDate(new Date(this.endTime));

    // Pets IDs - Loop through each id and add it to the submitted play date
    // Host Pets
    this.selectedHostPets.forEach(element => {
      this.playDate.hostPets.push(element.pet_id)
    });
    // Local Pets
    this.selectedLocalPets.forEach(element => {
      this.playDate.invitedPets.push(element.pet_id)
    });

    // Location ID
    if (this.location.id) {
      this.playDate.locationId = this.location.id;
      // Event ID
      if (this.event.id) {
        this.playDate.eventTypeId = this.event.id;
        this.apiService.addPlayDate(this.playDate).subscribe(response => {
          // Route to Play Date Details
          this.router.navigate(['playdates/' + response.id]);
          return;
        });
      } else {
        // Create Event
        this.apiService.addEvent(this.newEvent).subscribe(response => {
          this.playDate.eventTypeId = response.id
          this.apiService.addPlayDate(this.playDate).subscribe(response => {
            // Route to Play Date Details
            this.router.navigate(['playdates/' + response.id]);
          });
          return;
        });
    }
    } else {
      // Create Location
      this.apiService.addLocation(this.newLocation).subscribe(response => {
        this.playDate.locationId = response.id
        // Event ID
        if (this.event.id) {
          this.playDate.eventTypeId = this.event.id;
          this.apiService.addPlayDate(this.playDate).subscribe(response => {
            // Route to Play Date Details
            this.router.navigate(['playdates/' + response.id]);
          });
          return;
        } else {
          // Create Event
          this.apiService.addEvent(this.newEvent).subscribe(response => {
            this.playDate.eventTypeId = response.id
            // Post the Play Date
            this.apiService.addPlayDate(this.playDate).subscribe(response => {
              // Route to Play Date Details
              this.router.navigate(['playdates/' + response.id]);
            });
            return;
          });
    }
      });
    }

    
    // TODO Validate Location and Event Pet Restriction match
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

  onPetTypeSelect(petType: any) {
    console.log(petType)
    var petTypeId = this.petTypes.find(p => p.species == petType);
    if (petTypeId) {
      this.newLocation.petTypeId = petTypeId.id;
      this.newEvent.petTypeId = petType.id;
    } else {
      this.newLocation.petTypeId = 1;
      this.newEvent.petTypeId = 1;
    }
  }

  onRestrictionTypeSelect(restrictionType: any) {
    console.log(restrictionType)
    var restrictionTypeId = this.restrictionTypes.find(r => r.name == restrictionType);
    if (restrictionTypeId) {
      this.newEvent.restrictionTypeId = restrictionTypeId.id;
    } else {
      this.newEvent.restrictionTypeId = undefined;
    }
  }
}
