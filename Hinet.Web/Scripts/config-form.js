$(".select2").select2({});
$(".HuyenFilter").change(function () {
    var mahuyen = $(this).val();
    AjaxCall("/Home/getdropdownXa", "POST", { MaHuyen: mahuyen },
        (rs) => {
            var str = '<option value="">--Chọn Xã--</option>';
            rs.map(x => {
                str += `<option value=${x.Value}>${x.Text}</option>`
            })

            var eleXa = $(this).closest('.SearchBox').find('.XaFilter');
            //console.log(eleXa);
            eleXa.val('').trigger('change');
            eleXa.html("");
            eleXa.html(str);
        }
    )
});

$(".XaFilter").change(function () {
    var maXa = $(this).val();
});