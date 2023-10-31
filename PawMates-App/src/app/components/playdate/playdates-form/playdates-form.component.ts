import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { AuthenticationService } from 'src/app/services/authentication.service';


@Component({
  selector: 'app-playdates-form',
  templateUrl: './playdates-form.component.html',
  styleUrls: ['./playdates-form.component.css']
})
export class PlaydatesFormComponent implements OnInit{
  host: {fullName: string, id: number}
  cities: any[] = [];
  selectedItems: any[] = [];
  dropdownSettings: any = {};

  constructor(private authService: AuthenticationService, private apiService: ApiService, private router: Router) {
    this.host = { fullName: '', id: 0}
  }

  ngOnInit() {
    this.cities = [
        { item_id: 1, item_text: 'New Delhi' },
        { item_id: 2, item_text: 'Mumbai' },
        { item_id: 3, item_text: 'Bangalore' },
        { item_id: 4, item_text: 'Pune' },
        { item_id: 5, item_text: 'Chennai' },
        { item_id: 6, item_text: 'Navsari' }
    ];
    this.selectedItems = [{ item_id: 4, item_text: 'Pune' }, { item_id: 6, item_text: 'Navsari' }];
    this.dropdownSettings = {
        singleSelection: false,
        idField: 'item_id',
        textField: 'item_text',
        selectAllText: 'Select All',
        unSelectAllText: 'UnSelect All',
        itemsShowLimit: 3,
    };
}

onItemSelect(item: any) {
    console.log('onItemSelect', item);
}
onSelectAll(items: any) {
    console.log('onSelectAll', items);
}
}
