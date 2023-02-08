<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPrivileges.aspx.cs" Inherits="LegalSystemWeb.UserPrivileges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="card o-hidden border-0 shadow-lg my-3">
        <div class="card-header d-flex align-items-center justify-content-center" style="height: 5%">
            <h5 class="text-center  mt-3 mb-3">User Privileges</h5>
        </div>

        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row mb-3 ms-1 mt-3">
                        <div class="col-sm-6">

                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Literal ID="lblUser" runat="server" Text="Select User"></asp:Literal>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="btn btn-outline-dark dropdown-toggle form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <asp:Literal ID="ltlUserType" runat="server" Text="Current User Type"></asp:Literal>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblUserType" runat="server" Text="&nbsp " CssClass="form-control form-control-user"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row mb-3 ms-1 mt-3">
                        <div class="col-sm-8">
                            <div class="table-responsive" style="width: 100%;">

                                <asp:GridView ID="gvUserPrevilages" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                                    <Columns>
                                        <asp:BoundField DataField="FunctionId" HeaderText="Function Id" HeaderStyle-CssClass="table-dark">
                                            <HeaderStyle CssClass="table-dark"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FunctionName" HeaderText="Page or Function Name" HeaderStyle-CssClass="table-dark">
                                            <HeaderStyle CssClass="table-dark"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="table-dark">
                                            <HeaderStyle CssClass="table-dark"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                            <HeaderStyle CssClass="table-dark"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnChange" runat="server" CssClass="btn btn-info btn-user btn-block"
                                                    OnClick="btnChange_Click">
                                            Change
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
