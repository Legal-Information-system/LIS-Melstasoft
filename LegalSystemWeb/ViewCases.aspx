<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCases.aspx.cs" Inherits="LegalSystemWeb.ViewCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="height: 40px;">
    </div>
    <div class="card mb-4">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="card-header d-flex justify-content-between">
                    <div>
                        <i class="fas fa-table me-1 "></i>
                        Case Table
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlCaseStatus" runat="server" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled"
                            Style="margin-top: 5px" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="datatablesSimple" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="datatablesSimple_PageIndexChanging">
                        <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />

                        <Columns>
                            <asp:BoundField DataField="CaseNumber" HeaderText="Case Number" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IsPlaintif" HeaderText="Company Party" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="company.CompanyName" HeaderText="Company Name" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="companyUnit.CompanyUnitName" HeaderText="Company Unit" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="court.CourtName" HeaderText="Court" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="caseNature.CaseNatureName" HeaderText="Case Nature" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="location.locationName" HeaderText="Location" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ClaimAmount" HeaderText="Claim Amount" ItemStyle-HorizontalAlign="center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnView" CssClass="btn btn-info btn-user btn-block" runat="server" OnClick="btnView_Click">View Details</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
