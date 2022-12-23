import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
@Injectable({
  providedIn: 'root'
})
export class EditsystemdataService {

  constructor(private http: HttpClient) { }
  getSystemname(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/GETSystemName`, {withCredentials: true});
  }
  addSystemname(newsystemname:string){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/AddSystemName/`+newsystemname, {withCredentials: true});
  }
  deleteSystemname(systemname:string){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/DeleteSystemName/`+systemname, {withCredentials: true});
  }
  getSystemmember(systemname:string){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/GETSystemmember/`+systemname, {withCredentials: true});
  }
  getmemberrole(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/GETMemberrole`, {withCredentials: true});
  }
  addmember(systemname:string = "", member:string="", role:string=""){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/AddMember/`+systemname+`/`+role+`/`+member, {withCredentials: true});
  }
  deletemember(systemname:string = "", member:string="", role:string=""){
    return this.http.get<Array<string>>(`${environment.apiUrl}/EditSystemDataApi/DeleteMember/`+systemname+`/`+role+`/`+member, {withCredentials: true});
  }
  getemploy(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/Permission/GetPrincipal`, {withCredentials: true});
  }
  addemploy(name:string = ""){
    return this.http.get<Array<string>>(`${environment.apiUrl}/Permission/AddPrincipal/`+name, {withCredentials: true});
  }
  deleteemploy(name:string = ""){
    return this.http.get<Array<string>>(`${environment.apiUrl}/Permission/DeletePrincipal/`+name, {withCredentials: true});
  }
}
