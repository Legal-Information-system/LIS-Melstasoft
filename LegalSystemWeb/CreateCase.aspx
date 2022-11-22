<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCase.aspx.cs" Inherits="LegalSystemWeb.CreateCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="mainContainer">
        <div class="row justify-content-center">

            <div class="card o-hidden border-0 shadow-lg my-3" style="padding-left: unset; padding-right: unset">
                <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                    <h3 class="text-light text-center bg-dark ">Create Case</h3>
                </div>
                <div class="card-body">

                    <div class="row mb-3">
                        <div class="col-sm-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal1" runat="server" Text="Company"></asp:Literal>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                            <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="ddlCompany" ErrorMessage="Required"  ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-sm-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal2" runat="server" Text="Company Unit"></asp:Literal>
                            <asp:DropDownList ID="ddlCompanyUnit" runat="server" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                            <%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlCompanyUnit" ErrorMessage="Required"  ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <%--=========--%>
                    <div class="form-group row ">
                        <div class="col-md-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal5" runat="server" Text="Nature Of Case"></asp:Literal>
                            <asp:DropDownList ID="ddlNatureOfCase" runat="server" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                            <%--             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="ddlNatureOfCase" ErrorMessage="Required"  ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-6">

                            <asp:Literal ID="ltLastName" runat="server" Text="Case Description"></asp:Literal>
                            <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtCaseDescription" TextMode="MultiLine" Style="margin-top: 5px"></asp:TextBox>
                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txtCaseDescription" ErrorMessage="Required"  ValidationGroup="1" >*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>

                    <%--===========--%>
                    <div class="row mb-3">
                        <div class="col-md-6">

                            <asp:Literal ID="Literal3" runat="server" Text="Claim Amount"></asp:Literal>
                            <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtClaimAmount" Style="margin-top: 5px"></asp:TextBox>
                            <%--               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtClaimAmount" ErrorMessage="Required"  ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>

                    </div>

                    <%--===========--%>


                    <%--===========--%>
                    <div class="row mb-3">
                        <div class="col-md-6">

                            <asp:Literal ID="Literal15" runat="server" Text="Company Side"></asp:Literal>
                            <asp:RadioButtonList ID="rbIsPlantiff" runat="server" RepeatDirection="Horizontal" CssClass="margin-left:10px">
                                <asp:ListItem Value="P" CssClass="form-check-input" Style="margin-right: 50px">Plantiff</asp:ListItem>
                                <asp:ListItem Value="D" CssClass="form-check-input">Difendant</asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                        <div class="col-md-6">

                            <asp:Literal ID="Literal6" runat="server" Text="Other Side"></asp:Literal>
                            <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtDifendant" Style="margin-top: 5px"></asp:TextBox>
                            <%--                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txtDifendant" ErrorMessage="Required"  ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>

                    </div>

                    <%--===========--%>

                    <%--</div>--%>


                    <%--===========--%>
                    <div class="row mb-3">
                        <div class="col-md-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal9" runat="server" Text="Court"></asp:Literal>
                            <asp:DropDownList runat="server" ID="ddlCourt" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                            <%--           <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ControlToValidate="ddlCourt" ErrorMessage="Required"  ValidationGroup="1" >*</asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal10" runat="server" Text="Location"></asp:Literal>
                            <asp:DropDownList runat="server" ID="ddlLocation" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                            <%--                   <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="ddlLocation" ErrorMessage="Required"  ValidationGroup="1" >*</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>



                    <%--===========--%>
                    <div class="row mb-3">
                        <div class="col-md-6">

                            <asp:Literal ID="Literal11" runat="server" Text="Case Number"></asp:Literal>
                            <asp:TextBox runat="server" ID="txtCaseNumber" CssClass="form-control form-control-user" Style="margin-top: 5px"></asp:TextBox>
                            <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                    ControlToValidate="txtCaseNumber" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-md-6">

                            <asp:Literal ID="Literal12" runat="server" Text="Previous Case Number"></asp:Literal>
                            <asp:TextBox runat="server" ID="txtPreCaseNumber" CssClass="form-control form-control-user" Style="margin-top: 5px"></asp:TextBox>


                        </div>
                    </div>

                    <%--===========--%>
                    <div class="row mb-3">
                        <div class="col-md-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal13" runat="server" Text="Assigning Attorney "></asp:Literal>
                            <asp:DropDownList runat="server" ID="ddlAttorney" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                    ControlToValidate="ddlAttorney" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-6" style="display: flex; flex-direction: column">

                            <asp:Literal ID="Literal14" runat="server" Text="Counselor"></asp:Literal>
                            <asp:DropDownList runat="server" ID="ddlCounselor" CssClass="btn btn-primary dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>


                        </div>
                    </div>
                    <%--===========--%>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <asp:Button ID="btnDocUpload" runat="server" Text="Next" OnClientClick="btnDocUpload_CLick" CssClass="btn btn-primary btn-user btn-block" OnClick="btnDocUpload_Click1" />
                        </div>


                    </div>
                </div>

            </div>
            </div>
        </div>
</asp:Content>


