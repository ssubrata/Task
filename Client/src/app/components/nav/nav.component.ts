import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  ngOnInit(): void {

  }


  loginUser: User;
  constructor(private auth: AuthService) {
    debugger;
    this.auth.currentUser.subscribe(x => {
      this.loginUser = x;
    })
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }
  signOut() {
    this.auth.signOut();
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
