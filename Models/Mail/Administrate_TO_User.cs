using MimeKit.Utils;
using Questionnaire_and_FeedbackForm.Models;
using Questionnaire_and_FeedbackForm.Models.Mail;
public class Administrate_TO_User : BaseMail
    {
        public Administrate_TO_User(Questionnaire data,IPModel _ip) : base()
        {
            this.FromAddress = "alecchan1995@gmail.com";
            var Set_Body_Head = $@"
                    <html>
                        <head>
                            <style type=""text/css"">
                                   table {"{"}
                                border-collapse: collapse;
                                border-radius: 10px;
                                {"}"}

                                tr,
                                th,
                                td 
                                {"{"}
                                    border: 3px solid #223453;
                                    margin: 0px;
                                {"}"}

                                td
								{"{"}
									table-layout:fixed;
								{"}"}

                                th 
                                {"{"}
                                    height: 50px;
                                    text-align: center;
                                {"}"}

                                    </style>
                        </head>
                        
                    <body >
                            <div style=""font-size: 35px; font-weight:1000; >【需求處理完成通知】</div><br>
                            <div style=""font-size: 35px; font-weight:1000; >親愛的{data.SystemFeedbackForm.Fill_In_Person}您好:</div>
                            <div >
                            <div style=""font-size: 20px; font-weight:1000; color:blue;"">謝謝使用回饋系統，你的問題已解決，以下是你的回饙內容。<br>記得請幫我們點開一下低部的連結，幫我們填寫一下你的意見，你的意見是非常重要的。</div>
                            <div style=""font-size:50px; font-weight:1000; color:cornflowerblue;"">
                                <span >
                                    意見調查連結:
                                </span><a style=""color:red;"" href='{_ip.Frontweb}/questionnaire/{data.SystemFeedbackForm.ID}/5 '>非常滿意請按［讚］</a></span>
							</div>
                          <table style=""width:50%; margin-top:1rem"" >
                                <tr style=""height:35px;"">
                                    <th style="" width: 20%; background-color: cornflowerblue; color:aliceblue;   min-height: 50px;"">編號</th>
                                    <td>{data.SystemFeedbackForm.ID}</td>
                                </tr>
                                <tr>
                                    <th style="" width: 20%; background-color: cornflowerblue; color:aliceblue;"">系統名統</th>
                                    <td>{data.SystemFeedbackForm.System_Name}</td>
                                </tr>
                                <tr>
                                    <th style="" width: 20%; background-color: cornflowerblue; color:aliceblue;"">處理人</th>
                                    <td>{data.deal_with_person}</td>
                                </tr>
                                <tr>
                                    <th style="" width: 20%; background-color: cornflowerblue; color:aliceblue;"">處理時間</th>
                                    <td>{data.deal_with_time}</td>
                                </tr>
                                <tr>
                                    <th style="" width: 20%; background-color: cornflowerblue; color:aliceblue;"">處理方法</th>
                                    <td>{data.deal_with_idea}</td>
                                </tr>
                            </table>
                            <br>
                            <div style=""font-size:35px; font-weight:1000; color:cornflowerblue;""><span >意見調查連結:</span><a href='{_ip.Frontweb}/questionnaire/{data.SystemFeedbackForm.ID}/5'>HERE</a></span></div>
                            </div>
                            \n\n\n\n HHHHHHHH
                        </body>
                </html>";
            Body.HtmlBody = Set_Body_Head; 
        }
    }