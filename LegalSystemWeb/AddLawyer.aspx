<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLawyer.aspx.cs" Inherits="LegalSystemWeb.AddLawyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 40px;"></div>
    <div class="card" style="width: 70%; margin-left: auto; margin-right: auto">
        <h2 style="text-align: center; margin-bottom: 40px; margin-top: 30px;">Add Lawyer</h2>
        <div class="row mb-5" style="text-align: center; width: 100%; padding-left: 20px;">
            <div style="text-align: start; padding-left: 42px; margin-bottom: 5px;">
                <asp:Literal ID="Literal2" runat="server" Text="Lawyer Name"></asp:Literal>
            </div>
            <div class="col-sm-6" style="width: 100%; padding-left: 40px; padding-right: 40px; margin-bottom: 0px;">
                <asp:TextBox Style="width: 100%;" ID="txtName" runat="server"></asp:TextBox>
                <div class="d-flex text-danger">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtName" ErrorMessage="Name is Required" ValidationGroup="1">Name is Required </asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 40px; padding-right: 10px; margin-bottom: 0px;">
                <div style="text-align: start; padding-left: 2px; margin-bottom: 5px;">
                    <asp:Literal ID="Literal1" runat="server" Text="E-mail Address"></asp:Literal>
                </div>
                <asp:TextBox Style="width: 100%;" ID="txtEmail" runat="server"></asp:TextBox>
            <div class="col-sm-6" style="width: 50%; padding-left: 40px; padding-right: 10px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="txtEmail" runat="server" ValidationGroup="1"></asp:TextBox>
                <div class="d-flex text-danger">
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Email Required">Email is Required</asp:RequiredFieldValidator>--%>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Invalid email address"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">Email is not valid</asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-sm-6" style="width: 50%; padding-left: 10px; padding-right: 40px; margin-bottom: 0px;">
                <div style="text-align: start; padding-left: 2px; margin-bottom: 5px;">
                    <asp:Literal ID="Literal3" runat="server" Text="Contact Number"></asp:Literal>
                </div>
                <asp:TextBox Style="width: 100%;" ID="txtContact" runat="server"></asp:TextBox>
            <div class="col-sm-6" style="width: 50%; padding-left: 10px; padding-right: 40px; margin-bottom: 20px;">
                <asp:TextBox Style="width: 100%;" ID="txtContact" runat="server" ValidationGroup="1"></asp:TextBox>
                <div class="d-flex text-danger">
                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                        ControlToValidate="txtContact" ErrorMessage="Contact no. is Required">Required</asp:RequiredFieldValidator>--%>

                    <div class="d-flex text-danger">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="PhoneNumber is not  valid"
                            ControlToValidate="txtContact" ValidationExpression="[0-9]{10}" ValidationGroup="1">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>


            </div>
            <div class="col-sm-6" style="width: 30%; margin-left: auto; margin-right: auto">
                <asp:Button ID="btnSave" runat="server" Text="Add" Style="width: 80%;" OnClick="btnSave_Click" ValidationGroup="1" />
            </div>
            <div class="table-responsive" style="width: 100%; padding-left: 40px; padding-right: 40px;">
                <asp:GridView Style="margin-top: 30px;" ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                    <Columns>
                        <asp:BoundField DataField="LawyerId" HeaderText="Lawyer Id" />
                        <asp:BoundField DataField="LawyerName" HeaderText="Lawyer Name" />
                        <asp:BoundField DataField="LawyerEmail" HeaderText="E-mail" />
                        <asp:BoundField DataField="LawyerContact" HeaderText="Contact No." />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click">Edit                                 
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btndelete" runat="server">Delete
                                    <span class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div style="height: 40px;"></div>
</asp:Content>
