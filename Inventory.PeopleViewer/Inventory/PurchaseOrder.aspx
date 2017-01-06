<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.PurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelVendor" runat="server">
        <ContentTemplate>
            <hr />
            <div class="form-horizontal">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <b>Purchase Order</b>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <asp:Label runat="server" AssociatedControlID="ddlVendors" CssClass="col-md-1 control-label">Vendors</asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList runat="server" ID="ddlVendors" DataValueField="VendorID" DataTextField="Name" CssClass="form-control">
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
                                    <asp:DropDownList runat="server" ID="ddlPOType" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="-- Select --" />
                                        <asp:ListItem Value="1" Text="PO" />
                                        <asp:ListItem Value="2" Text="Contract" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" runat="server" ControlToValidate="ddlPOType"
                                        CssClass="text-danger" ValidationGroup="Create" ErrorMessage="The PO type field is required." />
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtPoCreatedDate" CssClass="col-md-1 control-label">PO Date</asp:Label>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtPoCreatedDate" placeholder="PO Created Date" TextMode="Date" CssClass="form-control"></asp:TextBox>
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
                                <asp:GridView ID="gridLineItems" DataKeyNames="ItemID,PurchaseOrderLineItemID,SrNo" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-striped table-bordered table-hover"
                                    OnRowCancelingEdit="gridLineItems_RowCancelingEdit" OnRowEditing="gridLineItems_RowEditing"
                                    OnRowDataBound="gridLineItems_RowDataBound" OnRowUpdating="gridLineItems_RowUpdating"
                                    OnRowDeleting="gridLineItems_RowDeleting" OnPageIndexChanging="gridLineItems_PageIndexChanging"
                                    runat="server" AllowPaging="true" PageSize="5">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <%# Eval("ItemDescription") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtItemDescription" placeholder="Item Description" TextMode="Search" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtItemDescription"
                                                    CssClass="text-danger" ErrorMessage="The item field is required." />
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                                    ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="100"
                                                    EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                                    CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                    runat="server">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtItemDescription" placeholder="Item Description" Text='<%# Eval("ItemDescription") %>' ValidationGroup="Update" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtItemDescription"
                                                    CssClass="text-danger" ErrorMessage="The item field is required." />
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                                    ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="300"
                                                    EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                                    CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                    runat="server">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <%# Eval("PurchasedQuantity") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtQuantity" placeholder="Quantity" TextMode="Number" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtQuantity"
                                                    CssClass="text-danger" ErrorMessage="The quantity is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQuantity" placeholder="Quantity" TextMode="Number" Text='<%# Eval("PurchasedQuantity") %>' ValidationGroup="Update" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtQuantity"
                                                    CssClass="text-danger" ErrorMessage="The quantity is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <%# Eval("Price","{0:f3}") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPrice" placeholder="Price" TextMode="Number" ValidationGroup="Create" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtPrice"
                                                    CssClass="text-danger" ErrorMessage="The price is required." />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPrice" placeholder="Price" TextMode="Number" Text='<%# Eval("Price","{0:f3}") %>' ValidationGroup="Update" CssClass="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtPrice"
                                                    CssClass="text-danger" ErrorMessage="The price is required." />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <%# Eval("Total","{0:f3}") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("Total","{0:f3}") %>
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
                                                <asp:LinkButton CssClass="btn btn-primary" ValidationGroup="Add" ID="linkButtonAdd" OnClick="linkButtonAdd_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Add
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
            <asp:HiddenField runat="server" ID="hiddenFieldPurchaseOrderID" />
            <asp:HiddenField runat="server" ID="hiddenFieldItemID" />
            <asp:HiddenField runat="server" ID="hiddenFieldLineItemID" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hiddenFieldItemID.ClientID %>").value = e.get_value();
        }
    </script>
</asp:Content>
