<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Catalog.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AstRostov.DefaultPage" %>

<%@ Register Src="Controls/Home/Features.ascx" TagName="Features" TagPrefix="uc" %>
<%@ Register Src="Controls/Home/MainSlider.ascx" TagName="MainSlider" TagPrefix="uc" %>
<%@ Register Src="Controls/News/RecentNews.ascx" TagName="RecentNews" TagPrefix="uc" %>
<%@ Register Src="Controls/News/LastMonthsForNewsArchive.ascx" TagName="LastMonthsForNewsArchive" TagPrefix="uc" %>
<%@ Register Src="Controls/Blog/RecentActivity.ascx" TagName="RecentPosts" TagPrefix="uc" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <title>АСТ-Ростов. Главная</title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row-fluid">
        <div class="span12">
            <uc:MainSlider runat="server" ID="ucMainSlider" />
        </div>
    </div>

    <div class="row-fluid">
        <div class="span8">
            <div class="row-fluid">
                <h1>Предложения недели</h1>

            </div>
            <div class="row-fluid hotproperties">
                <div class="span12">
                    <uc:Features runat="server" ID="ucFeatures" />
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="row-fluid">
                <div class="span12">
                    <uc:RecentNews runat="server" ID="ucRecentNews" />
                </div>
            </div>
        </div>
        <div id="sidebar1" class="fluid-sidebar sidebar span4" role="complementary">

            <div id="recent-posts-2" class="widget widget_recent_entries">
                <uc:RecentPosts ID="RecentPosts1" runat="server" />

            </div>
            <%-- <div id="recent-comments-2" class="widget widget_recent_comments">
                    <h4 class="widgettitle">Последние комментарии</h4>
                    <ul id="recentcomments"></ul>
                </div>--%>
            <div id="archives-2" class="widget widget_archive">
                <uc:LastMonthsForNewsArchive runat="server" />

            </div>
        </div>
    </div>
</asp:Content>
