<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LegalSystemWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Login - Melstacorp Legal</title>
    <link href="css/styles.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>
</head>

<body class="bg-image">

    <form id="form1" runat="server">
        <div id="layoutAuthentication">
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-6">
                                    <div class="card-header">
                                        <h3 class="text-center font-weight-light my-3<asp:RegularExpressionValidator runat=" regularexpressionvalidator="" server="">Login</h3>
                                    </div>
                                    <div class="card-body">

                                        <div class="form-floating mb-1">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-user"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass=""
                                                ControlToValidate="txtEmail" ErrorMessage="Email is Required">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="txtEmail" ErrorMessage="Email is not valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                *
                                            </asp:RegularExpressionValidator>
                                            <label for="inputEmail">Email address</label>
                                        </div>
                                        <div class="form-floating mb-1">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-user" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtPassword" ErrorMessage="Password Required">*</asp:RequiredFieldValidator>
                                            <label for="inputPassword">Password</label>
                                        </div>
                                        <div class="form-floating mb-1">
                                            <asp:CheckBox ID="chbxRememberMe" runat="server" />
                                            Remember Me
                                        </div>
                                        <div class="form-floating">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                                        </div>
                                        <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                            <a class="small" href="#"></a>
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-user btn-block" />
                                        </div>

                                    </div>
                                    <div class="card-footer text-center py-3">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
            <div id="layoutAuthentication_footer">
                <footer class="py-4 mt-auto">
                    <div class="container-fluid px-4">
                        <div class="d-flex align-items-center justify-content-between small">
                            <div class="text-muted">Copyright &copy; Your Website 2022</div>
                            <div>
                                <a href="#">Privacy Policy</a>
                                &middot;
                                <a href="#">Terms &amp; Conditions</a>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
    </form>

</body>
</html>

