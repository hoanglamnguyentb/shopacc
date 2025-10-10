if (!Array.prototype.last) {
    Array.prototype.last = function () {
        if (this != "" && this.length > 0)
            return this[this.length - 1];
        return "";
    };
}

if (!String.prototype.replaceInRange) {
    String.prototype.replaceInRange = function (
        start,
        end,
        oldValue,
        newValue
    ) {
        return (
            this.substring(0, start) +
            this.substring(start, end).replace(oldValue, newValue) +
            this.substring(end)
        );
    };
}

function GetActionOfStatus(listFunc, status) {
    let lstData = listFunc.filter((item) => {
        return item.Status == status
    });
    return lstData[0];
}

function getLink(configBtn, cl, prams) {
    var url = configBtn.LinkPattern;
    if (prams != null) {
        for (var i = 0; i < prams.length; i++) {
            url = url.replace("{" + i + "}", prams[i]);
        }
    }
    var icon = "";
    if (configBtn.Icon != null) {
        icon = "<i class='" + configBtn.Icon + "'> </i> ";
    }
    switch (configBtn.Type) {
        case 1:
            return "<a class='" + cl + "' href='" + url + "'>" + icon + configBtn.Name + "</a>";
        case 2:
            return "<a class='" + cl + "' href='javascripts:void(0)' onclick='EditAction(\"" + url + "\")'>" + icon + configBtn.Name + "</a>";
        case 3:
            return "<a class='" + cl + "' href='javascripts:void(0)' onclick='confirmLink(\"" + url + "\")'>" + icon + configBtn.Name + "</a>";
    }
}

