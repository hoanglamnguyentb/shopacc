$(document).ready(function () {
    const csrf_token = $('meta[name="csrf-token"]').attr('content');
    function getInfo() {
        const url = '/ajax/user/account_info';
        $.ajax({
            type: "POST",
            url: url,
            async: true,
            cache: false,
            data: {
                _token: csrf_token,
            },
            success: function (data) {
                if (data.status == true) {
                    auth_check = true;
                    auth_balance = parseInt(data.info.balance);

                    $('.account-balance').html('Số dư: ' + formatNumber(data.info.balance));
                }
            },
        });
    }
    $('.item-list').click(function () {
        $('#modalRuongVatPham').modal('show');
        if (auth_check) {
            $('.account-balance').html('Số dư: ' + formatNumber(auth_balance));
            var price = (parseInt($(this).data('price')));
            if (auth_balance < price) {
                var not_money = `<div class="card--gray c-mb-0 c-mt-16 c-pt-8 c-pb-8 c-pl-12 c-pr-12">
                            <p class="card--attr__payment_failed c-mb-0 fw-400 fz-13 lh-20">
                                Tài khoản của bạn không đủ để thanh toán, vui lòng nạp tiền để tiếp tục giao dịch
                            </p>
                        </div>`
                $('.not-enough-money').html(not_money);
                var html = `<button type="button" class="btn ghost" disabled>Thanh toán</button>
                            <button type="button" data-dismiss="modal" class="btn primary handle-recharge-modal" data-toggle="modal">Nạp tiền</button>`;
                $('.data__dangnhap').html(html);
            }
            else {
                var html = `<button type="submit" class="btn primary google_buy_minigame">Thanh toán</button>`;
                $('.data__dangnhap').html(html);
            }
        } else {
            var html = `<button type="button" class="btn primary" data-dismiss="modal" onclick="openLoginModal();">Đăng nhập</button>`;
            $('.data__dangnhap').html(html);
        }
    })

    $('body').on('click', '.modal_withdraw_items', function (e) {
        e.preventDefault();
        if (!auth_check) {
            let width = $(window).width();
            if (width > 992) {
                $('#loginModal').modal('show');
                $('#loginModal #modal-login-container').removeClass('right-panel-active');
                return;
            } else {
                $('.mobile-auth-form #formLoginMobile').css('display', 'flex');
                $('.mobile-auth-form #formRegisterMobile').css('display', 'none');
                $('.mobile-auth .head-mobile h1').text('Đăng nhập');
                $('.mobile-auth').css('transform', 'translateX(0)');
                return;
            }
        }

        $('#modalWithdraw').modal('show');
        $('#noticeModal').modal('hide');
    })

    $(document).on('submit', '#formRuongVatPham', function (e) {
        e.preventDefault();
        url = $(this).attr('action');
        const csrf_token = $('meta[name="csrf-token"]').attr('content');
        var id_group = $('.item-list').data('id');

        $.ajax({
            type: 'POST',
            url: url,
            data: {
                _token: csrf_token,
                id: id_group,
                numrollbyorder: '1',
                numrolllop: '1',
                typeRoll: 'real',
            },
            beforeSend: function (xhr) {
                $('#modalRuongVatPham').modal('hide');
                $('.j-load').addClass('is-load');
                if ($('.j-load').hasClass('is-load')) {
                    var divLoad = '';
                    divLoad += '<div class="loading-wrap"><span class="modal-loader-spin"></span></div>';
                    $('.j-load').prepend(divLoad);
                }
            },
            success: (data) => {
                getInfo();
                html = '';
                htmlImage = '';

                if (data.gift_detail.image && data.gift_detail.image != 'https://cdn.upanh.info/storage/upload/images/Anh%20footer/devgift.png') {
                    htmlImage += '<div class="modal-header justify-content-center image-quanhuy c-my-20">';
                    htmlImage += '<img class="img-quanhuy" src="' + data.gift_detail.image + '" alt="">';
                    htmlImage += '</div>';

                    $('.j-img-ruongQuanHuy').html(htmlImage);
                }else{
                    htmlImage += '<div class="text-center">';
                    htmlImage += '<img class="c-pt-20 c-pb-20" src="/assets/frontend/theme_5/image/son/success.png" alt="">';
                    htmlImage += '</div>';

                    $('.j-img-ruongQuanHuy').html(htmlImage);
                }
                if (data.gift_detail.name) {
                    html += '<span>' + data.gift_detail.name + '</span>';
                    html += '<div>Tổng vật phẩm: ' + data.arr_gift[0].params.percent + '</div>';

                    $('.j-quanhuy').html(html);
                    $('#modalRuongQuanHuy').modal('show');
                } else {
                    alert('Hết vật phẩm');
                }
            },
            complete: function(){
                $('.j-load').removeClass('is-load');
                $('.j-load').find('.loading-wrap').remove();
            }
        });
    });
})
