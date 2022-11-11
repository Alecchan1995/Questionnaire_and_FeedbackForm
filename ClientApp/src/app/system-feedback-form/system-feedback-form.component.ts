import { Component, OnInit } from '@angular/core';
import { QuestionnaireService } from "../service/questionnaire.service";
import { FormControl, Validators } from '@angular/forms';
import { FeedbackformService } from '../service/feedbackform.service';
import { UsernameService } from "../service/username.service";
import { Ifeedbackdata } from "../../interface/ifeedbackdata";
import { environment } from "src/environments/environment";
import { SuccessComponent } from '../success/success.component';
import { WarningComponent } from '../warning/warning.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
@Component({
  selector: 'app-system-feedback-form',
  templateUrl: './system-feedback-form.component.html',
  styleUrls: ['./system-feedback-form.component.css']
})
export class SystemFeedbackFormComponent implements OnInit {
  SystemNameOption: Array<string> = [];
  SystemProblemType: Array<string> = [];
  SystemNameCotrol = new FormControl('', Validators.required);
  Feedbackdata: Ifeedbackdata = { System_Name: "", description: "", filename: "", Fill_In_Person: "" ,Problem_Type:""};
  uploadFile = [];
  formData: FormData = new FormData();
  constructor(private QuestionnaireService: QuestionnaireService, public dialog: MatDialog, private UsernameService: UsernameService, private FeedbackformService: FeedbackformService,private router: Router) { }
  description = "";
  afuConfig = {
    multiple: true,
    uploadAPI: {
      url: `${environment.apiUrl}/QuestionDBfunction/Save`,
    },
    fileNameIndex: false,
    autoUpload: false,
    replaceTexts: {
      selectFileBtn: '附件',
      resetBtn: 'Reset',
      uploadBtn: 'UploadS',
      dragNDropBox: 'Drag N Drop',
      attachPinBtn: 'Attach Files...',
      afterUploadMsg_success: 'Successfully Uploaded !',
      afterUploadMsg_error: 'Upload Failed !',
      sizeLimit: 'Size Limit',
    },
  };
  ngOnInit(): void {
    this.FeedbackformService.getsystemname().subscribe(x => {
      this.SystemNameOption = x;
    });
    this.FeedbackformService.getproblemtype().subscribe(x => {
      this.SystemProblemType = x;
    });
    this.UsernameService.get_user_name().subscribe(data => {
      this.Feedbackdata.Fill_In_Person = data ;
    });
  }
  check_data() {
    //console.log("check data");
    if (this.Feedbackdata.System_Name.length == 0 || this.Feedbackdata.description.length == 0 || this.Feedbackdata.Problem_Type.length == 0) {
      return false;
    }
    return true;
  }
  open_dialog() {
    //console.log("open dislog");
    if (this.check_data())
    {
      this.send_data();
    }
    else
    {
      const dialogRef = this.dialog.open(WarningComponent, {
        width: '250px',
      });
      dialogRef.afterClosed().subscribe();
    }
  }
  send_data() {
    this.FeedbackformService.Sendquestionnairedata(this.Feedbackdata)
      .subscribe({
        next: (v) => console.log(v),
        error: (e) => console.error(e),
        complete: () => {
          this.FeedbackformService.Sendfile(this.formData)
            .subscribe({
              next: (v) => console.log(v),
              error: (e) => console.error(e),
              complete: () => {//console.log("uploadfile finish")
              }
            });
            const dialogRef = this.dialog.open(SuccessComponent, {
              width: '250px',
            });
            dialogRef.afterClosed().subscribe(x=>{
              //console.log("OK")
              window.location.assign('');
            });
        }
      });
  }
  show_data() {
    console.table(this.Feedbackdata);
  }
  filedata(file = []) {
    this.formData = new FormData();
    file.forEach(x => {
      this.uploadFile.push(x['name']);
      this.formData.append("files", x);
    });
    this.Feedbackdata.filename = this.uploadFile.join(",");
  }
}