function confirmLink(url, message) {
    var mss = "Xác nhận thao tác ?";
    if (message != null) {
        mss = message;
    }

    $.confirm({
        title: mss,
        content: "Bạn chắc chắn muốn thực hiện thao tác này",
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCall(url, 'post', null, function (rs) {
                        if (rs.Status) {
                            NotiSuccess("Thành công", rs.Message);
                            AfterSussessActionAjaxform();
                        } else {
                            NotiError("Lỗi xử lý", rs.Message);
                        }
                    })
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}
function TextOrDefault(val) {
    if (val) {
        return val;
    } else {
        return `<span class="font-italic text-danger">Chưa cập nhật</span>`
    }
}
function chuyendoidata(str) {
    return decodeURIComponent(escape(window.atob(str)));
}
//hàm confirm để gọi ajax
function onConfirmCallAjax(url, type, params, callBack, title, content) {
    if (!title) {
        title = 'Xác nhận thao tác';
    }

    if (!content) {
        content = 'Bạn chắc chắn muốn thực hiện thao tác này';
    }

    $.confirm({
        title: title,
        content: content,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCall(url, type, params, callBack);
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

// có thêm type và content
function onConfirmCallAjaxCustomer(url, type, params, callBack, contentType, dataType, title, content) {
    if (!title) {
        title = 'Xác nhận thao tác';
    }

    if (!content) {
        content = 'Bạn chắc chắn muốn thực hiện thao tác này';
    }

    $.confirm({
        title: title,
        content: content,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCallCustomer(url, type, contentType, dataType, params, callBack);
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

function AjaxCallCustomer(url, type, contentType, dataType, params, callBack, callbackError) {
    var isfunction = callBack && typeof (callBack) == "function";
    if (!isfunction) {
        callback = function () {
            console.log("Chưa cài đặt sự kiện thành công");
        }
    }
    var isfunction = callbackError && typeof (callbackError) == "function";
    if (!isfunction) {
        callbackError = function () {
            NotiError("Thao tác không thể thực hiện");
        }
    }
    $.ajax({
        url: url,
        type: type,
        contentType: contentType,
        dataType: dataType,
        data: params,
        success: callBack,
        error: callbackError
    })
}

//hàm confirm để gọi ajax check convert map
function onConfirmCallAjaxConvert(url, type, params, callBack, title, content, callBackCancle) {
    if (!title) {
        title = 'Xác nhận thao tác';
    }

    if (!content) {
        content = 'Bạn chắc chắn muốn thực hiện thao tác này';
    }

    $.confirm({
        title: title,
        content: content,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCall(url, type, params, callBack);
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                    callBackCancle()
                }
            }
        }
    });
}

//hàm confirm để gọi một hàm khác
function onConfirmCall(callBack, title, content) {
    if (!title) {
        title = 'Xác nhận thao tác';
    }

    if (!content) {
        content = 'Bạn chắc chắn muốn thực hiện thao tác này';
    }

    $.confirm({
        title: title,
        content: content,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    callBack();
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

function NotiSuccess(title, message) {
    if (message == null || message == '') {
        message = "Thành công";
    }
    Toastify({
        text: message,
        close: true,
        style: {
            background: "linear-gradient(to right, #00b09b, #96c93d)",
        },
        duration: 3000,
        gravity: "bottom",
        position: "right",
    }).showToast();
}
function NotiError(title, message) {
    if (message == null || message == '') {
        message = "Có lỗi xảy ra";
    }
    Toastify({
        text: message,
        close: true,
        style: {
            background: "linear-gradient(to right, rgb(255, 95, 109), rgb(255, 195, 113))",
        },
        duration: 3000,
        gravity: "bottom",
        position: "right",
    }).showToast();
}

function AfterSussessActionAjaxform() {
    location.reload();
}

function AjaxSearchSuccess(rs) {
    location.reload();
}

function AjaxFormSuccess(rs) {
    if (rs.Status) {
        $("#MasterModal").modal("hide");
        $("#MasterModal").empty();
        NotiSuccess("Thành công", "Cập nhật dữ liệu thành công");
        AfterSussessActionAjaxform();
    } else {
        NotiError("Lỗi xử lý", rs.Message);
    }
}

function AjaxFormSuccessSDI(rs) {
    if (rs.Status) {
        NotiSuccess("Thành công", rs.Message);

        if (rs.Param != null && rs.Param.href != null && rs.Param.href.length > 0) {
            // Lấy đối tượng form trong modal
            var form = $('#CreateAjaxForm');

            // Reset giá trị của form
            form[0].reset();
            $('.select2').val(null).trigger('change');

            CKEDITOR.instances['NoiDung']?.setData('');
        }
        else {
            $("#MasterModal").modal("hide");
            $("#MasterModal").empty();
            AfterSussessActionAjaxform();
        }
    } else {
        NotiError("Lỗi xử lý", rs.Message);
    }
}
function ClosePopup() {
    $("#MasterModal").modal("hide");
    $("#MasterModal").empty();
}
function AjaxFormSuccessRequestStatus(rs, url) {
    if (rs.Status) {
        $("#MasterModal").modal("hide");
        $("#MasterModal").empty();
        NotiSuccess("Thành công", "Cập nhật dữ liệu thành công");
        //FixedRightAction_6(url);
        AfterSussessActionAjaxform();
    } else {
        NotiError("Lỗi xử lý", rs.Message);
    }
}
function AjaxFormError() {
    NotiError("Có lỗi xảy ra", "Vui lòng kiểm tra lại thông tin");
}

function AjaxCall(url, type, data, callback, callbackError) {
    var isfunction = callback && typeof (callback) == "function";
    if (!isfunction) {
        callback = function () {
            console.log("Chưa cài đặt sự kiện thành công");
        }
    }
    var isfunction = callbackError && typeof (callbackError) == "function";
    if (!isfunction) {
        callbackError = function () {
            NotiError("Thao tác không thể thực hiện");
        }
    }
    $.ajax({
        url: url,
        type: type,
        data: data,
        success: callback,
        error: callbackError
    })
}
function AjaxCallSync(url, type, data, callback, callbackError) {
    var isfunction = callback && typeof (callback) == "function";
    if (!isfunction) {
        callback = function () {
            console.log("Chưa cài đặt sự kiện thành công");
        }
    }
    var isfunction = callbackError && typeof (callbackError) == "function";
    if (!isfunction) {
        callbackError = function () {
            NotiError("Thao tác không thể thực hiện");
        }
    }
    $.ajax({
        url: url,
        type: type,
        data: data,
        async: false,
        cache: false,
        timeout: 30000,
        success: callback,
        error: callbackError
    })
}

function EditAction(url, modal = null) {
    if (modal == null || modal == '') modal = 'MasterModal';
    AjaxCall(url, 'get', null, function (rs) {
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
}

function AfterEditAction() {
    console.log("AfterEditAction");
}

$(document).on('hidden.bs.modal', '.modal', function () {
    $('body').toggleClass('modal-open', $('.modal').hasClass('in'));
});

function ModalAction2(url) {
    AjaxCall(url, 'get', null, function (rs) {
        $("#MasterModal2").html(rs);
        $("#MasterModal2").modal("show");
    })
}
function JustAction(url) {
    try {
        AjaxCall(url, 'get', null, function (rs) {
            if (rs.Status) {
                NotiSuccess("Thành công", rs.Message);
                AfterSussessActionAjaxform();
            } else {
                NotiError("Lỗi xử lý", rs.Message);
            }
        })
    } catch (e) {
        console.log(e);
    }
}
function JustActionNonReload(url) {
    AjaxCall(url, 'get', null, function (rs) {
        if (rs.Status) {
            NotiSuccess("Thành công", rs.Message);
        } else {
            NotiError("Lỗi xử lý", rs.Message);
        }
    })
}

function CreateAction(url, modal) {
    ///debugger
    if (typeof ShowLoading === "function") {
        ShowLoading();
    }
    if (modal == null || modal == '') modal = 'MasterModal';
    AjaxCall(url, 'get', null, function (rs) {
        if (typeof HideLoading === "function") {
            HideLoading();
        }
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
}

function CreatePostAction(url, param) {
    AjaxCall(url, 'post', param, function (rs) {
        $("#MasterModal").html(rs);
        $("#MasterModal").modal("show");
        if ($('#MasterModal .modal-dialog.fixed-right').length > 0) {
            $(".modal-backdrop").css('display', 'none');
            $(".modal").css('pointer-events', 'none');
            $("button[data-dismiss=modal]").on('click', function () {
                $(".modal-backdrop").css('display', 'none');
                $(".modal").css('pointer-events', 'auto');
            });
        } else {
        }
    })
}

function DetailAction(url) {
    AjaxCall(url, 'get', null, function (rs) {
        $("#MasterModal").html(rs);
        $("#MasterModal").modal("show");
        if ($('#MasterModal .modal-dialog.fixed-right').length > 0) {
            $(".modal-backdrop").css('display', 'none');
            $(".modal").css('pointer-events', 'none');
            $("button[data-dismiss=modal]").on('click', function () {
                $(".modal-backdrop").css('display', 'none');
                $(".modal").css('pointer-events', 'auto');
            });
        } else {
        }
    })
}
//download fiel biểu mẫu
function OnDownload(url) {
    AjaxCall(url, 'post', null, function (rs) {
        if (rs.Status) {
            //NotiSuccess("Thành công", rs.Message);
            location.href = rs.Param;
        } else {
            NotiError("Lỗi xử lý", rs.Message);
        }
    })
}

function DetailFormAction(url, type) {
    AjaxCall(url, 'post', { type: type }, function (rs) {
        //$("#ModalLayoutSecon").html(rs);
        //$("#ModalLayoutSecon").modal("show");
        var targetModal = $('#modalSecond');
        if (!targetModal.length) {
            $('body').append('<div id="modalSecond" data-backdrop="static" data-keyboard="false" title="" role="dialog" class="modal fade" style="z-index:1054"></div>');
        }

        $('#modalSecond').html(rs);
        $('#modalSecond').modal('show');
        $('#modalSecond').on('hidden.bs.modal', function () {
            var modals = $('.modal.fade.in');
            if (modals.length > 0) {
                $('body').addClass("modal-open");
            }
        })
    });
}

function InsertPatial(url, box) {
    AjaxCall(url, 'get', null, function (rs) {
        $("#" + box).html(rs);
    })
}
function OpenModalFixed(url, type, data) {
    AjaxCall(url, type, data, function (rs) {
        $("#MasterModalFixed").html(rs);
        $("#MasterModalFixed").modal("show");
    })
}

function OpenModalPreview(url, type, data) {
    AjaxCall(url, type, data, function (rs) {
        $("#MasterModalPreview").html(rs);
        $("#MasterModalPreview").modal("show");
    })
}

function CloseModalPreview() {
    $("#MasterModalPreview").html('');
    $("#MasterModalPreview").modal("hide");
}

function OpenModal(url, type, data) {
    AjaxCall(url, type, data, function (rs) {
        $("#MasterModal").html(rs);
        $("#MasterModal").modal("show");
    })
}

function extractTimestamp(dateString) {
    // Sử dụng Regular Expression để lấy phần số trong chuỗi
    const match = dateString.match(/\/Date\((\d+)\)\//);
    return match ? parseInt(match[1]) : null;
}

function OpenModalV3(url, IdDotBaoCao_DoanhNghiep) {
    let newUrl = url;
    if (url) {
        newUrl = newUrl.replace('Detail', 'Detail2');
    };

    $.ajax({
        url: newUrl,
        type: 'post',
        data: { id: IdDotBaoCao_DoanhNghiep },
        success: function (rs) {
            $("#MasterModal").html(rs);
            $("#MasterModal").modal("show");
        },
        error: function () {
            NotiError("Thông báo", "Đã xảy ra lỗi trong quá trình lấy dữ liệu");
        }
    })
}

function OpenModalV2(url, idDotBaoCaoDN, GuidID) {
    $.ajax({
        url: url,
        type: 'post',
        data: { idDotBaoCaoDN: idDotBaoCaoDN, GuidID: GuidID },
        success: function (rs) {
            $("#MasterModal").html(rs);
            $("#MasterModal").modal("show");
        },
        error: function () {
            NotiError("Thông báo", "Đã xảy ra lỗi trong quá trình lấy dữ liệu");
        }
    })
}

//<div id="MasterModal" class="modal fade aside aside-right aside-vc no-backdrop aside-hidden" role="dialog" data-placement="right" data-backdrop="false" overflow-y="scroll" style="display: none;">
function closeFRModal() {
    if ($("#MasterModalFixed").length) {
        $("#MasterModalFixed").find(".close").click();
    }
}
function FixedRightActionModalMode(rs, width) {
    $("#MasterModalFixed").html(rs);
    $("#MasterModalFixed .modal-dialog").addClass("fixed-modal-right", "scroll-disable");
    $("#MasterModalFixed .modal-dialog").addClass("fmr-width-" + width);
    //$('.modal.aside').ace_aside();
    $("#MasterModalFixed").modal("show");
    $(".modal.aside.in").css("z-index", 1045);
}
function FixedRightAction(url) {
    closeFRModal();
    AjaxCall(url, 'get', null, function (rs) {
        FixedRightActionModalMode(rs, 20);
    })
    //affterFRModal();
}
function FixedRightAction_2(url) {
    closeFRModal();
    AjaxCall(url, 'get', null, function (rs) {
        FixedRightActionModalMode(rs, 20);
    })
}
function FixedRightAction_4(url) {
    closeFRModal();
    AjaxCall(url, 'get', null, function (rs) {
        FixedRightActionModalMode(rs, 40);
    })
}
function FixedRightAction_6(url) {
    closeFRModal();
    AjaxCall(url, 'get', null, function (rs) {
        FixedRightActionModalMode(rs, 60);
    })
}
function FixedRightAction_8(url) {
    closeFRModal();
    AjaxCall(url, 'get', null, function (rs) {
        FixedRightActionModalMode(rs, 80);
    })
}

//fixedActionCreate begin
function OpenModalAction(url) {
    AjaxCall(url, 'get', null, function (rs) {
        $("#MasterModal").html(rs);
        $("#MasterModal").modal("show");
    })
}

function CloseModelMaster() {
    $("#MasterModal").modal("hide");
}

function onDeleteConfirm(mesage, callBack) {
    if (mesage == null || mesage == '') {
        mesage = "Bạn xác nhận thực hiện thao tác này ?";
    }

    $.confirm({
        title: 'Xác nhận xóa',
        content: mesage,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    callBack();
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

function DeleteAction(url, mesage, callBack) {
    if (mesage == null || mesage == '') {
        mesage = "Bạn xác nhận thực hiện thao tác này ?";
    }
    $.confirm({
        title: 'Xác nhận xóa',
        content: mesage,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCall(url, 'post', null, function (rs) {
                        if (rs.Status) {
                            NotiSuccess("Thành công", rs.Message);
                            var isfunction = callBack && typeof (callBack) == "function";
                            if (isfunction) {
                                callBack(rs.Param);
                            } else {
                                AfterSussessActionAjaxform();
                            }
                        } else {
                            NotiError("Lỗi xử lý", rs.Message);
                        }
                    })
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

//function DeleteAction2(url, idDotBaoCaoDN, GuidID) {
//    const message = "Bạn có chắc chắn muốn xóa?";
//    $.confirm({
//        title: 'Xác nhận xóa',
//        content: message,
//        draggable: false,
//        theme: 'material',
//        buttons: {
//            confirm: {
//                btnClass: 'btn-primary',
//                text: "Xác nhận",
//                action: function () {
//                    $.ajax({
//                        url: url,
//                        type: 'post',
//                        data: { idDotBaoCaoDN: idDotBaoCaoDN, GuidID: GuidID },
//                        success: function (rs) {
//                            if (rs.Status) {
//                                NotiSuccess("Thông báo", rs.Message);
//                            } else {
//                                NotiError("Thông báo", rs.Message);
//                            }
//                        },
//                        error: function () {
//                            NotiError("Thông báo", "Xóa không thành công!")
//                        }
//                    })
//                }
//            },
//            cancel: {
//                text: "Đóng",
//                action: function () {
//                }
//            }
//        }
//    });
//}

function ConfirmDataAction(url, mesage) {
    if (mesage == null || mesage == '') {
        mesage = "Bạn xác nhận thực hiện thao tác này ?";
    }
    $.confirm({
        title: 'Xác nhận',
        content: mesage,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCall(url, 'post', null, function (rs) {
                        if (rs.Status) {
                            NotiSuccess("Thành công", rs.Message);
                            AfterSussessActionAjaxform();
                        } else {
                            NotiError("Lỗi xử lý", rs.Message);
                        }
                    })
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

function ConfirmDataAction2(url, mesage) {
    if (mesage == null || mesage == '') {
        mesage = "Bạn xác nhận thực hiện thao tác này ?";
    }
    $.confirm({
        title: 'Xác nhận',
        content: mesage,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: function () {
                    AjaxCall(url, 'post', null, function (rs) {
                        if (rs.Status) {
                            NotiSuccess("Thành công", rs.Message);
                            AfterSussessActionAjaxform2();
                        } else {
                            NotiError("Lỗi xử lý", rs.Message);
                        }
                    })
                }
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

function ConfirmAction(url, type, data, mesage) {
    if (mesage == null || mesage == '') {
        mesage = "Bạn xác nhận thực hiện thao tác này ?";
    }
    bootbox.confirm(mesage, function (result) {
        if (result) {
            AjaxCall(url, type, data, function (rs) {
                if (rs.Status) {
                    NotiSuccess("Thành công", rs.Message);
                    AfterSussessActionAjaxform();
                } else {
                    NotiError("Lỗi xử lý", rs.Message);
                }
            })
        }
    });
}

function OpenModalConfirm(classGhiChu, classNgayChot, fun) {
    $.confirm({
        title: 'CHỐT SỐ LIỆU',
        content: '' +
            '<form action="" class="formName">' +
            '<div class="form-group">' +
            '<label>Ghi chú</label>' +
            '<textarea class="form-control" id="GhiChuChung" required></textarea>' +
            '</div>' +
            '<div class="form-group">' +
            '<label>Chọn thời gian</label>' +
            '<input type="text" id="NgayChotChung" name="NgayChotChung" class="txt-picker datetimepicker form-control" required />' +
            '</div>' +
            '</form>',

        buttons: {
            formSubmit: {
                text: 'Lưu',
                btnClass: 'btn-success',
                action: function () {
                    let ngayChotSoLieu = this.$content.find('input[name="NgayChotChung"]').val();
                    if (!ngayChotSoLieu) {
                        $.alert('Vui lòng nhập ngày chốt!');
                        return false;
                    }

                    // đổ dữ liệu về form chính
                    let ghichu = $('#GhiChuChung').val();
                    let ngaychot = $('#NgayChotChung').val();

                    if ($(`.${classGhiChu}`)) {
                        $(`.${classGhiChu}`).each(function () {
                            $(this).val(ghichu);
                        })
                    }

                    if ($(`.${classNgayChot}`)) {
                        $(`.${classNgayChot}`).each(function () {
                            $(this).val(ngaychot);
                        })
                    }

                    if (typeof fun === "function") {
                        fun();
                    }
                }
            },
            cancel: function () {
                //close
            },
        },
        onContentReady: function () {
            $('input[name="NgayChotChung"]').datepicker({
                dateFormat: 'dd/mm/yyyy',
                changeMonth: true,
                autoclose: true,
                todayHighlight: true,
                changeYear: true, yearRange: "-50:+20",
                showWeek: false, weekHeader: "Tuần",
                language: 'vi',
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                endDate: new Date(),
                onSelect: function (date) {
                }
            });

            //lấy ngày hiện tại
            var today = new Date().toISOString().split('T')[0];
            var dateParts = today.split('-');
            var formattedDate = dateParts[2] + '/' + dateParts[1] + '/' + dateParts[0];

            this.$content.find('input[name="NgayChotChung"]').val(formattedDate);

            var jc = this;
            this.$content.find('form').on('submit', function (e) {
                // if the user submits the form by pressing enter in the field.
                e.preventDefault();
                jc.$$formSubmit.trigger('click'); // reference the button and click it
            });
        }
    });
}

function RequireConfirm(title, content, functionYes) {
    if (title == null || title == '') {
        title = "Bạn xác nhận thực hiện thao tác này ?";
    }
    if (content == null || content == '') {
        content = "Bạn chắc chắn muốn thực hiện thao tác này ?";
    }
    $.confirm({
        title: title,
        content: content,
        draggable: false,
        theme: 'material',
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                text: "Xác nhận",
                action: functionYes
            },
            cancel: {
                text: "Đóng",
                action: function () {
                }
            }
        }
    });
}

/**
 * @author:duynn
 * @create_date: 19/04/2019
 * @param {any} url
 * @param {any} mesage
 */
function onDelete(url, mesage) {
    if (mesage == null || mesage == '') {
        mesage = "Bạn xác nhận thực hiện thao tác này ?";
    }
    bootbox.confirm(mesage, function (result) {
        if (result) {
            AjaxCall(url, 'post', null, function (result) {
                if (result.Status) {
                    NotiSuccess("Thành công", result.Message);
                    AfterSussessActionAjaxform();
                } else {
                    NotiError("Lỗi xử lý", result.Message);
                }
            })
        }
    });
}

function ToTime(obj) {
    if (obj == null) {
        return "";
    } else {
        if (obj.indexOf('Date') >= 0) {
            var dateint = parseInt(obj.match(/\d+/)[0]);
            obj = new Date(dateint);
        } else {
            obj = new Date(obj);
        }
        var date_string = obj.getHours() + ":" + obj.getMinutes();
        return date_string;
    }
}

function ToTime2(obj) {
    if (obj == null) {
        return "";
    } else {
        if (obj.indexOf('Date') >= 0) {
            var dateint = parseInt(obj.match(/\d+/)[0]);
            obj = new Date(dateint);
        } else {
            obj = new Date(obj);
        }
        var hours = obj.getHours();
        var minutes = obj.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12 || 12; // Convert hour to 12-hour format, 0 should be 12
        var date_string = hours + ":" + (minutes < 10 ? '0' : '') + minutes + ' ' + ampm;
        return date_string;
    }
}

function ToDate(obj) {
    if (obj == null) {
        return "";
    } else {
        if (obj.indexOf('Date') >= 0) {
            var dateint = parseInt(obj.match(/\d+/)[0]);

            obj = new Date(dateint);
        } else {
            obj = new Date(obj);
        }
        var mon = '';
        if ((obj.getMonth() + 1) < 10) {
            mon = "0" + (obj.getMonth() + 1);
        } else {
            mon = (obj.getMonth() + 1);
        }
        var day = "";
        if (obj.getDate() < 10) {
            day = '0' + obj.getDate();
        } else {
            day = obj.getDate();
        }
        var date_string = day + "/" + mon + "/" + obj.getFullYear();
        return date_string;
    }
}

function ToNumber(val) {
    var sign = 1;
    if (val < 0) {
        sign = -1;
        val = -val;
    }

    // trim the number decimal point if it exists
    let num = val.toString().includes('.') ? val.toString().split('.')[0] : val.toString();

    while (/(\d+)(\d{3})/.test(num.toString())) {
        // insert comma to 4th last position to the match number
        num = num.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
    }

    // add number after decimal point
    if (val.toString().includes('.')) {
        num = num + '.' + val.toString().split('.')[1];
    }

    // return result with - sign if negative
    return sign < 0 ? '-' + num : num;
}

function ToDateTime(obj) {
    if (obj == null) {
        return "";
    } else {
        if (obj.indexOf('Date') >= 0) {
            var dateint = parseInt(obj.match(/\d+/)[0]);
            obj = new Date(dateint);
        } else {
            obj = new Date(obj);
        }
        var mon = '';
        if ((obj.getMonth() + 1) < 10) {
            mon = "0" + (obj.getMonth() + 1);
        } else {
            mon = (obj.getMonth() + 1);
        }
        var day = "";
        if (obj.getDate() < 10) {
            day = '0' + obj.getDate();
        } else {
            day = obj.getDate();
        }

        var hour = obj.getHours();
        if (hour < 10) {
            hour = "0" + hour;
        }
        var minute = obj.getMinutes()
        if (minute < 10) {
            minute = "0" + minute;
        }
        var date_string = day + "/" + mon + "/" + obj.getFullYear() + " " + hour + ":" + minute;
        return date_string;
    }
}

function onAddNewRow(element) {
    var parent = $(element).closest('table');
    if (!parent) {
        return;
    }
    var request = $(element).data('request');
    if (!request) {
        return;
    }

    $.get(request, function (result) {
        parent.find('tbody').append(result);
    })
}

function onRemoveRow(element) {
    var parent = $(element).closest('tr');
    if (!parent) {
        return;
    }
    $(parent).remove();
}

function validateRequired(container) {
    var isValid = true;
    $("#" + container + " .required").each(function () {
        var parent = $(this).parents(".form-group").first();
        if (parent.length == 0) {
            parent = $(this).parent();
        }
        var errorText = parent.find(".error");
        if ($(this).val() == null || $(this).val().length == 0 || $(this).val().toString().trim() == "") {
            errorText.addClass('error-required');
            isValid = false;
        } else {
            errorText.removeClass('error-required');
        }
    });
    return isValid;
}

function validateDate(container) {
    var isValid = true;
    var pattern = /^[0-3][0-9]\/[01][0-9]\/[12][0-9][0-9][0-9]$/;
    var inputs = $("#" + container + " .checkDateValid");

    inputs.each(function () {
        var parent = $(this).parents(".form-group").first();
        var errorText = parent.find(".error");
        if ($(this).val() != null &&
            $(this).val().length != 0 &&
            $(this).val().toString().trim() != "") {
            if (!pattern.test($(this).val())) {
                errorText.addClass("error-date-format");
                isValid = false;
            }
            else {
                errorText.removeClass("error-date-format");
            }
        }
    })
    return isValid;
}

function validateSelectOption(container) {
    var isValid = true;
    var inputs = $("#" + container + " select.requiredDropDownList");
    inputs.each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errorText = parent.find(".error");
        if ($(this).val() == null || $(this).val().length == 0) {
            errorText.addClass('error-required-dropdown');
            isValid = false;
        } else {
            errorText.removeClass('error-required-dropdown');
        }
    })
    return isValid;
}

function validateTextArea(container) {
    var isValid = true;
    $("#" + container + " .requiredTextArea").each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errorText = parent.find(".error");
        if ($(this).html() == null || $(this).html().trim() == "") {
            errText.addClass('error-required');
            isValid = false;
        } else {
            errorText.removeClass('error-required');
        }
    });
    return isValid;
}

function validateNumber(container) {
    var isValid = true;
    var inputs = $('#' + container + ' .checkIsNumeric');
    if (inputs.length > 0) {
        inputs.each(function () {
            var parent = $(this).parents('.form-group').first();
            var errorDOM = parent.find('.error');

            if ($(this).val() != null &&
                $(this).val().length != 0 &&
                $(this).val().toString().trim() != "") {
                if (!$.isNumeric($(this).val())) {
                    errorDOM.addClass("error-number-format");
                    isValid = false;
                } else {
                    errorDOM.removeClass("error-number-format");
                }
            }
        })
    }
    return isValid;
}

function validateHTMLInjection(container) {
    var isValid = true;
    var pattern = /<[a-z][\s\S]*>/i;
    var inputs = $('#' + container + ' .checkHTMLInjection');
    if (inputs.length > 0) {
        inputs.each(function () {
            var parent = $(this).parents('.form-group').first();
            var errorDOM = parent.find('.error');

            if ($(this).val() != null &&
                $(this).val().length != 0 &&
                $(this).val().toString().trim() != "") {
                if (pattern.test($(this).val())) {
                    errorDOM.addClass('error-html-format');
                    isValid = false;
                }
                else {
                    errorDOM.removeClass('error-html-format');
                }
            }
        })
    }
    return isValid;
}

function validateSpecialCharacter(container) {
    var isValid = true;
    var pattern = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/;
    var inputs = $('#' + container + ' .checkSpecialCharacter');
    if (inputs.length > 0) {
        inputs.each(function () {
            var parent = $(this).parents('.form-group').first();
            var errorDOM = parent.find('.error');

            if ($(this).val() != null &&
                $(this).val().length != 0 &&
                $(this).val().toString().trim() != "") {
                if (pattern.test($(this).val())) {
                    errorDOM.addClass('error-special-character');
                    isValid = false;
                }
                else {
                    errorDOM.removeClass('error-special-character');
                }
            }
        })
    }
    return isValid;
}

function validateForm(formId) {
    var error = 0;
    var isValid = true;
    error += this.validateRequired(formId) ? 0 : 1;
    error += this.validateDate(formId) ? 0 : 1;
    error += this.validateSelectOption(formId) ? 0 : 1;
    error += this.validateTextArea(formId) ? 0 : 1;
    error += this.validateNumber(formId) ? 0 : 1;
    error += this.validateHTMLInjection(formId) ? 0 : 1;
    error += this.validateSpecialCharacter(formId) ? 0 : 1;

    $('#' + formId).find('.error').each(function () {
        var message = '';
        if ($(this).hasClass('error-required')) {
            message = 'Vui lòng nhập thông tin';
        } else if ($(this).hasClass('error-date-format')) {
            message = 'Vui lòng nhập theo định dạng "ngày/tháng/năm"';
        } else if ($(this).hasClass('error-number-format')) {
            message = 'Vui lòng đúng định dạng số';
        } else if ($(this).hasClass('error-html-format')) {
            message = 'Vui lòng không nhập ký tự HTML';
        } else if ($(this).hasClass('error-required-dropdown')) {
            message = 'Vui lòng chọn thông tin';
        } else if ($(this).hasClass('error-special-character')) {
            message = 'Vui lòng không nhập ký tự đặc biệt';
        }

        if (message !== '') {
            $(this).html(message);
            $(this).css('display', 'inline');
        } else {
            $(this).css('display', 'none');
        }
    });

    if (error > 0) {
        isValid = false;
    } else {
        isValid = true;
    }
    return isValid;
}

function RenderDropdownAction(name, listAction) {
    var result = '<div class="btn-group"><button data-toggle="dropdown" class="btn btn-xs btn-primary btn-white dropdown-toggle" aria-expanded="false">' + name + '<i class="ace-icon fa fa-angle-down icon-on-right"></i></button><ul class="dropdown-menu">';

    for (var ac of listAction) {
        if (ac != null) {
            var itemResult = "<li>";
            if (ac.clickAction != null) {
                itemResult += "<a href='javascript:void(0)' onclick='" + ac.clickAction + "'   title = '" + ac.title + "'><i class='" + ac.icon + "'> </i> " + ac.text + "</a>";
            } else {
                itemResult += "<a href='" + ac.linkAction + "' title = '" + ac.title + "'><i class='" + ac.icon + "'> </i> " + ac.text + "</a>";
            }
            itemResult += "</li>";
            result += itemResult;
        }
    }
    result += "</ul></div>";
    return result;
}

function renderDropdownList(urlRe, elementId, defaultMess) {
    console.log(urlRe);
    $.ajax({
        url: urlRe,
        type: 'Post',
        success: function (rs) {
            var content = "<option value=''>" + defaultMess + "</option>";
            if (rs != null) {
                for (var i = 0; i < rs.length; i++) {
                    content += "<option value='" + rs[i].Value + "'>" + rs[i].Text + "</option>";
                }
            }
            $("#" + elementId).html(content);
        },
        error: function (e) {
            NotiError("Lỗi xử lý");
        }
    });
};

function renderDropdownListPost(urlRe, data, elementId, defaultMess) {
    $.ajax({
        url: urlRe,
        data: data,
        type: 'Post',
        success: function (rs) {
            var content = "<option value=''>" + defaultMess + "</option>";
            if (rs != null) {
                for (var i = 0; i < rs.length; i++) {
                    content += "<option value='" + rs[i].Value + "'>" + rs[i].Text + "</option>";
                }
            }
            $("#" + elementId).html(content);
        },
        error: function (e) {
            NotiError("Lỗi xử lý");
        }
    });
};

function renderDropdownListSelect2(urlRe, elementId) {
    console.log(urlRe);
    $.ajax({
        url: urlRe,
        type: 'Post',
        success: function (rs) {
            var content = "";
            if (rs != null) {
                for (var i = 0; i < rs.length; i++) {
                    content += "<option value='" + rs[i].Value + "'>" + rs[i].Text + "</option>";
                }
            }
            $("#" + elementId).html(content).trigger('change');
        },
        error: function (e) {
            NotiError("Lỗi xử lý");
        }
    });
}

function NhanDauMoiactionToolBar(view) {
    var permissionCodeString = permissonCodeString(view);
    return LinkRoleFunc("GetCheckBox()", "<i class='fa fa - pencil'></i> Nhận xử lý", permissionCodeString + "_NhanXuLy", "btn btn-primary btn-xs", "", "Nhận xử lý");
}

function DauMoi(stickid, currentUserId, id, CurrentUserName, area, view) {
    var permissionCodeString = permissonCodeString(view);
    var DamoiLinkRoleJson = {
        "NhanXuLy": ["GetProcess(\"/" + area + "/" + view + "/GetProcess/" + id + "\")", "Nhận xử lý", permissionCodeString + "_NhanXuLy", "btn btn-primary btn-xs", "", "Nhận là đầu mối xử lý hồ sơ này"],
        "ChuyenDauMoiXuLy": ["CreateAction(\"/" + area + "/" + view + "/PhanDauMoiIndex?id=" + currentUserId + "&item=" + id + "\")", "Chuyển đầu mối xử lý", permissionCodeString + "_ChuyenDauMoiXuLy", "btn btn-info btn-xs", "", "Chuyển đầu mối xử lý hồ sơ này"],
        "YeuCauXuLy": ["CreateAction(\"/" + area + "/" + view + "/PhanDauMoiIndex?id=" + currentUserId + "&item=" + id + "\")", "Chuyển đầu mối xử lý", permissionCodeString + "_ChuyenDauMoiXuLy", "btn btn-info btn-xs", "", "Chuyển đầu mối xử lý hồ sơ này"],
    }
    var DauMoiLinkRoleFunc = {
        "NhanXuLy": LinkRoleFunc(DamoiLinkRoleJson.NhanXuLy[0], DamoiLinkRoleJson.NhanXuLy[1], DamoiLinkRoleJson.NhanXuLy[2], DamoiLinkRoleJson.NhanXuLy[3], DamoiLinkRoleJson.NhanXuLy[4], DamoiLinkRoleJson.NhanXuLy[5]),
        "ChuyenDauMoiXuLy": LinkRoleFunc(DamoiLinkRoleJson.ChuyenDauMoiXuLy[0], DamoiLinkRoleJson.ChuyenDauMoiXuLy[1], DamoiLinkRoleJson.ChuyenDauMoiXuLy[2], DamoiLinkRoleJson.ChuyenDauMoiXuLy[3], DamoiLinkRoleJson.ChuyenDauMoiXuLy[4], DamoiLinkRoleJson.ChuyenDauMoiXuLy[5]),
        "YeuCauXuLy": LinkRoleFunc(DamoiLinkRoleJson.YeuCauXuLy[0], DamoiLinkRoleJson.YeuCauXuLy[1], DamoiLinkRoleJson.YeuCauXuLy[2], DamoiLinkRoleJson.YeuCauXuLy[3], DamoiLinkRoleJson.YeuCauXuLy[4], DamoiLinkRoleJson.YeuCauXuLy[5]),
    }

    return (stickid == null || stickid == 0) ? DauMoiLinkRoleFunc.NhanXuLy + "<div class='clearBoth' style='margin-bottom:5px;'></div>" + DauMoiLinkRoleFunc.ChuyenDauMoiXuLy : CurrentUserName + "</br>" + DauMoiLinkRoleFunc.ChuyenDauMoiXuLy;
}

function addBroadcastNoti(mess) {
    $("#boxRunText").append("<span class='textItem'>" + mess + "</span>");
}

function showBroadcastNoti() {
    if ($("#boxRunText .textItem").length > 0) {
        var firstItemNoti = $("#boxRunText .textItem").first();
        firstItemNoti.addClass("active");
    }
}

//cookie
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
//endcookie

function BackButtonByParentUrl() {
    var ref = document.referrer;
    location.href = ref;
}

function generate_slugable(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    return str;
}
function LoaiThuongNhanToChuc(typeOrganization) {
    switch (typeOrganization) {
        case 'Company': return '<span class="label label-info">Tài khoản doanh nghiệp</span>'
        case 'Organization': return '<span class="label label-default">Tài khoản tổ chức</span>'
    }
}

function HighLightContent(content) {
    return '<span class="highlightContent"><i class="fa fa-exclamation"></i><b>' + content + '</b></span>';
}

function ShowMessageDateLine(message, date) {
    if (message != null && message != "") {
        return '<p style="color:red;">' + message + ' (' + date + ' ngày)</p>';
    } else {
        return "";
    }
}

function GetExtention(path) {
    return "." + path.split('.').pop();
}

function CheckFileUpload(IDInput, size, typeAllow) {
    var listAllow = typeAllow.split(",");
    var uploadField = $(IDInput);
    if (IDInput != undefined) {
        for (var i = 0; i < IDInput.files.length; i++) {
            if (IDInput.files[i].size > size) {
                NotiError("Dung lượng tệp đã vượt quá giới hạn cho phép");
                IDInput.value = "";
                return false;
            };
            var extent = GetExtention(IDInput.files[i].name).toLowerCase();
            if (listAllow.indexOf(extent) == -1) {
                NotiError("Vui lòng chọn file có định dạng " + typeAllow);
                IDInput.value = "";
                return false;
            }
        }
    }
}

function formatMoney(num, currency = "", splitter = ",") {
    if (num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.') + " " + currency;
    }
    return num;
}

function calculateDiscountPercent(originalPrice, salePrice) {
    if (!originalPrice || !salePrice || originalPrice <= 0) {
        return "";
    }

    const discount = ((originalPrice - salePrice) / originalPrice) * 100;
    return `-${Math.round(discount)}%`;
}

function formatNumber(num, currency = "", splitter = ",", IsInteger) {
    if (num) {
        if (IsInteger) {
            num = num.toString().split(splitter)[0];
        }
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, `$1${splitter}`) + " " + currency;
    }
    return num;
}

function ShowStatus(objStatus) {
    let color = "#ffffff";
    let bgcolor = "#ecf0f1";
    let icon = "<i class='fa fa-circle'></i>"
    if (objStatus) {
        if (objStatus.Color) {
            color = objStatus.Color;
        }
        if (objStatus.BgColor) {
            bgcolor = objStatus.BgColor;
        }
        if (objStatus.Icon) {
            icon = "<i class='" + objStatus.Icon + "'></i>";
        }
        if (objStatus.Name) {
            return `<div class="statusInTable" style="color:${color}; background-color: ${bgcolor}">${icon} <span>${objStatus.Name}</span></div>`;
        } else {
            return `<div></div>`;
        }
    }
}

function ShowStatusMau(objStatus) {
    let color = "#ffffff";
    let bgcolor = "#ecf0f1";
    let icon = "<i class='fa fa-circle'></i>"
    let name = ""
    if (objStatus) {
        if (objStatus == "00") {
            name = "Hoạt động tốt"
            color = "#3BAEA9"
        } else if (objStatus == "01") {
            name = "Hiệu chuẩn"
            color = "#973365"
        } else if (objStatus == "02") {
            name = "Lỗi thiết bị"
            color = "#D58303"
        } else if (objStatus == "03") {
            name = "Vượt quy chuẩn"
            color = "#EC505B"
        } else if (objStatus == "04") {
            name = "Mất kết nối"
            color = "#C3C3C3"
        }

        if (name) {
            return `<div class="statusInTable" style="color:${color}; background-color: ${bgcolor}">${icon} <span>${name}</span></div>`;
        } else {
            return `<div></div>`;
        }
    }
}

function ShowTextAndOffNull(objStatus) {
    if (objStatus != null) {
        return `<span>${objStatus}</span>`;
    } else {
        return ``;
    }
}

function ShowValueNotNull(data) {
    if (data) {
        return data;
    }
    return "";
}

var MenuMin = () => {
    if (!$('#sidebar').hasClass("menu-min")) {
        $('#sidebar-toggle-icon').click();
    }
}
function parseDate(dateString) {
    var parts = dateString.split('/');
    var day = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10) - 1; // Đánh số tháng từ 0 đến 11
    var year = parseInt(parts[2], 10);
    return new Date(year, month, day);
}

function convertToRoman(num) {
    const romanNumerals = [
        ['M', 1000],
        ['CM', 900],
        ['D', 500],
        ['CD', 400],
        ['C', 100],
        ['XC', 90],
        ['L', 50],
        ['XL', 40],
        ['X', 10],
        ['IX', 9],
        ['V', 5],
        ['IV', 4],
        ['I', 1]
    ];
    let result = '';
    for (const [roman, value] of romanNumerals) {
        while (num >= value) {
            result += roman;
            num -= value;
        }
    }
    return result;
}

function removeVietnameseTones(str) {
    const map = [
        'aàáạảãâầấậẩẫăằắặẳẵ',
        'eèéẹẻẽêềếệểễ',
        'iìíịỉĩ',
        'oòóọỏõôồốộổỗơờớợởỡ',
        'uùúụủũưừứựửữ',
        'yỳýỵỷỹ',
        'dđ',
        'AÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴ',
        'EÈÉẸẺẼÊỀẾỆỂỄ',
        'IÌÍỊỈĨ',
        'OÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ',
        'UÙÚỤỦŨƯỪỨỰỬỮ',
        'YỲÝỴỶỸ',
        'DĐ'
    ];

    for (let i = 0; i < map.length; i++) {
        const regex = new RegExp(`[${map[i].slice(1)}]`, 'g');
        str = str.replace(regex, map[i][0]);
    }
    str = str.replace(/\s+/g, ''); // Remove all whitespace
    return str.toUpperCase();
}

function handleExclusiveCheckbox(groupClass) {
    const checkboxes = document.querySelectorAll('.' + groupClass);
    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener('click', function () {
            checkboxes.forEach(function (cb) {
                if (cb !== checkbox) {
                    cb.checked = false;
                }
            });
        });
    });
}

//// BÁO CÁO
function ReloadViewData(url, id, classNameTable) {
    $.ajax({
        url: url,
        type: 'post',
        data: { id: id },
        success: function (rs) {
            $(`.${classNameTable}`).html(rs);
        },
        error: function () {
        }
    })
}

function PageReload() {
    /*console.log("PageReload")*/
}