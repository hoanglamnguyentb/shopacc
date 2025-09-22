var myImg = "/assets/images/danger.png";

var NotiLibibox = function (title, mes, link) {
    Lobibox.notify('info', {
        title: title,
        width: 361,
        img: myImg,
        msg: mes,
        link: link,
        closable: true,
        closeOnClick: true,
        delay: 30000,                // Hide notification after this time (in miliseconds)
        delayIndicator: false,
    });
}

var NotiBrowser = function (mes, link) {
    var options = {
        title: 'Thông Báo Hệ Thống',
        options: {
            body: mes,
            icon: myImg,
            lang: 'vi',
            onClick: function () {
                location.href = link;
            }
        }
    };
    if (!$("#easyNotify").easyNotify(options)) {
        NotiLibibox(mes, link);
    }
}

var reloadNotify = function () {
    AjaxCall("/NotificationArea/Notification/ShowNotification", 'get', null, function (rs) {
        $("#NotiArea").empty();
        $("#NotiArea").html(rs);
    })
}

var reloadNotifyEndUser = function () {
    AjaxCall("/EndUserNotification/ShowNotification", 'get', null, function (rs) {
        $("#NotiArea").empty();
        $("#NotiArea").html(rs);
    })
}
$(function () {
    if (jsUserId > 0 && jsUserId != null) {
        // Reference the auto-generated proxy for the hub.
        var tbHub = $.connection.thongBaoHub;
        // Create a function that the hub can call back to display messages.

        //chat.client.userConnected = function (connectId) {
        //    console.log(connectId + " Đã tham gia");
        //}
        //chat.client.userLeft = function (connectId) {
        //    console.log(connectId + " Đã thoát");
        //}

        tbHub.client.CanhBao = function (userId, tram, message) {
            console.log(userId);
            if (jsUserId == userId) {
                Lobibox.notify('info', {
                    title: tram,
                    width: 361,
                    img: myImg,
                    msg: message,
                    link: '/LogCanhBaoArea/LogCanhBao',
                    closable: true,
                    closeOnClick: true,
                    delay: 30000,                // Hide notification after this time (in miliseconds)
                    delayIndicator: false,
                });
                reloadNotify();
            }
        };

        tbHub.client.thongbao = function (str, link, enduser) {
            if (enduser) {
                reloadNotifyEndUser();
            } else {
                reloadNotify();
            }
            NotiBrowser(str, link);
        };
        tbHub.client.thongbaoglobal = function (str, link) {
            $("#boxRunText").append("<span class='textItem'>" + str + "</span>");
        };

        $.connection.hub.start().done(function () {
            tbHub.server.init(jsUserId, $.connection.hub.id, typeAcc, false);
        });

        // This optional function html-encodes messages for display in the page.
        function htmlEncode(value) {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        }
    }
});