import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { QuestionnaireService } from "../service/questionnaire.service"
import { Questionnairedata } from "./Questionnairedatainit";
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SuccessComponent } from '../success/success.component';
import { WarningComponent } from '../warning/warning.component';
import { FormControl, Validators } from '@angular/forms';
import { UsernameService } from "../service/username.service";
import { Ifeedbackdata } from "../../interface/ifeedbackdata";
import { Iquestionnairedata } from '../../interface/iquestionnairedata'
import { ActivatedRoute  } from '@angular/router';
import { Router } from '@angular/router';
@Component({
  selector: 'app-questionnaire-table',
  templateUrl: './questionnaire-table.component.html',
  styleUrls: ['./questionnaire-table.component.css']
})

export class QuestionnaireTableComponent implements OnInit {
  systemname="";
  confirm_name="";
  Questionnairedata:Iquestionnairedata=Questionnairedata
  SystemNameOption: Array<string> = [];
  SystemNameCotrol = new FormControl('', Validators.required);
  scores: string[] = ['非常滿意', '值得嘉許', '尚可接受', '有待改進', '糟糕透了'];
  constructor(private QuestionnaireService: QuestionnaireService, public dialog: MatDialog,private UsernameService:UsernameService,private route: ActivatedRoute,private router: Router) { }
  ngOnInit(): void {
    const questionnaireID =this.route.snapshot.paramMap.get('questionnairenumber'); //取route 參數
    const score =this.route.snapshot.paramMap.get('score');
    this.QuestionnaireService.getquestiondata(Number(questionnaireID)).subscribe(x => {
      this.Questionnairedata = x;
      this.confirm_name = Object.values(this.Questionnairedata.SystemFeedbackForm)[3];
      this.check_permisson();
      this.systemname = Object.values(this.Questionnairedata.SystemFeedbackForm)[4];
      if(score =='5'){
        this.Questionnairedata.Service_Fraction="非常滿意";
        this.Questionnairedata.Process_Fraction="非常滿意";
      }
    });
  }
  check_permisson(){
    this.UsernameService.get_user_name().subscribe(x=>{
      if(x.replace("COMPAL\\","") != this.confirm_name){
        this.router.navigateByUrl('Error');
      }
    });
  }
  show_data(){
    //console.log(this.Questionnairedata);
  }
  send_data() {
    this.QuestionnaireService.Sendquestionnairedata(this.Questionnairedata).subscribe(x => {
      this.openDialog();
    });
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes["Questionnairedata"]) {
      this.Questionnairedata = changes["Questionnairedata"].currentValue;
    }
  }
  openDialog(): void {
    if (!this.Questionnairedata.Process_Fraction || !this.Questionnairedata.Service_Fraction ) {
      const dialogRef = this.dialog.open(WarningComponent, {
        width: '250px',
      });
      dialogRef.afterClosed().subscribe(result => {
      });
    }
    else {
      const dialogRef = this.dialog.open(SuccessComponent, {
        width: '250px',
      });
      dialogRef.afterClosed().subscribe(result => {
        this.router.navigateByUrl('');
      });
    }
  }

}
