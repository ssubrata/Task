import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('userToken')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }
  isLogged() {
    return !!localStorage.getItem("userToken");
  }

  signIn(model) {
    this.http.post(`${environment.apiUrl}/auth/signin`, model)
      .subscribe((reponse: any) => {
        let userToken: User = {
          id: reponse.id,
          fullName: reponse.fullName,
          token: reponse.token
        };
        debugger;
        localStorage.setItem('userToken', JSON.stringify(userToken));
        this.currentUserSubject.next(userToken);
        this.router.navigate(["/"]);
      });
  }

  signUp(model) {
    this.http.post(`${environment.apiUrl}/auth/signup`, model)
      .subscribe((reponse) => {
        this.router.navigateByUrl("/signIn");
      });
  }
  
  signOut() {
    localStorage.removeItem('userToken');
    this.currentUserSubject.next(null);
    this.router.navigateByUrl("/signIn");
  }
}
