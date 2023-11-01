import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventType } from 'src/app/model/eventtype';
import { Location } from 'src/app/model/location';
import { Pet } from 'src/app/model/pet';
import { PlayDate } from 'src/app/model/playdate';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-playdates',
  templateUrl: './playdates.component.html',
  styleUrls: ['./playdates.component.css']
})
export class PlaydatesComponent implements OnInit {
  event: EventType = {
    id: 0,
    restrictionTypeId: 0,
    name: '',
    description: ''
  };
  events: EventType[] = [];
  location: Location = {
    id: 0,
    petTypeId: 0,
    name: '',
    street1: '',
    city: '',
    state: '',
    postalCode: '',
    petAge: 0
  };
  locations: Location[] = [];
  playDate: PlayDate = {
    petParentId: 0,
    pets: [],
    locationId: 0,
    eventId: 0,
    startTime: new Date,
    endTime: new Date
  };
  playDates: PlayDate[] = [];
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.apiService.getEventById(+id)
      .subscribe({
        next: (response) => {
          this.event = response;
        }});

      this.apiService.getEvents()
      .subscribe({
        next: (events: EventType[]) => {
          this.events = events;
      }});

      this.apiService.getLocationById(+id)
      .subscribe({
        next: (response) => {
          this.location = response;
        }});

      this.apiService.getLocations()
      .subscribe({
        next: (locations: Location[]) => {
          this.locations = locations;
      }});
    }
  }
}
