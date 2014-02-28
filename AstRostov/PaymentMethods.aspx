﻿<%@ Page Language="C#" MasterPageFile="Simple.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <title>АСТ-Ростов. Способы оплаты</title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h1>Способы оплаты</h1>
    <br />
    <p>
        Заказы на нашем сайте вы можетеоплачивать двумя способами:
    </p>
    <ul>
        <li>через платежную систему ROBOKASSA</li>
        <li>оплатой по квитанции в отделении банка</li>
    </ul>
    <p>
        При оплате с использованием системы ROBOKASSA вы будуте перенаправлены на сайт платежной системы, 
        где и осуществите платеж выбранным методом.
    </p>
    <p>
        При способе оплаты банковским переводом для вас будет сформирована квитация с данными для оплаты заказа, 
        суммой и нашими реквизитами, которую вы сможете распечатать для заполнения вашими данными и оплаты в отделении банка.
        <br />
        После этого вы должны будете оповестить нас об оплате заказа с помощью формы обратной связи, 
        которую вы можете найти на странице деталей заказа в разделе "Список заказов" или по телефону указанному на странице контактов.
    </p>
    <p>
        Если у вас возникли трудности с оплатой заказа, пожалуйста, свяжитесь с нами! Наши специалисты окажут вам необходимую помощь в работе с сайтом.
    </p>
    <hr />
    <h3>Информация о принимаемых электронных деньгах</h3>
    <div class="row-fluid">
        <div class="span1">
            <a href="https://money.yandex.ru" target="_blank">
                <img src="https://money.yandex.ru/img/yamoney_logo120x60.gif"
                    alt="Я принимаю Яндекс.Деньги"
                    title="Я принимаю Яндекс.Деньги" border="0" width="120" height="60" /></a>
        </div>
        <div class="span11">
            <p>
                Яндекс.Деньги – доступный и безопасный способ платить за товары
и услуги через интернет. Оплачивать заказы можно в реальном времени 
                <a href="http://money.yandex.ru/" target="_blank">на сайте платежной системы</a>. 
                <br />
                Интерфейс платежной системы «Яндекс.Деньги» прост, понятен 
и удобен как для опытных, так и для начинающих пользователей. 
                <br />
                Для открытия счета в Яндекс.Деньгах достаточно зарегистрироваться на сайте. 
                <br />
                Следующий шаг - внесение денег на счет. Это можно сделать через терминалы, 
системы денежных переводов, предоплаченные карты, 
а также банки (в том числе банковские карты) во всех регионах России.
                <br />
                Деньги со счета можно в любой момент использовать для расчетов за услуги
и покупки через интернет. Платежи в интернет-магазин Яндекс.Деньгами - 
мгновенные, комиссия за них не взимается.
                <br />
                Посмотрите, какие 
                <a href="http://money.yandex.ru/prepaid/" target="_blank">возможности пополнения счета</a>
                есть в вашем регионе, и <a href="http://money.yandex.ru/" target="_blank">откройте счет в Яндекс.Деньгах</a>.
            </p>
        </div>
    </div>
    <br />
    <div class="row-fluid">
        <div class="span1">
            <img src="http://www.webmoney.ru/img/icons/88x31_wm_blue_on_white_ru.png" alt="WebMoney" />
        </div>
        <div class="span11">
            <p>
                Подробнее об оплате чере систему WebMoney Transfer вы можете узнать по <a href="http://wiki.webmoney.ru/projects/webmoney/wiki/%D0%9E%D0%BF%D0%BB%D0%B0%D1%82%D0%B0_%D1%82%D0%BE%D0%B2%D0%B0%D1%80%D0%BE%D0%B2_%D0%B8_%D1%83%D1%81%D0%BB%D1%83%D0%B3_%D1%87%D0%B5%D1%80%D0%B5%D0%B7_%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81_%D0%9C%D0%B5%D1%80%D1%87%D0%B0%D0%BD%D1%82">ссылке</a>.  
            </p>
        </div>
    </div>
</asp:Content>
