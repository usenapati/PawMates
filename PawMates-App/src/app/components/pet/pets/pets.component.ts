import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-pets',
  templateUrl: './pets.component.html',
  styleUrls: ['./pets.component.css']
})
export class PetsComponent implements OnInit {
  pets: any[] = [];
  constructor(private route: ActivatedRoute, private apiService: ApiService){
   }

  ngOnInit(): void {
      this.apiService.getPets().subscribe(pets => {
        this.pets = pets;
      });
  }
}
