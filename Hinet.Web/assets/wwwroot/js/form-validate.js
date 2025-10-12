/*
 * Hướng dẫn sử dụng
 *
 * Có các Rules:
 *   cách selector đang sử dụng là dùng document.querySelector() ( dùng giống như selector của CSS )
 *
 * 1. isRequired() - Được xác định để trường không được để trống , ĐỐI SỐ: 1. selector (trỏ từ ID form) 2.message (Nếu không truyền message lỗi mặc định là 'Vui lòng nhập trường này');
 *
 * 2. isEmail() - Được xác định để trường nhập phải là email, ĐỐI SỐ: giống với isRequired();
 *
 * 3. minLength() - Được xác định để giới hạn tối thiếu kí tự trường cần nhập, ĐỐI SỐ: 1.selector, 2 giới hạn kí tự tối thiếu (Int), 3. Message khi gặp lỗi;
 *
 * 4. maxLength() - Được xác định để giới hạn tối đa kí tự trường cần nhập, ĐỐI SỐ: giống như minLength();
 *
 * 5. isConfirm() - Được xác định để xác nhận giá trị giống với 1 trường khác, ĐỐI SỐ: 1 selector , 2 giá trị cần được xác nhận (viết như demo bên dưới là đc), 3 message khi lỗi;
 *
 * Demo như bên dưới ( Nếu không có onSubmit như bên dưới thì form sẽ submit theo cách mặc định);
 *
 * Xử lí call api trong onSubmit kia nếu không muốn dùng submit mặc định;
 *
 * Nếu muốn thêm Rule nào thì mọi người định nghĩa thêm trong file thư viện gốc là được;
 *
 * Outfocus hoặc Submit form thì form mới bắt đầu validate chứ không phải lỗi đâu nhớ ^^
 * */

Validator({
  form: '#formLogin',
  formGroupSelector: '.input-group',
  errorSelector: '.text-error',
  rules: [
    Validator.isRequired('[name=Username]', 'Bạn chưa nhập tên tài khoản'),
    Validator.isRequired('[name=Password]', 'Bạn chưa nhập mật khẩu'),
  ],
  onSubmit: function (data) {
    const loginBtn = '#btnLogin';
    showButtonSpinner(loginBtn);
    $.ajax({
      url: '/Account/Login', 
      type: 'POST',
      data: data,
      success: function (res) {
        if (res.success) {
          $('#loginError')
            .removeClass('text-error')
              .addClass('text-success')
              .text(res.message)
          // Reset form
          $('#formLogin')[0].reset()
            location.reload()
        } else {
          $('#loginError')
            .removeClass('text-success')
            .addClass('text-error')
            .text(res.message || 'Có lỗi xảy ra')
        }
      },
      error: function (xhr) {
        $('#loginError')
          .removeClass('text-success')
          .addClass('text-error')
          .text('Lỗi hệ thống, vui lòng thử lại sau.')
        },
        complete: function () {
            hideButtonSpinner(loginBtn);
        },
    })
  },
})

Validator({
  form: '#formRegister',
  formGroupSelector: '.input-group',
  errorSelector: '.text-error',
  rules: [
    Validator.isRequired('[name=UserName]', 'Bạn chưa nhập tên tài khoản'),
    Validator.isRequired('[name=Password]', 'Bạn chưa nhập mật khẩu'),
    Validator.isRequired(
      '[name=ConfirmPassword]',
      'Bạn chưa nhập mật khẩu xác nhận'
    ),
  ],
    onSubmit: function (data) {
    const registerBtn = '#btnRegister';
    showButtonSpinner(registerBtn);
    $.ajax({
      url: '/Account/Register',
      type: 'POST',
      data: data,
      success: function (res) {
        if (res.success) {
          $('#registError')
            .removeClass('text-error')
              .addClass('text-success')
              .text(res.message)
          // Reset form
          $('#formRegister')[0].reset()
            location.reload()
        } else {
          $('#registError')
            .removeClass('text-success')
            .addClass('text-error')
            .text(res.message || 'Có lỗi xảy ra')
        }
      },
      error: function (xhr) {
        $('#registError')
          .removeClass('text-success')
          .addClass('text-error')
          .text('Lỗi hệ thống, vui lòng thử lại sau.')
        },
        complete: function () {
            hideButtonSpinner(registerBtn);
        },
    })
  },
})

Validator({
  form: '#form-changePassword',
  formGroupSelector: '.input-group',
  errorSelector: '.text-error',
  rules: [
    Validator.isRequired('[name=old_password]', 'Bạn chưa nhập mật khẩu'),
    Validator.isRequired('[name=password]', 'Bạn chưa nhập mật khẩu mới'),
    Validator.isRequired(
      '[name=password_confirmation]',
      'Bạn chưa nhập mật khẩu xác nhận'
    ),
    Validator.isConfirm(
      '[name=password_confirmation]',
      function () {
        return document.querySelector('#form-changePassword [name=password]')
          .value
      },
      'Mật khẩu xác nhận chưa chính xác'
    ),
  ],
  onSubmit: function (data) {
    changePassword(data)
  },
})
