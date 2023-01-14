using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ASMWeb.Models
{
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(50)]
		public string FirstName { get; set; }

		[MaxLength(50)]
		public string LastName { get; set; }

		//[MaxLength(30)]
		//public string UserId { get; set; }

		//[MaxLength(50)]
		//public string AppRole { get; set; }
	}
}
