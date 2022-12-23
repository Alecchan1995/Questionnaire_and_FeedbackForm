import { Component, OnInit , Inject} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {FormControl} from '@angular/forms';
import {EditsystemdataService} from "../service/editsystemdata.service";
import { AutoHelpSeviceManagerService } from '../service/auto-help-sevice-manager.service';
import { PermissionService } from "../service/permission.service"
@Component({
  selector: 'app-editsystemdata',
  templateUrl: './editsystemdata.component.html',
  styleUrls: ['./editsystemdata.component.css']
})
export class EditsystemdataComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<EditsystemdataComponent>,
              public EditsystemdataServices:EditsystemdataService,
              public AutoHelpSeviceManagerServices:AutoHelpSeviceManagerService,
              public PermissionServices:PermissionService) { }
  choose = "";
  newitem ="";
  system_name = "";
  Add_member = "";
  chooserole = "RD";
  roles :Array<string> = [];
  memberList:Array<string> = [];
  dataList:Array<string>=[];
  ComparisonList:Array<string> = [];
  Items = [
    {value: 'editsystem', viewValue: '修改系統'},
    {value: 'editadministrator', viewValue: '修改系統管理權限'},
    {value: 'editemploy', viewValue: '修改編輯人員'}
  ];
  ngOnInit(): void {
    this.EditsystemdataServices.getmemberrole().subscribe(x=>this.roles = x);
    this.PermissionServices.getprincipal().subscribe(x=>{ this.ComparisonList = x.map(x => x.replace("COMPAL\\",""));});
  }
  closedialog() {
    this.dialogRef.close();
  }
  chooseItem(data:string){
    this.choose = data;
    this.newitem = "";
    this.dataList = [];
    this.memberList = [];
    if(this.choose =="editsystem"){
      this.EditsystemdataServices.getSystemname().subscribe(x=>{this.dataList = x;});
    }
    else if(this.choose =="editadministrator"){
      this.EditsystemdataServices.getSystemname().subscribe(x=>{this.dataList = x;});
    }
    else if(this.choose =="editemploy"){
      this.EditsystemdataServices.getemploy().subscribe(x=>{this.dataList = x;});
    }
  }
  AddItem(){
    if(this.choose =="editsystem"){
      if(this.dataList.indexOf(this.newitem) != -1){alert("User is exist!");return}
      this.EditsystemdataServices.addSystemname(this.newitem).subscribe(x=>{this.dataList = x;});
    }
    //systemname:string = "", member:string="", role:string=""
    else if(this.choose =="editadministrator"){
      if(this.memberList.indexOf(this.newitem) != -1){alert("User is exist!");return}
      if(this.ComparisonList.indexOf(this.newitem) != -1){
        if(this.chooserole == "Lender"){
          this.memberList.forEach( x => {
            if(x.search("Lender") != -1){
              alert("請先Delete 原Lender才能修改");
              return;
            }
          });
        }
        this.EditsystemdataServices.addmember(this.system_name,this.newitem,this.chooserole).subscribe(x=>{this.memberList = x;});
      }
      else{alert("他沒有管理權限");return}
    }
    else if(this.choose =="editemploy"){
      if(this.dataList.indexOf("COMPAL\\"+this.newitem) != -1){alert("User is exist!");return}
      this.AutoHelpSeviceManagerServices.Check_User_Name(this.newitem).subscribe(x=>{
        if(x[0] != "Error"){
          this.EditsystemdataServices.addemploy(this.newitem).subscribe(x=>{this.dataList = x;});
        }
        else{alert("Account Error");return}
      });
    }
  }
  deleteSystemname(systemname:string){
    if(this.choose =="editsystem"){
      this.EditsystemdataServices.deleteSystemname(systemname).subscribe(x=>{this.dataList = x;});
    }
    else if(this.choose =="editadministrator"){
      this.EditsystemdataServices.deletemember(this.system_name,systemname.replace("-> Lender",""),this.chooserole).subscribe(x=>{this.memberList = x;});
    }
    else if(this.choose =="editemploy"){
      this.EditsystemdataServices.deleteemploy(systemname.replace("COMPAL\\","")).subscribe(x=>{this.dataList = x;});
    }
  }
  GETSystemmember(systemname:string){
     this.system_name = systemname;

     this.EditsystemdataServices.getSystemmember(systemname).subscribe(x=>{this.memberList = x;});
  }
}
