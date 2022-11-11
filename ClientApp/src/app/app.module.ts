import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { QuestionnaireTableComponent } from './questionnaire-table/questionnaire-table.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { QuestionnaireHeadComponent } from './questionnaire-head/questionnaire-head.component';
import {MatRadioModule} from '@angular/material/radio';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import { SuccessComponent } from './success/success.component';
import {MatDialogModule} from '@angular/material/dialog';
import { WarningComponent } from './warning/warning.component';
import { SystemFeedbackFormComponent } from './system-feedback-form/system-feedback-form.component';
import { UploadfileComponent } from './uploadfile/uploadfile.component';
import { AppRoutingModule } from './app-routing.module';
import { EditPrincipalComponent } from './administrator-content/edit-principal/edit-principal.component';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { ErrorviewComponent } from './errorview/errorview.component';
import { PrivateDataTableComponent } from './private-data-table/private-data-table.component';
import {MatTableModule} from '@angular/material/table';
import { AdministratorComponent } from './administrator/administrator.component';
import { AdministratorContentComponent } from './administrator-content/administrator-content.component';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatListModule} from '@angular/material/list';
import {MatTooltipModule} from '@angular/material/tooltip';
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { RecordEditDelWithPersonComponent } from './administrator-content/record-edit-del-with-person/record-edit-del-with-person.component';
import { CancelOrderComponent } from './cancel-order/cancel-order.component';
@NgModule({
  declarations: [
    AppComponent,
    QuestionnaireTableComponent,
    QuestionnaireHeadComponent,
    SuccessComponent,
    WarningComponent,
    SystemFeedbackFormComponent,
    UploadfileComponent,
    ErrorviewComponent,
    PrivateDataTableComponent,
    AdministratorComponent,
    AdministratorContentComponent,
    EditPrincipalComponent,
    RecordEditDelWithPersonComponent,
    CancelOrderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatSliderModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatRadioModule,
    MatInputModule,
    MatCardModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatSelectModule,
    MatDialogModule,
    ReactiveFormsModule,
    AppRoutingModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    MatListModule,
    MatTooltipModule,
    MatAutocompleteModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
