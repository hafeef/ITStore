﻿<%@ Page Title="Item" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="Inventory.PeopleViewer.Administration.Items" %>

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
                                <asp:GridView ID="gridItems" DataKeyNames="ItemID,CategoryID,ItemTypeID,BrandID" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-striped table-responsive table-bordered table-hover" runat="server"
                                    OnRowDeleting="gridItems_RowDeleting" OnRowUpdating="gridItems_RowUpdating" AllowSorting="true"
                                    OnRowCancelingEdit="gridItems_RowCancelingEdit" OnRowEditing="gridItems_RowEditing"
                                    OnRowDataBound="gridItems_RowDataBound" ItemType="Inventory.ViewModels.Administration.ItemVM" AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>" OnPageIndexChanging="gridItems_PageIndexChanging"
                                    EmptyDataText="No Records Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item ID">
                                            <ItemTemplate>
                                                <%#: Item.ItemID %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#: Item.ItemID %>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <%#: Item.Description %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewItemDescription" TextMode="Search" placeholder="New Item Description" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewItemDescription"
                                                    CssClass="text-danger" ErrorMessage="The item description field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdateItemDescription" TextMode="Search" placeholder="Item Description" ValidationGroup="Update" Text='<%#: Item.Description %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateItemDescription"
                                                    CssClass="text-danger" ErrorMessage="The item description field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Part Number">
                                            <ItemTemplate>
                                                <%#: Item.PartNumber %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewPartNumber" TextMode="Search" placeholder="New Part Number" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewPartNumber"
                                                    CssClass="text-danger" ErrorMessage="The part number field is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpdatePartNumber" TextMode="Search" placeholder="Part Number" ValidationGroup="Update" Text='<%#: Item.PartNumber %>' CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdatePartNumber"
                                                    CssClass="text-danger" ErrorMessage="The part number field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <%#:Item.CategoryName %>
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
                                                <%#:Item.ItemTypeName %>
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
                                                <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="ddlEditItemType"
                                                    CssClass="text-danger" InitialValue="0" ErrorMessage="The item type field is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand">
                                            <ItemTemplate>
                                                <%#: Item.BrandName %>
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
                                                <asp:LinkButton CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonDelete" runat="server">
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
