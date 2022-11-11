import { Component, OnInit ,ViewChild} from '@angular/core';
import { AutoHelpSeviceManagerService } from "../service/auto-help-sevice-manager.service";
import { PrivateDataInit } from './../../interface/PrivateDataInit';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AdministratorContentComponent } from '../administrator-content/administrator-content.component';
import {IPrivateData} from "../../interface/IPrivateData";
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { FormControl, Validators } from '@angular/forms';
import { PermissionService } from "../service/permission.service"
import { Router ,ActivatedRoute, ParamMap} from '@angular/router';
@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.css']
})
export class AdministratorComponent implements OnInit {

  constructor(
    public AutoHelpSeviceManagerService: AutoHelpSeviceManagerService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    public PermissionService:PermissionService,
    public router:Router,
   ) { }
  searchpeople ="";
  searchno = "";
  Alldata : IPrivateData[] = [];
  storage_data : IPrivateData[] = [];
  SearchstringForm = new FormControl('')
  displayedColumns: string[] = ['ID','SystemFeedbackForm.Send_Time','SystemFeedbackForm.Fill_In_Person', 'SystemFeedbackForm.Problem_Type', 'SystemFeedbackForm.description', 'deal_with_time', 'deal_with_idea','deal_with_person','principal', 'SystemFeedbackForm.deal_with_state'];
  PrivateDataInits = new MatTableDataSource<IPrivateData>(PrivateDataInit);
  @ViewChild (MatPaginator) paginator!: MatPaginator;
  ngOnInit(): void {
    this.PermissionService.getpermission_and_roled().subscribe(x=>{
      if (x != null) {
      this.check_administrator_permission(x.Role);
    }});
    this.GetAlldata(); // 拿全部data
  }
  GetAlldata(){
    console.log("all data");
    this.AutoHelpSeviceManagerService.Getalldata().subscribe(x => {
      //console.log(x);
      this.Alldata = x; //用來search 資料
      this.set_data_list(x);
      var get_id = this.route.snapshot.queryParamMap.get('id'); // 取一筆資料
      console.log("--route:",this.route.snapshot.queryParamMap.get('id'));
      if (get_id != null ||this.route.snapshot.queryParamMap.get('id') != undefined){
        var data = this.Alldata.filter(x => x.ID == parseInt(get_id!));
        if (data.length > 0){this.openDialog(data[0])};
      }
    })

  }
  GetYourmaintainer(){
    this.AutoHelpSeviceManagerService.GetYourmaintainer()
    .subscribe(x => {
      //console.log(x);
      this.set_data_list(x);
    })
  }
  check_administrator_permission(Role: string) {
    if (Role != 'Administrator') {
      this.router.navigateByUrl('Error');
    }
  }
  set_data_list(data:IPrivateData[]){
    this.PrivateDataInits = new MatTableDataSource<IPrivateData>(data) ;
    this.PrivateDataInits.paginator = this.paginator;
  }
  check(type:string){
    this.storage_data = []; //把他清空
    if(this.searchpeople.length == 0 || this.searchno.length == 0){
     this.set_data_list(this.Alldata) ;
    }
    if(type == 'people'){
      this.Alldata.forEach(x=>{
        if (x.SystemFeedbackForm.Fill_In_Person.search(this.searchpeople) != -1){
          this.storage_data.push(x);
        }
      });
      this.set_data_list(this.storage_data);
    }else if(type =="no"){
       this.Alldata.forEach(x=>{
        if (x.ID == Number(this.searchno)){
          this.storage_data.push(x);
          this.set_data_list(this.storage_data);
          return
        }
      });
    }
    //console.log("===========",this.storage_data);
  }
  openDialog(element:any): void {
    const dialogRef = this.dialog.open(AdministratorContentComponent, {
      width: '40%',
      minHeight: '70%',
      disableClose: true,
      data: {content:element},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.AutoHelpSeviceManagerService.Getalldata().subscribe(x => {
        //console.log(x);
        this.set_data_list(x);
      })
      //this.animal = result;
    });
  }
}

