    $(document).on("change", ".drop_box input[type = 'file']", function (e) {
        var fileName = e.target.files[0].name;
        let dropbox = $(this).closest(".drop_box");
        let fileInput = $(this);
        let fileExtension = $(this).data("fileextension");
        let name = $(this).data("name");
        fileFormData = dropbox.html();
        let filedata = `<div class="form">
                            <span>${fileName}</span>
                            <button type="button" class="btn btn-xs btn-danger btn-upload-delete"
                            data-Name="${name}"
                            data-FileExtension="${fileExtension}"><i class="fa fa-trash-o"></i> Xoá file</button>
                        </div>`;
        dropbox.html(filedata);
        dropbox.append(fileInput);
        // Flag delete    
        $('#isDeleted_' + name).val('false');
    });

    $(document).on("click", ".drop_box .btn-upload-delete", function () {
        let fileExtension = $(this).data("fileextension");
        let name = $(this).data("name");
        let filedata = `<label class="control-label">Tài liệu đính kèm </label>
                            <div>Hỗ trợ file: ${fileExtension}</div>
                            <input type="file" hidden accept="${fileExtension}" id="${name}" name="${name}" style="display:none;"
                                    data-Name="${name}"
                                    data-FileExtension="${fileExtension}">
                        <label for="${name}" class="btn btn-xs btn-success btn-upload"><i class="fa fa-upload"></i> Chọn tệp</label>`
        let dropbox = $(this).closest(".drop_box");
        dropbox.html(filedata);
        dropbox.find("input[type='file']").val('');
        // Flag delete    
        var elment = $('#isDeleted_' + name);
        $('#isDeleted_' + name).val('true');
    });
