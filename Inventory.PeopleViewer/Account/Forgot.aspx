<%@ Page Title="Forgot password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="Inventory.PeopleViewer.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="updatePanelRegister" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Forgot password</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <br />
                                <div class="row">
                                    <InventoryUC:Information runat="server" ID="ucInformation" />
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" TextMode="Email" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Create" ControlToValidate="txtEmail"
                                            CssClass="text-danger" ErrorMessage="The email field is required." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">New Password</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtPassword" placeholder="Password" TextMode="Password" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Create" ControlToValidate="txtPassword"
                                            CssClass="text-danger" ErrorMessage="The password field is required." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtConfirmPassword" placeholder="Confirm Password" TextMode="Password" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword"
                                            CssClass="text-danger" Display="Dynamic" ValidationGroup="Create" ErrorMessage="The confirm password field is required." />
                                        <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                            CssClass="text-danger" Display="Dynamic" ValidationGroup="Create" ErrorMessage="The password and confirmation password do not match." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-offset-2 col-md-4">
                                        <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonSave" ValidationGroup="Create" OnClick="linkButtonSave_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Change
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
