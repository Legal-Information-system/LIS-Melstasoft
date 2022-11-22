using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class UserRole
    {
        [DBField("user_role_id")]
        public int RoleId { get; set; }

        [DBField("user_role_name")]
        public string RoleName { get; set; }
    }
}
