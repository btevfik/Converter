<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Currency Converter</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script>
        //show the selected currency rate in CurrentRate label
        $(document).ready(function () {
            var handle = (function () {
                var item = $("#CurrencyBox option:selected");
                $("#CurrentRate").text(item.val());
            });
            $("#CurrencyBox").change(handle).keyup(handle);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <h1>Currency Converter</h1>
            Target Currency
            <br />
            <asp:ListBox CssClass="box" ID="CurrencyBox" runat="server" DataSourceID="Sqldatasource1" DataTextField="Country" DataValueField="Rate" Height="143px" Width="290px" OnSelectedIndexChanged="CurrencyBox_SelectedIndexChanged"></asp:ListBox>

            <br />

            <asp:Button CssClass="button" ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" OnClientClick="if(!confirm('Are you sure?'))return false;"
                Text="Delete" />

            <br />
            <br />

            <div>
                Current Rate:
            <asp:Label ID="CurrentRate" runat="server">N/A</asp:Label>
                , New Rate:
             <asp:TextBox CssClass="box" ID="Rate" runat="server" Width="40px"></asp:TextBox>
                <asp:Button CssClass="button" ID="ModifyButton" runat="server" OnClick="ModifyButton_Click" Text="Update" />
            </div>

            <br />

            <div>
                New Currency:
        <asp:TextBox CssClass="box" ID="NewCurrency" runat="server"></asp:TextBox>
                , Rate:
        <asp:TextBox CssClass="box" ID="NewRate" runat="server" Width="40px"></asp:TextBox>
                <asp:Button CssClass="button" ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Add" />
            </div>

            <br />
            <br />
            Amount in U.S Dollars
            <br />
            <asp:TextBox CssClass="box" ID="USD" Width="256" runat="server" />
            <br />
            <br />
            <asp:Button CssClass="button" Text="Convert" ID="ConvertButton" Width="124px" OnClick="ConvertButton_Click" runat="server" />
            <br />
            <br />
            <asp:Label ID="Output" runat="server" />
        </div>

        <div class="bottom">
            <p style="text-align: center">&copy; Baris Tevfik</p>
        </div>

        <asp:SqlDataSource ID="Sqldatasource1" runat="server" ConnectionString="<%$ ConnectionStrings:CurrencyConverterDB %>" SelectCommand="SELECT Id, Country, Rate FROM [Table] ORDER BY Country" DeleteCommand="DELETE FROM [Table] WHERE (Country = @countryName)" InsertCommand="INSERT INTO [Table] (Country, Rate) VALUES (@newCurrency, @newRate)" UpdateCommand="UPDATE [Table] SET Rate = @newRate WHERE (Country = @country)">
            <DeleteParameters>
                <asp:ControlParameter ControlID="CurrencyBox" Name="countryName" PropertyName="SelectedItem.Text" />
            </DeleteParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="NewCurrency" Name="newCurrency" PropertyName="Text" />
                <asp:ControlParameter ControlID="NewRate" Name="newRate" PropertyName="Text" />
            </InsertParameters>
            <UpdateParameters>
                <asp:ControlParameter ControlID="Rate" Name="newRate" PropertyName="Text" />
                <asp:ControlParameter ControlID="CurrencyBox" Name="country" PropertyName="SelectedItem.Text" />
            </UpdateParameters>
        </asp:SqlDataSource>

    </form>
</body>
</html>
