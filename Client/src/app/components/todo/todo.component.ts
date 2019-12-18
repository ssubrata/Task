import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {

  constructor(private route: ActivatedRoute, private todoService: TodoService, private router: Router) {

  }
  todo = {
    id: 0,
    title: "",
    description: "",
    location: "",
    date: "",
    from: "",
    to: "",
    notifyTime: "",
    notifyBy: "Email",
    userId: 0,
  }
  todos: [] = [];

  ngOnInit() {
    debugger
    this.todoService.getTodoByUser().subscribe((data: any) => {
      if (data) {
        let id = this.route.snapshot.paramMap.get('id');
        if (id) {
          data.forEach((value: any) => {
            if (value.id == id) {
              this.todo = value;
            }
          });
        }



      }

    })


  }

  onSubmit() {
    if (this.todo.id == 0) {
      this.todoService.postTodo(this.todo).subscribe(data => {
        this.router.navigate(["/"]);
      })
    }
    else {
      this.todoService.putTodo(this.todo).subscribe(data => {
        this.router.navigate(["/"]);
      })
    }

  }
}
