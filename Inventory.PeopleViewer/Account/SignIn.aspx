<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Inventory.PeopleViewer.Account.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In</title>
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
</head>
<body style="background-image: url('../Images/bg.png');">
    <div class="container">
        <form id="form1" class="form-signin" runat="server">
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
                                <div class="col-md-offset-1 form-group">
                                    <asp:Label runat="server" AssociatedControlID="Email" CssClass="control-label">Email</asp:Label>
                                    <asp:TextBox runat="server" ID="Email" placeholder="Email address" CssClass="form-control input-lg" TextMode="Email" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                        CssClass="text-danger" ErrorMessage="The email field is required." />
                                </div>
                                <div class="col-md-offset-1 form-group">
                                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Password</asp:Label>
                                    <asp:TextBox runat="server" ID="Password" placeholder="Password" TextMode="Password" CssClass="form-control input-lg" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                                </div>
                                <div class="col-md-offset-1 checkbox">
                                    <label>
                                        <asp:CheckBox runat="server" ID="RememberMe" />Remember me?
                                    </label>
                                </div>
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnLoginIn" OnClick="btnLoginIn_Click" Text="Log in" CssClass="btn btn-primary" />
                                    <asp:Button runat="server" Text="Reset" ID="btnReset" OnClick="btnReset_Click" CausesValidation="false" CssClass="btn btn-primary" />
                                </div>
                                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                    <p class="text-danger">
                                        <asp:Literal runat="server" ID="FailureText" />
                                    </p>
                                </asp:PlaceHolder>
                            </section>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
