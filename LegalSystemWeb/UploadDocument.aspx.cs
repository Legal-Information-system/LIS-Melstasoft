using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class UploadDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CreateCase.aspx");
        }

        protected void btnUploadedFile_Click(object sender, EventArgs e)
        {

            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile uploadFile = uploadFiles[i];
                if (uploadFile.ContentLength > 0)
                {



                    uploadFile.SaveAs(Server.MapPath("~/FilesUploaded/") + uploadFile.FileName);
                    lblListOfUploadedFiles.Text += String.Format("{0}<br />", uploadFile.FileName);
                }
            }


        }
    }
}