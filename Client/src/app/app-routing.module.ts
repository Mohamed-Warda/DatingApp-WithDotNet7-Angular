import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './auth/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';



const routes: Routes = [
  {path: '', component: HomeComponent},
  {path:'',runGuardsAndResolvers:'always', canActivate:[AuthGuard], 
    children:[     
      {path: 'members', component: MemberListComponent},
      {path: 'members/:username', component: MemberDetailComponent},
      {path: 'member/edit', component: MemberEditComponent},

      {path: 'lists', component: ListsComponent},
      {path: 'messages', component: MessagesComponent},
        ] 
    
    }
    ,
  {path: 'errors', component: TestErrorComponent},
 
        

    ];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
