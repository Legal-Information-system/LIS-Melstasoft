<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UpdateCaseActivity.aspx.cs" Inherits="LegalSystemWeb.UpdateCaseActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="card o-hidden border-0 shadow-lg my-3">
        <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
            <h5 class="text-light text-center bg-dark " id="hTitle" runat="server"></h5>
        </div>
        <div class="card-body">

            <form class="user">



                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <ContentTemplate>
                        <div class="row mb-3 ms-1" id="dvCaseNumber" runat="server">
                            <div class="col-sm-6">

                                <div class="row mb-3">
                                    <div class="col-sm-6">
                                        <asp:Literal ID="Literal16" runat="server" Text="Case Number"></asp:Literal>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlCase" runat="server" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" AutoPostBack="true" OnSelectedIndexChanged="BindCaseDetails"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="1"
                                            ControlToValidate="ddlCase" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row mb-5">
                            <div class="col-sm-6">
                                <div class="card o-hidden border-0 shadow">
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-sm-6">
                                                <p>Company</p>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblCompany" runat="server" Text="N/A"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-sm-6">
                                                <p>Case Description</p>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblDescription" runat="server" Text="N/A"></asp:Label>
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
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="card o-hidden border-0 shadow">
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-sm-6">
                                                <p>Company Unit</p>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblCompanyUnit" runat="server" Text="N/A"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-sm-6">
                                                <p>Nature of Case</p>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblNature" runat="server" Text="N/A"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p>Defendant</p>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblDefendant" runat="server" Text="N/A"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row mb-3 ms-1">
                    <div class="col-sm-6">
                        <div class="row mb-3">
                            <div class="col-sm-6">
                                <asp:Literal ID="Literal17" runat="server" Text="Date"></asp:Literal>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-user" TextMode="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="1"
                                    ControlToValidate="txtDate" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row mb-3 ms-1">
                    <div class="col-sm-6">
                        <asp:Literal ID="Literal2" runat="server" Text="Appeared Lawyer ( Company Side )"></asp:Literal>
                    </div>
                    <div class="col-sm-6">
                        <asp:Literal ID="Literal4" runat="server" Text="Appeared Lawyer ( Other Side )"></asp:Literal>
                    </div>
                </div>

                <div class="row mb-3 ms-1">
                    <div class="col-sm-6">
                        <div class="row mb-3">
                            <div class="col-sm-6">
                                <asp:Literal ID="Literal1" runat="server" Text="Assigning Attorney"></asp:Literal>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlAssignAttorney" runat="server" CssClass="btn btn-outline-dark dropdown-toggle form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                                    ControlToValidate="ddlAssignAttorney" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row mb-5">
                            <div class="col-sm-6">
                                <asp:Literal ID="Literal6" runat="server" Text="Counselor"></asp:Literal>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlCounselor" runat="server" CssClass="btn btn-outline-dark dropdown-toggle form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
                                    ControlToValidate="ddlCounselor" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="row mb-3">
                            <div class="col-sm-6">
                                <asp:Literal ID="Literal3" runat="server" Text="Name"></asp:Literal>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtOtherLawyer" runat="server" CssClass="form-control form-control-user"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
                                    ControlToValidate="txtOtherLawyer" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row mb-3 ms-1">
                    <div class="col-sm-6">
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Literal ID="Literal5" runat="server" Text="Judge Name"></asp:Literal>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtJudgeName" runat="server" CssClass="form-control form-control-user"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1"
                                    ControlToValidate="txtJudgeName" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Literal ID="Literal7" runat="server" Text="Company Representer"></asp:Literal>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtCompanyRepresenter" runat="server" CssClass="form-control form-control-user"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="1"
                                    ControlToValidate="txtCompanyRepresenter" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>


                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="row mb-3 ms-1">
                            <div class="col-sm-6">
                                <div class="row mb-3">
                                    <div class="col-sm-6">
                                        <asp:Literal ID="Literal8" runat="server" Text="Action Taken"></asp:Literal>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlActionTaken" AutoPostBack="true" runat="server" CssClass="btn btn-outline-dark dropdown-toggle form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1"
                                            ControlToValidate="ddlActionTaken" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="row mb-3">
                                    <div class="col-sm-6">
                                        <asp:Literal ID="Literal10" runat="server" Text="Next Action"></asp:Literal>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlNextAction" AutoPostBack="true" runat="server" CssClass="btn btn-outline-dark dropdown-toggle form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="1"
                                            ControlToValidate="ddlNextAction" ErrorMessage="Required">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mb-3 ms-1">
                            <div class="col-sm-6">
                                <div class="row mb-3">
                                    <div class="col-sm-6">
                                        <asp:Literal ID="Literal9" runat="server" Text="Next Date"></asp:Literal>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtNextDate" runat="server" CssClass="form-control form-control-user" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mb-3 ms-1">
                            <div class="col-sm-3">
                                <asp:Literal ID="Literal12" runat="server" Text="Action Remarks"></asp:Literal>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-user" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <%if (ddlActionTaken.SelectedItem.Text == "Judgement" && ddlNextAction.SelectedItem.Text == "Case Close")
                            {  %>

                        <div id="case-close">
                            <div class="row mb-3 ms-1">
                                <div class="col-md-3">
                                    <asp:Literal ID="Literal15" runat="server" Text="Judgement"></asp:Literal>
                                </div>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlJudgement" runat="server" CssClass="btn btn-outline-dark dropdown-toggle">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="row mb-3 ms-1">
                                <div class="col-sm-3">
                                    <asp:Literal ID="Literal11" runat="server" Text="Outcome"></asp:Literal>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtOutcome" runat="server" CssClass="form-control form-control-user" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-3 ms-1">
                                <div class="col-sm-3">
                                    <asp:Literal ID="Literal13" runat="server" Text="Case Close Remarks"></asp:Literal>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtCaseCloseRemarks" runat="server" CssClass="form-control form-control-user" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <%} %>
                        <div class="row mb-5 mt-3">

                            <div class="col-sm-4">
                                <asp:FileUpload ID="Uploader" runat="server" AllowMultiple="false" CssClass="btn " />
                                <%--<asp:Label ID="lblListOfUploadedFiles" runat="server" />--%>
                            </div>

                            <div class="col-md-2 mb-3 d-flex justify-content-end" style="display: flex; flex-direction: column">

                                <asp:Button ID="btnUpload" Text="upload" runat="server" CssClass="btn btn-primary btn-user btn-block " BackColor="#212529" BorderColor="#212529" OnClick="AddFiles" />

                            </div>

                        </div>

                        <div class="row">
                            <asp:GridView ID="fileGridview" UseAccessibleHeader="true" runat="server" CssClass="table table-hover table-striped" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Files Uploaded">
                                <Columns>
                                    <asp:BoundField DataField="DocumentName" HeaderText="File Name" />


                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:LinkButton ID="DeleteLink" runat="server" Text="Delete" OnClick="DeleteFiles" CssClass="btn btn-primary btn-user btn-block " BackColor="#993333" BorderColor="#212529"></asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                            <script type="text/javascript">    

                                $(document).ready(function () {
                                    $('#fileGridview').DataTable();
                                });
                            </script>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />

                    </Triggers>
                </asp:UpdatePanel>


                <div class="row mb-3 ms-1">
                    <div class="col-sm-3">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-secondary btn-user btn-block" BackColor="#212529" BorderColor="#212529" OnClick="btnReset_Click" />
                        <asp:Button ID="btnUpdateActivity" runat="server" Text="Update Case" CssClass="btn btn-primary btn-user btn-block" ValidationGroup="1" OnClick="btnUpdateActivity_Click" />
                    </div>
                </div>
                <div class="col-sm-6 m-3">
                    <asp:Label ID="lblSuccessMsg" runat="server" Text="" ForeColor="#33cc33"></asp:Label>
                </div>
            </form>
        </div>
    </div>



</asp:Content>
