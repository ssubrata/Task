import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  model = {
    userName: "",
    password: ""
  };

  constructor(
    private authService: AuthService,
  ) { }


  onSubmit() {
    this.authService.signIn(this.model)
  }
}
