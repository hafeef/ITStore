<%@ Page Title="Inventory Issue" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Issue.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.Issue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelIssue" runat="server">
        <ContentTemplate>
            <hr />
            <div class="container">
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <b>Inventory Issue</b>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtCivilID" CssClass="col-md-2 control-label">Civil ID</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtCivilID" AutoPostBack="true" OnTextChanged="txtCivilID_TextChanged" placeholder="Civil ID" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Add" ControlToValidate="txtCivilID"
                                            CssClass="text-danger" ErrorMessage="The civil id is required." />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderCivilID" TargetControlID="txtCivilID"
                                            ServiceMethod="FindEmployeeByCivilID" MinimumPrefixLength="2" CompletionInterval="10"
                                            EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="EmployeeSelected"
                                            CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                            runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtEmployeeName" CssClass="col-md-2 control-label">Employee Name</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtEmployeeName" disabled placeholder="Employee Name" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" AssociatedControlID="txtHelpDeskTicket" CssClass="col-md-2 control-label">HelpDesk Ticket</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtHelpDeskTicket" placeholder="HelpDesk Ticket" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Add" ControlToValidate="txtHelpDeskTicket"
                                            CssClass="text-danger" ErrorMessage="The helpdesk ticket is required." />
                                    </div>
                                    <div class="col-md-offset-2 col-md-3">
                                        <asp:LinkButton CssClass="btn btn-primary" ID="linkButtonSave" ValidationGroup="Create" OnClick="linkButtonSave_Click" runat="server">
                                            <span class="glyphicon glyphicon-floppy-save"></span> Save
                                        </asp:LinkButton>
                                        <asp:LinkButton CssClass="btn btn-primary" CausesValidation="false" ID="linkButtonReset" OnClick="linkButtonReset_Click" runat="server">
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
                                    <asp:GridView ID="GridInventoryIssue" DataKeyNames="InventoryIssueID,SerialNo,ItemID" AutoGenerateColumns="false" ShowFooter="true"
                                        CssClass="table table-striped table-bordered table-hover" runat="server" AllowPaging="true" PageSize="<%$ appSettings:GridViewPageSize %>"
                                        OnRowDeleting="GridInventoryIssue_RowDeleting" OnRowUpdating="GridInventoryIssue_RowUpdating" AllowSorting="true"
                                        OnRowCancelingEdit="GridInventoryIssue_RowCancelingEdit" OnPageIndexChanging="GridInventoryIssue_PageIndexChanging"
                                        OnRowEditing="GridInventoryIssue_RowEditing" Visible="false" OnRowDataBound="GridInventoryIssue_RowDataBound" EmptyDataText="No Records Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <%# Eval("ItemDescription") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemDescription" placeholder="Item Description" TextMode="Search" ValidationGroup="Add" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtItemDescription"
                                                        CssClass="text-danger" ErrorMessage="The item field is required." />
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                                        ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="10"
                                                        EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                                        CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                        runat="server">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtItemDescription" TextMode="Search" placeholder="Item Description" Text='<%# Eval("ItemDescription") %>' ValidationGroup="Update" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtItemDescription"
                                                        CssClass="text-danger" ErrorMessage="The item field is required." />
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderItemDescription" TargetControlID="txtItemDescription"
                                                        ServiceMethod="GetItemDescription" MinimumPrefixLength="2" CompletionInterval="10"
                                                        EnableCaching="true" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                                        CompletionSetCount="20" ServicePath="~/Services/AutoCompleteService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                        runat="server">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                    <%# Eval("SerialNo") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSerialNo" TextMode="Search" placeholder="Serial No" ValidationGroup="Add" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Add" runat="server" ControlToValidate="txtSerialNo"
                                                        CssClass="text-danger" ErrorMessage="The serial no is required." />
                                                </FooterTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSerialNo" TextMode="Search" placeholder="Serial No" Text='<%# Eval("SerialNo") %>' ValidationGroup="Update" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtSerialNo"
                                                        CssClass="text-danger" ErrorMessage="The serial no is required." />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HelpDesk Ticket">
                                                <ItemTemplate>
                                                    <%# Eval("HelpDeskTicket") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHelpDeskTicket" TextMode="Search" placeholder="Serial No" Text='<%# Eval("HelpDeskTicket") %>' ValidationGroup="Update" CssClass="form-control" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="Update" runat="server" ControlToValidate="txtHelpDeskTicket"
                                                        CssClass="text-danger" ErrorMessage="The helpdesk ticket is required." />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Is Returned">
                                                <ItemTemplate>
                                                    <%# (bool.Parse(Eval("IsReturned").ToString())) ? "Yes" : "No" %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkIsReturned" Checked='<%# Eval("IsReturned") %>' CssClass="checkbox" runat="server" />
                                                    </div>
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
            <asp:HiddenField runat="server" ID="hiddenFieldEmployeeID" />
            <asp:HiddenField runat="server" ID="hiddenFieldItemID" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function EmployeeSelected(sender, e) {
            var employee = e.get_value().split(',');
            $get("<%=hiddenFieldEmployeeID.ClientID %>").value = employee[0];
            $get("<%=txtEmployeeName.ClientID %>").value = employee[1];
        }

        function ClientItemSelected(sender, e) {
            $get("<%=hiddenFieldItemID.ClientID %>").value = e.get_value();
        }
    </script>
</asp:Content>
