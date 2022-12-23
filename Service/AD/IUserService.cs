using Questionnaire_and_FeedbackForm.ViewModels;

namespace Questionnaire_and_FeedbackForm
{
    public interface IUserService
    {
        void GetUsers();
        List<DicVM> GetTPEADUserInfoByADName(string ad, string[] propertyList);
        List<DicVM> GetKSADADUserInfoByADName(string ad, string[] propertyList);
        List<DicVM> GetADUserInfoById(string id, string[] propertyList);
        List<UserWithDepartmentVM> GetADUserInfoByDepartment(string department, string division);
        List<DicVM> GetOutlookGroupUser(string groupName, string[] propertyList);
    }
}