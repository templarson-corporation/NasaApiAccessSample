<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>NASA's Astronomy Picture of the Day</h1>
        <p class="lead">One of the most popular websites at NASA is the Astronomy Picture of the Day. In fact, this website is one of the most popular websites across all federal agencies.</p>
        <p><a href="https://api.nasa.gov/api.html#apod" class="btn btn-primary btn-lg">Learn more &raquo;</a>
        </p>

    </div>

    <div class="row">
        <asp:Table ID="Images" runat="server"></asp:Table>
        
        <asp:Image ID="Image1" runat="server" />
    </div>
</asp:Content>
