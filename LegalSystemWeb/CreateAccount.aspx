<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="LegalSystemWeb.CreateAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="vh-100 gradient-custom h-100">
      <div class="container py-5 h-100">
        <div class="row justify-content-center align-items-center h-100">
          <div class="col-12 col-lg-9 col-xl-7">
            <div class="card shadow-2-strong card-registration" style="border-radius: 15px;">
              <div class="card-body p-4 p-md-5">
                <h3 class="mb-4 pb-2 pb-md-0 mb-md-5">Registration Form</h3>
                <form>

                  <div class="row mb-3 my-5">
                        <div class="col-2 align-middle" style="display:flex;flex-direction:row">
                                                       
                        <asp:Literal ID="Literal1" runat="server" Text="User Name:  "></asp:Literal> 
                                           
                    </div>
                    <div class="col" style="display:flex;flex-direction:row">
                                                       
                        <asp:TextBox  runat="server" CssClass="form-control form-control-user" ID="txtUserName" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="txtUserName" ErrorMessage="Required" >*</asp:RequiredFieldValidator>  
                                           
                    </div>
                    </div>
                    <div class="row mb-3 my-5">
                        <div class="col-2 align-middle" style="display:flex;flex-direction:row">
                                                       
                        <asp:Literal ID="Literal5" runat="server" Text="Password:  "></asp:Literal> 
                                           
                    </div>
                    <div class="col" style="display:flex;flex-direction:row">
                                                       
                        <asp:TextBox  runat="server" CssClass="form-control form-control-user" ID="txtPassword" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ControlToValidate="txtPassword" ErrorMessage="Required" >*</asp:RequiredFieldValidator>  
                                           
                    </div>
                    </div>
                    <div class="row mb-3 my-5">
                        <div class="col-2 align-middle" style="display:flex;flex-direction:row">
                                                       
                            <asp:Literal ID="Literal2" runat="server" Text="User Type:  "></asp:Literal> 
                                           
                        </div>
                        <div class="col" style="display:flex;flex-direction:row">
                                                       
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="inlineRadioOptions" id="normalUser"
                                  value="option1" />
                                <label class="form-check-label" for="normalUser">Normal User</label>
                              </div>

                              <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="inlineRadioOptions" id="branchManager"
                                  value="option2" />
                                <label class="form-check-label" for="branchManager">Branch Manager</label>
                              </div>

                              <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="inlineRadioOptions" id="financeOfficer"
                                  value="option3" />
                                <label class="form-check-label" for="financeOfficer">Finance Officer</label>
                              </div>  
                                           
                        </div>

                    </div>
                    <div class="row mb-3 my-5">
                        <div class="col-2 align-middle" style="display:flex;flex-direction:row">
                                                       
                            <asp:Literal ID="Literal3" runat="server" Text="Company Name: "></asp:Literal> 
                                           
                        </div>
                        <div class="col" style="display:flex;flex-direction:row">
                                                       
                            <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompanyName" ErrorMessage="Required">*</asp:RequiredFieldValidator> 
                                           
                        </div>
                    </div>
                    <div class="row mb-3 my-5">
                        <div class="col-2 align-middle" style="display:flex;flex-direction:row">
                                                       
                            <asp:Literal ID="Literal4" runat="server" Text="Company Unit Name: "></asp:Literal> 
                                           
                        </div>
                        <div class="col" style="display:flex;flex-direction:row">
                                                       
                            <asp:DropDownList ID="ddlCompanyUnitName" runat="server" CssClass="btn btn-primary dropdown-toggle w-100"></asp:DropDownList>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCompanyUnitName" ErrorMessage="Required">*</asp:RequiredFieldValidator> 
                                           
                        </div>
                    </div>

                 

                  <div class="mt-4 pt-3 text-end">
                    <input class="btn btn-primary btn-lg" type="submit" value="Submit" />
                  </div>

                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
</asp:Content>
