using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.EntityFrameWork.Model
{
    public class ApplicationUser:IdentityUser
    {
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
    }
}
