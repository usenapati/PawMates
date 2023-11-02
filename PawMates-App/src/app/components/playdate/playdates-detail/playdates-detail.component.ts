import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventType } from 'src/app/model/eventtype';
import { Location } from 'src/app/model/location';
import { PetType } from 'src/app/model/petType';
import { PlayDate } from 'src/app/model/playdate';
import { PlayDateDTO } from 'src/app/model/playdatedto';
import { RestrictionType } from 'src/app/model/restrictionType';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-playdates-detail',
  templateUrl: './playdates-detail.component.html',
  styleUrls: ['./playdates-detail.component.css']
})
export class PlaydatesDetailComponent implements OnInit{
  playDate: PlayDateDTO = {
    hostName: '',
    locationName: '',
    eventName: '',
    eventDescription: '',
    startTime: new Date,
    endTime: new Date,
    numberOfPets: 0,
    pets: []
  };
  playDateInfo: PlayDate = {
    id: 0,
    locationId: 0,
    eventTypeId: 0,
    petParentId: 0,
    startTime: new Date,
    endTime: new Date,
    hostPets: [],
    invitedPets: [],
  };
  event: EventType = {
    id: 0,
    restrictionTypeId: 0,
    name: '',
    description: ''
  }
  location: Location = {
    id: 0,
    petTypeId: 0,
    name: '',
    street1: '',
    city: '',
    state: '',
    postalCode: ''
  }
  petType: PetType = {

  }
  restrictionType: RestrictionType = {
    
  }

  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    
  }
}
