var chargeDataSend = {
    type: null,
    pin: null,
    serial: null,
    captcha: null,
    amount: null,
    finger: null,
    _token: $('meta[name="csrf-token"]').attr('content'),
}

// function formatNumber(num) {
//     return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.')
// }

//append new data into confirm modal
function prepareConfirmData(widgetType) {
    let cardChecked;
    let confirmTitle;
    let confirmDiscount;
    let confirmPromotion;
    let confirmPrice;
    if (widgetType == 1) {
        cardChecked = $('#chargeCardForm input[name="amount"]:checked');
        confirmTitle = $('#telecom').val();
        confirmDiscount = $(cardChecked).data('ratio');
        confirmPromotion = $(cardChecked).data('promotion');
        confirmPrice = $(cardChecked).val();
    }
    if (widgetType == 2) {
        cardChecked = $('#rechargeModal input[name="amount"]:checked');
        confirmTitle = $('#telecom_modal').val();
        confirmDiscount = $(cardChecked).data('ratio');
        confirmPromotion = $(cardChecked).data('promotion');
        confirmPrice = $(cardChecked).val();
    }
    if (widgetType == 3) {
        cardChecked = $('#chargeCardHomeForm input[name="amount"]:checked');
        confirmTitle = $('#telecom').val();
        confirmDiscount = $(cardChecked).data('ratio');
        confirmPromotion = $(cardChecked).data('promotion');
        confirmPrice = $(cardChecked).val();
    }

    if ( !confirmPromotion ) {
        confirmPromotion = 0;
    }

    if ( !confirmDiscount || confirmDiscount < 0 ) {
        confirmDiscount = 100;
    }

    let saleAmount = confirmPrice - (confirmPrice * confirmDiscount / 100);
    let totalAmount = confirmPrice - saleAmount;

    if ( saleAmount > 0  && totalAmount > 0 ) {
        $('#orderCharge #totalBill').text(`${ formatNumber(totalAmount)}`);
        $('#chargeConfirmStep #totalBillMobile').text(`${ formatNumber(totalAmount)}`);
    } else {
        $('#orderCharge #totalBill').text(`${ formatNumber(confirmPrice)}`);
        $('#chargeConfirmStep #totalBillMobile').text(`${ formatNumber(confirmPrice)}`);
    }

    $('#orderCharge #confirmTitle').text(confirmTitle);
    $('#chargeConfirmStep #confirmTitleMobile').text(confirmTitle);
    $('#orderCharge #confirmPrice').text(`${formatNumber( confirmPrice )} đ`);
    $('#chargeConfirmStep #confirmPriceMobile').text(`${formatNumber( confirmPrice )} đ`);
    $('#orderCharge #confirmDiscount').text(`${confirmDiscount}%`);
    $('#chargeConfirmStep #confirmDiscountMobile').text(`${confirmDiscount}%`);
    $('#orderCharge #confirmPromotion').text(`${confirmPromotion}%`);
    $('#chargeConfirmStep #confirmPromotionMobile').text(`${confirmPromotion}%`);
}

//Append new data to submit to backend
function prepareDataSend(widgetType) {
    let cardChecked;
    let pin;
    let serial;
    let captcha;
    let type;
    let amount;
    if (widgetType == 1) {
        cardChecked = $('#chargeCardForm input[name="amount"]:checked');
        pin = $('#chargeCardForm input[name="pin"]').val().trim();
        serial = $('#chargeCardForm input[name="serial"]').val().trim();
        captcha = $('#chargeCardForm input[name="captcha"]').val().trim();
        type = $('#telecom').val();
        amount = $(cardChecked).val();
    }
    if (widgetType == 2) {
        cardChecked = $('#rechargeModal input[name="amount"]:checked');
        pin = $('#rechargeModal input[name="pin"]').val().trim();
        serial = $('#rechargeModal input[name="serial"]').val().trim();
        captcha = $('#rechargeModal input[name="captcha"]').val().trim();
        type = $('#telecom_modal').val();
        amount = $(cardChecked).val();
    }
    if (widgetType == 3) {
        cardChecked = $('#chargeCardHomeForm input[name="amount"]:checked');
        pin = $('#chargeCardHomeForm input[name="pin"]').val().trim();
        serial = $('#chargeCardHomeForm input[name="serial"]').val().trim();
        captcha = $('#chargeCardHomeForm input[name="captcha"]').val().trim();
        type = $('#telecom').val();
        amount = $(cardChecked).val();
    }

    chargeDataSend.type = type;
    chargeDataSend.pin = pin;
    chargeDataSend.serial = serial;
    chargeDataSend.captcha = captcha;
    chargeDataSend.amount = amount;
}

