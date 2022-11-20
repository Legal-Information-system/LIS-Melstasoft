<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCases.aspx.cs" Inherits="LegalSystemWeb.ViewCases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height:40px;">
    </div>
    <div class="card mb-4">
        <div class="card-header">
            <i class="fas fa-table me-1"></i>
            Approve Payment Table
        </div>
        
           
    </div>
    <asp:GridView Id="datatablesSimple" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
         
            <Columns>
                <asp:BoundField DataField="CaseNo" HeaderText="Case No"/>
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name"/>
                <asp:BoundField DataField="CompanyUnit" HeaderText="Company Unit"/>
                <asp:BoundField DataField="PartyType" HeaderText="Party Type"/>
                <asp:BoundField DataField="Plentiff" HeaderText="Plaintiff"/>
                <asp:BoundField DataField="Defendent" HeaderText="Defendent"/>
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="View Details">
                    <ItemTemplate>
                        <asp:LinkButton ID="btndelete" runat="server">View Details</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        
    </asp:GridView>
</asp:Content>
