<%@ Page Language="C#" AutoEventWireup="true" ViewStateMode="Disabled" CodeBehind="SignIn.aspx.cs" Inherits="Inventory.PeopleViewer.Account.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - IT Store</title>
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
</head>
<body style="background-image: url('../Images/bg.png');">
    <div class="container">
        <form id="form1" defaultfocus="Email" defaultbutton="linkButtonLogin" runat="server">

            <div class="row">
                <div class="col-md-offset-4 col-md-4" style="margin-top: 100px;">
                    <div class="panel panel-primary">
                        <div class="panel-heading text-center">
                            <h4>
                                <b>Sign In </b>
                            </h4>
                        </div>
                        <div class="panel-body">
                            <section id="loginForm">
                                <br />
                                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                    <p class="col-md-offset-1 text-danger">
                                        <asp:Literal runat="server" ID="FailureText" />
                                    </p>
                                </asp:PlaceHolder>
                                <div class="col-md-offset-1 form-group">
                                    <asp:Label runat="server" AssociatedControlID="Email" CssClass="control-label">Email</asp:Label>
                                    <asp:TextBox runat="server" ID="Email" placeholder="Email address" CssClass="form-control input-lg" TextMode="Email" />
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="Email" ValidationGroup="LogIn"
                                        CssClass="text-danger" ErrorMessage="The email field is required." />
                                    <asp:RegularExpressionValidator runat="server" CssClass="text-danger" ValidationGroup="LogIn" ControlToValidate="Email"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email address." Display="Dynamic" />
                                </div>
                                <div class="col-md-offset-1 form-group">
                                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Password</asp:Label>
                                    <asp:TextBox runat="server" ID="Password" placeholder="Password" TextMode="Password" CssClass="form-control input-lg" />
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="LogIn" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                                </div>
                                <div class="col-md-offset-1 checkbox">
                                    <label>
                                        <asp:CheckBox runat="server" ID="RememberMe" />Remember me?
                                    </label>
                                    <label>
                                        <asp:HyperLink runat="server" NavigateUrl="~/Account/Forgot.aspx">
                                            Forgot Password?
                                        </asp:HyperLink>
                                    </label>
                                </div>
                                <div class="text-center">
                                    <asp:LinkButton CssClass="btn btn-primary" ValidationGroup="LogIn" ID="linkButtonLogin" OnClick="linkButtonLogin_Click" runat="server">
                                                     <span class="glyphicon glyphicon-log-in"></span> Login
                                    </asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonReset" OnClick="linkButtonReset_Click" runat="server">
                                                     <span class="glyphicon glyphicon-repeat"></span> Reset
                                    </asp:LinkButton>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
