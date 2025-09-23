//Show login modal and mobile login
function openLoginModal(){
    let width = $(window).width();
    setTimeout(function(){
        if ( width > 992 ) {
            $('#loginModal').modal('show');
            setTimeout(() => {
                $('#loginModal #modal-login-container').removeClass('right-panel-active');
            }, 200);
        } else {
            $('.mobile-auth-form #formLoginMobile').css('display', 'flex');
            $('.mobile-auth-form #formRegisterMobile').css('display', 'none');
            $('.mobile-auth .head-mobile p').text('Đăng nhập');
            $('.mobile-auth').css('transform', 'translateX(0)');
        }
    }, 0);
}
function openRegisterModal(){
    let width = $(window).width();
    setTimeout(function(){
        if ( width > 992 ) {
            $('#loginModal').modal('show');
            setTimeout(() => {
                $('#loginModal #modal-login-container').addClass('right-panel-active');
            }, 200);
        } else {
            $('.mobile-auth-form #formLoginMobile').css('display', 'none');
            $('.mobile-auth-form #formRegisterMobile').css('display', 'flex');
            $('.mobile-auth .head-mobile p').text('Đăng ký');
            $('.mobile-auth').css('transform', 'translateX(0)');
        }
    }, 0);
}


$(document).ready(function () {

    //Button đăng nhập đăng ký để chuyển animation
    const signUpButton = $('#signUp');
    const signInButton = $('#signIn');
    const container = $('#modal-login-container');



    //Click sigin signup button
    $(signUpButton).click(function (e) {
        container.addClass("right-panel-active");
    });

    $(signInButton).click(function (e) {
        container.removeClass("right-panel-active");
    });

    //Close login modal
    $('.close-login-modal').click(function (e) {
        e.preventDefault();
        $('#loginModal').modal('hide');
        $('.modal-login-error').text('');
    });

    //Hide and show password
    $('.password-input-show').click(function(e) {
        let inputPassword = $(this).parent().find('input');
        let eyeHide = $(this).parent().find('.password-input-hide');
        $(this).css('display','none');
        eyeHide.css('display','block');
        $(inputPassword).attr('type', 'text');
    });

    $('.password-input-hide').click(function(e) {
        let inputPassword = $(this).parent().find('input');
        let eyeShow = $(this).parent().find('.password-input-show');
        $(this).css('display','none');
        eyeShow.css('display','block');
        $(inputPassword).attr('type', 'password');
    });

    //End js hide show

    //Close login mobile
    $('.mobile-auth-header img').click(function(e) {
        $('.mobile-auth').removeClass('mobile-auth-show');
    });

    // $('.notification img').click( function() {
    //     $('.mobile-auth').removeClass('mobile-auth-show');
    // });
    // $('.nav-bar-info-search').click( function() {
    //     $('.mobile-auth').removeClass('mobile-auth-show');
    // });

    //Click in mobile auth nav action

    $('#handleLoginPopup').click(function (e) {
        e.preventDefault();
        $('.mobile-auth-form #formLoginMobile').css('display', 'flex');
        $('.mobile-auth-form #formRegisterMobile').css('display', 'none');
        $('.mobile-auth .head-mobile h1').text('Đăng nhập');
        $('.mobile-auth').css('transform', 'translateX(0)');
    });

    $('#handleRegisterPopup').click(function (e) {
        e.preventDefault();
        $('.mobile-auth-form #formLoginMobile').css('display', 'none');
        $('.mobile-auth-form #formRegisterMobile').css('display', 'flex');
        $('.mobile-auth .head-mobile h1').text('Đăng ký');
        $('.mobile-auth').css('transform', 'translateX(0)');
    });

    $('.changeFormLogin').click(function (e) {
        e.preventDefault();
        $('.mobile-auth-form #formLoginMobile').fadeIn(500);
        $('.mobile-auth-form #formRegisterMobile').attr('style', 'display: none !important');
        $('.mobile-auth .head-mobile h1').text('Đăng nhập');
    });

    $('.changeFormRegister').click(function (e) {
        e.preventDefault();
        $('.mobile-auth-form #formLoginMobile').attr('style', 'display: none !important');
        $('.mobile-auth-form #formRegisterMobile').fadeIn(500);
        $('.mobile-auth .head-mobile h1').text('Đăng ký');
    });

    //Submit Login Desktop
    // $('#formLogin, #formLoginMobile').submit(function (e) {
    //     e.preventDefault();
    //     var formSubmit = $(this);
    //     var url = formSubmit.attr('action');
    //     var btnSubmit = formSubmit.find(':submit');
    //     btnSubmit.text('Đang xử lý...');
    //     btnSubmit.prop('disabled', true);
    //     $.ajax({
    //         type: "POST",
    //         url: url,
    //         cache:false,
    //         data: formSubmit.serialize(), // serializes the form's elements.
    //         beforeSend: function (xhr) {
    //             formSubmit.find('#usernameError').text('');
    //             formSubmit.find('#passwordError').text('');
    //             $('#formLoginMobile input').removeClass('input-error');
    //             $('#formLogin input').removeClass('input-error');
    //         },
    //         success: function (response) {
    //
    //         },
    //         error: function (response) {
    //             if(response.status === 422 || response.status === 429) {
    //                 let errors = response.responseJSON.errors;
    //                 let keys = Object.keys(errors);
    //                 $.each(keys, function (index, key) {
    //                     if ( key == "username" ) {
    //                         $('#formLogin input[name="username"]').addClass('input-error');
    //                         $('#formLoginMobile input[name="username"]').addClass('input-error');
    //                         formSubmit.find('#usernameError').text(errors[key]);
    //                     } else if ( key == "password" ) {
    //                         $('#formLogin input[name="password"]').addClass('input-error');
    //                         $('#formLoginMobile input[name="password"]').addClass('input-error');
    //                         formSubmit.find('#passwordError').text(errors[key]);
    //                     }
    //                 });
    //                 return false;
    //             } else {
    //                 alert('Kết nối với hệ thống thất bại.Xin vui lòng thử lại');
    //             }
    //         },
    //         complete: function (data) {
    //             btnSubmit.text('Đăng nhập');
    //             btnSubmit.prop('disabled', false);
    //         }
    //     });
    // });
    //
    // $('#formRegister, #formRegisterMobile').submit(function (e) {
    //     e.preventDefault();
    //     var formSubmit = $(this);
    //     var url = formSubmit.attr('action');
    //     var btnSubmit = formSubmit.find('button[type="submit"]');
    //     btnSubmit.text('Đang xử lý...');
    //     btnSubmit.prop('disabled', true);
    //
    //     $.ajax({
    //         type: "POST",
    //         url: url,
    //         data: formSubmit.serialize(), // serializes the form's elements.
    //         beforeSend: function (xhr) {
    //             formSubmit.find('#emailRegisterError').text('');
    //             formSubmit.find('#usernameRegisterError').text('');
    //             formSubmit.find('#usernameRegisterErrorMobile').text('');
    //             formSubmit.find('#emailRegisterErrorMobile').text('');
    //             formSubmit.find('#passwordRegisterError').text('');
    //             formSubmit.find('#phoneRegisterError').text('');
    //             $('#formRegisterMobile input').removeClass('input-error');
    //             $('#formRegister input').removeClass('input-error');
    //         },
    //         success: function (response) {
    //
    //         },
    //         error: function (response) {
    //             if(response.status === 422 || response.status === 429) {
    //                 let errors = response.responseJSON.errors;
    //
    //                 let keys = Object.keys(errors);
    //                 $.each(keys, function (index, key) {
    //                     if ( key == "email" ) {
    //                         formSubmit.find('#emailRegisterError').text(errors[key]);
    //                         formSubmit.find('#emailRegisterErrorMobile').text(errors[key]);
    //                         $('#formRegisterMobile input[name="email"]').addClass('input-error');
    //                         $('#formRegister input[name="email"]').addClass('input-error');
    //                     } else if ( key == "password" ) {
    //                         formSubmit.find('#passwordRegisterError').text(errors[key]);
    //                         $('#formRegisterMobile input[name="password"]').addClass('input-error');
    //                         $('#formRegister input[name="password"]').addClass('input-error');
    //                     } else if ( key == "phone" ) {
    //                         formSubmit.find('#phoneRegisterError').text(errors[key]);
    //                         formSubmit.find('#phoneRegisterErrorMobile').text(errors[key]);
    //                         $('#formRegisterMobile input[name="phone"]').addClass('input-error');
    //                         $('#formRegister input[name="phone"]').addClass('input-error');
    //                     } else if ( key == "username" ) {
    //                         formSubmit.find('#usernameRegisterError').text(errors[key]);
    //                         formSubmit.find('#usernameRegisterErrorMobile').text(errors[key]);
    //                         $('#formRegisterMobile input[name="username"]').addClass('input-error');
    //                         $('#formRegister input[name="username"]').addClass('input-error');
    //                     }
    //                 });
    //                 return false;
    //             } else {
    //                 alert('Kết nối với hệ thống thất bại.Xin vui lòng thử lại');
    //
    //             }
    //         },
    //         complete: function (data) {
    //             btnSubmit.text('Đăng ký');
    //             btnSubmit.prop('disabled', false);
    //             $('.modal-loader-container').css('display', 'none');
    //         }
    //     });
    // });

});
