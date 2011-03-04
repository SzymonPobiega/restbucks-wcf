<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Restbucks.Service._default" %>
<%@ Import Namespace="System.Web.Routing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ul>
        
        <% foreach (Route route in RouteTable.Routes)
           {%>
           <li><%=route.Url %></li>
           <%
           }%>
           </ul>
    </div>
    </form>
</body>
</html>
