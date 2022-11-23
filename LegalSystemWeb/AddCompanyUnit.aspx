<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCompanyUnit.aspx.cs" Inherits="LegalSystemWeb.AddCompanyUnit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <div class="card-header d-flex align-items-center justify-content-center" style="height: 50px">
            <h2 style="text-align: center; margin-bottom: 80px; margin-top: 80px;">Add Company Unit</h2>
        </div>
        <div class="card-body m-5">
            <div class="row mb-3">
                <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                    <asp:Literal ID="Literal1" runat="server" Text="Company Name"></asp:Literal>

                </div>
                <div class="col-sm-6" style="display: flex; flex-direction: row">
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="btn btn-default dropdown-toggle" Style="margin-top: 5px"></asp:DropDownList>
                </div>
            </div>
            <div class="row mb-3 mt-5">
                <div class="col-3 align-middle d-flex">

                    <asp:Literal ID="Literal3" runat="server" Text="Company Unit Name"></asp:Literal>

                </div>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtCompanyUnitName"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCompanyUnitName" ErrorMessage="Required" ValidationGroup="vg1">*</asp:RequiredFieldValidator>
                </div>


            </div>

            <div class="row mb-3">
                <div class="col-sm-6" style="width: 30%; margin-left: auto; margin-right: auto">
                    <asp:Button ID="btnSave" runat="server" Text="Add" Style="width: 80%;" OnClick="btnSave_Click" />
                </div>
            </div>

            <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">

                <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                    <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="false">
                        <Columns>
                            <asp:BoundField DataField="CompanyId" HeaderText="Company Id" ItemStyle-CssClass="display: none;">
                                <ItemStyle CssClass="display: none;"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Company Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("company.CompanyName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("company.CompanyName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CompanyUnitName" HeaderText="Company Unit Name" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btndelete" runat="server">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div style="height: 40px;"></div>
</asp:Content>
