import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlayDate } from 'src/app/model/playdate';
import { PlayDateDTO } from 'src/app/model/playdatedto';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-playdates',
  templateUrl: './playdates.component.html',
  styleUrls: ['./playdates.component.css']
})
export class PlaydatesComponent implements OnInit {
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
  playDates: PlayDateDTO[] = [];
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
      this.apiService.getPlayDates()
      .subscribe({
        next: (playDates: PlayDateDTO[]) => {
          this.playDates = playDates;
      }});
  }
}
