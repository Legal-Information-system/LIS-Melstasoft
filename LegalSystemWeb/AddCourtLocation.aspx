<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCourtLocation.aspx.cs" Inherits="LegalSystemWeb.AddCourt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <h2 style="text-align: center; margin-bottom: 40px; margin-top: 30px;">Add Court Location</h2>
        <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">

            <div class="col-sm-6" style="width: 50%; padding-left: 40px; padding-right: 10px; margin-bottom: 0px;">
                <div style="text-align: start; padding-left: 2px; margin-bottom: 5px;">
                    <asp:Literal ID="Literal2" runat="server" Text="Court Name"></asp:Literal>
                </div>
                <asp:DropDownList ID="ddlCourt" runat="server" CssClass="btn btn-primary dropdown-toggle w-100" ValidationGroup="1"></asp:DropDownList>
                <asp:RequiredFieldValidator class="row" ID="RequiredFieldValidator1" runat="server" Style="padding-left: 12px;"
                    ControlToValidate="ddlCourt" ErrorMessage="Court Name is Required">* Court Name is Required</asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 10px; padding-right: 40px; margin-bottom: 0px;">
                <div style="text-align: start; padding-left: 2px; margin-bottom: 5px;">
                    <asp:Literal ID="Literal1" runat="server" Text=" Court Location"></asp:Literal>
                </div>
                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="btn btn-primary dropdown-toggle w-100" ValidationGroup="1"></asp:DropDownList>
                <asp:RequiredFieldValidator class="row" ID="RequiredFieldValidator15" runat="server" Style="padding-left: 12px;"
                    ControlToValidate="ddlLocation" ErrorMessage="Location Name is Required">* Location Name is Required </asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6" style="width: 30%; margin-left: auto; margin-right: auto">
                <asp:Button ID="btnSave" runat="server" Text="Add" Style="width: 80%;" OnClick="btnSave_Click" ValidationGroup="1" />
            </div>
            <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="CourtId" HeaderText="Court Id" />
                        <asp:BoundField DataField="court.CourtName" HeaderText="Court Name" />
                        <asp:BoundField DataField="location.location" HeaderText="Court Location" />
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
</asp:Content>
