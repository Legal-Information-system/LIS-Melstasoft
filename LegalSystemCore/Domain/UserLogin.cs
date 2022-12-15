using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class UserLogin
    {
        [DBField("user_login_id")]
        public int UserId { get; set; }

        [DBField("user_login_name")]
        public string UserName { get; set; }

        [DBField("user_password")]
        public string Password { get; set; }

        [DBField("company_id")]
        public string CompanyId { get; set; }

        [DBField("company_unit_id")]

        public string CompanyUnitId { get; set; }

        [DBField("user_role_id")]
        public int UserRoleId { get; set; }
    }
}
