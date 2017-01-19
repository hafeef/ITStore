<%@ Page Title="Warehouse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Warehouses.aspx.cs" Inherits="Inventory.PeopleViewer.Administration.Warehouses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelWareHouse" runat="server">
        <ContentTemplate>
            <hr />
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Warehouses</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ValidationGroup="Search" placeholder="Search Warehouse" ID="txtWarehouseSearch" CssClass="form-control col-md-offset-1" />
                                        <asp:RequiredFieldValidator ValidationGroup="Search" ID="rfvSearch" runat="server" ControlToValidate="txtWarehouseSearch"
                                            CssClass="text-danger col-md-offset-1" ErrorMessage="The warehouse name is required." />
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
                                    <asp:GridView ID="gridWarehouse" DataKeyNames="WareHouseID,LocationID" AutoGenerateColumns="false" ShowFooter="true"
                                        CssClass="table table-striped table-bordered table-hover" runat="server"
                                        OnRowDeleting="gridWarehouse_RowDeleting" OnRowUpdating="gridWarehouse_RowUpdating" AllowSorting="true"
                                        OnRowCancelingEdit="gridWarehouse_RowCancelingEdit" OnRowEditing="gridWarehouse_RowEditing"
                                        OnRowDataBound="gridWarehouse_RowDataBound" AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>" OnPageIndexChanging="gridWarehouse_PageIndexChanging"
                                        EmptyDataText="No Records Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Warehouse ID">
                                                <ItemTemplate>
                                                    <%# Eval("WareHouseID") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%# Eval("WareHouseID") %>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warehouse Name">
                                                <ItemTemplate>
                                                    <%# Eval("Name") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNewWarehouse" placeholder="New Warehouse Name" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="txtNewWarehouse"
                                                        CssClass="text-danger" ErrorMessage="The warehouse name is required." />
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtUpdateWarehouse" ValidationGroup="Update" Text='<%# Eval("Name") %>' CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtUpdateWarehouse"
                                                        CssClass="text-danger" ErrorMessage="The warehouse name is required." />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <%# Eval("LocationName") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlFooterLocation" DataTextField="Name" DataValueField="LocationID" CssClass="form-control" ValidationGroup="Create" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="Create" runat="server" ControlToValidate="ddlFooterLocation"
                                                        CssClass="text-danger" InitialValue="0" ErrorMessage="The location is required." />
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEditLocation" DataTextField="Name" DataValueField="LocationID" CssClass="form-control" ValidationGroup="Update" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="ddlEditLocation"
                                                        CssClass="text-danger" InitialValue="0" ErrorMessage="The location is required." />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action Links">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this warehouse?');" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonDelete" runat="server">
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
