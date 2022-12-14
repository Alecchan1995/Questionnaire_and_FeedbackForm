using Questionnaire_and_FeedbackForm.ViewModels;
using System.Diagnostics;
using System.DirectoryServices;

namespace Questionnaire_and_FeedbackForm.Helpers
{
    public static class ADHelper
    {
        private static string TPEQuery = "LDAP://tpedcs01.compal.com/OU=Users,OU=OU-TPE,DC=compal,DC=com";
        private static string TPEOutlookGroupTPEQuery = $"LDAP://tpedcs01.compal.com/OU=Organization DL,OU=DL Group,OU=OU-TPE,DC=compal,DC=com";
        private static string KSADQuery = "LDAP://KSPDCS04.gi.compal.com/OU=TW Managers,OU=Users,OU=OU-CET,DC=gi,DC=compal,DC=com";
        private static string KSADOutlookGroupQuery = $"LDAP://KSPDCS04.gi.compal.com/OU=Organization-DL,OU=DL Group,OU=OU-CET,DC=gi,DC=compal,DC=com";
        private static string CQADQuery = "LDAP://KSPDCS04.gi.compal.com/OU=Local Users,OU=Users,OU=OU-CQ,DC=gi,DC=compal,DC=com";
        private static string CQADOutlookGroupQuery = $"LDAP://KSPDCS04.gi.compal.com/OU=Organization-DL,OU=DL Group,OU=OU-CQ,DC=gi,DC=compal,DC=com";
        public static void getADUsers()
        {
            DirectoryEntry de = new DirectoryEntry(TPEQuery);
            DirectorySearcher ds = new DirectorySearcher(de);
            Dictionary<string, List<string>> InfoDic = new Dictionary<string, List<string>>();

            ds.Filter = "(objectClass = user)";
            ds.PropertiesToLoad.Add("SAMAccountName");
            var all = ds.FindAll();
            foreach (SearchResult sr in all)
            {
                Debug.WriteLine(sr.Properties["SAMAccountName"][0].ToString());
            }
        }

        public static List<DicVM> getTPEADUserInformation(string adName, string[] propertyList)
        {
            return searchSingle(TPEQuery, "SAMAccountName", adName, propertyList);
        }
        public static List<DicVM> getKSADADUserInformation(string adName, string[] propertyList)
        {
            return searchSingle(KSADQuery, "SAMAccountName", adName, propertyList);
        }
        public static List<DicVM> getADUserInformationById(string id, string[] propertyList)
        {
            return searchSingle(TPEQuery, "employeeID", id, propertyList);
        }
        
        public static List<UserWithDepartmentVM> getADUserInfoByDepartment(string department, string division)
        {
            DirectoryEntry de = new DirectoryEntry(TPEQuery);
            DirectorySearcher ds = new DirectorySearcher(de);
            List<UserWithDepartmentVM> InfoDic = new List<UserWithDepartmentVM>();

            var filterStr = "(department=" + department.Trim();
            if(division != null)
                filterStr += " \\ " + division.Trim();
            
            ds.Filter = filterStr + ")";

            ds.PropertiesToLoad.Add("SAMAccountName");
            ds.PropertiesToLoad.Add("department");
            ds.PropertiesToLoad.Add("cn");
            ds.PropertiesToLoad.Add("distinguishedName");
            ds.PropertiesToLoad.Add("employeeID");

            var all = ds.FindAll();

            foreach(SearchResult sr in all)
            {
                InfoDic.Add(new UserWithDepartmentVM(){
                    Name = sr.Properties["SAMAccountName"][0].ToString(),
                    Department = sr.Properties["department"][0].ToString(),
                    OutlookCN = sr.Properties["distinguishedName"][0].ToString(),
                    EmployeeID = sr.Properties["employeeID"].Count == 0 ? "": sr.Properties["employeeID"][0].ToString(),
                    ADLocation = "TPM",
                });
            }

            return InfoDic;
        }

        public static List<DicVM> getOutlookGroupUser(string groupName, string[] propertyList)
        {
            return searchSingle(TPEOutlookGroupTPEQuery, "cn", groupName, propertyList);
        }

        private static List<DicVM> searchSingle(string Query, string filterFieldName, string searchStr, string[] propertyList)
        {
            DirectoryEntry de = new DirectoryEntry(Query);
            DirectorySearcher ds = new DirectorySearcher(de);
            List<DicVM> InfoDic = new List<DicVM>();

            // ?????????????????????????????? [?????????????????????] ???????????????, ??????????????? 0 hr 0 min 60s
            ds.ClientTimeout = new TimeSpan(0, 0, 60);
            // ?????????????????????????????????????????????????????????
            ds.PageSize = 1000;
            // ??????????????????????????????????????? [?????????????????????] ?????????????????????
            ds.ServerPageTimeLimit = new TimeSpan(0, 0, 60);
            // ?????????????????????????????????????????????????????? (LDAP) ???????????????????????????
            ds.Filter = "(" + filterFieldName + "=" + searchStr +")";
            // ??????????????????????????????????????????????????????
            ds.PropertiesToLoad.AddRange(propertyList);

            // ???????????????????????????????????????????????????????????????????????????
            SearchResult sr = ds.FindOne();

            if(sr != null)
            {
                foreach(var getPropertyKey in propertyList)
                {
                    var propValue = sr.Properties[getPropertyKey];
                    var list = new List<string>{};

                    foreach(var v in propValue)
                    {
                        list.Add(v.ToString());
                    }

                    InfoDic.Add(new DicVM(){
                        Property = getPropertyKey,
                        Value = list
                    });
                }
            }

            return InfoDic;
        }

    }
}