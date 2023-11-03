import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlayDateDTO } from 'src/app/model/playdatedto';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-playdates',
  templateUrl: './playdates.component.html',
  styleUrls: ['./playdates.component.css']
})
export class PlaydatesComponent implements OnInit {
  playDate: PlayDateDTO = {
    id: 0,
    hostId: 0,
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
  playDates: PlayDateDTO[] = [];
  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
      this.apiService.getPlayDates()
      .subscribe({
        next: (playDates: PlayDateDTO[]) => {
          this.playDates = playDates;
      }});
}
  //most recent events first
  public sortPlayDatesByDateDesc() {
    let sorted = this.playDates.sort((a, b) => new Date(a.startTime).getTime() - new Date(b.startTime).getTime());
    return sorted;
  }
  //oldest events first
  public sortPlayDatesByDateAsc() {
    let sorted = this.playDates.sort((a, b) => new Date(b.startTime).getTime() - new Date(a.startTime).getTime());
    return sorted;
  }

  public futureEvents() {
    let today = new Date().getTime();
    let futureEvents = this.sortPlayDatesByDateDesc().filter(
      p => {
        let time = new Date(p.startTime).getTime();
        return (time > today);
      });
    return futureEvents;
  }

  public pastEvents() {
    let today = new Date().getTime();
    let pastEvents = this.sortPlayDatesByDateAsc().filter(
      p => {
        let time = new Date(p.startTime).getTime();
        return (time < today);
      });
    return pastEvents;
  }

}
