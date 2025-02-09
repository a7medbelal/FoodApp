

using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.authVIewModel;
using FoodApp.Core.Enums;
using FoodApp.Core.interfaces;
using FoodApp.Core.Entities;
using FoodApp.Core.FoodApp.Service.RoleService;
using FoodApp.Core;

namespace FoodApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleFeature> _roleFeatureRepository;

        public RoleService(IRepository<RoleFeature> repository)
        {
            _roleFeatureRepository = repository;

        }
        public ResponsiveView<bool> addRole(RoleFeatureViewModel roleFeature)
        {
            if (roleFeature == null) return new FailerResView<bool>(Errorcode.emptyData, "ples put data ");

            var RoleFeature = roleFeature.MapTo<RoleFeature>();

            _roleFeatureRepository.Add(RoleFeature);
            _roleFeatureRepository.SaveChanges();
            return new SuccessResView<bool>(true, "add succssufly");
        }



        public ResponsiveView<IEnumerable<features>> GetFeature(Role role)
        {
            if (role == null) return new FailerResView<IEnumerable<features>>(Errorcode.unfoundData, "this role not exits");

            var featurs = _roleFeatureRepository.Get(c => c.role == role).Select(x => x.feature);

            if (featurs.Any()) ;

            return new SuccessResView<IEnumerable<features>>(featurs, "this feature assign to this role");

        }

        public ResponsiveView<bool> HasAccess(Role role, features features)
        {
            var check = _roleFeatureRepository.Get(c => !c.Deleted && c.role == role && c.feature == features).Any();
            if (!check) return new FailerResView<bool>(Errorcode.unfoundData, "this role exits");
            return new SuccessResView<bool>(check, "this feature to this specific role ");
        }
    }
}
