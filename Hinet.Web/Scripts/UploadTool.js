function addNewFile(idTool, type) {
    var countItem = $("#" + idTool + " tbody tr[data-type=rowItem]").length;
    var strNew = '<tr data-type="rowItem">';
    strNew += '<td data-type="stt">' + (countItem + 1) + '</td>';
    if ($('#loai_' + type).length) {
        strNew += "<td>" + $('#loai_' + type).parent().html() + "</td>";
    }
    strNew += '<td><input type="text" class="form-control" name="name_' + type + '" data-type="intFilename" placeholder="Tên tài liệu" /></td>';
    strNew += '<td><input type="file" class="form-control" name="' + type + '" data-type="inpFile" /></td>';
    strNew += '<td class="center"><a href="javascript:void(0)" onclick="RemoveItem(this,\'' + idTool + '\',0)" style="vertical-align:middle"><i class="fa fa-trash" style = "color:red" aria-hidden="true"></i> </a></td>';
    strNew += '</tr>';
    $("#" + idTool + " tbody").append(strNew);
}

//Tính lại số thứ tự cho các dòng
function calStt(idTool) {
    var stt = 1;
    $("#" + idTool + " tbody tr[data-type=rowItem]").each(function () {
        $(this).find("td[data-type=stt]").html(stt++);
    });
}

function RemoveItem(element, idTool, id) {
    var obj = $(element);
    if (id > 0) {
        $.confirm({
            title: 'Xác nhận xóa',
            content: 'Bạn muốn xóa đối tượng này?',
            buttons: {
                cancel: {
                    text: "Đóng",
                    action: function () { }	// Nothing to do in this case. You can as well omit the action property.
                },
                confirm: {
                    btnClass: 'btn-danger',
                    text: "Xác nhận",
                    action: function () {
                        $.ajax({
                            url: '/Common/DeleteFile',
                            type: 'post',
                            cache: false,
                            data: {
                                id: id
                            },
                            success: function (data) {
                                var parentObj = obj.parents("tr[data-type=rowItem]");
                                parentObj.remove();
                                calStt(idTool);
                            },
                            error: function (xhr) {
                                NotiError("Xóa thất bại");
                            }
                        });
                    }
                }
            }
        });
    } else {
        var parentObj = obj.parents("tr[data-type=rowItem]");
        parentObj.remove();
        calStt(idTool);
    }
}