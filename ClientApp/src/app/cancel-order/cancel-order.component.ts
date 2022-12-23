import { Component, OnInit ,Inject, SimpleChanges} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IPrivateData } from "../../interface/IPrivateData";
import { PrivateDataInit } from "../../interface/PrivateDataInit";
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import {CancelOrderService} from "../service/cancel-order.service";
import {CancelOption} from "../../interface/CancelOption";
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-cancel-order',
  templateUrl: './cancel-order.component.html',
  styleUrls: ['./cancel-order.component.css']
})

export class CancelOrderComponent implements OnInit {
  myControl = new FormControl('');
  options: string[] = [];
  datacontentdata : Array<IPrivateData> = [];
  filteredOptions :any = [];
  Canceloptions :Array<CancelOption> =[];
  reason = "";
  no="";
  choose_data = PrivateDataInit;
  constructor(
    public dialogRef: MatDialogRef<CancelOrderComponent>,
    public CancelOrderServices:CancelOrderService) { }

  ngOnInit(): void {
    this.CancelOrderServices.Getprivatedata().subscribe(x =>
    {
        this.datacontentdata = x ;
        this.datacontentdata.forEach(data=>{
          this.options.push(data.ID.toString());
        });
        this.firstfunction();
    });
    this.firstfunction();
    this.CancelOrderServices.getproblemtype().subscribe(x => {this.Canceloptions = x;})
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
    const filterValue = value;
    this.myControl.valueChanges.subscribe(data => console.log(data));
    return this.options.filter(option => option.includes(filterValue));
  }
  choose_you_data(){
      if(this.myControl.value.length ==0 ){return}
  }
  showdata(){
    //console.log("show data",this.reason);
  }
  ngOnChanges(changes: SimpleChanges) {
    console.log("Change");
    // changes.prop contains the old and the new value...
  }
  send_data(){
    this.CancelOrderServices.save_data(this.myControl.value,this.reason).subscribe(x=>{alert("successful");this.closedialog();})
  }
}
