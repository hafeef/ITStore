<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Inventory.PeopleViewer.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="updatePanelRegister" runat="server">
        <ContentTemplate>
            <hr />
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Create a new account</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <br />
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" TextMode="Search" ID="txtFirstName" placeholder="First Name" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName"
                                            CssClass="text-danger" ErrorMessage="The first name is required." />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtLastName" placeholder="Last Name" TextMode="Search" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName"
                                            CssClass="text-danger" ErrorMessage="The last name is required." />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="ddlGender" CssClass="col-md-2 control-label">Gender</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control">
                                            <asp:ListItem Text="-- Select --" Value="0" />
                                            <asp:ListItem Text="Male" Value="1" />
                                            <asp:ListItem Text="Female" Value="2" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" runat="server" ControlToValidate="ddlGender"
                                            CssClass="text-danger" ErrorMessage="The gender is required." />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" TextMode="Email" />
                                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtEmail"
                                            CssClass="text-danger" ErrorMessage="The email field is required." />
                                        <asp:RegularExpressionValidator runat="server" CssClass="text-danger" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ErrorMessage="Invalid Email address." Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" TextMode="Password" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                                            CssClass="text-danger" ErrorMessage="The password field is required." />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtConfirmPassword" placeholder="Confirm Password" TextMode="Password" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword"
                                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                        <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-offset-1  col-md-4">
                                        <asp:Label runat="server" ID="lblInfo" CssClass="text-success control-label"></asp:Label>
                                    </div>
                                    <div class="col-md-offset-2 col-md-4">
                                        <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonSave" OnClick="linkButtonSave_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Create
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonReset" OnClick="linkButtonReset_Click" runat="server">
                                          <span class="glyphicon glyphicon glyphicon-repeat"></span> Reset
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
