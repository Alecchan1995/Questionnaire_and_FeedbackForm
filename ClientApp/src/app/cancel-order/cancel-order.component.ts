import { Component, OnInit ,Inject} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IPrivateData } from "../../interface/IPrivateData";
import { PrivateDataInit } from "../../interface/PrivateDataInit";
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { AutoHelpSeviceManagerService } from "../service/auto-help-sevice-manager.service";
import {FormControl} from '@angular/forms';
interface Food {
  value: string;
  viewValue: string;
}
@Component({
  selector: 'app-cancel-order',
  templateUrl: './cancel-order.component.html',
  styleUrls: ['./cancel-order.component.css']
})

export class CancelOrderComponent implements OnInit {
  myControl = new FormControl('');
  options: string[] = [];
  datacontentdata : Array<IPrivateData> = [];
  filteredOptions: any = [];
  reason = "";
  choose_data = PrivateDataInit;
  foods: Food[] = [
    {value: 'steak-0', viewValue: 'Steak'},
    {value: 'pizza-1', viewValue: 'Pizza'},
    {value: 'tacos-2', viewValue: 'Tacos'},
  ];
  constructor(public AutoHelpSeviceManagerService:AutoHelpSeviceManagerService,public dialogRef: MatDialogRef<CancelOrderComponent>,@Inject(MAT_DIALOG_DATA) public data: { content: Array<IPrivateData> }) { }

  ngOnInit(): void {
    this.AutoHelpSeviceManagerService.Getprivatedata().subscribe(x =>
    {
        this.datacontentdata = x ;
        this.datacontentdata.forEach(data=>{
          this.options.push(data.ID.toString());
        });
        this.firstfunction();
    });
    this.firstfunction();
  }
  firstfunction(){
      this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }
  closedialog() {
    this.dialogRef.close();
  }
  private _filter(value: string): string[] {
    console.log("----------22222");
    const filterValue = value;
    return this.options.filter(option => option.includes(filterValue));
  }
  choose_you_data(){
      this.choose_data = this.datacontentdata.filter(data=>data.ID == this.myControl.value);
      console.log("this.choose_data",this.choose_data);
  }
}
