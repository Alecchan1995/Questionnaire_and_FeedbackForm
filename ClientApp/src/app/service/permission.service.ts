import { Injectable } from '@angular/core';
import { environment } from "src/environments/environment";
import { HttpClient } from '@angular/common/http';
import { Ipermission } from "../../interface/Ipermission";
@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor(private http: HttpClient) { }
  getpermission_and_roled(){
    return this.http.get<Ipermission>(`${environment.apiUrl}/Permission/GetRole/`,{withCredentials: true});
  }
getprincipal(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/Permission/GetPrincipal/`,{withCredentials: true});
  }
}
