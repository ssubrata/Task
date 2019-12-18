import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
    providedIn: "root"
})
export class AuthGuard implements CanActivate {
    constructor(private auth: AuthService, private router: Router) { }

    canActivate() {
        debugger;
        var current = this.auth.currentUserValue;
        var token = localStorage.getItem("userToken");
        if (current == null && token == null) {
            this.router.navigate(['/signIn']);
            return false;
        }

        return true;
    }
}