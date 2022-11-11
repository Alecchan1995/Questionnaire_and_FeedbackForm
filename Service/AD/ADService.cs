using Questionnaire_and_FeedbackForm.ViewModels;
using Questionnaire_and_FeedbackForm.Helpers;
namespace Questionnaire_and_FeedbackForm
{
    public class ADService : IADService
    {
        public void GetUsers()
        {
            ADHelper.getADUsers();
        }

        public List<DicVM> GetTPEADUserInfoByADName(string ad, string[] propertyList)
        {
            return ADHelper.getTPEADUserInformation(ad, propertyList);
        }
        public List<DicVM> GetKSADADUserInfoByADName(string ad, string[] propertyList)
        {
            return ADHelper.getKSADADUserInformation(ad, propertyList);
        }

        public List<DicVM> GetADUserInfoById(string id, string[] propertyList)
        {
            return ADHelper.getADUserInformationById(id, propertyList);
        }

        public List<UserWithDepartmentVM> GetADUserInfoByDepartment(string department, string division)
        {
            return ADHelper.getADUserInfoByDepartment(department, division);
        }
        
        public List<DicVM> GetOutlookGroupUser(string groupName, string[] propertyList)
        {
            return ADHelper.getOutlookGroupUser(groupName, propertyList);
        }
    }
}