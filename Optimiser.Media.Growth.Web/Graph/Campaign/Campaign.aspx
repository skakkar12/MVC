<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="WebApplication3.Report.Campaign.Campaign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <h1>MEDIA CHANNEL GRAPH</h1>
   
<%--    <h3>GNU General Public License</h3>--%>
<%--    <span>For the explanation go to the article: <a href="http://www.balsamino.com/component/k2/20-highcharts-databinding-in-c.html" target="_blank">C# Sql Editor</a></span>--%>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://code.highcharts.com/highcharts.js"></script>
    <script type="text/javascript" src="http://code.highcharts.com/modules/exporting.js"></script>
        
    <div>
        <asp:Literal ID="chrtMyChart" runat="server"></asp:Literal>
    </div>

    </form>
</body>
</html>
