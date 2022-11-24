using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICaseActivityController
    {
        int Save(CaseActivity updateCase);
        int Update(CaseActivity updateCase);
        List<CaseActivity> GetUpdateCaseList();
    }
    public class CaseActivityControllerImpl : ICaseActivityController
    {
        public List<CaseActivity> GetUpdateCaseList()
        {
            throw new NotImplementedException();
        }

        public int Save(CaseActivity updateCase)
        {
            throw new NotImplementedException();
        }

        public int Update(CaseActivity updateCase)
        {
            throw new NotImplementedException();
        }
    }
}
