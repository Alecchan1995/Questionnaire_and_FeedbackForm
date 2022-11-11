import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionnaireTableComponent } from './questionnaire-table/questionnaire-table.component';
import { SystemFeedbackFormComponent } from './system-feedback-form/system-feedback-form.component';
import { RouterModule, Routes } from '@angular/router';
import { ErrorviewComponent } from './errorview/errorview.component';
import { AdministratorComponent } from './administrator/administrator.component';
const routes: Routes = [
  {path:'',component:SystemFeedbackFormComponent},
  {path:'feedbackform',component:SystemFeedbackFormComponent},
  {path:'questionnaire/:questionnairenumber',component:QuestionnaireTableComponent},
  {path:'questionnaire/:questionnairenumber/:score',component:QuestionnaireTableComponent},
  {path:'Error',component:ErrorviewComponent},
  {path:'Administrator',component:AdministratorComponent},
  {path:'Administrator/:id',component:AdministratorComponent},
  {path:'**',component:ErrorviewComponent},
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
