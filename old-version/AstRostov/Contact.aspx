<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Simple.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="AstRostov.Contact" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <title>АСТ-Ростов. Контакты</title>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
    <script type="text/javascript">
        var map;
        var TILE_SIZE = 256;
        var almaz = new google.maps.LatLng(47.273690, 39.822944);

        function bound(value, opt_min, opt_max) {
            if (opt_min != null) value = Math.max(value, opt_min);
            if (opt_max != null) value = Math.min(value, opt_max);
            return value;
        }

        function degreesToRadians(deg) {
            return deg * (Math.PI / 180);
        }

        function radiansToDegrees(rad) {
            return rad / (Math.PI / 180);
        }

        /** @constructor */
        function MercatorProjection() {
            this.pixelOrigin_ = new google.maps.Point(TILE_SIZE / 2,
                TILE_SIZE / 2);
            this.pixelsPerLonDegree_ = TILE_SIZE / 360;
            this.pixelsPerLonRadian_ = TILE_SIZE / (2 * Math.PI);
        }

        MercatorProjection.prototype.fromLatLngToPoint = function (latLng,
            opt_point) {
            var me = this;
            var point = opt_point || new google.maps.Point(0, 0);
            var origin = me.pixelOrigin_;

            point.x = origin.x + latLng.lng() * me.pixelsPerLonDegree_;

            // Truncating to 0.9999 effectively limits latitude to 89.189. This is
            // about a third of a tile past the edge of the world tile.
            var siny = bound(Math.sin(degreesToRadians(latLng.lat())), -0.9999,
                0.9999);
            point.y = origin.y + 0.5 * Math.log((1 + siny) / (1 - siny)) *
                -me.pixelsPerLonRadian_;
            return point;
        };

        MercatorProjection.prototype.fromPointToLatLng = function (point) {
            var me = this;
            var origin = me.pixelOrigin_;
            var lng = (point.x - origin.x) / me.pixelsPerLonDegree_;
            var latRadians = (point.y - origin.y) / -me.pixelsPerLonRadian_;
            var lat = radiansToDegrees(2 * Math.atan(Math.exp(latRadians)) -
                Math.PI / 2);
            return new google.maps.LatLng(lat, lng);
        };

        function createInfoWindowContent() {
            var numTiles = 1 << map.getZoom();
            var projection = new MercatorProjection();
            var worldCoordinate = projection.fromLatLngToPoint(almaz);
            var pixelCoordinate = new google.maps.Point(
                worldCoordinate.x * numTiles,
                worldCoordinate.y * numTiles);
            var tileCoordinate = new google.maps.Point(
                Math.floor(pixelCoordinate.x / TILE_SIZE),
                Math.floor(pixelCoordinate.y / TILE_SIZE));

            return '<span style="color: black;">Авторынок "Алмаз" <br> пав. №4, ряд 10</span>';
        }

        function initialize() {
            var mapOptions = {
                zoom: 15,
                center: almaz,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            map = new google.maps.Map(document.getElementById('map-canvas'),
                mapOptions);

            var coordInfoWindow = new google.maps.InfoWindow();
            coordInfoWindow.setContent(createInfoWindowContent());
            coordInfoWindow.setPosition(almaz);
            coordInfoWindow.open(map);

            google.maps.event.addListener(map, 'zoom_changed', function () {
                coordInfoWindow.setContent(createInfoWindowContent());
                coordInfoWindow.open(map);
            });
        }

        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
    <div class="row-fluid">
        <hgroup class="title">
            <div class="span12">
                <h1>Контакты</h1>
            </div>

        </hgroup>
    </div>
    <div class="row-fluid">
        <div class="span4">
            <section>
                <header>
                    <h3>Организация</h3>
                </header>
                <p>ТурбоДрайв-РОСТОВ (ИП Алёхина В.А.)</p>
                <p>

                    <span class="label">Время работы:</span>
                    <span>с 10:00 до 16:00</span>

                </p>
                <p>
                    <span class="label">Выходной:</span>
                    <span>Воскресенье</span>

                </p>
            </section>
            <section class="contact">
                <header>
                    <h3>Телефон:</h3>
                </header>
                <p>
                    <span class="label">Основной:</span>
                    <a href="callto:+79508620990"><span>+7(950)862-09-90</span></a>
                </p>
                <%--<p>
                    <span class="label">В нерабочее время:</span>
                    <span>425.555.0199</span>
                </p>--%>
            </section>

            <section class="contact">
                <header>
                    <h3>Email:</h3>
                </header>
                <p>
                    <span class="label">Поддержка клиентов:</span>
                    <span><a href="mailto:marketing@ast-rostov.ru">Support@ast-rostov.ru</a></span>
                </p>
                <p>
                    <span class="label">Отдел продаж:</span>
                    <span><a href="mailto:marketing@ast-rostov.com">Marketing@ast-rostov.ru</a></span>
                </p>
                <p>
                    <span class="label">Основной:</span>
                    <span><a href="mailto:sasha2507alexin@yandex.ru">sasha2507alexin@yandex.ru</a></span>
                </p>
            </section>

            <section class="contact">
                <header>
                    <h3>Адрес:</h3>
                </header>
                <p>
                    пос. Янтарный, ул. Малое зеленое кольцо 3<br />
                    Автомобильный рынок «Алмаз»<br />
                    <span class="label">павильон:</span> 4 <span class="label">ряд:</span> 10
                </p>
            </section>
            
            <section class="contact">
                <header>
                    <h3>Данные об организации:</h3>
                </header>
                <p>
                     <span class="label">ИП</span> Алехина В. А.
                </p>
                <p>
                     <span class="label">ИНН</span> 616700770626
                </p>
            </section>

            <section class="contact">
                <header>
                    <h3>Юридический адрес:</h3>
                </header>
                <p>
                    344048 г. Ростов-на-Дону<br />
                    пер. Равенства, д. 45
                </p>
            </section>

        </div>
        <div class="span8">
            <h3>Карта:</h3>
            <div id="map-canvas" style="margin: 0; padding: 0; height: 450px;">
            </div>
        </div>
    </div>
</asp:Content>
