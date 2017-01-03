<%@ Page Title="Receiving Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceivingOrder.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.ReceivingOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelVendor" runat="server">
        <ContentTemplate>
            <hr />
            <div class="form-horizontal">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <b>Receiving Order</b>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <asp:Label runat="server" AssociatedControlID="ddlVendors" CssClass="col-md-1 control-label">Vendors</asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList runat="server" ID="ddlVendors" disabled DataValueField="VendorID" DataTextField="Name" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlVendors"
                                        CssClass="text-danger" ErrorMessage="The vendor field is required." />
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtPoOrContractNumber" CssClass="col-md-1 control-label">PO/Contract</asp:Label>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtPoOrContractNumber" placeholder="PO Or Contract Number" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ValidationGroup="Create" ControlToValidate="txtPoOrContractNumber"
                                        CssClass="text-danger" ErrorMessage="The po or contract no is required." />
                                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ValidationGroup="Search" ControlToValidate="txtPoOrContractNumber"
                                        CssClass="text-danger" ErrorMessage="The po or contract no is required." />
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonSearch" ValidationGroup="Search" OnClick="linkButtonSearch_Click" runat="server">
                                            <span class="glyphicon glyphicon-search"></span> Search
                                    </asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonReset" OnClick="linkButtonReset_Click" runat="server">
                                            Reset
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label runat="server" AssociatedControlID="ddlPOType" CssClass="col-md-1 control-label">PO Type</asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList runat="server" disabled ID="ddlPOType" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="-- Select --" />
                                        <asp:ListItem Value="1" Text="PO" />
                                        <asp:ListItem Value="2" Text="Contract" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" runat="server" ControlToValidate="ddlPOType"
                                        CssClass="text-danger" ValidationGroup="Create" ErrorMessage="The PO type field is required." />
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtPoCreatedDate" CssClass="col-md-1 control-label">PO Date</asp:Label>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" disabled ID="txtPoCreatedDate" placeholder="PO Created Date" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ValidationGroup="Create" ControlToValidate="txtPoCreatedDate"
                                        CssClass="text-danger" ErrorMessage="The PO created date is required." />
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonCreate" ValidationGroup="Create" OnClick="linkButtonCreate_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Save
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <InventoryUC:Information runat="server" ID="ucInformation" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView Visible="false" DataKeyNames="PurchaseOrderLineItemID" ID="gridLineItems" AutoGenerateColumns="false"
                                    CssClass="table table-responsive table-striped table-bordered table-hover"
                                    runat="server" OnRowDataBound="gridLineItems_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <%# Eval("ItemDescription") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial Nos">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSerialNos" ValidationGroup="Create" Columns="20" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Part No">
                                            <ItemTemplate>
                                                <%# Eval("PartNumber") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlLocations" ValidationGroup="Create" DataValueField="LocationID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Rack">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRacks" ValidationGroup="Create" DataValueField="RackID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shelf">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlShelves" ValidationGroup="Create" DataValueField="ShelfID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Warehouse">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlWarehouses" ValidationGroup="Create" DataValueField="WarehouseID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtReceivedDate" ValidationGroup="Create" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Warranty Year">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtWarrantyYear" ValidationGroup="Create" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Expiry">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtExpiry" ValidationGroup="Create" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Unit Price">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtUnit" ValidationGroup="Create" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtQuantity" ValidationGroup="Create" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <%# Eval("Price","{0:f3}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hiddenFieldPurchaseOrderID" />
            <asp:HiddenField runat="server" ID="hiddenFieldItemID" />
            <asp:HiddenField runat="server" ID="hiddenFieldLineItemID" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
