<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LastMonthsForNewsArchive.ascx.cs" Inherits="AstRostov.Controls.News.LastMonthsForNewsArchive" %>
<h4 class="widgettitle">Архив новостей</h4>
<ul>
    <asp:Repeater runat="server" ID="rptLastMonthsForNewsArchive">
        <ItemTemplate>
            <li>
                <a href='<%#ResolveUrl(String.Format("~/NewsArchive?year={0}&month={1}", Eval("Year"), Eval("Month")))%>' title='<%#Eval("DateTitle") %>'><%#Eval("DateTitle") %></a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
    <%--<li><a href="#" title="April 2013">Апрель 2013</a></li>
                        <li><a href="#" title="March 2013">Март 2013</a></li>
                        <li><a href="#" title="January 2013">Январь 2013</a></li>
                        <li><a href="#" title="December 2012">Декабрь 2012</a></li>
                        <li><a href="#" title="November 2012">Ноябрь 2012</a></li>
                        <li><a href="#" title="October 2012">Октябрь 2012</a></li>
                        <li><a href="#" title="September 2012">Сентябрь 2012</a></li>
                        <li><a href="#" title="August 2012">Август 2012</a></li>
                        <li><a href="#" title="July 2012">Июль 2012</a></li>--%>
</ul>
