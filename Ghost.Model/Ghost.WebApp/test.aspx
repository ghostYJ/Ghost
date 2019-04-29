<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="btnClick" OnClick="btnClick_Click" Text="保存" />
            <asp:Label ID="lblTest" runat="server"></asp:Label>
        </div>
        <asp:GridView runat="server" ID="gv" OnRowDataBound="gv_RowDataBound" ShowHeaderWhenEmpty="true" OnRowCommand="gv_RowCommand" AutoGenerateColumns="true">
            <Columns>
                <asp:TemplateField HeaderText="按钮">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnChange" OnClick="btnChange_Click" CommandName="Change" Text="变化"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="tbTest1"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数据">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblTest2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>

</body>
</html>
