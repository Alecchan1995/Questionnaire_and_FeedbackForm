using Questionnaire_and_FeedbackForm.ViewModels;
using Questionnaire_and_FeedbackForm.Helpers;
namespace Questionnaire_and_FeedbackForm
{
    public class UserService : IUserService
    {
        public void GetUsers()
        {
            UserHelper.getADUsers();
        }

        public List<DicVM> GetTPEADUserInfoByADName(string ad, string[] propertyList)
        {
            return UserHelper.getTPEADUserInformation(ad, propertyList);
        }
        public List<DicVM> GetKSADADUserInfoByADName(string ad, string[] propertyList)
        {
            return UserHelper.getKSADADUserInformation(ad, propertyList);
        }

        public List<DicVM> GetADUserInfoById(string id, string[] propertyList)
        {
            return UserHelper.getADUserInformationById(id, propertyList);
        }

        public List<UserWithDepartmentVM> GetADUserInfoByDepartment(string department, string division)
        {
            return UserHelper.getADUserInfoByDepartment(department, division);
        }
        
        public List<DicVM> GetOutlookGroupUser(string groupName, string[] propertyList)
        {
            return UserHelper.getOutlookGroupUser(groupName, propertyList);
        }
    }
}