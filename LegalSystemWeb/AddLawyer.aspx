<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLawyer.aspx.cs" Inherits="LegalSystemWeb.AddLawyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <h2 style="text-align: center; margin-bottom: 40px; margin-top: 30px;">Add Lawyer</h2>
        <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">
            <div class="col-sm-6" style="width: 100%; padding-left: 40px; padding-right: 40px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 40px; padding-right: 10px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="TextBox2" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 10px; padding-right: 40px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="TextBox3" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-6" style="width: 30%; margin-left: auto; margin-right: auto">
                <asp:Button ID="Button1" runat="server" Text="Add" Style="width: 80%;" />
            </div>
            <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="OrderID" HeaderText="Lawyer Id" />
                        <asp:BoundField DataField="CustomerID" HeaderText="Lawyer Name" />
                        <asp:BoundField DataField="EmployeeID" HeaderText="E-mail" />
                        <asp:BoundField DataField="Freight" HeaderText="Contact No." />
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
