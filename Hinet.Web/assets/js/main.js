var maTieuChi;
$('.modal-document input[type=text]').on('contextmenu', function (event) {
    event.preventDefault();  // Ngăn chặn menu chuột phải mặc định

    var menu = $('#customMenu');
    var input = $(this); // Tham chiếu đến phần tử hiện tại
    maTieuChi = input.attr('name');

    // Tính toán vị trí của phần tử input
    var inputOffset = input.offset();
    var inputWidth = input.outerWidth();
    var inputHeight = input.outerHeight();

    // Đặt menu ở bên phải phần tử input
    menu.css({
        'display': 'block',
        'left': inputOffset.left + inputWidth + 5 + 'px',  // Cách phần tử input 10px về bên phải
        'top': inputOffset.top + 'px'                        // Căn cùng đỉnh với phần tử input
    });

    // Đóng menu khi người dùng nhấn chuột vào một chỗ khác
    $(document).on('click', function () {
        menu.css('display', 'none');
    });
});


function handleCauHinhTieuChi(data) {
    var url = '/TieuChiThongKeArea/TieuChiThongKe/Save';
    var modal = 'ModalSave';
    AjaxCall(url, 'get', data, function (rs) {
        $("#" + modal).html(rs);
        $("#" + modal).modal("show");
        if ($('#' + modal + ' .modal-dialog.fixed-right').length > 0) {
            $(".modal-backdrop").css('display', 'none');
            $(".modal").css('pointer-events', 'none');
            $("button[data-dismiss=modal]").on('click', function () {
                $(".modal-backdrop").css('display', 'none');
                $(".modal").css('pointer-events', 'auto');
            });
        } else {
        }
    })
};