<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyCases.aspx.cs" Inherits="LegalSystemWeb.DailyCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">

        function printDiv() {
            var divContents = document.getElementById("lt").innerHTML;
            var a = window.open('', '');
            a.document.write('<html><head>' +

                '<style>' +
                ' @media print {' +
                '   .parent {' +
                'overflow: scroll;' +
                'display: block;' +
                ' }' +
                ' .pb_after  { page -break-after: always!important; }' +
                ' }    ' +
                '</style > ' +

                '<link href="https://cdn.jsdelivr.net/npm/simple-datatables@latest/dist/style.css" rel="stylesheet" media=\"print\"/>' +
                '<link href="css/styles.css" rel="stylesheet" />' +
                '<link href="Styles/sweetalert.css" rel="stylesheet" media=\"print\" /></head>');
            a.document.write('<body > ');
            a.document.write(divContents);
            a.document.write('</body></html>');
            a.document.close();
            setTimeout(function () { a.print(); }, 1000);

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5" id="main">
        <div class="row justify-content-center">

            <div class="card o-hidden border-0 shadow-lg p-0 my-3" style="padding-left: unset; padding-right: unset; width: 1000px;">
                <div class="card-header d-flex align-items-center justify-content-center" style="background-color: #212529; height: 50px">
                    <h3 class="text-light text-center bg-dark">Daily Case List Report</h3>
                </div>
                <div class="card-body m-5">

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
                            </Triggers>--%>
                        <ContentTemplate>
                            <div class="row mb-3">
                                <div class="col-sm-3">

                                    <asp:Literal ID="Literal6" runat="server" Text="Select Option"></asp:Literal>

                                </div>
                                <div class="col-sm-6">
                                    <asp:RadioButtonList ID="rbSelectOption" runat="server" RepeatDirection="Horizontal" CssClass="margin-left:10px" OnTextChanged="rbFilter_SelectedValueChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" CssClass="form-check-input" Style="margin-right: 50px">  &nbsp;By Created Date</asp:ListItem>
                                        <asp:ListItem Value="0" CssClass="form-check-input">  &nbsp;By Next Action Date</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="row mb-3" id="dvDate" runat="server">
                                <div class="col-sm-3">

                                    <asp:Label ID="ltDate" runat="server"></asp:Label>

                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" CssClass="form-control form-control-user" TextMode="Date" ID="txtCaseOpenDate" OnTextChanged="DateChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtCaseOpenDate" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                </div>


                                <div class="col-sm-3" id="btnPrint" runat="server">

                                    <asp:button runat="server" cssclass="btn btn-primary btn-user btn-block" onclientclick="javascript:printDiv();" text="Print" xmlns:asp="#unknown" />

                                </div>

                            </div>
                            <div class="row mb-3" id="company" runat="server">
                                <div class="col-sm-3">

                                    <asp:Literal ID="Literal1" runat="server" Text="Filter company"></asp:Literal>

                                </div>
                                <div class="col-sm-6">
                                    <asp:RadioButtonList ID="rbCompany" runat="server" RepeatDirection="Horizontal" CssClass="margin-left:10px" OnTextChanged="rbCompany_SelectedValueChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" CssClass="form-check-input" Style="margin-right: 50px">  &nbsp;Company Wise</asp:ListItem>
                                        <asp:ListItem Value="0" CssClass="form-check-input">  &nbsp;Global</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>


                            </div>
                            <div class="row mb-3" id="companyUnit" runat="server">
                                <div class="col-sm-3">

                                    <asp:Literal ID="Literal4" runat="server" Text="Filter Company"></asp:Literal>

                                </div>
                                <div class="col-sm-6">
                                    <asp:RadioButtonList ID="rbCompanyUnit" runat="server" RepeatDirection="Horizontal" CssClass="margin-left:10px" OnTextChanged="rbCompanyUnit_SelectedValueChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" CssClass="form-check-input" Style="margin-right: 50px">  &nbsp;Company Unit Wise</asp:ListItem>
                                        <asp:ListItem Value="0" CssClass="form-check-input"> &nbsp; Global</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>


                            </div>
                            <div class="row mb-3" id="dvCompany" runat="server">
                                <div class="col-sm-3">

                                    <asp:Literal ID="Literal2" runat="server" Text="Company Name"></asp:Literal>

                                </div>
                                <div class="col-sm-6">

                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="btn btn-outline-dark dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCompany" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>


                            </div>
                            <div class="row mb-3" id="dvCompanyUnit" runat="server">
                                <div class="col-sm-3">

                                    <asp:Literal ID="Literal3" runat="server" Text="Company Unit"></asp:Literal>

                                </div>
                                <div class="col-sm-6">

                                    <asp:DropDownList ID="ddlCompanyUnit" runat="server" CssClass="btn btn-outline-dark dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyUnit_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompanyUnit" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>


                            </div>

                            <%--<div class="row mb-3">
                                <div class="col-3 align-middle" style="display: flex; flex-direction: row">

                                    <asp:Literal ID="Literal1" runat="server" Text="Case No"></asp:Literal>

                                </div>
                                <div class="col-sm-6" style="display: flex; flex-direction: row">

                                    <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="btn btn-outline-dark dropdown-toggle"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCaseNo" ErrorMessage="Required" ValidationGroup="1">*</asp:RequiredFieldValidator>

                                </div>


                            </div>--%>
                            <div id="lt">
                                <asp:Literal ID="ltDetails" runat="server"></asp:Literal>
                            </div>
                            <%--<table class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">Case Number</th>
                                        <th scope="col">Created Date</th>
                                        <th scope="col">Case Open Date</th>
                                        <th scope="col">Claim Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>Mark</td>
                                        <td>Otto</td>
                                        <td>@mdo</td>
                                    </tr>
                                    <tr>
                                        <th scope="row">2</th>
                                        <td>Jacob</td>
                                        <td>Thornton</td>
                                        <td>@fat</td>
                                    </tr>
                                    <tr>
                                        <th scope="row">3</th>
                                        <td>Larry</td>
                                        <td>the Bird</td>
                                        <td>@twitter</td>
                                    </tr>
                                </tbody>
                            </table>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
