<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCaseActivity.aspx.cs" Inherits="LegalSystemWeb.ViewCaseActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="row mb-5 ">

        <h4 style="text-align: center; margin-bottom: 20px;">Case Activity Details -
            <asp:Label ID="lblCaseNumber" runat="server" Text="N/A"></asp:Label></h4>

        <div class="col-sm-6 mb-3">
            <div class="card" style="width: 85%; margin-left: auto;">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Activity Id</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblActivityId" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Created Date</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCreatedDate" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Assigned Attorny</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAssignedAttorney" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Counsellor</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCounsellor" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Opposite Side Lawyer</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblOppositeSideLawyer" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Judge Name</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblJudgeName" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-sm-6 mb-3">
            <div class="card" style="width: 85%; margin-right: auto">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Company Representatve</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCompanyReperesentative" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Action Taken</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblActionTaken" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Next Date</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblNextDate" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Remarks</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRemarks" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>

                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Next Action</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblNextAction" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Created By</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCreatedBy" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
        </div>



        <hr />

        <div class="card mb-4">

            <div class="card-header">
                Documents
            </div>

            <div class="card-body table-responsive">
                <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                    CellPadding="4" EmptyDataText="No Files Uploaded" ForeColor="#333333" GridLines="None" AllowPaging="True" Width="100%">
                    <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />
                    <Columns>
                        <asp:BoundField DataField="DocumentCaseActivityId" HeaderText="Id" />
                        <asp:BoundField DataField="DocumentName" HeaderText="Name" ItemStyle-HorizontalAlign="center" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" CssClass="btn btn-info btn-user btn-block"
                                    AutoPostBack="true" runat="server" OnClick="btnView_Click">
                                    Download
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        -<% if ((rolePrivileges.Where(x => x.FunctionId == 35 || x.FunctionId == 36).Any()
                                        && !(userPrivileges.Any(x => x.FunctionId == 35 || x.FunctionId == 36 && x.IsGrantRevoke == 0))) ||
                                        userPrivileges.Any(x => x.FunctionId == 35 || x.FunctionId == 36 && x.IsGrantRevoke == 1))
             {
        %>

        <div class="row">
            <% if ((rolePrivileges.Where(x => x.FunctionId == 35).Any()
                          && !(userPrivileges.Any(x => x.FunctionId == 35 && x.IsGrantRevoke == 0))) ||
                          userPrivileges.Any(x => x.FunctionId == 35 && x.IsGrantRevoke == 1))
                {
            %>
            <div class="col-3">
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-warning" Width="100%" Height="150%" BorderStyle="None" OnClick="btnEdit_Click" />
            </div>
            <%
                }
                if ((rolePrivileges.Where(x => x.FunctionId == 36).Any()
                    && !(userPrivileges.Any(x => x.FunctionId == 36 && x.IsGrantRevoke == 0))) ||
                    userPrivileges.Any(x => x.FunctionId == 36 && x.IsGrantRevoke == 1))
                {
            %>
            <div class="col-3">
                <asp:Button ID="btnDelete" runat="server" Text="Delete Case Activity" CssClass="btn-danger" Width="100%" Height="150%" BorderStyle="None" OnClick="btnDelete_Click" />
            </div>
            <%} %>
        </div>

        <%} %>
    </div>
</asp:Content>
