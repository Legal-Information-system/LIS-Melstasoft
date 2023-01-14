using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public partial class UserPrivilege
    {
        [DBField("user_login_id")]
        public int UserLoginId { get; set; }

        [DBField("is_grant_revoke")]
        public int IsGrantRevoke { get; set; }

        [DBField("function_id")]
        public int FunctionId { get; set; }
    }
}