function showConfirmContent () {
    prepareConfirmData(1);
    prepareDataSend(1);
    //Close recharge modal if page has this modal
    $('#rechargeModal').modal('hide');
    if ( $(window).width() >= 992 ) {
        $('#orderCharge').modal('show');
    } else {
        $('#chargeConfirmStep').css('transform', 'translateX(0)');
    }
}

function showModalConfirmContent () {
    prepareConfirmData(2);
    prepareDataSend(2);
    $('#rechargeModal').modal('hide');
    if ( $(window).width() >= 992 ) {
        $('#orderCharge').modal('show');
    } else {
        $('#chargeConfirmStep').css('transform', 'translateX(0)');
    }
}

function showHomeConfirmContent () {
    prepareConfirmData(3);
    prepareDataSend(3);
    if ( $(window).width() >= 992 ) {
        $('#orderCharge').modal('show');
    } else {
        $('#chargeConfirmStep').css('transform', 'translateX(0)');
    }
}
function checked_input_cardamout(){
    if ( $(window).width() >= 992 ) {
        // $('#cardAmount>div:first-child input').prop('checked', true);
        $('#cardAmountMobile>div:first-child input').prop('checked', false);
        // $('#cardAmountModal>div:first-child input').prop('checked', true);
    }else {
        // $('#cardAmountMobile>div:first-child input').prop('checked', true);
        $('#cardAmount>div:first-child input').prop('checked', false);
    }
}
$(document).ready(function () {
    checked_input_cardamout()
    getPromotionLockMoney();
    // getTelecom();

    if (window.location.pathname.includes("/recharge-atm")) {
        // Nếu có, giả lập click vào #atmNavTab
        $('#atmNavTab').click();
    }
    $('#chargeNavTab').click(function () {
        let telecom = $('#telecom').val();
        getAmount(telecom, 1);
    });

    $('#telecom').on('change', function () {
        let telecom = $(this).val();
        getAmount(telecom, 1);
    });

    $('#telecom_modal').on('change', function () {
        let telecom = $(this).val();
        getAmount(telecom, 2);
    });

    $('#reload_1, #reload_modal_btn').click(function () {
        $.ajax({
            type: 'GET',
            async:true,
            cache:false,
            url: '/reload-captcha',
            beforeSend: function () {
                $('.refresh-captcha img').removeClass("paused");
                $(".capchaImage").empty();
                $('input[name="captcha"]').val(null);
            },
            success: function (data) {
                $(".capchaImage").html(data);
            },
            complete: function () {
                setTimeout( function () {
                    $('.refresh-captcha img').addClass("paused");
                }, 1000);
            }
        });
    });

    $(document).on('click', '#orderCharge #confirmSubmitButton', function(e) {
        e.preventDefault();
        import(`../../lib/fingerprint/fingerprint.js`)
            .then(FingerprintJS => FingerprintJS.load())
            .then(fp => fp.get())
            .then(result => {
                const visitorId = result.visitorId;
                chargeDataSend.finger = visitorId ?? null;
                continueFormSubmission(visitorId);
            })
            .catch(error => console.error(error));
        function continueFormSubmission(visitorId) {
            $.ajax({
                url:'/nap-the',
                type:'POST',
                data: chargeDataSend,
                beforeSend: function () {
                    $('#orderCharge #confirmSubmitButton').prop("disabled", true);
                    $('#orderCharge #confirmSubmitButton').text("Đang xử lý");
                    //Delete text in success and fail modal
                    $('#modalSuccessPayment #successMessage').text('');
                    $('#modalFailPayment #failMessage').text('');
                },
                success:function (res) {

                    $('#orderCharge').modal('hide');

                    if(res.status == 1) {
                        $('#modalSuccessPayment #successMessage').text(res.message);
                        $('#modalSuccessPayment').modal('show');
                    }
                    else if(res.status == 401){
                        $('#modalFailPayment #failMessage').text('Bạn cần phải đăng nhập để hoàn thiện giao dịch!');
                        $('#modalFailPayment').modal('show');
                    }
                    else if(res.status == 0){
                        $('#modalFailPayment #failMessage').text(res.message);
                        $('#modalFailPayment').modal('show');
                    }
                    else{
                        $('#modalFailPayment #failMessage').text('Đã có lỗi xảy ra!');
                        $('#modalFailPayment').modal('show');
                    }
                },
                error: function (res) {
                    $('#orderCharge').modal('hide');
                    $('#modalFailPayment #failMessage').text('Đã có lỗi xảy ra!');
                    $('#modalFailPayment').modal('show');
                },
                complete: function () {
                    generateCaptcha()
                    $('#reload_1').trigger('click');
                    $('#orderCharge #confirmSubmitButton').prop("disabled", false);
                    $('#orderCharge #confirmSubmitButton').text("Xác nhận");

                    $('#chargeCardForm input[name="pin"]').val('');
                    $('#chargeCardForm input[name="serial"]').val('');
                    $('#chargeCardForm input[name="captcha"]').val('');

                    $('#chargeCardModalForm input[name="pin"]').val('');
                    $('#chargeCardModalForm input[name="serial"]').val('');
                    $('#chargeCardModalForm input[name="captcha"]').val('');
                }
            });
        }
    });
    $("#reloadCaptcha").on('click', function () {
        generateCaptcha();
    });
    $("#reloadModalCaptcha").on('click', function () {
        generateCaptcha();
    });
    function generateCaptcha() {
        $.ajax({
            type: "GET",
            cache: false,
            async: true,
            url: "/reload-captcha",
            success: function (data) {
                $(".captcha span").html(data.captcha);
            },
        });
    }

    $(document).on('click', '#chargeConfirmStep #confirmSubmitButtonMobile', function(e) {
        e.preventDefault();
        import(`../../lib/fingerprint/fingerprint.js`)
            .then(FingerprintJS => FingerprintJS.load())
            .then(fp => fp.get())
            .then(result => {
                const visitorId = result.visitorId;
                chargeDataSend.finger = visitorId ?? null;
                continueFormSubmission(visitorId);
            })
            .catch(error => console.error(error));
        function continueFormSubmission(visitorId) {
            $.ajax({
                url:'/nap-the',
                type:'POST',
                data: chargeDataSend,
                beforeSend: function () {
                    $('#chargeConfirmStep #confirmSubmitButtonMobile').prop("disabled", true);
                    $('#chargeConfirmStep #confirmSubmitButtonMobile').text("Đang xử lý");
                    //Delete text in success and fail modal
                    $('#modalSuccessPayment #successMessage').text('');
                    $('#modalFailPayment #failMessage').text('');
                },
                success:function (res) {
                    if(res.status == 1) {
                        $('#modalSuccessPayment #successMessage').text(res.message);
                        $('#modalSuccessPayment').modal('show');
                    }
                    else if(res.status == 401){
                        $('#modalFailPayment #failMessage').text('Bạn cần phải đăng nhập để hoàn thiện giao dịch!');
                        $('#modalFailPayment').modal('show');
                    }
                    else if(res.status == 0){
                        $('#modalFailPayment #failMessage').text(res.message);
                        $('#modalFailPayment').modal('show');
                    }
                    else{
                        $('#modalFailPayment #failMessage').text('Đã có lỗi xảy ra!');
                        $('#modalFailPayment').modal('show');
                    }
                },
                error: function (res) {
                    $('#modalFailPayment #failMessage').text('Đã có lỗi xảy ra!');
                    $('#modalFailPayment').modal('show');
                },
                complete: function () {
                    generateCaptcha()
                    $('#reload_1').trigger('click');
                    $('#chargeConfirmStep #confirmSubmitButtonMobile').prop("disabled", false);
                    $('#chargeConfirmStep #confirmSubmitButtonMobile').text("Xác nhận");

                    $('#chargeCardForm input[name="pin"]').val('');
                    $('#chargeCardForm input[name="serial"]').val('');
                    $('#chargeCardForm input[name="captcha"]').val('');

                    $('#chargeCardModalForm input[name="pin"]').val('');
                    $('#chargeCardModalForm input[name="serial"]').val('');
                    $('#chargeCardModalForm input[name="captcha"]').val('');
                }
            });
        }
    });

    function reload_captcha() {
        $.ajax({
            type: 'GET',
            async:true,
            cache:false,
            url: '/reload-captcha',
            beforeSend: function () {
                $('.refresh-captcha img').removeClass("paused");
                $(".capchaImage").empty();
            },
            success: function (data) {
                $(".capchaImage").html(data.captcha);
            },
            complete: function () {
                $('input[name="captcha"]').val(null);
                setTimeout( function () {
                    $('.refresh-captcha img').addClass("paused");
                }, 1000);
            }
        });
    };
    function getPromotionLockMoney() {
        $.ajax({
            type: 'GET',
            async:true,
            cache:false,
            url: '/get-promotion-lock-money',
            beforeSend: function () {

            },
            success: function (data) {
                htmlLockMoney = ''

                if (data.type){
                    if (data.type == 1){
                        $('.promotion_lock_charge').html('KM '+data.data+'%');
                        htmlLockMoney += '<div class="c-mx-n16 c-px-12 c-py-12 d-flex c-mb-12">';
                        htmlLockMoney += '<div class="c-mr-8"><img src="/assets/frontend/theme_5/image/lock_money/bonus.png" alt=""></div>';
                        htmlLockMoney += '<div class="d-flex flex-column my-auto">';
                        htmlLockMoney += '<div class="fz-15 lh-16 fw-400 text-ghost">Nạp thẻ cào để nhận khuyến mãi lên đến <span class="fz-15 lh-16 fw-700">'+ data.data +'%</span></div>';
                        htmlLockMoney += '</div>';
                        htmlLockMoney += '</div>';
                        $('.amount_promotion').data('promotion', data.data);
                    }else if (data.type == 2){
                        var telecom = data.data;
                        if (telecom){
                            $.each(telecom,function(telecom_key,amount){
                                $.each(amount,function(key_amount,value){
                                    if (value && value > 0){
                                        $('.promotion_lock_charge_'+telecom_key+'_'+key_amount).html('KM '+value+'%');
                                        $('.amount_promotion_'+telecom_key+'_'+key_amount).data('promotion', value);
                                    }
                                })
                            })
                        }
                    }

                }
                $("#lockMoneyRecharge").html(htmlLockMoney)

            },
            complete: function () {

            }
        });
    };

    // Get card data
    function getTelecom () {
        let url = '/ajax/get-tele-card';

        $.ajax({
            type: "GET",
            async:true,
            cache:false,
            url: url,
            success: function (data) {
                if(data.status == 1){
                    let html = '';

                    if ( data.data.length > 0 ) {
                        $.each( data.data, function ( key, value ) {
                            html += '<option value="'+value.key+'">'+value.title+'</option>';
                        });
                    } else {
                        html += '<option value="">-- Vui lòng chọn nhà mạng --</option>';
                    }

                    $('select#telecom').html(html)
                    $('.select-form').niceSelect('update');

                    let typeValue = $('#telecom').val();

                    if ( typeValue ) {
                        getAmount(typeValue);
                    } else {

                    }
                }
            },
            error: function (data) {
                swal({
                    title: "Lỗi !",
                    text: "Có lỗi phát sinh vui lòng liên hệ QTV để kịp thời xử lý.",
                    icon: "error",
                    buttons: {
                        cancel: "Đóng",
                    },
                })
            },
            complete: function (data) {
                $('#charge_card .loader-container').remove();
                $('#charge_card .content-block').removeClass('d-none');
            }
        });
    }

    $('body').on('click','.daylamapin',function(){
        console.log("capcha")
        $.ajax({
            type: 'GET',
            async:true,
            cache:false,
            url: '/reload-captcha',
            beforeSend: function () {
                $('.refresh-captcha img').removeClass("paused");
                $(".capchaImage").empty();
            },
            success: function (data) {
                $(".capchaImage").html(data.captcha);
            },
            complete: function () {
                $('input[name="captcha"]').val(null);
                setTimeout( function () {
                    $('.refresh-captcha img').addClass("paused");
                }, 1000);
            }
        });
    });



    function getAmount (telecom, widgetType) {
        let url = '/ajax/get-amount-tele-card';
        $.ajax({
            type: "GET",
            async:true,
            cache:false,
            url: url,
            data: {
                telecom: telecom
            },
            beforeSend: function () {
                if (widgetType == 1) {
                    $('#cardAmount').empty();
                    $('#cardAmountMobile').empty();
                }
                if (widgetType == 2) {
                    $('#cardAmountModal').empty();
                }
                if (widgetType == 3) {
                    $('#cardAmount').empty();
                    $('#cardAmountMobile').empty();
                    $('#cardAmountModal').empty();
                }
                $('.money-form-group .loader').removeClass('d-none');
            },
            success: function (data) {
                if ( data.status == 1 ) {
                    // Append new data both in mobile and desktop
                    let html = '';
                    let htmlMobile = '';
                    let htmlModal ='';
                    if ( data.data.length > 0 ) {
                        $.each( data.data, function( key, value ) {
                            html += '<div class="col-4 c-px-4 money-radio-form my-auto">';
                            htmlMobile += '<div class="col-4 c-px-4 money-radio-form my-auto">';
                            htmlModal += '<div class="col-4 c-px-4 money-radio-form my-auto">';
                            // if ( key == 0 ) {
                            //     if ( $(window).width() >= 992 ) {
                            //         html += '<input name="amount" type="radio" id="recharge_amount_'+key+'" data-ratio="'+value.ratio_true_amount+'" value="'+value.amount+'" hidden checked>';
                            //         htmlMobile += '<input name="amount" type="radio" id="recharge_amount_mobile_'+key+'" data-ratio="'+value.ratio_true_amount+'" value="'+value.amount+'" hidden>';
                            //     } else {
                            //         html += '<input name="amount" type="radio" id="recharge_amount_'+key+'" data-ratio="'+value.ratio_true_amount+'" value="'+value.amount+'" hidden >';
                            //         htmlMobile += '<input name="amount" type="radio" id="recharge_amount_mobile_'+key+'" data-ratio="'+value.ratio_true_amount+'" value="'+value.amount+'" hidden checked>';
                            //     }
                            //
                            //     htmlModal += '<input name="amount" type="radio" id="recharge_amount_modal_'+key+'" data-ratio="'+value.ratio_true_amount+'" value="'+value.amount+'" hidden checked>';
                            //
                            // } else {
                            if (value.promotion_ratio && value.promotion_ratio > 0){
                                html += '<input name="amount" type="radio" id="recharge_amount_'+key+'" data-ratio="'+value.ratio_true_amount+'" data-promotion="'+value.promotion_ratio+'" value="'+value.amount+'" hidden>';
                                htmlMobile += '<input name="amount" type="radio" id="recharge_amount_mobile_'+key+'" data-ratio="'+value.ratio_true_amount+'" data-promotion="'+value.promotion_ratio+'" value="'+value.amount+'" hidden>';
                                htmlModal += '<input name="amount" type="radio" id="recharge_amount_modal_'+key+'" data-ratio="'+value.ratio_true_amount+'" data-promotion="'+value.promotion_ratio+'" value="'+value.amount+'" hidden>';
                            }else {
                                html += '<input name="amount" type="radio" id="recharge_amount_'+key+'" data-ratio="'+value.ratio_true_amount+'" data-promotion="0" value="'+value.amount+'" hidden>';
                                htmlMobile += '<input name="amount" type="radio" id="recharge_amount_mobile_'+key+'" data-ratio="'+value.ratio_true_amount+'" data-promotion="0" value="'+value.amount+'" hidden>';
                                htmlModal += '<input name="amount" type="radio" id="recharge_amount_modal_'+key+'" data-ratio="'+value.ratio_true_amount+'" data-promotion="0" value="'+value.amount+'" hidden>';
                            }

                            // }

                            html += '<label for="recharge_amount_'+key+'" class="c-py-16 c-px-8 c-mb-8 brs-8 p_recharge_amount">';
                            html += '<p class="fw-500 fs-15 mb-0">'+ formatNumber(value.amount)  +'đ</p>';
                            html += '<p class="fw-500 fs-15 mb-0" style="color: #82869E;font-size: 12px"> Nhận '+ value.ratio_true_amount  +'%</p>';
                            if (value.promotion_ratio && value.promotion_ratio > 0){
                                html += '<span class="fw-500 fz-11 mb-0 promotion_lock_charge" >KM '+ value.promotion_ratio  +'% </span>';
                            }
                            html += '</label>';
                            html += '</div>';

                            htmlMobile += '<label for="recharge_amount_mobile_'+key+'" class="c-py-16 c-px-8 c-mb-8 brs-8 p_recharge_amount">';
                            htmlMobile += '<p class="fw-500 fs-15 mb-0">'+ formatNumber(value.amount)  +'đ</p>';
                            htmlMobile += '<p class="fw-500 fs-15 mb-0" style="color: #82869E;font-size: 11px"> Nhận '+ value.ratio_true_amount  +'%</p>';
                            if (value.promotion_ratio && value.promotion_ratio > 0){
                                htmlMobile += '<span class="fw-500 fz-11 mb-0 promotion_lock_charge" >KM '+ value.promotion_ratio  +'% </span>';
                            }
                            htmlMobile += '</label>';
                            htmlMobile += '</div>';

                            htmlModal += '<label for="recharge_amount_modal_'+key+'" class="c-py-16 c-px-8 c-mb-8 brs-8 p_recharge_amount">';
                            htmlModal += '<p class="fw-500 fs-15 mb-0">'+ formatNumber(value.amount)  +'đ</p>';
                            htmlModal += '<p class="fw-500 fs-15 mb-0" style="color: #82869E;font-size: 12px"> Nhận '+ value.ratio_true_amount  +'%</p>';
                            if (value.promotion_ratio && value.promotion_ratio > 0){
                                htmlModal += '<span class="fw-500 fz-11 mb-0 promotion_lock_charge" > KM '+ value.promotion_ratio  +'% </span>';
                            }
                            htmlModal += '</label>';
                            htmlModal += '</div>';
                        });
                    }

                    //Append new amount data
                    if (widgetType == 1) {
                        $('#cardAmount').html(html);
                        $('#cardAmountMobile').html(htmlMobile);
                    }
                    if (widgetType == 2) {
                        $('#cardAmountModal').html(htmlModal);
                    }
                    if (widgetType == 3) {
                        $('#cardAmount').html(html);
                        $('#cardAmountMobile').html(htmlMobile);
                        $('#cardAmountModal').html(htmlModal);
                    }
                    //Refresh captcha
                    reload_captcha();
                }
                checked_input_cardamout()
            },
            error: function (data) {
                swal({
                    title: "Lỗi !",
                    text: "Có lỗi phát sinh vui lòng liên hệ QTV để kịp thời xử lý.",
                    icon: "error",
                    buttons: {
                        cancel: "Đóng",
                    },
                })
            },
            complete: function (data) {
                if (!$('.money-form-group .loader').hasClass('d-none')){
                    $('.money-form-group .loader').addClass('d-none');
                }
                // reload_captcha();
            }
        });
    }

    // Active swiper atm bank list both in modal and in pages
    let swiper_bank_lists = new Swiper('.swiper-bank-list', {
        autoplay: false,
        updateOnImagesReady: true,
        watchSlidesVisibility: false,
        lazyLoadingInPrevNext: false,
        lazyLoadingOnTransitionStart: false,
        slidesPerView: 6,
        speed: 300,
        spaceBetween: 16,
        allowTouchMove: false,
        grabCursor: false,
        observer: true,
        observeParents: true,
        freeMode: false,
        breakpoints: {
            992: {
                allowTouchMove: true,
                slidesPerView: 5,
            },
            768: {
                allowTouchMove: true,
                slidesPerView: 2.8,
                spaceBetween: 12,
            }
        }
    });

    $('body').on('click','#rechargeModal .daylamapinmodal',function(){
        $.ajax({
            type: 'GET',
            async:true,
            cache:false,
            url: '/reload-captcha',
            beforeSend: function () {
                $('.refresh-captcha img').removeClass("paused");
                $(".capchaImage").empty();
            },
            success: function (data) {
                $(".capchaImage").html(data.captcha);
            },
            complete: function () {
                $('input[name="captcha"]').val(null);
                setTimeout( function () {
                    $('.refresh-captcha img').addClass("paused");
                }, 1000);
            }
        });
    });

});
