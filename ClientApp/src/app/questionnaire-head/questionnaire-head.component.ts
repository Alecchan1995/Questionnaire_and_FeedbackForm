import { ConditionalExpr } from '@angular/compiler';
import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { PermissionService } from "../service/permission.service"
import { MatDialog} from '@angular/material/dialog';
import { Ipermission } from "../../interface/Ipermission";
import { CancelOrderComponent } from '../cancel-order/cancel-order.component';
@Component({
  selector: 'app-questionnaire-head',
  templateUrl: './questionnaire-head.component.html',
  styleUrls: ['./questionnaire-head.component.css']
})
export class QuestionnaireHeadComponent implements OnInit {
  administrator_switch: boolean = false;
  @Input() User_Name: string | undefined;
  constructor(private router: Router,public dialog: MatDialog, private PermissionService: PermissionService) { }
  ngOnInit(): void {
    this.PermissionService.getpermission_and_roled()
      .subscribe(x => {
        if (x != null) {
          this.check_administrator_permission(x.Role);
          console.log(x);
        }
      })
  }
  ngOnChanges(changes: SimpleChanges,) {
    if (changes["User_Name"]) {
      this.User_Name = changes["User_Name"].currentValue;
    }
  }
  URL_to_Administrator() {
    this.router.navigateByUrl('Administrator');
  }
  URL_to_mian() {
    this.router.navigateByUrl('');
  }
  check_administrator_permission(Role: string) {
    if (Role == 'Administrator') {
      this.administrator_switch = true;
    }
  }
  openCancelOrderComponent(): void {
    const dialogRef = this.dialog.open(CancelOrderComponent, {
      width: '40%',
      minHeight: '70%',
      disableClose: true,
      data: {content:"hi"},
    });
  }
}
