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
  }
  closedialog() {
    this.dialogRef.close(this.data.deal_with_person_name_list);
  }
  add_data() {
    if(this.myControls.value.length>0){
      this.AutoHelpSeviceManagerService.Check_User_Name(this.myControls.value).subscribe(x => {
        if (x[0] == "Error")
          {
            this.message = "帳號錯誤，請重填寫。";
          }
        else
          {
            this.message = "更新成功，請記得按下傳送/保存";
            this.data.deal_with_person_name_list = this.myControls.value;
          }
      })
    }
    this.myControl.setValue("");
  }
  delete_data(){
    if (this.data.deal_with_person_name_list.split(",").indexOf(this.myControl.value) != -1 ) {
        this.data.deal_with_person_name_list = this.data.deal_with_person_name_list.split(",").filter(x => x != this.myControl.value).join(",");
        this.myControl.setValue("");
    }
  }

}
