<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCInformation.ascx.cs" Inherits="Inventory.PeopleViewer.Controls.UCInformation" %>
<div class="col-md-4">
    <asp:Label ID="lblInformation" runat="server" />
</div>
<script type="text/javascript">
    function ClearInformationLablesAutomatically() {
        var seconds = 10;
        setTimeout(function Clear() {
            $("#<%=lblInformation.ClientID %>").text('');
            }, seconds * 1000)
    }
</script>

