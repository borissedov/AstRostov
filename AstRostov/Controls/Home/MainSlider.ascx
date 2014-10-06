<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainSlider.ascx.cs" Inherits="AstRostov.Controls.Home.MainSlider" %>

<div id="slide-wrapper">
    <div id="banner">
        <asp:Repeater runat="server" ID="rptMailSliderItems" OnItemDataBound="MainSliderItemDataBound">
            <ItemTemplate>
                <div class="oneByOne_item" style="height: 341px;">
                    <img src='<%# ResolveUrl(String.Format("~/img/main-slider/{0}",Eval("ImageFile"))) %>' alt="Placeholder" class="bigImage">
                    <span class="slide5Txt1"><%#Eval("Title") %></span>
                    <asp:Label runat="server" class="slide5Txt2" id="lblPrice"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="buttonArea">
        <div class="buttonCon" style="cursor: pointer; display: none;">
            <a class="theButton" rel="0">1</a>
            <a class="theButton" rel="1">2</a>
            <a class="theButton active" rel="2">3</a>
            <a class="theButton" rel="3">4</a>
        </div>
    </div>
    <div class="arrowButton" style="cursor: pointer; display: none;">
        <div class="prevArrow" style="top: 130.5px;"></div>
        <div class="nextArrow" style="top: 130.5px;"></div>
    </div>

</div>
