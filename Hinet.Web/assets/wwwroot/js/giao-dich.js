// file: recharge-modal.js
function openGiaoDichModal(data) {
    Swal.fire({
        title: 'Đang tạo đơn nạp...',
        text: 'Vui lòng chờ trong giây lát',
        allowOutsideClick: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });
    $.ajax({
        url: '/GiaoDich/TaoGiaoDichVaQRCode',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function (res) {
            Swal.close();
            if (res.Success && res.Data) {
                $('#rechargeQrImage').attr('src', res.Data.QrUrl).show();
                $("#displayAmount").text(res.Data.SoTien.toLocaleString('vi-VN') + ' VND')
                $("#displayNoiDung").text(res.Data.NoiDungChuyenKhoan)
                $('#rechargeModal .btn-cancel').attr('data-code', res.Data.NoiDungChuyenKhoan);
                $('#rechargeModal').modal('show');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: res.Error || res.Message || 'Có lỗi xảy ra, vui lòng thử lại!'
                });
            }
        },
        error: function (err) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Không kết nối được server!'
            });
        }
    });
}

function huyGiaoDich(el) {
    var maGiaoDich = $('#displayNoiDung').text().trim();

    $.ajax({
        url: '/GiaoDich/HuyGiaoDich?maGiaoDich=' + encodeURIComponent(maGiaoDich),
        type: 'DELETE',
        success: function (res) {
            $('#rechargeModal').modal('hide');
            if (res.Success) {
                console.log("Hủy giao dịch thành công:", maGiaoDich);
            } else {
                console.log("Có lỗi khi hủy giao dịch:", res.Message);
            }
        },
        error: function () {
            console.log("Lỗi kết nối khi huỷ giao dịch");
        }
    });
}