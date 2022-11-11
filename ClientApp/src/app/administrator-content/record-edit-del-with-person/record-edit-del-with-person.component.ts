import { Component, OnInit ,Inject} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AutoHelpSeviceManagerService } from "../../service/auto-help-sevice-manager.service";
declare var WNJsoft: any;
export interface DelWithPerson_record {
  ID :number
  order_id  :number;
  deal_with_persons: string;
  access_time: string;
}
export interface DelWithPerson_recordID {
  ID: number;
}
@Component({
  selector: 'app-record-edit-del-with-person',
  templateUrl: './record-edit-del-with-person.component.html',
  styleUrls: ['./record-edit-del-with-person.component.css']
})

export class RecordEditDelWithPersonComponent implements OnInit {

  constructor(public AutoHelpSeviceManagerService: AutoHelpSeviceManagerService,public dialogRef: MatDialogRef<RecordEditDelWithPersonComponent>, @Inject(MAT_DIALOG_DATA) public data: DelWithPerson_recordID) { }
  dataSource :any = [];
  ngOnInit(): void {
    this.AutoHelpSeviceManagerService.GetDeal_With_Person(this.data.ID).subscribe( x =>
      {
        console.log("x = ",x);
        this.dataSource = x;
        this.dataSource.access_time = new Date(this.dataSource.access_time);
      });
  }
  closedialog() {
  //   var v_file = new WNJsoft.ADPCore.File({
  //     config: new WNJsoft.ADPCore.Config({
  //         viewerURL: 'http://10.110.15.5:80/ADPViewer/wnjp', //viewer的網址,放浮水印範本
  //         dsServerIP: '10.110.15.72', //ds server的ip
  //         dsServerPort: '80',         //ds server的port
  //         paramServerIP: '10.110.15.5',
  //         paramServerPort: '80'
  //         //offlineMode: true              //是否離線模式,離線模式不會把參數放於ds server,適用於舊版ds server或是無ds server

  //     }),   //設定變數
  //     reponame: 'IIS_KM',     //repository
  //     repoparm: "/assets/1.xlsx"
  //     //repoparam: "localhost:44439".replace(/(^\w+:|^)\/\//, '') //檔案路徑和把https://去掉
  // });

  //   v_file.OnlineViewConfirm({     //呼叫 OnlineView
  //       height: 'auto',   /** 確認的視窗參數 **/
  //       width: 450,
  //       title: '安裝閱讀軟體提示',
  //       content: '開啟公告區加密檔，必須先安裝閱讀工具。若未安裝請【<a href="file://10.110.15.55/coe/tools/ADP/install_adp.exe" style="color:blue;">按此處進行安裝</a>】；<br>若已安裝請勾選下方【不要再提醒我】，謝謝！<br>　<br><font color="red">註：請將IE瀏覽器及Adobe Acrobat Reader設成預設值。</font>',
  //       buttonConfirm: '確定',
  //       noPromptLabel: '不要再提醒我',   /** 確認的視窗參數 **/
  //       userid: '',       //使用者名稱
  //       canprint: false,    //指定可否列印
  //   });
    this.dialogRef.close();
  }
  Cancel_data(){

  }
}
