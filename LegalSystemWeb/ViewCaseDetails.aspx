<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCaseDetails.aspx.cs" Inherits="LegalSystemWeb.ViewCaseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="height: 40px;"></div>
    <div class="row mb-5 ">

        <h4 style="text-align: center; margin-bottom: 20px;">Case Details -
            <asp:Label ID="lblCaseNumber" runat="server" Text="N/A"></asp:Label></h4>

        <div class="col-sm-6 mb-3">
            <div class="card" style="width: 85%; margin-left: auto;">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Company</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCompany" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Claim Amount</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblClaimAmount" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Created Date</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCreateDate" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Plaintiff</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPlaintiff" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Court</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCourt" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Court Location</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblLocationi" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Case Status</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblStatus" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Created User</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblUser" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <%if (lblStatus.Text == "Closed")
                        { %>
                    <div class="row">
                        <div class="col-sm-6">
                            <p>Judgement Type</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblJudgement" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Case Outcome</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCloseOutcome" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-6">
                            <p>Closed Date</p>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCloseDate" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <% }%>
                </div>
            </div>
        </div>
        <div class="col-sm-6 mb-3">
            <div class="card" style="width: 85%; margin-right: auto">
                <div class="card-body" style="padding-left: 30px;">
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Company Unit</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblCompanyUnit" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Nature of Case</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblNature" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Previous Case Number</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblPrevCase" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <p>Defendant</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblDefendant" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Assign Attorney</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblAttorney" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Counsilor</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblCounsilor" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Case Description</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblDescription" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <%if (lblStatus.Text == "Closed")
                        { %>
                    <div class="row">
                        <div class="col-sm-5">
                            <p>Closed Remarks</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblCLoseRemarks" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-1">
                        <div class="col-sm-5">
                            <p>Closed User</p>
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblCloseUser" runat="server" Text="N/A"></asp:Label>
                        </div>
                    </div>

                    <% }%>
                </div>
            </div>
        </div>

        <hr />
        <div class="card mb-4">
            <div class="card-header">
                Activity Details
            </div>

            <div class="card-body table-responsive">
                <asp:GridView ID="gvCaseActivity" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                    <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />
                    <Columns>
                        <asp:BoundField DataField="CaseActivitId" HeaderText="Id" />
                        <asp:BoundField DataField="ActivityDateString" HeaderText="Date" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="assignAttorney.LawyerName" HeaderText="Assign Attorney" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="counsilor.LawyerName" HeaderText="Counsilor" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="OtherSideLawyer" HeaderText="Other Side Lawyer" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="JudgeName" HeaderText="Judge Name" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="CompanyRep" HeaderText="Company Rep" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="actionTaken.ActionName" HeaderText="ActionT aken" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="NextDateString" HeaderText="Next Date" ItemStyle-HorizontalAlign="center" />
                        <asp:BoundField DataField="nextAction.ActionName" HeaderText="Next Action" ItemStyle-HorizontalAlign="center" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>


        <hr />
        <div class="card mb-4">

            <div class="card-header">
                Documnets
            </div>

            <div class="card-body table-responsive">
                <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-responsive table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" Width="35%">
                    <HeaderStyle BackColor="#212529" ForeColor="white" HorizontalAlign="center" />
                    <Columns>
                        <asp:BoundField DataField="DocumentCaseId" HeaderText="Id" />
                        <asp:BoundField DataField="DocumentName" HeaderText="Name" ItemStyle-HorizontalAlign="center" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" CssClass="btn btn-info btn-user btn-block"
                                    AutoPostBack="true" runat="server" OnClick="btnView_Click">
                                    View File
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>



    </div>

</asp:Content>
