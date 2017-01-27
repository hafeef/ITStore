<%@ Page Title="Inventory Scrap" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Scrap.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.Scrap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelInventoryScrap" runat="server">
        <ContentTemplate>
            <hr />
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Inventory Scraps</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ValidationGroup="Search" OnTextChanged="txtItemDescription_TextChanged"
                                            AutoPostBack="true" TextMode="Search" placeholder="Item Description" ID="txtItemDescription" CssClass="form-control col-md-offset-1" />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                            ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="10"
                                            EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                            CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                            runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <div class="col-md-offset-1 col-md-2">
                                        <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonReset" OnClick="btnReset_Click" runat="server">
                                          <span class="glyphicon glyphicon-repeat"></span> Reset
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonSave" OnClick="linkButtonSave_Click" runat="server">
                                          <span class="glyphicon glyphicon-floppy-save"></span> Save
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <InventoryUC:Information runat="server" ID="ucInformation" />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridViewInventoryScraps" DataKeyNames="InventoryScrapID,ItemID,SerialNo" AutoGenerateColumns="false" ShowFooter="true"
                                        CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>"
                                        OnRowDeleting="GridViewInventoryScraps_RowDeleting" OnPageIndexChanging="GridViewInventoryScraps_PageIndexChanging"
                                        OnRowDataBound="GridViewInventoryScraps_RowDataBound" EmptyDataText="No Records Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Description">
                                                <ItemTemplate>
                                                    <%# Eval("ItemDescription") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemDescription" placeholder="Item Description" TextMode="Search" ValidationGroup="Add" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtItemDescription"
                                                        CssClass="text-danger" ErrorMessage="The item description is required." />
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                                        ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="10"
                                                        EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                                        CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                        runat="server">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial Number">
                                                <ItemTemplate>
                                                    <%# Eval("SerialNo") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSerialNumber" TextMode="Search" placeholder="Serial Number" ValidationGroup="Add" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtSerialNumber"
                                                        CssClass="text-danger" ErrorMessage="The serial number is required." />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action Links">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this rack?');" CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonDelete" runat="server">
                                        <span class="glyphicon glyphicon-trash"></span> Delete
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton CssClass="btn btn-primary" ValidationGroup="Add" ID="linkButtonAdd" OnClick="linkButtonAdd_Click" runat="server">
                                            <span class="glyphicon glyphicon-plus"></span> Add
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
            <asp:HiddenField runat="server" ID="hiddenFieldItemID" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=hiddenFieldItemID.ClientID %>").value = e.get_value();
        }
    </script>
</asp:Content>

