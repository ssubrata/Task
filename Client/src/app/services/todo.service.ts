import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  apiUrl = environment.apiUrl;
  constructor(
    private http: HttpClient,
    private auth: AuthService) {


  }

  getTodoByUser() {
    debugger;
    let id = this.auth.currentUserValue.id;
    return this.http.get(`${environment.apiUrl}/todo/` + id);
  }
  postTodo(todo) {
    todo.userId=this.auth.currentUserValue.id;
    return this.http.post(`${environment.apiUrl}/todo`, todo);

  }
  putTodo(todo) {
    return this.http.put(`${environment.apiUrl}/todo`, todo);
  }

  deleteTodo(id) {
    debugger;
    return this.http.delete(`${environment.apiUrl}/todo/` + id);

  }
}
