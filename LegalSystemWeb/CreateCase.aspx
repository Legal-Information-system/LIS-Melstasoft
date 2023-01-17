<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCase.aspx.cs" Inherits="LegalSystemWeb.CreateCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">

        <ContentTemplate>
            <div class="container" id="mainContainer">
                <div class="row justify-content-center">

                    <div class="card o-hidden border-0 shadow-lg my-3" style="padding-left: unset; padding-right: unset">
                        <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                            <h3 class="text-light text-center bg-dark " id="hTitle" runat="server"></h3>

                        </div>
                        <div class="card-body form-group">


                            <div class="row mb-3 ">
                                <div class="col-sm-6 dropdown" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal1" runat="server" Text="Company"></asp:Literal>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlCompany" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-6" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal2" runat="server" Text="Company Unit"></asp:Literal>
                                    <asp:DropDownList ID="ddlCompanyUnit" runat="server" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="ddlCompanyUnit" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <%--=========--%>
                            <div class="form-group row ">
                                <div class="col-md-6" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal5" runat="server" Text="Nature Of Case"></asp:Literal>
                                    <asp:DropDownList ID="ddlNatureOfCase" runat="server" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlNatureOfCase" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">

                                    <asp:Literal ID="ltLastName" runat="server" Text="Case Description"></asp:Literal>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtCaseDescription" TextMode="MultiLine" Style="margin-top: 5px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtCaseDescription" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <%--===========--%>

                            <div class="row mb-3">
                                <div class="col-md-6 mb-3" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal3" runat="server" Text="Claim Amount"></asp:Literal>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtClaimAmount" Style="margin-top: 5px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtClaimAmount" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClaimAmount"
                                ErrorMessage="RegularExpressionValidator" ValidationGroup="1">*
                            </asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-md-6 mb-3 d-flex justify-content-end" style="display: flex; flex-direction: column">
                                    <asp:Button ID="btnCheckInWords" runat="server" Text="Check in Words" CssClass="btn btn-primary btn-user btn-block " BackColor="#212529" BorderColor="#212529" OnClick="claimAmountInWords" />
                                </div>


                            </div>


                            <div class="row mb-3">
                                <asp:Label ID="lblClaimAmountInWords" runat="server" Text="" ForeColor="#000000"></asp:Label>
                            </div>



                            <%--===========--%>


                            <%--===========--%>
                            <div class="row mb-3">
                                <div class="col-md-6">

                                    <asp:Literal ID="Literal15" runat="server" Text="Company Side"></asp:Literal>
                                    <asp:RadioButtonList ID="rbIsPlantiff" runat="server" RepeatDirection="Horizontal" CssClass="margin-left:10px">
                                        <asp:ListItem Value="1" CssClass="form-check-input" Style="margin-right: 50px">&nbsp;Plaintiff</asp:ListItem>
                                        <asp:ListItem Value="0" CssClass="form-check-input">&nbsp;Defendant</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="rbIsPlantiff" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-6">

                                    <asp:Literal ID="Literal4" runat="server" Text="Case Open Date"></asp:Literal>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" TextMode="Date" ID="txtCaseOpenDate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtCaseOpenDate" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClaimAmount"
                                ErrorMessage="RegularExpressionValidator" ValidationGroup="1">*
                            </asp:RegularExpressionValidator>--%>
                                </div>

                            </div>

                            <div class="row ">
                                <div class="col-md-4 mb-3" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="ltPlaintifSide" runat="server" Text="Plaintif Party"></asp:Literal>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtPlaintif" Style="margin-top: 5px"></asp:TextBox>


                                </div>
                                <div class="col-md-2 mb-3 d-flex justify-content-end" style="display: flex; flex-direction: column">
                                    <asp:Button ID="btnPlaintifAdd" runat="server" Text="Add" CssClass="btn btn-primary btn-user btn-block " BackColor="#212529" BorderColor="#212529" OnClick="btnAddPlaintif_Click" />
                                </div>
                            </div>
                            <div class="row " id="dPlaintifError" runat="server">
                                <asp:Label ID="lblPlaintifError" runat="server" Text="" ForeColor="#ff0000"></asp:Label>
                            </div>
                            <div class="row " id="dPlaintif" runat="server">

                                <asp:Label ID="lblPlaintif" runat="server" Text="" ForeColor="#ff3300"></asp:Label>

                            </div>
                            <div class="row mb-3">
                                <div class="table-responsive" style="width: 100%;">
                                    <asp:GridView Style="margin-top: 30px;" ID="gvPlaintif" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridView2_OnPageIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="PartyName" HeaderText="Plaintif Side" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_ClickPlaintiff">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" OnClick="btndelete_ClickPlaintif">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row ">
                                <div class="col-md-4 mb-3" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="ltDefendent" runat="server" Text="Defendent Party"></asp:Literal>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" ID="txtDefendent" Style="margin-top: 5px"></asp:TextBox>


                                </div>
                                <div class="col-md-2 mb-3 d-flex justify-content-end" style="display: flex; flex-direction: column">
                                    <asp:Button ID="btnAddDefendent" runat="server" Text="Add" CssClass="btn btn-primary btn-user btn-block " BackColor="#212529" BorderColor="#212529" OnClick="btnAddDefendent_Click" />
                                </div>
                            </div>
                            <div class="row" id="dDefendentError" runat="server">
                                <asp:Label ID="lblDefendentError" runat="server" Text="" ForeColor="#ff0000"></asp:Label>
                            </div>
                            <div class="row " id="dDefendent" runat="server">

                                <asp:Label ID="lblDefendent" runat="server" Text="" ForeColor="#ff3300"></asp:Label>

                            </div>
                            <div class="row">
                                <div class="table-responsive" style="width: 100%;">
                                    <asp:GridView Style="margin-top: 30px;" ID="gvDefendent" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridView2_OnPageIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="PartyName" HeaderText="Defendent Side" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_ClickDefendent">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" OnClick="btndelete_ClickDefendent">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-6" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal9" runat="server" Text="Court"></asp:Literal>
                                    <asp:DropDownList runat="server" ID="ddlCourt" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px" AutoPostBack="true" OnSelectedIndexChanged="ddlCourt_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                        ControlToValidate="ddlCourt" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal10" runat="server" Text="Location"></asp:Literal>
                                    <asp:DropDownList runat="server" ID="ddlLocation" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                        ControlToValidate="ddlLocation" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <%--===========--%>
                            <div class="row mb-3">
                                <div class="col-md-6">

                                    <asp:Literal ID="Literal11" runat="server" Text="Case Number"></asp:Literal>
                                    <asp:TextBox runat="server" ID="txtCaseNumber" CssClass="form-control form-control-user" Style="margin-top: 5px"></asp:TextBox>
                                    <div class="d-flex text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtCaseNumber" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <asp:Label ID="lblCaseNumberError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">

                                    <asp:Literal ID="Literal12" runat="server" Text="Previous Case Number"></asp:Literal>
                                    <asp:TextBox runat="server" ID="txtPreCaseNumber" CssClass="form-control form-control-user" Style="margin-top: 5px"></asp:TextBox>
                                    <div class="d-flex text-danger">
                                        <asp:Label ID="lblPrevCaseNumberError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-6" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal13" runat="server" Text="Assigning Attorney "></asp:Literal>
                                    <asp:DropDownList runat="server" ID="ddlAttorney" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                        ControlToValidate="ddlAttorney" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="row " runat="server" id="dAttorney">

                                <asp:Label ID="lblAttorney" runat="server" Text="" ForeColor="#ff3300"></asp:Label>

                            </div>
                            <div class="row">
                                <div class="col-md-6 mb-3" style="display: flex; flex-direction: column">

                                    <asp:Literal ID="Literal14" runat="server" Text="Counselor"></asp:Literal>
                                    <asp:DropDownList runat="server" ID="ddlCounselor" CssClass="btn btn-outline-dark dropdown-toggle dropdown-item.disabled" Style="margin-top: 5px"></asp:DropDownList>


                                </div>
                                <div class="col-md-6 mb-3 d-flex justify-content-end" style="display: flex; flex-direction: column">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary btn-user btn-block " BackColor="#212529" BorderColor="#212529" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="row " runat="server" id="dCounselor">

                                <asp:Label ID="lblCounselor" runat="server" Text="" ForeColor="#ff3300"></asp:Label>

                            </div>
                            <div class="row mb-3">
                                <div class="table-responsive" style="width: 100%;">
                                    <asp:GridView Style="margin-top: 30px;" ID="gvCounselor" runat="server" EnableViewState="true" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridView2_OnPageIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="LawyerName" HeaderText="Counselor Name" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" OnClick="btndelete_Click">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <%--===========--%>
                            <%--                    <div class="row mb-3">
                        <div class="col-md-6">
                            <asp:Button ID="btnDocUpload" runat="server" Text="Next" OnClientClick="btnDocUpload_CLick" CssClass="btn btn-primary btn-user btn-block" OnClick="btnDocUpload_Click1" />
                        </div>



                    </div>--%>

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



                            <div class="row mb-3">

                                <div class="col-sm-6">

                                    <asp:Button ID="btnBack" runat="server" Text="Reset" CssClass="btn btn-primary btn-user btn-block" BackColor="#212529" BorderColor="#212529" OnClick="btnBack_Click" />
                                    <asp:Button ID="btnSave" runat="server" Text="Create Case" CssClass="btn btn-primary btn-user btn-block" ValidationGroup="1" OnClick="btnSave_Click" />

                                </div>
                            </div>

                            <div class="col-sm-6 m-3">
                                <asp:Label ID="lblSuccessMsg" runat="server" Text="" ForeColor="#33cc33"></asp:Label>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


