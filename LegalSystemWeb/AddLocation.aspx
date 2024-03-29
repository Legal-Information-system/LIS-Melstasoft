﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLocation.aspx.cs" Inherits="LegalSystemWeb.AddLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <h2 style="text-align: center; margin-bottom: 40px; margin-top: 30px;">Add Location</h2>
        <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">
            <div style="text-align: start; padding-left: 42px; margin-bottom: 5px;">
                <asp:Literal ID="Literal2" runat="server" Text="Location"></asp:Literal>
            </div>
            <div class="col-sm-6" style="width: 70%">
                <asp:TextBox Style="width: 90%;" ID="txtLocationName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator class="row" ID="RequiredFieldValidator15" runat="server" Style="padding-left: 42px;"
                    ControlToValidate="txtLocationName" ErrorMessage="Location Name is Required" ValidationGroup="1">
                    * Location Name is Required</asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6" style="width: 30%">
                <asp:Button ID="btnSave" runat="server" Text="Add" Style="width: 80%;" OnClick="btnsave_Click" ValidationGroup="1" />
            </div>
            <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridView2_OnPageIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="LocationId" HeaderText="Location Id" />
                        <asp:BoundField DataField="locationName" HeaderText="Location" />
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
