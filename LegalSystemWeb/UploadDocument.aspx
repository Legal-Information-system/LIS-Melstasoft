<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadDocument.aspx.cs" Inherits="LegalSystemWeb.UploadDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container" id="mainContainer">
        <div class="row justify-content-center">

            <div class="card o-hidden border-0 shadow-lg my-5">
                <div class="card-body">


                    <div class="row mb-3">
                        <div class="col-sm-6" style="display: flex; flex-direction: column">
                            <div style="margin-right: 20px">
                                <asp:Literal ID="Literal16" runat="server" Text="Case No."></asp:Literal>
                            </div>

                            <div>
                                <asp:DropDownList ID="ddlDropCaseNo" runat="server" CssClass="btn btn-primary dropdown-toggle" Width="300px"></asp:DropDownList>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                    ControlToValidate="ddlDropCaseNo" ErrorMessage="Required">*</asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal17" runat="server" Text="Document Type"></asp:Literal>
                            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="btn btn-primary dropdown-toggle" Width="300px"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                ControlToValidate="ddlDocType" ErrorMessage="Required">*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-6" style="display: flex; flex-direction: column">
                            <asp:FileUpload ID="Uploader" runat="server" AllowMultiple="true" />
                        </div>

                    </div>
                    <div class="row mb-3">

                        <div class="col-sm-6">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary btn-user btn-block" BackColor="#212529" BorderColor="#212529" OnClick="btnBack_Click1" />
                            <%--                   <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary btn-user btn-block" />--%>
                            <asp:Button ID="btnSave" runat="server" Text="Create Case" CssClass="btn btn-primary btn-user btn-block" />

                        </div>
                    </div>
                </div>




            </div>

        </div>

        <%--OnClientClick="btnDocUpload_CLick"--%>
    </div>





</asp:Content>
