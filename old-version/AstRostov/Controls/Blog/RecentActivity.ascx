<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentActivity.ascx.cs" Inherits="AstRostov.Controls.Blog.RecentActivity" %>
<h4 class="widgettitle">Последние статьи</h4>
<ul>
    <asp:Repeater runat="server" ID="rptRecentPosts">
        <ItemTemplate>
            <li>
                <a href='<%#ResolveUrl(String.Format("~/Post.aspx?id={0}", Eval("PostId"))) %>'><%#Eval("Author")%> - <%#Eval("Title")%></a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
   <%-- <li>
        <a href="#" title="Nuffield Theatre Pitches">Nuffield Theatre Pitches</a>
    </li>
    <li>
        <a href="#" title="What is that Ugly Building on Campus?">What is that Ugly Building on Campus?</a>
    </li>
    <li>
        <a href="#" title="Money, Money, Money!">Money, Money, Money!</a>
    </li>
    <li>
        <a href="#" title="#Engage12 – P-P-Pick up a Performing Arts!">#Engage12 – P-P-Pick up a Performing Arts!</a>
    </li>
    <li>
        <a href="#" title="#Engage12 – Take it to the Floor">#Engage12 – Take it to the Floor</a>
    </li>--%>
</ul>
