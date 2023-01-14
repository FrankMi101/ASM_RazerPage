using ASMWeb.DataAccess.Repository;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class CommonList
    {
        public static IEnumerable<SelectListItem> AppsList(IAppActionsCatalog _action)
        {
            return _action.Apps.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.AppName,
                     Value = d.AppID
                 });
        }
        public static IEnumerable<SelectListItem> AppsModelList(IAppActionsCatalog _action)
        {
            return _action.AppModels.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.ModelName,
                     Value = d.ModelID
                 });
        }
        public static IEnumerable<SelectListItem> AreaList(IAppActionsCatalog _action)
        {
            return _action.Areas.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.AreaCode,
                     Value = d.AreaCode
                 });
        }

        public static IEnumerable<SelectListItem> DeveloperList(IAppActionsCatalog _action)
        {
            return _action.Developers.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.UserName,
                     Value = d.UserID
                 });
        }
        public static IEnumerable<SelectListItem> DistrictList(IAppActionsCatalog _action)
        {
            return _action.Districts.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.DistrictName,
                     Value = d.DistrictCode
                 });
        }
        public static IEnumerable<SelectListItem> GrantTypeList(IAppActionsCatalog _action)
        {
            return _action.GrantTypes.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.TypeCode,
                       Value = d.TypeCode
                   });
        }
        public static IEnumerable<SelectListItem> PanelList(IAppActionsCatalog _action)
        {
            return _action.SchoolTypes.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.TypeName,
                       Value = d.TypeCode
                   });
        }
        public static IEnumerable<SelectListItem> PermissionList(IAppActionsCatalog _action)
        {
            return _action.Permissions.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.PermissionType,
                       Value = d.PermissionType
                   });
        }

        public static IEnumerable<SelectListItem> PrincipalList(IAppActionsCatalog _action)
        {
            return _action.Principals.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.PrincipalName,
                       Value = d.PrincipalID
                   });
        }
        public static IEnumerable<SelectListItem> ScopeList(IAppActionsCatalog _action)
        {
            return _action.Scopes.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.AccessScope,
                       Value = d.AccessScope
                   });
        }
        public static IEnumerable<SelectListItem> SchoolList(IAppActionsCatalog _action)
        {
            return _action.Schools.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.UnitName,
                       Value = d.UnitCode
                   });
        }

        public static List<SelectListItem> SearchByList()
        {

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text = "Panel", Value = "Panel", Selected = true });
            selectListItems.Add(new SelectListItem { Text = "Area", Value = "Area" });
            selectListItems.Add(new SelectListItem { Text = "District", Value = "District" });
            return selectListItems;
        }

        public static List<SelectListItem> GradeList(IAppActionsCatalog _action)
        {
            var para = new { Operate = "Geade", Para1 = "20202021", Para2 = "0204" };
            string sp = "dbo.ASM_CommonList";
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> SearchList(IAppActionsCatalog _action)
        {
            var para = new { Operate = "SearchSchoolby", Para1 = "20202021" };
            string sp = "dbo.ASM_CommonList";
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> SchoolList(IAppActionsCatalog _action, string panel)
        {
            var para = new { Operate = "SchoolList", Para1 = "20202021", Para2 = panel };
            string sp = "dbo.ASM_CommonList";
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> SchoolListDynamicPara(IAppActionsCatalog _action, string panel)
        {
            var para = new DynamicParameters();

            para.Add("@Operate", "SchoolList");
            para.Add("@Para1", "20202021");
            para.Add("@Para2", panel);

            string sp = "dbo.ASM_CommonList";
            return GeneralList(_action, sp, para);
        }
        private static List<SelectListItem> GeneralList(IAppActionsCatalog _action, string sp, object para)
        {
            return _action.SP_Call.ReturnList<SelectListItem>(sp, para).ToList();

        }
        private static List<SelectListItem> GeneralList(IAppActionsCatalog _action, string sp, DynamicParameters para)
        {
            return _action.SP_Call.ReturnList<SelectListItem>(sp, para).ToList();

        }
        private static string GetSpNamebyType(string listType)
        {
            return "dbo.ASM_CommonList";
        }
    }

}
