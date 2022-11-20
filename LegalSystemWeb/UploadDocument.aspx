<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadDocument.aspx.cs" Inherits="LegalSystemWeb.UploadDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" id="mainContainer">
        <div class="row justify-content-center">

            <div class="card o-hidden border-0 shadow-lg my-5" style="display: flex; flex-direction: column; justify-content: center; align-items: center">
                <div class="card-body">

                    <div style="margin-bottom: 10px; display: flex; align-items: center">

                        <div style="margin-right: 20px">
                            <asp:Literal ID="Literal16" runat="server" Text="Case No."></asp:Literal>
                        </div>

                        <div>
                            <asp:DropDownList ID="ddlDropCaseNo" runat="server" CssClass="btn btn-primary dropdown-toggle" Width="300px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                ControlToValidate="ddlDropCaseNo" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                        </div>




                    </div>
                    <div class="card" style="width: 500px; height: 300px">
                        <div class="card-header">
                            <h4 class="text-center font-weight-light my-4">Upload Documnet</h4>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-sm-6" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal17" runat="server" Text="Document Type"></asp:Literal>
                                    <asp:DropDownList ID="ddlDocType" runat="server" CssClass="btn btn-primary dropdown-toggle" Width="300px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                        ControlToValidate="ddlDocType" ErrorMessage="Required">*</asp:RequiredFieldValidator>


                                </div>
                                <asp:FileUpload ID="Uploader" runat="server" />
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6">

                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary btn-user btn-block" />
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-primary btn-user btn-block" />
                                </div>




                            </div>
                        </div>


                    </div>
                    <asp:Button ID="btnSave" runat="server" Text="Create Case" CssClass="btn btn-primary btn-user btn-block" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary btn-user btn-block" />
                </div>
            </div>
            <%--OnClientClick="btnDocUpload_CLick"--%>
        </div>
    </div>
    </div>

</asp:Content>
