$(function () {
    var $popup = $('#notification-popup');
    var $btn = $('#btn-notification');
    var $close = $('.close-notification-popup');
    var $result = $('#result-notification');
    var $loadMore = $('#load-more-btn');
    var $numNotify = $('#num-notification');
    var $numNotifyMobile = $('#num-notification-mobile');

    var isLoaded = false;

    $btn.on('click', function () {
        $popup.toggle();
        if (!isLoaded && $popup.is(':visible')) {
            loadNotifications(1);
            isLoaded = true;
        }
    });

    $close.on('click', function () {
        $popup.hide();
    });

    $loadMore.on('click', function () {
        var nextPage = parseInt($loadMore.attr('data-page')) + 1;
        loadNotifications(nextPage);
    });

    var isFirstLoad = true;

    function loadNotifications(page) {
        $.ajax({
            url: '/Home/NotificationsPartial',
            method: 'GET',
            data: { page: page, pageSize: 10 },
            beforeSend: function () {
                $loadMore.find('span').text('Đang tải...');
            },
            success: function (html) {
                html = html.trim();

                if (html.length > 0) {
                    var decodedHtml = $('<textarea/>').html(html).text();
                    $result.append(decodedHtml);
                    var nextPage = parseInt($loadMore.attr('data-page')) + 1;
                    $loadMore.attr('data-page', nextPage);
                    $loadMore.find('span').text('Xem thêm');
                } else {
                    if (isFirstLoad) {
                        $result.html('<p>Chưa có thông báo</p>');
                    }
                    $loadMore.hide();
                }

                isFirstLoad = false;
            },
            error: function (err) {
                console.error('Lỗi tải thông báo:', err);
                $loadMore.find('span').text('Xem thêm');
            }
        });
    }


    getNotificationCount();
    function getNotificationCount() {
        $.ajax({
            url: '/Home/GetUnreadNotificationCount',
            method: 'GET',
            success: function (res) {
                var count = res.count || res.Count || 0;
                if (count > 0) {
                    var displayText = count > 9 ? '9+' : count;
                    $numNotify.text(displayText).removeClass('d-none');
                    $numNotifyMobile.text(displayText).removeClass('d-none');
                } else {
                    $numNotify.addClass('d-none');
                    $numNotifyMobile.addClass('d-none');
                }
            },
            error: function (err) {
                console.error('Lỗi lấy số lượng thông báo:', err);
            }
        });
    }
});