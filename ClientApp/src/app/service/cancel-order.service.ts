import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import {CancelOption} from "../../interface/CancelOption";
import { IPrivateData } from "../../interface/IPrivateData";
export interface canceldata{
  no:string,
  data :string ,
 }

@Injectable({
  providedIn: 'root'
})

export class CancelOrderService {

  constructor(private http: HttpClient) { }
  Getprivatedata(){
    return this.http.get<Array<IPrivateData>>(`${environment.apiUrl}/CancelOrderAPI/GetPrivateData`, {withCredentials: true});
  }
  getproblemtype(){
    return this.http.get<Array<CancelOption>>(`${environment.apiUrl}/CancelOrderAPI/GetCancelOrderOption`,{withCredentials: true});
  }
  save_data(no:string,data:string){
    var canceldata:canceldata = {no:no,data:data};
    console.log(canceldata);
    return this.http.post(`${environment.apiUrl}/CancelOrderAPI/SaveData`,canceldata,{responseType: "text",withCredentials: true});
  }
}
