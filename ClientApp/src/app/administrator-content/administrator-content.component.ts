import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IPrivateData } from "../../interface/IPrivateData";
import { PrivateDataInit } from "../../interface/PrivateDataInit";
import { AutoHelpSeviceManagerService } from "../service/auto-help-sevice-manager.service";
import {EditPrincipalComponent} from "./edit-principal/edit-principal.component";
import {RecordEditDelWithPersonComponent} from "./record-edit-del-with-person/record-edit-del-with-person.component";
import { UsernameService } from "../service/username.service";
export interface Section {
  name: string;
  updated: Date;
}


export type ProjectNameModalData<T> = (row: T) => void;
@Component({
  selector: 'app-administrator-content',
  templateUrl: './administrator-content.component.html',
  styleUrls: ['./administrator-content.component.css']
})
export class AdministratorContentComponent implements OnInit {
  processidea:Array<string>=[];
  permission:boolean=false;
  constructor(public dialogRef: MatDialogRef<AdministratorContentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { content: IPrivateData },
    public AutoHelpSeviceManagerServices:AutoHelpSeviceManagerService,
    public UsernameService:UsernameService,
    public dialog: MatDialog,) { }
    datacontentdata : IPrivateData={
    ID: 0,
    Service_Fraction: "",
    Process_Fraction: "",
    Other_Idea: "",
    deal_with_idea: "",
    deal_with_time: "",
    deal_with_person:"",
    principal:"",
    SystemFeedbackForm: {
      description: "",
      filename: "",
      Fill_In_Person: "",
      System_Name: "",
      Send_Time: "",
      deal_with_state: "",
      Problem_Type: "",
    }
  };
  ngOnInit(): void {
    this.datacontentdata = Object.assign({},this.data.content);
    this.AutoHelpSeviceManagerServices.GetStateType().subscribe(x=>{
      this.processidea = x;
    })
    this.check_permission();
  }
  check_permission(){
    this.UsernameService.get_user_name().subscribe(name => {
      //負責人和PM都可以有權限
      if(this.datacontentdata.deal_with_person.search(name.replace("COMPAL\\","")) != -1 ||this.datacontentdata.principal.search(name.replace("COMPAL\\","")) != -1){
        this.permission = true;
      }
    });
  }
  closedialog() {
    this.dialogRef.close();
  }
  senddtata() {
    this.AutoHelpSeviceManagerServices.GetSenddata(this.datacontentdata).subscribe(x=>{console.log("處理成功");window.location.reload();});
    this.dialogRef.close();
  }
  downfile(filename:string){
    this.AutoHelpSeviceManagerServices.Downfile(filename).subscribe(x=>
      {
        console.log("down OK");
        let blob:Blob=x.body as Blob;
        let a = document.createElement('a');
        a.download = filename!;
        a.href = window.URL.createObjectURL(blob);
        a.click();

      });
  }
  open_edit_deal_with_person(){
    const dialogRef = this.dialog.open(EditPrincipalComponent, {
      width: '30%',
      minHeight: '70%',
      disableClose: true,
      data: {deal_with_person_name_list: this.datacontentdata.deal_with_person,ID:this.datacontentdata.ID},
    });
    dialogRef.afterClosed().subscribe(result => {
      this.datacontentdata.deal_with_person = result;
    })
  }
  open_edit_record_edit_del_with_person(ID:number){
    const dialogRef = this.dialog.open(RecordEditDelWithPersonComponent, {
      width: '30%',
      minHeight: '70%',
      disableClose: true,
      data:{ID:ID},
    });
    dialogRef.afterClosed().subscribe(result => {
      //this.datacontentdata.deal_with_person = result;
    })
  }
}
