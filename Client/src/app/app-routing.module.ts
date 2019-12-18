import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { TodoComponent } from './components/todo/todo.component';
import { TodosComponent } from './components/todos/todos.component';
import { AuthGuard } from './guard/auth.guard';


const routes: Routes = [
  { path: '', component: TodosComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: "signUp", component: SignUpComponent },
  { path: "signIn", component: SignInComponent },
  { path: "todo", component: TodoComponent, canActivate: [AuthGuard] },
  { path: "todo/:id?", component: TodoComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
