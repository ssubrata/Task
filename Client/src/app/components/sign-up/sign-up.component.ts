import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  model = {
    fullName: "",
    email: "",
    password: "",
    birthDate: ""
  }
  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }
  onSubmit() {
    debugger;
    this.auth.signUp(this.model);
    this.router.navigate(["/signin"]);
  }
}
