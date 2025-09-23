var auth_check = false;
var auth_balance = false;
let auth_balance_lock = 0;
var auth_user = null;
var width = $(window).width();
function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.')
}
function fn(text, count) {
    return text.slice(0, count) + (text.length > count ? "..." : "");
}
// $(document).ready(function () {
//     getInfo();
// });

const csrf_token = $('meta[name="csrf-token"]').attr('content');
const token = $('meta[name="jwt"]').attr('content');
var  check_index = 0
function getInfo() {
    const url = '/ajax/user/account_info';
    $.ajax({
        type: "POST",
        url: url,
        async: true,
        cache: false,
        data: {
            _token: csrf_token,
            jwt: token
        },
        beforeSend: function (xhr) {

        },
        success: function (data) {
            if (data.status === "LOGIN") {
                $('.box-loading').hide();
                $('.box-logined').show();
                $('.box-account').hide();
                // đăng nhập, đăng ký
                let html = '';
                html += '<div class="box-icon brs-8 " >';
                html += ' <img src="/assets/frontend/theme_5/image/nam/profile.svg" alt="" >';
                html += '</div>';

                $('.account-logined').html(html);
                $('.account-logined').removeClass("box-account-open");
                $('.account-logined').attr('onclick', 'openLoginModal()');
                $('.box-account_nologined').show();
                $('.box-account_logined').hide();
                $('meta[name="jwt"]').attr('content', '');

            }
            if (data.status == 401) {
                $('.box-loading').hide();
                $('.box-logined').show();
                $('.box-account').hide();
                // đăng nhập, đăng ký
                let html = '';
                html += '<div class="box-icon brs-8 " >';
                html += ' <img src="/assets/frontend/theme_5/image/nam/profile.svg" alt="" >';
                html += '</div>';

                $('.account-logined').html(html);
                $('.account-logined').removeClass("box-account-open");
                $('.account-logined').attr('onclick', 'openLoginModal()');
                $('.box-account_nologined').show();
                $('.box-account_logined').hide();
                $('meta[name="jwt"]').attr('content', '');

            }
            if (data.status === "ERROR") {
                alert('Lỗi dữ liệu, vui lòng load lại trang để tải lại dữ liệu')
            }
            if (data.status == true) {
                auth_check = true;
                auth_balance = parseInt(data.info.balance);
                if(data.info.in_active_captcha_charge != '' && data.info.in_active_captcha_charge == 1){
                    $('.captchaShow').addClass('d-none');
                }
                if (data.info.balance_lock){
                    auth_balance_lock = parseInt(data.info.balance_lock);
                }

                $('#domain-referral').val('https://' + window.location.host + '/register?ref_id=' + data.info.id)
                $('.domain-referral_check').html('https://' + window.location.host + '/register?ref_id=' + data.info.id)
                $('.info-withdraw').val('Số tiền hiện có từ giới thiệu: 213.222 ACoin');
                if(data.info.balance_affiliate != null && data.info.balance_affiliate > 0){
                    $('#balance_affiliate').html('<span style="color: red;text-decoration: none;font-weight: 600;font-style: italic"><span class="balance_affiliate_current">'+formatNumber(data.info.balance_affiliate)+'</span> ACoin</span>')
                    $('#balance_affiliate_noformat').val(data.info.balance_affiliate);
                    $('#info-withdraw').val('Tiền giới thiệu: '+ formatNumber(data.info.balance_affiliate) +' ACoin');
                }else{
                    $('#balance_affiliate').html('<span style="color: red;text-decoration: none;font-weight: 600;font-style: italic"><span class="balance_affiliate_current">0</span> ACoin</span>')
                    $('#balance_affiliate_noformat').val(0);
                    $('#info-withdraw').val('Tiền giới thiệu: 0 ACoin');
                }

                auth_user = data.info;
                $('.box-loading').hide();
                $('.box-account_nologined').hide();
                $('.box-account_logined').show();
                $('.account-logined').addClass('box-account-open');

                // profile
                let html = '';
                html += '<div class="d-flex ">';
                html += '<div class="account-name">';
                html += '<div  class="text-right title-color fw-500">' + fn(data.info.fullname ?? data.info.username, 12) + '</div>';
                html += '<div class="account-balance fw-400">Số dư: ' + formatNumber(data.info.balance) + '</div>';
                html += '</div>';
                html += '<div class="account-avatar c-ml-12">';
                html += '<img src="/assets/frontend/theme_5/image/nam/anhdaidien.svg" alt="">';
                html += '</div>';
                html += '</div>';

                $('.account-logined').html(html);
                $('.account-name-sidebar').html(data.info.fullname ?? data.info.username);
                $('.account-balance-sidebar').html('Số dư: <span>' + formatNumber(data.info.balance) + '</span>');
                $('.account-id-sidebar').html('ID: <span>' + data.info.id + '</span>');

                let htmtLogout = '';
                htmtLogout += '<a href="javascript:void(0);" onclick="event.preventDefault();document.getElementById(\'logout-form\').submit();" class="d-block align-items-center d-flex">';
                htmtLogout += '<div class="sidebar-item-icon brs-8 c-p-8 c-mr-12">';
                htmtLogout += '<img src="/assets/frontend/theme_5/image/nam/log-out.svg" alt="" style="width: 24px;height: 24px">';
                htmtLogout += '</div>';
                htmtLogout += '<p class="sidebar-item-text fw-400 fz-12 mb-0">Đăng xuất</p>';
                htmtLogout += '</a>';

                $('.log-out-button').html(htmtLogout);


                let htmlProfile = '';
                htmlProfile += '<div class="sidebar-section-avt brs-100 c-mr-12">'
                htmlProfile += '<img class="brs-100" src="/assets/frontend/theme_5/image/nam/avatar.png" alt="">'
                htmlProfile += '</div>'
                htmlProfile += '<div class="sidebar-section-info">'
                htmlProfile += '<div  class="sidebar-section-title c-mb-4 fz-15 fw-500">' + fn(data.info.fullname ?? data.info.username, 12) + '</div>'
                htmlProfile += '<div class="sidebar-section-info-text c-mb-4 fz-13 fw-500 sidebar-user-balance">Số dư: <span>' + formatNumber(data.info.balance) + 'đ</span></div>'
                htmlProfile += '<div class="sidebar-section-info-text c-mb-4 fz-13 fw-500 sidebar-user-balance">Số dư Acoin: <span>' + formatNumber(data.info.balance_affiliate??0) + ' Acoin</span></div>'
                htmlProfile += '<div class="sidebar-section-info-text c-mb-4 fz-13 fw-500 sidebar-user-balance">Số dư khuyến mãi: <span>' + formatNumber(data.info.balance_lock??0) + 'đ</span></div>'
                htmlProfile += ' <p class="sidebar-section-info-text mb-0 fz-13 fw-500 sidebar-user-id">ID: <span>' + data.info.id + '</span></p>'
                htmlProfile += ' </div>'

                $('.sidebar-user-profile').html(htmlProfile);

                $('meta[name="jwt"]').attr('content', data.token);

                $('.checkauth_login_instruct').css('visibility', '');
            }
        },
        error: function (data) {
            // alert('Có lỗi phát sinh, vui lòng liên hệ QTV để kịp thời xử lý(account_info)!')
            return;
        },
        complete: function (data) {
                CheckLoginRef();
        }
    });
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function CheckLoginRef() {
    var parameterValue = getParameterByName('ref_id');

    if (parameterValue != '' && parameterValue != null) {
        if (auth_check) {
            localStorage.setItem("information", parameterValue);
        }
        // else {
        //     window.location.href = '/?ref_id=' + parameterValue;
        //
        //     // localStorage.setItem('redirected', 'true');
        //     // return;
        //     // openRegisterModal();
        //     // window.location.href = '/'
        //     // $('.refId').val(parameterValue);
        //
        // }
        else {
            const urlParams = new URLSearchParams(window.location.search);
            const redirect = urlParams.get('redirect');
            $('.refId').val(parameterValue);
            if (redirect !== '0') {
                window.location.href = '/?ref_id=' + parameterValue + '&redirect=0';
            }
        }
    }


    if (localStorage.getItem("information") != null && localStorage.getItem("information") != '') {
        if (window.location.pathname === '/thong-tin') {
            $('#ref_id').val(localStorage.getItem("information"));
            localStorage.removeItem("information");
        }else if(window.location.pathname === '/register' ){
            window.location.href = '/?ref_id='+localStorage.getItem("information");
            localStorage.removeItem("information");
        } else {
            window.location.href = '/thong-tin';
        }
    }
}
function scrollToTop(){
    $('body, html').animate({
        scrollTop: 0
    },'slow');
}
// làm thử vấn đề xem
$(document).ready(function () {
    getInfo();
    scrollToTop();

});

