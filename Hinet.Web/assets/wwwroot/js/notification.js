$(document).ready(function () {

    // ============================
    // 🧰 HÀM COOKIE DÙNG CHUNG
    // ============================
    function setCookie(name, value, minute) {
        var expires = "";
        if (minute) {
            var date = new Date();
            date.setTime(date.getTime() + (minute * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    function getCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function eraseCookie(name) {
        document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }

    // ============================
    // 📢 LOGIC HIỂN THỊ MODAL
    // ============================

    var danhMucId = window.currentDanhMucId || 0;
    var cookieName = danhMucId && danhMucId !== 0
        ? 'sys_noti_popup_' + danhMucId
        : 'sys_noti_popup';

    var cookieValue = getCookie(cookieName);

    if (!cookieValue) {
        $('#noticeModal').modal('show');
    }

    // Khi nhấn nút "Tắt trong 1h"
    $('body').on('click', '.openModalNoti', function () {
        setCookie(cookieName, 'noty', 60);  // 60 phút
    });

    // Khi nhấn nút "Đóng" → tắt trong 5 phút để tránh bật lại ngay
    $('body').on('click', '#noticeModal .btn-light, #noticeModal .close', function () {
        setCookie(cookieName, 'noty', 5);
    });

});
