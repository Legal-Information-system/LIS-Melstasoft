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
                                            <div class="row mb-3">
                                                 <div class="col-2" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:Literal ID="Literal1" runat="server" Text="Case No: "></asp:Literal> 
                                           
                                                </div>
                                                <div class="col-sm-6" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCaseNo" ErrorMessage="Required">*</asp:RequiredFieldValidator> 
                                           
                                                </div>


                                            </div>
                                            <div class="row mb-3">
                                                 <div class="col-2" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:Literal ID="Literal2" runat="server" Text="Lawyer Name: "></asp:Literal> 
                                           
                                                </div>
                                                <div class="col-sm-6" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:DropDownList ID="ddlLawyerName" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLawyerName" ErrorMessage="Required">*</asp:RequiredFieldValidator> 
                                           
                                                </div>

                                            </div>

                                            <div class="row mb-3">
                                                 <div class="col-2" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:Literal ID="Literal5" runat="server" Text="Activities: "></asp:Literal> 
                                           
                                                </div>
                                                <div class="col-sm-6" style="display:flex;flex-direction:column">
                                                       
                                                    <div class="form-check">
                                                      <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault"/>
                                                      <label class="form-check-label" for="flexCheckDefault">
                                                        Activity 1
                                                      </label>
                                                    </div>
                                                    <div class="form-check">
                                                      <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked" />
                                                      <label class="form-check-label" for="flexCheckChecked">
                                                        Activity 2
                                                      </label>
                                                    </div>
                                           
                                                </div>

                                            </div>

                                            <div class="row mb-3">
                                                 <div class="col-2" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:Literal ID="Literal3" runat="server" Text="Total Payable Amount : "></asp:Literal> 
                                           
                                                </div>
                                                <div class="col-sm-6" style="display:flex;flex-direction:row">
                                                       <asp:TextBox  runat="server" CssClass="form-control form-control-user" ID="txtTotalPayableAmount" ></asp:TextBox>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="txtTotalPayableAmount" ErrorMessage="Required" >*</asp:RequiredFieldValidator>  
                                                </div>

                                            </div>

                                            <div class="row mb-3">
                                                 <div class="col-2" style="display:flex;flex-direction:row">
                                                       
                                                    <asp:Literal ID="Literal4" runat="server" Text="Upload Documents / Slips : "></asp:Literal> 
                                           
                                                </div>
                                                <div class="col-sm-6" style="display:flex;flex-direction:row">
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload Document"  CssClass="btn btn-primary btn-user btn-block" />
                                                </div>

                                            </div>

                                            <div class="row mb-3 ">
                                                 <div class="d-flex justify-content-end " style="display:flex;flex-direction:row">
                                                       
                                                    <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="btn btn-primary btn-user btn-block m-2" />
                                                    <asp:Button ID="btnReset" runat="server"  Text="Reset"  CssClass="btn btn-primary btn-user btn-block m-2" />
                                                </div>

                                            </div>

                                    </form>
                                </div>
                            </div>

        </div>
        </div>
</asp:Content>
