using MimeKit.Utils;
using Questionnaire_and_FeedbackForm.Models.Mail;
using System.Net;
namespace Questionnaire_and_FeedbackForm.Models.MailTemplate
{
    public class User_TO_Administrate : BaseMail
    {
        public User_TO_Administrate(SystemFeedbackForm data,List<string> choose_list,string message, IWebHostEnvironment Environment,bool principal_url = false) : base()
        {
            this.FromAddress = data.Fill_In_Person + "@compal.com";
            string wwwPath = Environment.WebRootPath;
            var Set_Body_Head = $@"
                    <html>
                        <head>
                             <style type=""text/css"">
                                table{"{"}
                                border-collapse: collapse;
                                border-radius: 10px;
                                font-family: 微軟正黑體; 
                                font-size: 14px;
                                border: 1px solid #223453;
                               {"}"}
                                tr,
                                td 
                               {"{"}
                                    border: 1px solid #223453;
                                    margin: 0px;
                               {"}"}
                                td
								{"{"}
									table-layout:fixed;
								{"}"}
                                th 
                               {"{"}
                                    font-weight: bold;
                                    background-color:#E9ECF1;
                                    height: 50px;
                               {"}"}
                                    </style>
                        </head>
                        
                    <body >
                            <div style=""font-size: 35px; font-weight:1000; color:#284775;"">系統回饙</div>";
                if(message.Length > 0){
                    Set_Body_Head+= $@"<div style=""font-size:20px; font-weight:1000;"">{message}</div>";
                }
                Set_Body_Head+= $@"<table style=""width:50%; margin-top:1rem;"" >
                            <tr style=""height:35px;"">
                                <th style="" width: 20%;  color:#333333;   min-height: 50px; background-color:#E9ECF1;"">編號</th>
                                <td style=""background-color: #E9ECF1;"">{data.ID}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%; background-color: #F7F6F3; color:#284775;"" >系統名統</th>
                                <td  style="" background-color:#F7F6F3;"">{data.System_Name}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%;  color:#333333; background-color:#E9ECF1;"">提發者</th>
                                <td style=""background-color: #E9ECF1;"">{data.Fill_In_Person}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%; background-color: #F7F6F3; color:#284775;"">種顃</th>
                                <td  style="" background-color:#F7F6F3;"">{data.Problem_Type}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%;  color:#333333; background-color:#E9ECF1;"">時間</th>
                                <td style=""background-color: #E9ECF1;"">{data.Send_Time}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%; background-color: #F7F6F3; color:#284775;"">裝態</th>
                                <td style="" background-color:#F7F6F3;"">{data.deal_with_state}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%;  color:#333333; background-color:#E9ECF1;"">內容</th>
                                <td style=""background-color: #E9ECF1;"">{data.description.Replace("\n","<br>")}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%; background-color: #F7F6F3; color:#284775;"">處理人</th>
                                <td style="" background-color:#F7F6F3;"">{data.Questionnaire.deal_with_person}</td>
                            </tr>
                            <tr>
                                <th style="" width: 20%;  color:#333333; background-color:#E9ECF1;"">附件</th>
                                <td style=""background-color: #E9ECF1;"">";
                foreach(var file_name in data.filename.Split(",")){
                            Set_Body_Head+=$@"<a href=""https://localhost:7262/api/Send_Mail_/Downfile/{file_name}"">{file_name}<br>";
                }
            Set_Body_Head+=$@"</td></tr>";
            if(choose_list.Count() > 0){
                Set_Body_Head+=$@" <tr>
                                    <th style="" width: 20%; background-color: #F7F6F3; color:#284775;"">請選負責人員</th>
                                    <td style="" background-color:#F7F6F3;"">";
                                foreach(var member in choose_list){
                                    if(member.IndexOf(":PM") == -1){
                                            Set_Body_Head +=$@"<a href=""https://localhost:7262/api/Send_Mail_/Send_TO_Member/{member}/{data.ID} "">{member} </a> <br>";
                                    }                                  
                                }
                                Set_Body_Head +=  $@"</td></tr>";
            }
            Set_Body_Head+=$@"</table></body> </html>";
            Body.HtmlBody = Set_Body_Head;
        }

    }
}