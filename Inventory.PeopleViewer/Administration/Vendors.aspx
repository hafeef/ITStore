<%@ Page Title="Vendor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vendors.aspx.cs" Inherits="Inventory.PeopleViewer.Administration.Vendors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelVendor" runat="server">
        <ContentTemplate>
            <hr />
            <div class="form-horizontal">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <b>Vendors</b>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ValidationGroup="Search" placeholder="Search Vendor" ID="txtVendorSearch" CssClass="form-control col-md-offset-1" />
                                    <asp:RequiredFieldValidator ValidationGroup="Search" runat="server" ControlToValidate="txtVendorSearch"
                                        CssClass="text-danger col-md-offset-1" ErrorMessage="The vendor name is required." />
                                </div>
                                <asp:Button runat="server" ValidationGroup="Search" ID="btnSearch" Text="Go!" CssClass="btn btn-default" OnClick="btnSearch_Click" />
                                <asp:LinkButton CssClass="btn btn-default" CausesValidation="false" ID="linkButtonReset" OnClick="btnReset_Click" runat="server">
                                          <span class="glyphicon glyphicon glyphicon-repeat"></span> Reset
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <InventoryUC:Information runat="server" ID="ucInformation" />
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gridVendor" DataKeyNames="VendorID" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>"
                                    OnRowDeleting="gridVendor_RowDeleting" OnRowUpdating="gridVendor_RowUpdating" AllowSorting="true"
                                    OnRowCancelingEdit="gridVendor_RowCancelingEdit" OnPageIndexChanging="gridVendor_PageIndexChanging"
                                    OnRowEditing="gridVendor_RowEditing" ItemType="Inventory.ViewModels.Administration.VendorVM" OnRowDataBound="gridVendor_RowDataBound" EmptyDataText="No Records Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vendor ID">
                                            <ItemTemplate>
                                                <%#: Item.VendorID %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#: Item.VendorID %>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor Name">
                                            <ItemTemplate>
                                                <%#: Item.Name %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewVendor" TextMode="Search" placeholder="New Vendor Name" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewVendor"
                                                    CssClass="text-danger" ErrorMessage="The vendor name is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateVendor" TextMode="Search" placeholder="Vendor Name" ValidationGroup="Update" Text='<%#: Item.Name %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateVendor"
                                                    CssClass="text-danger" ErrorMessage="The vendor name is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <%#: Item.MobileNo %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewMobileNo" TextMode="Phone" placeholder="Mobile No" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" Display="Dynamic" runat="server" ControlToValidate="txtNewMobileNo"
                                                    CssClass="text-danger" ErrorMessage="The mobile no is required." />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="txtNewMobileNo" CssClass="text-danger" Display="Dynamic" ValidationGroup="Create" ErrorMessage="Invalid mobile no."
                                                    ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateMobileNo" TextMode="Phone" placeholder="Mobile No" ValidationGroup="Update" Text='<%#: Item.MobileNo %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" Display="Dynamic" runat="server" ControlToValidate="txtUpdateMobileNo"
                                                    CssClass="text-danger" ErrorMessage="The mobile no is required." />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="txtUpdateMobileNo" CssClass="text-danger" Display="Dynamic" ValidationGroup="Update" ErrorMessage="Invalid mobile no."
                                                    ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E-Mail">
                                            <ItemTemplate>
                                                <%#: Item.Email %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewEmail" TextMode="Email" placeholder="E-Mail" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" Display="Dynamic" runat="server" ControlToValidate="txtNewEmail"
                                                    CssClass="text-danger" ErrorMessage="The email is required." />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="txtNewEmail" CssClass="text-danger" Display="Dynamic" ValidationGroup="Create" ErrorMessage="Invalid e-mail address."
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateEmail" TextMode="Email" placeholder="E-Mail" ValidationGroup="Update" Text='<%#: Item.Email %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateEmail"
                                                    CssClass="text-danger" ErrorMessage="The email is required." />
                                                <asp:RegularExpressionValidator runat="server"
                                                    ControlToValidate="txtUpdateEmail" CssClass="text-danger" Display="Dynamic" ValidationGroup="Update" ErrorMessage="Invalid e-mail address."
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Telephone No">
                                            <ItemTemplate>
                                                <%#: Item.TelephoneNo %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewTelePhoneNo" TextMode="Phone" placeholder="Telephone No" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewTelePhoneNo"
                                                    CssClass="text-danger" ErrorMessage="The telephone no is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateTelePhoneNo" TextMode="Phone" placeholder="Telphone No" ValidationGroup="Update" Text='<%#: Item.TelephoneNo %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateTelePhoneNo"
                                                    CssClass="text-danger" ErrorMessage="The telephone no is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action Links">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this vendor?');" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonDelete" runat="server">
                                        <span class="glyphicon glyphicon-trash"></span> Delete
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                    <asp:LinkButton CommandName="Edit" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonEdit" runat="server">
                                        <span class="glyphicon glyphicon-edit"></span> Edit
                                    </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton CommandName="Update" CssClass="btn btn-primary" ValidationGroup="Update" ID="linkButtonSave" runat="server">
                                        <span class="glyphicon glyphicon-floppy-save"></span> Update
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                    <asp:LinkButton CommandName="Cancel" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonCancel" runat="server">
                                        <span class="glyphicon glyphicon-floppy-remove"></span> Cancel
                                    </asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton CssClass="btn btn-primary" ValidationGroup="Create" ID="linkButtonSave" OnClick="linkButtonSave_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Save
                                                </asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
