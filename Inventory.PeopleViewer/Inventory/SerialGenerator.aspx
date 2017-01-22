<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SerialGenerator.aspx.cs" Inherits="Inventory.PeopleViewer.Inventory.SerialGenerator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtNumberOfSerials" TextMode="Number" />
            <asp:Button Text="Generate" runat="server" OnClick="GenerateSerials" />
        </div>
    </form>
</body>
</html>
