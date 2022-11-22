<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLawyer.aspx.cs" Inherits="LegalSystemWeb.AddLawyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <h2 style="text-align: center; margin-bottom: 40px; margin-top: 30px;">Add Lawyer</h2>
        <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">
            <div class="col-sm-6" style="width: 100%; padding-left: 40px; padding-right: 40px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtName" ErrorMessage="Name is Required">Name is Required </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 40px; padding-right: 10px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="Email Required">Email is Required</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="Invalid email address"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 10px; padding-right: 40px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="txtContact" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                    ControlToValidate="txtContact" ErrorMessage="Contact no. is Required">Contact no. is Required</asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6" style="width: 30%; margin-left: auto; margin-right: auto">
                <asp:Button ID="btnSave" runat="server" Text="Add" Style="width: 80%;" OnClick="btnSave_Click" />
            </div>
            <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="LawyerId" HeaderText="Lawyer Id" />
                        <asp:BoundField DataField="LawyerName" HeaderText="Lawyer Name" />
                        <asp:BoundField DataField="LawyerEmail" HeaderText="E-mail" />
                        <asp:BoundField DataField="LawyerContact" HeaderText="Contact No." />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server">Edit</asp:LinkButton>
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
    <div style="height: 40px;"></div>
</asp:Content>
