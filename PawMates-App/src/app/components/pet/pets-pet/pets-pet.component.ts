import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';

@Component({
  selector: 'app-pets-pet',
  templateUrl: './pets-pet.component.html',
  styleUrls: ['./pets-pet.component.css']
})
export class PetsPetComponent implements OnInit  {
  @Input() pet: any;
  @Input() index!: number;

  constructor() { }
  ngOnInit(): void { }

  handleImageError(){
    this.pet.imageUrl = "../../assets/for-pet-without-image.png";
  }
}
