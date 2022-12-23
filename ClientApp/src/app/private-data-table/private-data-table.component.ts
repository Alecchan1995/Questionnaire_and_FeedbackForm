import { PrivateDataInit } from './../../interface/PrivateDataInit';
import { Component, OnInit ,ViewChild} from '@angular/core';
import { AutoHelpSeviceManagerService } from "../service/auto-help-sevice-manager.service";
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {IPrivateData} from "../../interface/IPrivateData";
import { AdministratorContentComponent } from '../administrator-content/administrator-content.component';
import { MatDialog} from '@angular/material/dialog';
import { PermissionService } from "../service/permission.service"
@Component({
  selector: 'app-private-data-table',
  templateUrl: './private-data-table.component.html',
  styleUrls: ['./private-data-table.component.css']
})
export class PrivateDataTableComponent implements OnInit {

  constructor(public AutoHelpSeviceManagerService:AutoHelpSeviceManagerService,public dialog: MatDialog,) { }
  administrator_switch: boolean = false;
  PrivateDataInits = new MatTableDataSource<IPrivateData>(PrivateDataInit);
  @ViewChild (MatPaginator) paginator!: MatPaginator;
  displayedColumns: string[] = ['ID','SystemFeedbackForm.Send_Time', 'SystemFeedbackForm.Problem_Type', 'SystemFeedbackForm.description', 'deal_with_time','deal_with_idea','deal_with_person','principal','SystemFeedbackForm.deal_with_state'];
  ngOnInit(): void {
    this.AutoHelpSeviceManagerService.Getprivatedata().subscribe(x =>
      {
        this.PrivateDataInits = new MatTableDataSource<IPrivateData>(x) ;
        this.PrivateDataInits.paginator = this.paginator;
      });
  }
  openDialog(element:any): void {
    const dialogRef = this.dialog.open(AdministratorContentComponent, {
      width: '40%',
      minHeight: '70%',
      disableClose: true,
      data: {content:element},
    });

    dialogRef.afterClosed().subscribe(result => {
      //console.log('The dialog was closed');
      this.AutoHelpSeviceManagerService.Getalldata().subscribe(x => {
        //console.log(x);
        //this.set_data_list(x);
      })
      //this.animal = result;
    });
  }
}
