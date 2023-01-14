
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM.DataAccess.Repository
{
    public class CommonList
    {
        public static IEnumerable<SelectListItem> AppsList(IAppServices _action)
        {
            return _action.Apps.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.AppName,
                     Value = d.AppID
                 });
        }
        public static IEnumerable<SelectListItem> AppsModelList(IAppServices _action)
        {
            return _action.AppModels.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.ModelName,
                     Value = d.ModelID
                 });
        }
        public static IEnumerable<SelectListItem> UserRoleList(IAppServices _action)
        {
            var para = new { Operate = "UserRole", Para1 = "20202021" };
            string sp = GetList_SPName(""); 
            return GeneralList(_action, sp, para);
        }
        public static IEnumerable<SelectListItem> AreaList(IAppServices _action)
        {
            return _action.Areas.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.AreaCode,
                     Value = d.AreaCode
                 });
        }

        public static IEnumerable<SelectListItem> DeveloperList(IAppServices _action)
        {
            return _action.Developers.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.UserName,
                     Value = d.UserID
                 });
        }
        public static IEnumerable<SelectListItem> DistrictList(IAppServices _action)
        {
            return _action.Districts.GetAll().Select(
                 d => new SelectListItem
                 {
                     Text = d.DistrictName,
                     Value = d.DistrictCode
                 });
        }
        public static IEnumerable<SelectListItem> GrantTypeList(IAppServices _action)
        {
            return _action.GrantTypes.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.TypeCode,
                       Value = d.TypeCode
                   });
        }
        public static IEnumerable<SelectListItem> GrantGroupList(IAppServices _action)
        {
            return _action.GrantGroups.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.GroupCode,
                       Value = d.GroupCode
                   });
        }
        public static IEnumerable<SelectListItem> GrantValueList(IAppServices _action, string GroupType)
        {
            var para = new { Operate = "GrantValue", Para1 = GroupType };
            string sp = GetList_SPName("");
            return GeneralList(_action, sp, para);
        }
        public static IEnumerable<SelectListItem> PanelList(IAppServices _action)
        {
            return _action.SchoolTypes.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.TypeName,
                       Value = d.TypeCode
                   });
        }
        public static IEnumerable<SelectListItem> PermissionList(IAppServices _action)
        {
            return _action.Permissions.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.PermissionType,
                       Value = d.PermissionType
                   });
        }

        public static IEnumerable<SelectListItem> PrincipalList(IAppServices _action)
        {
            return _action.Principals.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.PrincipalName,
                       Value = d.PrincipalID
                   });
        }
        public static IEnumerable<SelectListItem> ScopeList(IAppServices _action)
        {
            return _action.Scopes.GetAll().Select(
                   d => new SelectListItem
                   {
                       Text = d.AccessScope,
                       Value = d.AccessScope
                   });
        }
        public static IEnumerable<SelectListItem> SchoolList(IAppServices _action)
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

        public static List<SelectListItem> GradeList(IAppServices _action)
        {
            var para = new { Operate = "Geade", Para1 = "20202021", Para2 = "0204" };
            string sp = GetList_SPName(""); ;
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> SearchList(IAppServices _action)
        {
            var para = new { Operate = "SearchSchoolby", Para1 = "20202021" };
            string sp = GetList_SPName(""); ;
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> SchoolList(IAppServices _action, string panel)
        {
            var para = new { Operate = "SchoolList", Para1 = "20202021", Para2 = panel };
            string sp = GetList_SPName(""); 
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> PositionTypeList(IAppServices _action)
        {
            var para = new { Operate = "PositionType", Para1 = "20202021"  };
            string sp = GetList_SPName("");
            return GeneralList(_action, sp, para);
        }
        public static List<SelectListItem> SchoolListDynamicPara(IAppServices _action, string panel)
        {
            var para = new DynamicParameters();

            para.Add("@Operate", "SchoolList");
            para.Add("@Para1", "20202021");
            para.Add("@Para2", panel);

            string sp = GetList_SPName(""); 
            return GeneralList(_action, sp, para);
        }
        private static List<SelectListItem> GeneralList(IAppServices _action, string spName, object para)
        {
            string sp = GetList_SPName(spName); 

            return _action.SP_Call.ReturnList<SelectListItem>(sp, para).ToList();

        }
        private static List<SelectListItem> GeneralList(IAppServices _action, string sp, DynamicParameters para)
        {
            return _action.SP_Call.ReturnList<SelectListItem>(sp, para).ToList();

        }
        //private static string GetSpNamebyType(string listType)
        //{
        //    return CommomListSP;
        //}
        private static string GetList_SPName(string spName)
        {
           if (spName == "")
                return "dbo.ASM_CommonList";
           else
                return spName;  
        }
    }

}
