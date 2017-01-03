<%@ Page Title="Item" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="Inventory.PeopleViewer.Administration.Items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelWareHouse" runat="server">
        <ContentTemplate>
            <hr />
            <div class="form-horizontal">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <b><%: Title %></b>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ValidationGroup="Search" placeholder="Search Item" ID="txtItemSearch" CssClass="form-control col-md-offset-1" />
                                    <asp:RequiredFieldValidator ValidationGroup="Search" ID="rfvSearch" runat="server" ControlToValidate="txtItemSearch"
                                        CssClass="text-danger col-md-offset-1" ErrorMessage="The item name field is required." />
                                </div>
                                <asp:Button runat="server" ValidationGroup="Search" ID="btnSearch" Text="Go!" CssClass="btn btn-default" OnClick="btnSearch_Click" />
                                <asp:Button runat="server" ID="btnReset" Text="Reset" CausesValidation="false" CssClass="btn btn-default" OnClick="btnReset_Click" />
                            </div>
                            <div class="row">
                                <InventoryUC:Information runat="server" ID="ucInformation" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gridItems" DataKeyNames="ItemID,CategoryID,ItemTypeID,BrandID" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-striped table-responsive table-bordered table-hover" runat="server"
                                    OnRowDeleting="gridItems_RowDeleting" OnRowUpdating="gridItems_RowUpdating" AllowSorting="true"
                                    OnRowCancelingEdit="gridItems_RowCancelingEdit" OnRowEditing="gridItems_RowEditing"
                                    OnRowDataBound="gridItems_RowDataBound" AllowPaging="true" PageSize="5" OnPageIndexChanging="gridItems_PageIndexChanging"
                                    EmptyDataText="No Records Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item ID">
                                            <ItemTemplate>
                                                <%# Eval("ItemID") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("ItemID") %>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <%# Eval("Description") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewItemDescription" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewItemDescription"
                                                    CssClass="text-danger" ErrorMessage="The item description field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateItemDescription" ValidationGroup="Update" Text='<%# Eval("Description") %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateItemDescription"
                                                    CssClass="text-danger" ErrorMessage="The item description field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Part Number">
                                            <ItemTemplate>
                                                <%# Eval("PartNumber") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewPartNumber" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewPartNumber"
                                                    CssClass="text-danger" ErrorMessage="The part number field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdatePartNumber" ValidationGroup="Update" Text='<%# Eval("PartNumber") %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdatePartNumber"
                                                    CssClass="text-danger" ErrorMessage="The part number field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <%# Eval("CategoryName") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFooterCategory" DataTextField="Name" DataValueField="CategoryID" CssClass="form-control" ValidationGroup="Create" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="ddlFooterCategory"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The category field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEditCategory" DataTextField="Name" DataValueField="CategoryID" CssClass="form-control" ValidationGroup="Update" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="ddlEditCategory"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The category field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Type">
                                            <ItemTemplate>
                                                <%# Eval("ItemTypeName") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFooterItemType" DataTextField="Name" DataValueField="ItemTypeID" CssClass="form-control" ValidationGroup="Create" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="ddlFooterItemType"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The item type field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEditItemType" DataTextField="Name" DataValueField="ItemTypeID" CssClass="form-control" ValidationGroup="Update" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="ddlEditCategory"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The item type field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand">
                                            <ItemTemplate>
                                                <%# Eval("BrandName") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFooterBrand" DataTextField="Name" DataValueField="BrandID" CssClass="form-control" ValidationGroup="Create" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="ddlFooterBrand"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The brand field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEditBrand" DataTextField="Name" DataValueField="BrandID" CssClass="form-control" ValidationGroup="Update" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="ddlEditBrand"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The brand field is required." />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
