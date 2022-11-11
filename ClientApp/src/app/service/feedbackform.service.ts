import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ifeedbackdata } from "../../interface/ifeedbackdata";
import { environment } from "src/environments/environment";
@Injectable({
  providedIn: 'root'
})
export class FeedbackformService {

  constructor(private http: HttpClient) { }
  Sendquestionnairedata(Feedbackdata:Ifeedbackdata){
    return this.http.post(`${environment.apiUrl}/FeedbackformDBfunction/Save`,Feedbackdata, {responseType: "text",withCredentials: true});
  }
  Sendfile(file: FormData){
    return this.http.post(`${environment.apiUrl}/FeedbackformDBfunction/UploadFile`,file, {responseType: "text",withCredentials: true});
  }
  getsystemname(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/FeedbackformDBfunction/GetSystemName`,{withCredentials: true});
  }
  getproblemtype(){
    return this.http.get<Array<string>>(`${environment.apiUrl}/FeedbackformDBfunction/GetProblemType`,{withCredentials: true});
  }
}
//UploadFile
