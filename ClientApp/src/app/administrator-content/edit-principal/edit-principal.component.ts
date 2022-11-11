import { Component, OnInit, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PermissionService } from "../../service/permission.service"
import { AutoHelpSeviceManagerService } from "../../service/auto-help-sevice-manager.service";
export interface DialogPrincipalData {
  deal_with_person_name_list: string;
  ID: number;
}
@Component({
  selector: 'app-edit-principal',
  templateUrl: './edit-principal.component.html',
  styleUrls: ['./edit-principal.component.css']
})

export class EditPrincipalComponent implements OnInit {
  myControl = new FormControl('');
  myControls = new FormControl('');
  message="";
  options: string[] = ['One', 'Two', 'Three'];
  constructor(
    public AutoHelpSeviceManagerService: AutoHelpSeviceManagerService,
    public dialogRef: MatDialogRef<EditPrincipalComponent>,
    public PermissionService:PermissionService,
    @Inject(MAT_DIALOG_DATA) public data: DialogPrincipalData,
  ) { }

  ngOnInit(): void {
    this.PermissionService.getprincipal().subscribe(x=>{this.options = x.map(x => x.replace("COMPAL\\",""))})
    console.log(this.data.deal_with_person_name_list);

  }
  closedialog() {
    this.dialogRef.close(this.data.deal_with_person_name_list);
  }
  add_data() {
    if (this.data.deal_with_person_name_list.split(",").indexOf(this.myControl.value) == -1 && this.data.deal_with_person_name_list.length != 0 && this.myControl.value.length > 0) {
      this.data.deal_with_person_name_list = this.data.deal_with_person_name_list +','+this.myControl.value;
      this.myControl.setValue("");
    }
    else if(this.data.deal_with_person_name_list.split(",").indexOf(this.myControl.value) == -1 && this.data.deal_with_person_name_list.length == 0 && this.myControl.value.length > 0){
      this.data.deal_with_person_name_list = this.myControl.value;
      this.myControl.setValue("");
    }
     //接案人
    if(this.myControls.value.length>0){this.add_orderprincipal();}
    if (this.data.deal_with_person_name_list.split(",").indexOf(this.myControls.value) == -1 && this.data.deal_with_person_name_list.length != 0 && this.myControls.value.length>0) {
      console.log("entry 接案人");
      this.data.deal_with_person_name_list = this.data.deal_with_person_name_list +','+this.myControls.value;
      this.myControls.setValue("");
    }
    else if(this.data.deal_with_person_name_list.split(",").indexOf(this.myControls.value) == -1 && this.data.deal_with_person_name_list.length == 0 && this.myControls.value.length>0){
      this.data.deal_with_person_name_list = this.myControls.value;
      this.myControls.setValue("");
    }
    // if (this.data.deal_with_person_name_list.search(this.myControl.value) == -1 && this.data.deal_with_person_name_list.length != 0) {
    //   this.data.deal_with_person_name_list = this.data.deal_with_person_name_list +','+this.myControl.value;
    //   this.myControl.setValue("");
    // }
    // else if(this.data.deal_with_person_name_list.search(this.myControl.value) == -1 && this.data.deal_with_person_name_list.length == 0){
    //   this.data.deal_with_person_name_list = this.myControl.value;
    //   this.myControl.setValue("");
    // }
    //接案人
    // if(this.data.deal_with_person_name_list.search(this.myControls.value) == -1){this.add_orderprincipal();}
    // if (this.data.deal_with_person_name_list.search(this.myControls.value) == -1 && this.data.deal_with_person_name_list.length != 0) {
    //   this.data.deal_with_person_name_list = this.data.deal_with_person_name_list +','+this.myControls.value;
    //   this.myControls.setValue("");
    // }
    // else if(this.data.deal_with_person_name_list.search(this.myControls.value) == -1 && this.data.deal_with_person_name_list.length == 0){
    //   this.data.deal_with_person_name_list = this.myControls.value;
    //   this.myControls.setValue("");
    // }
  }
  add_orderprincipal(){
    this.AutoHelpSeviceManagerService.AddOrderPrincipal(this.myControls.value,this.data.ID).subscribe(x=>{console.log(x);this.message=x});
  }
  delete_data(){
    if (this.data.deal_with_person_name_list.split(",").indexOf(this.myControl.value) != -1 ) {
        this.data.deal_with_person_name_list = this.data.deal_with_person_name_list.split(",").filter(x => x != this.myControl.value).join(",");
       // this.data.deal_with_person_name_list = this.data.deal_with_person_name_list.replace(this.myControl.value+",","");
       // this.data.deal_with_person_name_list = this.data.deal_with_person_name_list.replace(this.myControl.value,"");
        this.myControl.setValue("");
    }
  }

}
