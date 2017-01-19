<%@ Page Title="Inventory Transfer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Transfer.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelTransfer" runat="server">
        <ContentTemplate>
            <hr />
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Transfer</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtItemDescription" CssClass="col-md-2 control-label">Item Description</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtItemDescription" placeholder="Item Description" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ValidationGroup="Create" ControlToValidate="txtItemDescription"
                                            CssClass="text-danger" ErrorMessage="The item description is required." />
                                        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ValidationGroup="Search" ControlToValidate="txtItemDescription"
                                            CssClass="text-danger" ErrorMessage="The item description is required." />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                            ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="10"
                                            EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                            CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                            runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtSerialNo" CssClass="col-md-2 control-label">Serial Nos</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtSerialNo" placeholder="Serial Nos" TextMode="MultiLine" Height="80" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Create" ControlToValidate="txtSerialNo"
                                            CssClass="text-danger" ErrorMessage="The serial no is required." />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="ddlFromWarehouses" CssClass="col-md-2 control-label">From Warehouse</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlFromWarehouses" DataValueField="WareHouseID" DataTextField="Name" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlFromWarehouses"
                                            CssClass="text-danger" ErrorMessage="From warehouse is required." />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlToWarehouses" CssClass="col-md-2 control-label">To Warehouse</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlToWarehouses" DataValueField="WareHouseID" DataTextField="Name" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlToWarehouses"
                                            CssClass="text-danger" ErrorMessage="To warehouse is required." />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="ddlFromRacks" CssClass="col-md-2 control-label">From Rack</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlFromRacks" DataValueField="RackID" DataTextField="Name" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlFromRacks"
                                            CssClass="text-danger" ErrorMessage="From rack is required." />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlToRacks" CssClass="col-md-2 control-label">To Rack</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlToRacks" DataValueField="RackID" DataTextField="Name" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlToRacks"
                                            CssClass="text-danger" ErrorMessage="To rack is required." />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="ddlFromshelves" CssClass="col-md-2 control-label">From Shelf</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlFromshelves" DataValueField="ShelfID" DataTextField="Name" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlFromshelves"
                                            CssClass="text-danger" ErrorMessage="From shelf is required." />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlToshelves" CssClass="col-md-2 control-label">To Shelf</asp:Label>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlToshelves" DataValueField="ShelfID" DataTextField="Name" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="0" ValidationGroup="Create" runat="server" ControlToValidate="ddlToshelves"
                                            CssClass="text-danger" ErrorMessage="To shelf is required." />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtTransferredDate" CssClass="col-md-2 control-label">Transferred Date</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtTransferredDate" placeholder="Transferred Date" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Create" ControlToValidate="txtTransferredDate"
                                            CssClass="text-danger" ErrorMessage="Transferred date is required." />
                                    </div>
                                    <div class="col-md-offset-2 col-md-3">
                                        <asp:LinkButton CssClass="btn btn-info" ID="linkButtonSearch" ValidationGroup="Search" OnClick="linkButtonSearch_Click" runat="server">
                                            <span class="glyphicon glyphicon-search"></span> Search
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-success" ID="linkButtonSave" ValidationGroup="Create" OnClick="linkButtonSave_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Save
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-warning" CausesValidation="false" ID="linkButtonReset" OnClick="linkButtonReset_Click" runat="server">
                                          <span class="glyphicon glyphicon glyphicon-repeat"></span> Reset
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <InventoryUC:Information runat="server" ID="ucInformation" />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridTransferHistory" DataKeyNames="TransferID" AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered table-hover" Visible="false" runat="server"
                                        AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>"
                                        OnRowDeleting="GridTransferHistory_RowDeleting" AllowSorting="true"
                                        OnPageIndexChanging="GridTransferHistory_PageIndexChanging"
                                        OnRowDataBound="GridTransferHistory_RowDataBound" OnRowCreated="GridTransferHistory_RowCreated"
                                        EmptyDataText="No Records Found." GridLines="Vertical">
                                        <Columns>
                                            <asp:BoundField HeaderText="From Warehouse" DataField="FromWarehouseName" />
                                            <asp:BoundField HeaderText="To Warehouse" DataField="ToWarehouseName" />
                                            <asp:BoundField HeaderText="From Rack" DataField="FromRackName" />
                                            <asp:BoundField HeaderText="To Rack" DataField="ToRackName" />
                                            <asp:BoundField HeaderText="From Shelf" DataField="FromShelfName" />
                                            <asp:BoundField HeaderText="To Shelf" DataField="ToShelfName" />
                                            <asp:BoundField HeaderText="Transferred Date" DataField="TransferDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:TemplateField HeaderText="Action Links">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="Delete" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this event?');" CausesValidation="false" ID="linkButtonDelete" runat="server">
                                        <span class="glyphicon glyphicon-trash"></span> Delete
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hiddenFieldItemID" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hiddenFieldItemID.ClientID %>").value = e.get_value();
        }
    </script>
</asp:Content>
