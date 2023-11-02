import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlayDateDTO } from 'src/app/model/playdatedto';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-playdates-detail',
  templateUrl: './playdates-detail.component.html',
  styleUrls: ['./playdates-detail.component.css']
})
export class PlaydatesDetailComponent implements OnInit {
  playDate: PlayDateDTO = {
    id: 0,
    hostName: '',
    locationName: '',
    address: '',
    city: '',
    state: '',
    postalCode: '',
    eventName: '',
    eventDescription: '',
    startTime: new Date,
    endTime: new Date,
    numberOfPets: 0,
    pets: []
  };
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.apiService.getPlayDateById(+id)
      .subscribe({
        next: (response) => {
          this.playDate = response;
      }});
    }
  }
}
