using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;

namespace AlmondWeb.BusinessLayer
{
    public class ProfileManager : BaseManager<ProfileTable>
    {
        public int CreateProfil(int? ownerId)
        {
            ProfileTable newProfile = new ProfileTable();
            UserManager userManager = new UserManager();
            AlmondUserTable user = userManager.FindwithExpression(x => x.Id == ownerId);
            int result = Insert(new ProfileTable
            {
                Id = ownerId.Value
            });
            return result;
        }
    }
}
