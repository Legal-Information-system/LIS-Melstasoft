using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class UserRolePrivilege
    {
        [DBField("user_role_id")]
        public int UserRoleId { get; set; }

        [DBField("function_id")]
        public int FunctionId { get; set; }
    }
}
