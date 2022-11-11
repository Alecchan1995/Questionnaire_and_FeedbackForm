import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Iquestionnairedata } from "src/interface/iquestionnairedata";


@Injectable({
  providedIn: 'root'
})


export class QuestionnaireService {

  constructor(private http: HttpClient) { }

  Sendquestionnairedata(QuestionnaireDatas:Iquestionnairedata){
    return this.http.post(`${environment.apiUrl}/QuestionDBfunction/Save`,QuestionnaireDatas, {responseType: "text",withCredentials: true});
  }

  getquestiondata(ID:number){
    return this.http.get<Iquestionnairedata>(`${environment.apiUrl}/QuestionDBfunction/GetQuestionnaireForm/${ID}`,{withCredentials: true});
  }
}
