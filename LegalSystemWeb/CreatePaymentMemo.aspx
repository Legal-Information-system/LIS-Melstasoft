<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePaymentMemo.aspx.cs" Inherits="LegalSystemWeb.CreatePaymentMemo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mw-100 my-4">
                        <div class="row justify-content-center">
                            
                                <div class="card o-hidden border-0 shadow-lg p-0">
                                    <div class="card-header"><h3 class="text-center font-weight-light my-4">Create Payment Invoice</h3></div>
                                    <div class="card-body m-5">
                                        <form class="user" action="">
                                            <div class="d-flex flex-row my-5">
                                                <div class="px-2 w-25" style="display:flex;flex-direction:column">
                                                    <h4 class="font-weight-light min-vw-400">Case No : </h4>                                                                
                                                    
                                                </div>
                                                <div class="ml-4 w-50" style="display:flex;flex-direction:column; min-width:300px;"> 
                                                         <asp:DropDownList ID="ddlCompanyUnit" runat="server" CssClass="btn btn-primary dropdown-toggle"></asp:DropDownList>    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlCompanyUnit" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                                                   
                                                </div>
                                               </div>
                                            <div class="d-flex flex-row my-5">
                                                <div class="px-2 w-25" style="display:flex;flex-direction:column">
                                                    <h4 class="font-weight-light">Lawyer Name : </h4>                                                                
                                                    
                                                </div>
                                                <div class="ml-4 w-50" style="display:flex;flex-direction:column; min-width:300px;"> 
                                                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="btn btn-primary dropdown-toggle"></asp:DropDownList>    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="ddlCompanyUnit" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                                                   
                                                </div>
                                               </div>
                                                         <%--=========--%>


                                            <%--===========--%>
                                             <div class="d-flex flex-row my-5">
                                                    <div class="px-2 w-25" style="display:flex;flex-direction:column">
                                                        <h4 class="font-weight-light min-vw-400">Total Payable Amount : </h4>                                                                
                                                    
                                                    </div>
                                                    
                                                    <div class="ml-4 w-50" style="display:flex;flex-direction:column; min-width:300px;">
                                                                       
                                                       <asp:TextBox  runat="server" CssClass="form-control form-control-user" ID="txtClaimAmount" ></asp:TextBox>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtClaimAmount" ErrorMessage="Required" >*</asp:RequiredFieldValidator>

                                                    </div>
                                                   
                    
                                               </div>

                                             <div class="d-flex flex-row my-5">
                                                 <div class="px-2 w-25" style="display:flex;flex-direction:column">
                                                        <h4 class="font-weight-light min-vw-400">Upload Documents / Slips : </h4>                                                                
                                                    
                                                    </div>
                                                  <div class="ml-4 w-25" style="display:flex;flex-direction:column; min-width:150px;">
                                                        <asp:Button ID="btnUpload" runat="server" Text="Upload Document"  CssClass="btn btn-primary btn-user btn-block" />
                                                  </div>

                                             </div>
                               
                                            <div class="d-flex flex-row my-5">
                                                <div class="mx-5 px-5 w-50 " style="display:flex;flex-direction:column;">
                                                    <div class="w-30 text-end my-5" style="min-width:150px;">
                                                        <asp:Button ID="btnSave" runat="server" Text="Create Case"  CssClass="btn btn-primary btn-user btn-block btn-lg px-5" />
                                                    </div>
                                                </div>
                                                <div class="mx-5 px-5 w-50" style="display:flex;flex-direction:column;">
                                                    <div class="w-30 test-start my-5" style="min-width:150px;">   
                                                        <asp:Button ID="btnReset" runat="server"  Text="Reset"  CssClass="btn btn-primary btn-user btn-block btn-lg px-5" />
                                                    </div>
                                                </div>

                                    </div>
                                    </form>
                                </div>
                            </div>

        </div>
        </div>
</asp:Content>
