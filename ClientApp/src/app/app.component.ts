import { Component } from '@angular/core';
import { Questionnairedata } from "./questionnaire-table/Questionnairedatainit";
import { UsernameService } from "./service/username.service";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(private UsernameService:UsernameService) { }
  Questionnairedata = Questionnairedata;
  ngOnInit(): void {
    this.UsernameService.get_user_name().subscribe(data => {
      this.Questionnairedata.SystemFeedbackForm.Fill_In_Person = data;
    });
  }
  title = 'app';
}
