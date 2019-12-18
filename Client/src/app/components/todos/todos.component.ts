import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss']
})
export class TodosComponent implements OnInit {
  todos: any[] = [];
  constructor(
    private todoService: TodoService,
    private router: Router
  ) { }

  ngOnInit() {
    this.getTodosByUser();
  }

  getTodosByUser() {

    this.todoService.getTodoByUser().subscribe(
      (data: any) => {
        this.todos = data;
      }
    )
  }
  onDelete(todo) {
    this.todoService.deleteTodo(todo.id).subscribe(response=>{
      let index=this.todos.findIndex(f=>f.id==todo.id);
      this.todos.splice(index);
    })
  }
  onEdit(todo) {
    this.router.navigate(["/todo", { id: todo.id }]);

  }

}
