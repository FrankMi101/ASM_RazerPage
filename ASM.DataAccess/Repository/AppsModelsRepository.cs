
using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class AppsModelsRepository : Repository<AppsModels>, IAppsModelsRepository
    {
        private ApplicationDbContext _db;
        public AppsModelsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //public override void Update(AppsModels obj)
        //{
        //   // _db.ASM_AppModels.Update(obj);

        //    //var objFromDb = _db.ASM_AppModels.FirstOrDefault(u => u.Id == obj.Id);
        //    //objFromDb.DeveloperId = obj.DeveloperId;
        //    //objFromDb.Comments = obj.Comments;
        //    //objFromDb.StartDate = obj.StartDate;
        //    //objFromDb.Owners = obj.Owners;
        //    //_db.SaveChanges();

        //}
        public override object SPName(string action)
        {
            if (action == "") return "dbo.ASM_AppModelList";
            if (action == "Read") return "dbo.ASM_AppModelList";
            if (action == "Edit") return "dbo.ASM_AppModelEdit";
            return action;
        }
    }
}
