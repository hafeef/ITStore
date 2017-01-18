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
                                    <div class="col-md-offset-7 col-md-3">
                                        <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonSearch" ValidationGroup="Search" OnClick="linkButtonSearch_Click" runat="server">
                                            <span class="glyphicon glyphicon-search"></span> Search
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonSave" ValidationGroup="Create" OnClick="linkButtonSave_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Save
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonReset" OnClick="linkButtonReset_Click" runat="server">
                                            Reset
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <InventoryUC:Information runat="server" ID="ucInformation" />
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
