<%@ Page Title="Shelf" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shelves.aspx.cs" Inherits="Inventory.PeopleViewer.Administration.Shelves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelShelves" runat="server">
        <ContentTemplate>
            <hr />
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Shelves</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ValidationGroup="Search" placeholder="Search Shelf" ID="txtShelfSearch" CssClass="form-control col-md-offset-1" />
                                        <asp:RequiredFieldValidator ValidationGroup="Search" runat="server" ControlToValidate="txtShelfSearch"
                                            CssClass="text-danger col-md-offset-1" ErrorMessage="The shelf name field is required." />
                                    </div>
                                    <asp:Button runat="server" ValidationGroup="Search" ID="btnSearch" Text="Go!" CssClass="btn btn-default" OnClick="btnSearch_Click" />
                                    <asp:Button runat="server" ID="btnReset" Text="Reset" CausesValidation="false" CssClass="btn btn-default" OnClick="btnReset_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <InventoryUC:Information runat="server" ID="ucInformation" />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gridShelves" DataKeyNames="ShelfID" AutoGenerateColumns="false" ShowFooter="true"
                                        CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>"
                                        OnRowDeleting="gridShelves_RowDeleting" OnRowUpdating="gridShelves_RowUpdating" AllowSorting="true"
                                        OnRowCancelingEdit="gridShelves_RowCancelingEdit" OnPageIndexChanging="gridShelves_PageIndexChanging"
                                        OnRowEditing="gridShelves_RowEditing" OnRowDataBound="gridShelves_RowDataBound" EmptyDataText="No Records Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Shelf ID">
                                                <ItemTemplate>
                                                    <%# Eval("ShelfID") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%# Eval("ShelfID") %>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shelf Name">
                                                <ItemTemplate>
                                                    <%# Eval("Name") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNewShelf" placeholder="New Shelf Name" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewShelf"
                                                        CssClass="text-danger" ErrorMessage="The shelf name field is required." />
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtUpdateShelf" ValidationGroup="Update" Text='<%# Eval("Name") %>' CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateShelf"
                                                        CssClass="text-danger" ErrorMessage="The shelf name field is required." />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action Links">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="Delete" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonDelete" runat="server">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
