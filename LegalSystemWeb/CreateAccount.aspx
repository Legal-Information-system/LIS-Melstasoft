<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="LegalSystemWeb.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container" id="mainContainer">
                <div style="display: flex; justify-content: center">

                    <div class="card o-hidden border-0 shadow-lg my-3" style="width: 500px">
                        <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                            <h3 class="text-light text-center bg-dark ">Register User</h3>
                        </div>
                        <div class="card-body">

                            <div class="col my-3 py-1" style="display: flex; flex-direction: column">

                                <asp:Literal ID="Literal1" runat="server" Text="User Name"></asp:Literal>
                                <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtUserName"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUserName" ValidationGroup="1" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>

                            <div class="col my-3 py-1" style="display: flex; flex-direction: column">

                                <asp:Literal ID="Literal5" runat="server" Text="Password"></asp:Literal>
                                <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtPassword" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword" ValidationGroup="1" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtPassword"></asp:RegularExpressionValidator>--%><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                            </div>
                            <div class="col my-3 py-1" style="display: flex; flex-direction: column">
                                <asp:Literal ID="Literal2" runat="server" Text="User Type"></asp:Literal>
                                <asp:DropDownList runat="server" ID="ddlUserType" CssClass="btn btn-default dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>

                            </div>
                            <div class="col my-3 py-1" style="display: flex; flex-direction: column">
                                <asp:Literal ID="Literal3" runat="server" Text="Company Name: "></asp:Literal>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="btn btn-default dropdown-toggle" Style="margin-top: 5px" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompany" ValidationGroup="1" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>

                            <div class="col my-3 py-1" style="display: flex; flex-direction: column">
                                <asp:Literal ID="Literal4" runat="server" Text="Company Unit Name"></asp:Literal>
                                <asp:DropDownList ID="ddlCompanyUnit" runat="server" CssClass="btn btn-default dropdown-toggle w-100" Style="margin-top: 5px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCompanyUnit" ValidationGroup="1" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col d-flex flex-row justify-content-center">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="1" />
                            </div>

                            <div class="col-sm-6 m-3">
                                <asp:Label ID="lblSuccessMsg" runat="server" Text="" ForeColor="#33cc33"></asp:Label>
                            </div>
                            <div class="col-sm-6 m-3">
                                <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>


                            <%--=========--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
