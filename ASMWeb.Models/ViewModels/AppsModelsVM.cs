using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ASMWeb.Models.ViewModels
{
    public class AppsModelsVM
    {
       public AppsModels AppsModels { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> DeveloperList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AccessMethodList { get; set; }
    }
}
