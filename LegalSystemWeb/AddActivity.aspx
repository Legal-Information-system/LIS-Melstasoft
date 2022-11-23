<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddActivity.aspx.cs" Inherits="LegalSystemWeb.AddActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <h2 style="text-align: center; margin-bottom: 40px; margin-top: 30px;">Add Activity</h2>
        <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">
            <div class="col-sm-6" style="width: 70%">
                <asp:TextBox Style="width: 90%;" ID="txtAddActivity" runat="server"></asp:TextBox>
                <div class="d-flex text-danger">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAddActivity" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1">Required</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-6" style="width: 30%">
                <asp:Button ID="btnSave" runat="server" Text="Add" Style="width: 80%;" OnClick="btnSave_Click" ValidationGroup="1" />

            </div>
            <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="ActivityId" HeaderText="Activity Id" />
                        <asp:BoundField DataField="ActivityName" HeaderText="Activity" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btndelete" runat="server" OnClick="btndelete_Click">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div style="height: 40px;"></div>
</asp:Content>
