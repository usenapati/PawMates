import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Location } from 'src/app/model/location';
import { EventType } from 'src/app/model/event';

@Component({
  selector: 'app-playdates-form',
  templateUrl: './playdates-form.component.html',
  styleUrls: ['./playdates-form.component.css']
})
export class PlaydatesFormComponent implements OnInit{
  myForm: FormGroup;
  host: {fullName: string, id: number}
  // Location
  selectedLocation: any;
  newLocation: Location;
  locationOptions: any[] = []; // Location Model
  isNewLocation: boolean = false;

  // Event
  selectedEvent: any;
  newEvent: EventType;
  eventOptions: any[] = []; // Location Model
  isNewEvent: boolean = false;

  // Host Pets
  dropdownList : any[] = [];
  selectedItems: any[] = [];
  dropdownSettings: any = {};

  // Local Pets

  constructor(private fb: FormBuilder, private authService: AuthenticationService, private apiService: ApiService, private router: Router) {
    this.host = { fullName: '', id: 0}
    this.myForm = this.fb.group({
      city: [this.selectedItems]
  });
    this.newLocation = {
      id: 0,
      name: '',
      streetAddress: '',
      city: '',
      state: '',
      postalCode: ''
    };
    this.newEvent = {
      id: 0,
      name: '',
      description: '',
      petTypeId: 0,
      restrictionId: 0
    }
  }

  ngOnInit() {
    this.dropdownList  = [
        { item_id: 1, item_text: 'New Delhi' },
        { item_id: 2, item_text: 'Mumbai' },
        { item_id: 3, item_text: 'Bangalore' },
        { item_id: 4, item_text: 'Pune' },
        { item_id: 5, item_text: 'Chennai' },
        { item_id: 6, item_text: 'Navsari' }
    ];
    this.locationOptions = [
      { id: 1, name: "Create New Location"},
      { id: 2, name: "Test"},
    ];
    this.eventOptions = [
      { id: 1, name: "Create New Event"},
      { id: 2, name: "Test"},
    ];
    //this.selectedItems = [{ item_id: 4, item_text: 'Pune' }, { item_id: 6, item_text: 'Navsari' }];
    this.dropdownSettings = {
        singleSelection: false,
        idField: 'item_id',
        textField: 'item_text',
        selectAllText: 'Select All',
        unSelectAllText: 'UnSelect All',
        itemsShowLimit: 5,
    };
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
}

onEventSelect(event: any) {
  if (event == "Create New Event") {
    this.isNewEvent = true;
  } else {
    this.isNewEvent = false;
  }
}
}
