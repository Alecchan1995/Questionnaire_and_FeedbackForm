import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { IPrivateData } from "../../interface/IPrivateData";
@Injectable({
  providedIn: 'root'
})
export class AutoHelpSeviceManagerService {

  constructor(private http: HttpClient) { }
  data:any;
  Getprivatedata(){
    return this.http.get<Array<IPrivateData>>(`${environment.apiUrl}/AutoHelpSystemDBfunction/GetPrivateData`, {withCredentials: true});
  }
  Getalldata(){
    return this.http.get<Array<IPrivateData>>(`${environment.apiUrl}/AutoHelpSystemDBfunction/GetAllData`, {withCredentials: true});
  }
  GetStateType(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/AutoHelpSystemDBfunction/GetStateType`, {withCredentials: true});
  }
  GetSenddata(data:IPrivateData){
    return this.http.post<Array<string>>(`${environment.apiUrl}/AutoHelpSystemDBfunction/Senddata`,data, {withCredentials: true});
  }
  GetSearchPeople(user:String){
    return this.http.post(`${environment.apiUrl}/AutoHelpSystemDBfunction/GetSearchPeople`,user, {withCredentials: true});
  }
  GetYourmaintainer(){
    return this.http.get<Array<IPrivateData>>(`${environment.apiUrl}/AutoHelpSystemDBfunction/GetYourmaintainer`, {withCredentials: true});
  }
  Downfile(filenames:String){
    var Filename={filename:filenames};
    return this.http.post(`${environment.apiUrl}/AutoHelpSystemDBfunction/Downfile`, Filename,{observe:'response',responseType:"blob" as "json",withCredentials: true});
  }
  GetDeal_With_Person(Id:number){
    return this.http.get(`${environment.apiUrl}/AutoHelpSystemDBfunction/GetDeal_With_Person/${Id}`, {withCredentials: true });
  }
  Check_User_Name(name:string){
    return this.http.get<Array<string>>(`${environment.apiUrl}/AutoHelpSystemDBfunction/Check_User_Name/${name}`, { withCredentials: true});
  }
}

