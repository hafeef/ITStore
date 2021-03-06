﻿<%@ Page Title="Receiving Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceivingOrder.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.ReceivingOrder" %>

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
                                          <span class="glyphicon glyphicon glyphicon-repeat"></span> Reset
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
                        </div>
                        <div class="row">
                            <InventoryUC:Information runat="server" ID="ucInformation" />
                        </div>
                        <div class="row">
                            <div class="col-md-12 table-responsive">
                                <asp:GridView Visible="false" DataKeyNames="PurchaseOrderLineItemID,PurchaseOrderID,ItemID" ID="gridLineItems" 
                                    AutoGenerateColumns="false"
                                    CssClass="table table-striped table-bordered table-hover" OnRowDataBound="gridLineItems_RowDataBound"
                                    runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <%# Eval("ItemDescription") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Part No">
                                            <ItemTemplate>
                                                <%# Eval("PartNumber") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <%# Eval("RemainingQuantity") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-2" HeaderText="Serial Nos">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSerialNos" placeholder="Serial Numbers" ValidationGroup="Create" Columns="30"
                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlLocations" ValidationGroup="Create" DataValueField="LocationID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Rack">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRacks" ValidationGroup="Create" DataValueField="RackID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Shelf">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlShelves" ValidationGroup="Create" DataValueField="ShelfID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Warehouse">
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
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Warranty Date">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtWarrantyYear" ValidationGroup="Create" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtExpiry" ValidationGroup="Create" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Receiving Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtQuantity" placeholder="Quantity" ValidationGroup="Create" CssClass="form-control" TextMode="Number">
                                                </asp:TextBox>
                                                <asp:RangeValidator ControlToValidate="txtQuantity" ValidationGroup="Create" CssClass="text-danger" MinimumValue="1" MaximumValue='<%# Eval("RemainingQuantity") %>' Type="Integer"
                                                    Text="Invalid Quantity" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-horizontal" runat="server" visible="false" id="divReceivedItems">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <b>Received Items</b>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12 table-responsive">
                                <asp:GridView Visible="false" DataKeyNames="PurchaseOrderLineItemID,PurchaseOrderID,ItemID,ReceivedLineItemID,WarehouseID,RackID,ShelfID"
                                    ID="GridViewReceivedItems" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                    OnRowCancelingEdit="GridViewReceivedItems_RowCancelingEdit" OnRowDeleting="GridViewReceivedItems_RowDeleting"
                                    runat="server" OnRowEditing="GridViewReceivedItems_RowEditing" OnRowDataBound="GridViewReceivedItems_RowDataBound"
                                    OnRowUpdating="GridViewReceivedItems_RowUpdating" PageSize="<%$ appSettings:GridViewPageSize %>" AllowPaging="true"
                                    OnPageIndexChanging="GridViewReceivedItems_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <%# Eval("ItemDescription") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Part No">
                                            <ItemTemplate>
                                                <%# Eval("PartNumber") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Serial No">
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSerialNos" Text='<%# Eval("SerialNo") %>' placeholder="Serial Numbers" ValidationGroup="Edit"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSerialNos"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Serial no is required." />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# Eval("SerialNo") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Rack">
                                            <ItemTemplate>
                                                <%# Eval("RackName") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRacks" ValidationGroup="Edit" DataValueField="RackID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" runat="server" ControlToValidate="ddlRacks"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Rack is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Shelf">
                                            <ItemTemplate>
                                                <%# Eval("ShelfName") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlShelves" ValidationGroup="Edit" DataValueField="ShelfID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" runat="server" ControlToValidate="ddlShelves"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Shelf is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Warehouse">
                                            <ItemTemplate>
                                                <%# Eval("WarehouseName") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlWarehouses" ValidationGroup="Edit" DataValueField="WarehouseID" DataTextField="Name" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator InitialValue="0" runat="server" ControlToValidate="ddlWarehouses"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Warehouse is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Received Date">
                                            <ItemTemplate>
                                                <%# Eval("ReceivedDate", "{0:dd-MMM-yyyy}") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="txtReceivedDate" ValidationGroup="Edit" Text='<%# Eval("ReceivedDate", "{0:yyyy-MM-dd}") %>' CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReceivedDate"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Received date is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Warranty Date">
                                            <ItemTemplate>
                                                <%# Eval("WarrantyDate", "{0:dd-MMM-yyyy}") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="txtWarrantyYear" ValidationGroup="Edit" Text='<%# Eval("WarrantyDate", "{0:yyyy-MM-dd}") %>' CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtWarrantyYear"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Warranty date is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="col-xs-1" HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <%# Eval("ExpiryDate", "{0:dd-MMM-yyyy}") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="txtExpiry" ValidationGroup="Edit" Text='<%# Eval("ExpiryDate", "{0:yyyy-MM-dd}") %>' CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtExpiry"
                                                    CssClass="text-danger" ValidationGroup="Edit" ErrorMessage="Expiry date is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action Links">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this received item?');" CssClass="btn btn-primary btn-sm" CausesValidation="false" ID="linkButtonDelete" runat="server">
                                        <span class="glyphicon glyphicon-trash"></span> Delete
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                    <asp:LinkButton CommandName="Edit" CssClass="btn btn-primary btn-sm" CausesValidation="false" ID="linkButtonEdit" runat="server">
                                        <span class="glyphicon glyphicon-edit"></span> Edit
                                    </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton CommandName="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="Edit" ID="linkButtonSave" runat="server">
                                        <span class="glyphicon glyphicon-floppy-save"></span> Update
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                    <asp:LinkButton CommandName="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="false" ID="linkButtonCancel" runat="server">
                                        <span class="glyphicon glyphicon-floppy-remove"></span> Cancel
                                    </asp:LinkButton>
                                            </EditItemTemplate>
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
