import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Parent } from 'src/app/model/parent';
import { ParentService } from 'src/app/services/parent/parent.service';

@Component({
  selector: 'app-petparent-detail',
  templateUrl: './petparent-detail.component.html',
  styleUrls: ['./petparent-detail.component.css']
})
export class PetparentDetailComponent implements OnInit {
  //pets: Pet[] = [];
  parent: Parent = {
    id: 0,
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    profileImageURL: ''
  };

  constructor(private route: ActivatedRoute, private parentService: ParentService, private router: Router) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
        this.parentService.getParentById(id)
          .subscribe({
            next: (response) => {
              this.parent = response;
              console.log(this.parent);
              console.log('Parent');
            }
          })
    }
  }

}
