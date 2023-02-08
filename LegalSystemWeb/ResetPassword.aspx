<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="LegalSystemWeb.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="card o-hidden border-0 shadow-lg my-3">
        <div class="card-header d-flex align-items-center justify-content-center" style="height: 5%">
            <h5 class="text-center  mt-3 mb-3">User Privileges</h5>
        </div>

        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row mb-3 ms-1 mt-3">
                        <div class="col">

                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Literal ID="lblUser" runat="server" Text="Select User"></asp:Literal>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="btn btn-outline-dark dropdown-toggle form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Literal ID="ltlUserType" runat="server" Text="Current User Type"></asp:Literal>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblUserType" runat="server" Text="&nbsp " CssClass="form-control form-control-user"></asp:Label>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Literal ID="Literal3" runat="server" Text="New Password"></asp:Literal>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtPassword" Style="margin-top: 5px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Literal ID="Literal1" runat="server" Text="Re-Type Password"></asp:Literal>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtRetypePassword" Style="margin-top: 5px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Label ID="lblError" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnSave" runat="server" Text="Reset" CssClass="btn btn-primary btn-user btn-block" Style="width: 100%;" OnClick="btnSave_Click" />
                                </div>

                            </div>
                        </div>

                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
