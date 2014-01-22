<%@ Page Title="" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="AstRostov.BlogPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>АСТ-Ростов. Бортжурнал</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnItemId" />
    <div>
        <h1>
            <asp:Literal runat="server" ID="litBlogTitle"></asp:Literal></h1>
    </div>
    <section>
        <p>
            <span class="label">Марка авто:</span>
            <asp:Label runat="server" ID="lblManufacturer"></asp:Label>
        </p>
        <p>
            <span class="label">Модель авто:</span>
            <asp:Label ID="lblModel" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">Год выпуска:</span>
            <asp:Label ID="lblManufYear" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">Год покупки:</span>
            <asp:Label ID="lblBuyYear" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">Цвет:</span>
            <asp:Label ID="lblColor" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">Комментарий к модели:</span>
            <asp:Label ID="lblModelComment" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label runat="server" ID="lblDriving" Text="Я езжу на этой машине"></asp:Label>
        </p>
        <p>
            <asp:Label runat="server" ID="lblSelling" Text="Я продаю эту машину"></asp:Label>
        </p>
        <p>
            <span class="label">Мощность:</span>
            <asp:Label ID="lnlPower" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">Объем двигателя:</span>
            <asp:Label ID="lblEngineVolume" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">Госномер:</span>
            <asp:Label ID="lblGosNumber" runat="server"></asp:Label>
        </p>
        <p>
            <span class="label">VIN-код:</span>
            <asp:Label ID="lblVin" runat="server"></asp:Label>
        </p>
    </section>

    <div class="container">
        <asp:Repeater runat="server" ID="rptPosts" OnItemDataBound="rptPosts_OnItemDataBound">
            <ItemTemplate>
                <div class="row-fluid">
                    <div class="span12">
                        <div>
                            <h2 id="title" itemprop="name headline">
                                <a href="#"><%#Eval("Title") %></a>
                            </h2>
                        </div>
                        <div>
                            <i class="icon-calendar icon-white"></i>
                            <time itemprop="datePublished"><%=((DateTime)Eval("Created")).ToString("F") %></time>
                        </div>
                        <div>
                            <i class="icon-tags icon-white"></i>
                            <span class="label label-info">Блог</span>
                        </div>
                    </div>
                </div>
                <div class="row-fluid" itemprop="articleBody">
                    <div class="span12">
                        <%#Eval("ShortContent") %>
                        <div class="pull-right">
                            <asp:HyperLink class="btn btn-inverse" runat="server" ID="hlinkEdit" ToolTip="Редактировать"><i class="icon-white icon-edit"></i></asp:HyperLink>
                            <a class="btn" href='<%#ResolveUrl(String.Format("~/Post.aspx?id={0}", Eval("PostId"))) %>'>Читать полностью »</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="pagination pagination-centered pagination-inverse">
            <ul>
                <li class='<%= 1 == CurrentPageNo ? "disabled" : String.Empty %>'>
                    <a class="btn-inverse" href='<% = CurrentPageNo == 1 ? "#" : ResolveClientUrl(String.Format("~/Blog.aspx?id={1}&page={0}", CurrentPageNo - 1, ItemId)) %>'>« Предыдущая
                    </a>
                </li>

                <asp:Repeater runat="server" ID="rptPaging">
                    <ItemTemplate>
                        <li class='<%# (int)Eval("PageNo") == CurrentPageNo ? "active" : String.Empty %>'>
                            <a class="btn-inverse" href='<%# (int)Eval("PageNo") == CurrentPageNo ? "#" : ResolveClientUrl(String.Format("~/Blog.aspx?id={1}&page={0}", Eval("PageNo"), ItemId)) %>'>
                                <%#Eval("PageNo") %>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>

                <li class='<%= PageCount == CurrentPageNo ? "disabled" : String.Empty %>'>
                    <a class="btn-inverse" href='<% = CurrentPageNo == PageCount ? "#" : ResolveClientUrl(String.Format("~/Blog.aspx?id={1}&page={0}", CurrentPageNo + 1, ItemId)) %>'>Следующая »
                    </a>
                </li>
            </ul>
        </div>
        <%--<div class="row-fluid">
            <div class="span12">
                <div>
                    <h2 id="title" itemprop="name headline">
                        <a href="#">Его первый пост</a>
                    </h2>
                </div>
                <div>
                    <i class="icon-calendar icon-white"></i>
                    <time itemprop="datePublished"><%=DateTime.Now.ToString("F") %></time>
                </div>
                <div>
                    <i class="icon-tags icon-white"></i>
                    <span class="label label-info">Тюнинг</span>
                    <span class="label label-info">Двигатель</span>
                </div>
            </div>
        </div>
        <div class="row-fluid" itemprop="articleBody">
            <div class="span12">
                <p>
                    <img src="http://copy.com/RBA9WHvgtxgt/personne.png" style="float: left; margin-right: 5px" alt="Quelqu'un?" title="Quelqu'un?" width="196" height="139">
                    C’est l’histoire de quatre individus: Chacun, Quelqu’un, Quiconque et Personne. Un travail important devait être fait, et on avait demandé à Chacun de s’en occuper.
                </p>
                <p>Chacun était assuré que Quelqu’un allait le faire. Quiconque aurait pu s’en occuper, mais Personne ne l’a fait..</p>
                <div class="pull-right"><a class="btn" href="#">Читать полностью »</a></div>
            </div>
        </div>--%>
    </div>

</asp:Content>
