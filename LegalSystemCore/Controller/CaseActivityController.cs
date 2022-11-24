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

    }
}
