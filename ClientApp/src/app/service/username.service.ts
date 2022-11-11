import { Injectable } from '@angular/core';
import { shareReplay } from 'rxjs/operators';
import { environment } from "src/environments/environment";
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class UsernameService {

  constructor(private http: HttpClient) { }
  get_user_name(){
    return this.http.get(`${environment.apiUrl}/User_Name`, {responseType: "text",withCredentials: true}).pipe(
      shareReplay(1)
    );
  }
}
