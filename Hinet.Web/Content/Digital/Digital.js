
var centerView = { lat: 19.93762071039967, lng: 105.30275209871071 };
var TypeAll = "";
var ValSearch = ""
var PageIndex = 1;
var loadingData = false;
var isLazyLoad = true;
var countItem;
var boudingbox;
var HuyenId = "";
var XaId = "";
var objType = {
    //QLAnten: "QLAnten",
    QLTramBTS: "QLTramBTS",
    QLLoaPhatThanh: "QLLoaPhatThanh",
    QLBuuCuc: "QLBuuCuc",
    InternetVaTroChoi: "InternetVaTroChoi",
    DichVuVienThong: "DichVuVienThong",
    TuyenDuongThu: "TuyenDuongThu",
    TuyenViba: "TuyenViba",
    QuyHoachTramBTS: "QuyHoachTramBTS",
    HaTangTuyenHinh: "HaTangTuyenHinh",
    TramVienThong: "TramVienThong",
}
var urlIcon = {
    Anten: '/Uploads/IconMarker/markerAnten.png',
    BTS: '/Uploads/IconMarker/markerTramBTS2.png',
    LoaPhatThanh: '/Uploads/IconMarker/markerLoaPhatThanh.png',
    BuuCuc: '/Uploads/IconMarker/markerBuuCuc.png',
    DichVuVienThong: '/Uploads/IconMarker/markerDvvt.png',
    InternetVaTroChoi: '/Uploads/IconMarker/markerTroChoi.png',
    TuyenDuongThu: '/Uploads/IconMarker/markerThu.png',
    Viba: '/Uploads/IconMarker/markerViba.png',
    QuyHoachTramBTS: '/Uploads/IconMarker/markerTramBTS.png',
    HaTangTuyenHinh: '/Uploads/IconMarker/markerHtth.png',
    TramVienThong: '/Uploads/IconMarker/markerTramVienThong.png'

}

var autoComplete;
var idSearch = "input-search";

var currentInfo;
var ListMarker = [];
var ListMarker_QuyHoachTramBTS = [];
var lCircle = [];
var lCircle_QuyHoach = [];
var directionsResults_Mail = [];
var directionsResults_Viba = [];
var totalNumber = 0;
var markersThuTu = [];
var names = [];
var AdvancedMarkerElement1;
var markerCluster;
var markerCluster_QuyHoachTramBTS;
var lstTech = [];
var oldHuyenCenter;
var lstVungCam = [];
var lstBTS = [];
var oldMarkerBTS;
var oldBtsCirCle;
var dataBTS = [];
var directionsResults_MailItem = [];
var country_code = "vn";
var zoomlv = 9.5;
var map;
var wmsLayer;
var wmsLayer_DanCu;
var huyenLayer;
var indexOfwmsLayer;
var indexOfwmsLayer_DanCu;
var markers = [];
var desPoint;
var desPointName;
var currentPoint;
var currentPointName;
var oldDirect;
var oldDirect_vibaOption;
var oldDirection_viba;
var oldMarkerFocus;

var StylesLayerMap = [
    {
        featureType: 'poi',
        stylers: [{ visibility: 'off' }] // Hide points of interest
    },
    {
        featureType: "administrative.country",
        elementType: "geometry",
        stylers: [{ visibility: "off" }],
    },
    {
        featureType: "administrative.country",
        elementType: "labels",
        stylers: [{ visibility: "off" }],
        featureType: "administrative",
        elementType: "labels",
        stylers: [{ visibility: "off" }],
    },
    {
        featureType: "administrative",
        elementType: "geometry",
        stylers: [{ visibility: "off" }],
    },
    {
        featureType: "landscape",
        elementType: "geometry",
        stylers: [{ visibility: "off" }],
    },
];


//var bboxDanCu;
//let wsmDanCu = "http://123.31.20.197:8080/geoserver/gisthanhhoa/wms?service=WMS&version=1.1.0&request=GetMap&layers=gisthanhhoa%3ADanCu36&bbox=104.81298828125%2C19.297449111938477%2C106.08024597167969%2C20.641963958740234&width=723&height=768&srs=EPSG%3A4326&styles=&format=application/openlayers"


// Hàm bật tắt lớp bản đồ
function changeMapType(mapType) {
    if (mapType != "nen" && mapType != "dancu") {
        map.setMapTypeId(mapType);
        sessionStorage.setItem('mapType', mapType);
    }
    else {
        if (mapType == "nen") {
            if (indexOfwmsLayer != null && indexOfwmsLayer != undefined && indexOfwmsLayer >= 0) {
                map.overlayMapTypes.removeAt(indexOfwmsLayer);
            } else {
                map.overlayMapTypes.push(wmsLayer);
                if (indexOfwmsLayer_DanCu == 0) {

                    var temp = map.overlayMapTypes.getAt(0); // Lưu tạm thời lớp overlay tại index1
                    map.overlayMapTypes.setAt(0, map.overlayMapTypes.getAt(1)); // Đặt lớp overlay tại index2 vào vị trí index1
                    map.overlayMapTypes.setAt(1, temp);
                }
            }
        } else {
            if (indexOfwmsLayer_DanCu != null && indexOfwmsLayer_DanCu != undefined && indexOfwmsLayer_DanCu >= 0) {
                map.overlayMapTypes.removeAt(indexOfwmsLayer_DanCu);
            } else {
                map.overlayMapTypes.push(wmsLayer_DanCu);
            }
        }

        indexOfwmsLayer = map.overlayMapTypes.indexOf(wmsLayer);
        indexOfwmsLayer_DanCu = map.overlayMapTypes.indexOf(wmsLayer_DanCu);
    }
}


//Vẽ khi click vào 1 loại phương tiện
function SetOptionDrive(target) {
    $(".active_drive").removeClass("active_drive");
    $(target).addClass("active_drive");
    typeDrive = $(target).data("drive");

    if (!currentPoint || !desPoint) return;
    drawDirect(currentPoint, desPoint, typeDrive);
}


// Hàm vẽ đường đi
function drawDirect(origin, destination, TypeTravel) {
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer({
        preserveViewport: true
    });
    directionsRenderer.setMap(map);

    if (oldDirect != null && oldDirect != undefined) {
        oldDirect.setMap(null);
    }

    var request = {
        origin: origin,
        destination: destination,
        travelMode: TypeTravel,
    };

    directionsService.route(request, function (response, status) {
        if (status === 'OK') {
            directionsRenderer.setDirections(response);
        } else {
            window.alert('Không thể tìm thấy đường đi theo phương thức di chuyển này!');
        }
    });

    oldDirect = directionsRenderer;

}

$("#input-search").change(function () {
    $('.detailObjClick').hide();

    PageIndex = 1;
    ValSearch = $(this).val();

    $('.box-bando').removeClass('left-bando');
    $('.lopBanDo').removeClass('left-bando2');

    $('.box-bando').addClass('left-bando');
    $('.lopBanDo').addClass('left-bando2');

    if ($('#Grid_resultSearch').is(':hidden')) {
        $(".loader").show();

        getdata((rs, arr) => {
            if (TypeAll === objType.TuyenDuongThu) {
                getDirectionForMail(false, arr);
            } else
                if (TypeAll === objType.TuyenViba) {
                    GetDirection_Viba(false, arr);
                } else
                    Getmarker(false, arr);

            $(".loader").hide();

            $(".Grid_resultSearch").show();
            $(".map-search").addClass("background_resultSearch");
            $(".map-search").removeClass("hidden-content");
            $(".time-search").show();
            $(".content_ketqua").html(rs);

        })
    } else {
        getdata((rs, arr) => {
            if (TypeAll === objType.TuyenDuongThu) {
                getDirectionForMail(false, arr);
            } else
                if (TypeAll === objType.TuyenViba) {
                    GetDirection_Viba(false, arr);
                } else
                    Getmarker(false, arr);

            $(".content_ketqua").html(rs);

            $("#numberRow").html(`        <span class="color_text">
                ${arr.length} kết quả được tìm thấy </span>`)

        })
    }

})


$('#searchAll').click(function () {
    getAll();
    activeSeachAll();
})


function activeSeachAll() {
    $('#searchAll').attr('style', 'background-color: #06cadd;');
    $(".avtiveBox").removeClass("avtiveBox");
    $(".time-search").click();
    // ẩn các nút tính năng
    $('.btn-twoColumns').hide();
    $('.btn-synthetic').hide();
    $('.btn-review').hide();

    $(".map-search").removeClass("hidden-content");
    if (typeof map !== 'undefined' && map !== null) {
        map.setCenter(centerView);
        map.setZoom(9);
    }
}

// Hàm vẽ tất cả đối tượng lên bản đồ
function getAll() {
    // Lấy dữ liệu rồi vẽ tất cả đối tượng lên bản đồ
    $.ajax({
        url: '/QLBanDoArea/QLBanDo/GetListResultSearchAll',
        type: 'GET',
        data: null,
        success: (rs) => {
            Getmarker(false, rs);
            GetDirection_Viba(false, rs);
            getDirectionForMail(false, rs);
        },
        error: null
    })
}
function getdata(callback) {
    // Lấy giá trị các bộ lọc
    HuyenId = $('#Huyen').val() != null && $('#Huyen').val() != undefined ? $('#Huyen').val() : "";
    XaId = $('#Xa').val() != null && $('#Xa').val() != undefined ? $('#Xa').val() : "";
    NhaMangId = $('#NhaMang').val() != null && $('#NhaMang').val() != undefined ? $('#NhaMang').val() : "";

    let value2G = $('#chk-2G').val();
    let value3G = $('#chk-3G').val();
    let value4G = $('#chk-4G').val();
    let value5G = $('#chk-5G').val();

    if (!$("#chk-2G").prop("checked")) {
        value2G = "";
    };
    if (!$("#chk-3G").prop("checked")) {
        value3G = "";
    };
    if (!$("#chk-4G").prop("checked")) {
        value4G = "";
    };
    if (!$("#chk-5G").prop("checked")) {
        value5G = "";
    };

    if (TypeAll !== objType.QLTramBTS) {
        value2G = ""; value3G = ""; value4G = ""; value5G = "";
    }

    $.ajax({
        url: `/QLBanDoArea/QLBanDo/GetListResultSearch?valSearch=${ValSearch}&type=${TypeAll}&PageIndex=${PageIndex}&HuyenId=${HuyenId}&XaId=${XaId}&value2G=${value2G}&value3G=${value3G}&value4G=${value4G}&value5G=${value5G}&NhaMangId=${NhaMangId}`,
        type: 'POST',
        data: null,
        success: (rs) => {
            genResultSearch(rs, callback);
        },
        error: null
    })
}

function syntheticBTS(callback) {

    if (!boundsBTS) return;

    const syned = dataBTS.filter(item => {
        let lat = (item.Lat != null && item.Lat != undefined) ? parseFloat(item.Lat.replace(',', '.')) : null;
        let lng = (item.Lng != null && item.Lng != undefined) ? parseFloat(item.Lng.replace(',', '.')) : null;

        if (!lat || !lng) return false;
        const point = new google.maps.LatLng(lat, lng);
        if (pointInsideBound(point, boundsBTS)) return true;
    })

    genResultSearch(syned, callback);

    isLazyLoad = false;
}

function getDataQuyHoach_Anten(callback) {
    const typeQuyHoach = objType.QuyHoachTramBTS;
    $.ajax({
        url: `/QLBanDoArea/QLBanDo/GetListResultSearch?type=${typeQuyHoach}`,
        type: 'POST',
        data: null,
        success: function (rs) {
            callback(rs);
        }
    })
}

// hàm sử dụng khi bấm nút xem tất cả trong thanh tìm kiếm
function getdataAll(callback) {

    // Lấy giá trị các bộ lọc
    HuyenId = $('#Huyen').val() != null && $('#Huyen').val() != undefined ? $('#Huyen').val() : "";
    XaId = $('#Xa').val() != null && $('#Xa').val() != undefined ? $('#Xa').val() : "";
    NhaMangId = $('#NhaMang').val() != null && $('#NhaMang').val() != undefined ? $('#NhaMang').val() : "";

    let value2G = $('#chk-2G').val();
    let value3G = $('#chk-3G').val();
    let value4G = $('#chk-4G').val();
    let value5G = $('#chk-5G').val();

    if (!$("#chk-2G").prop("checked")) {
        value2G = "";
    };
    if (!$("#chk-3G").prop("checked")) {
        value3G = "";
    };
    if (!$("#chk-4G").prop("checked")) {
        value4G = "";
    };
    if (!$("#chk-5G").prop("checked")) {
        value5G = "";
    };

    if (TypeAll !== objType.QLTramBTS) {
        value2G = ""; value3G = ""; value4G = ""; value5G = "";
    }

    // lấy dữ liệu
    $.ajax({
        url: `/QLBanDoArea/QLBanDo/GetListResultSearch2?valSearch=${ValSearch}&type=${TypeAll}&PageIndex=${PageIndex}&HuyenId=${HuyenId}&XaId=${XaId}&value2G=${value2G}&value3G=${value3G}&value4G=${value4G}&value5G=${value5G}&NhaMangId=${NhaMangId}`,
        type: 'POST',
        data: null,
        success: (rs) => {
            genResultSearch(rs, callback);
        },
        error: null
    })
}

// Hàm gen danh sách
function genResultSearch(rs, callback) {
    var htmlStr = ``;
    if (!rs) return;

    rs.map((x, index) => {
        var detail = x.detailInfor != undefined ? JSON.parse(x.detailInfor) : undefined;

        let latStart = null;
        let lngStart = null;
        let latEnd = null;
        let lngEnd = null;

        if (x.LatStart && x.LngStart && x.LatEnd && x.LngEnd) {
            latStart = x.LatStart.replace(",", ".") * 1.0;
            lngStart = x.LngStart.replace(",", ".") * 1.0;
            latEnd = x.LatEnd.replace(",", ".") * 1.0;
            lngEnd = x.LngEnd.replace(",", ".") * 1.0;
        }
        //Ẩn tuyến con
        if (x.Type === objType.TuyenDuongThu) {
            x.AnhDaiDien = "tuyenduongthu.jpg";
            if (detail.ParentId != null) {
                htmlStr += `<div style="display: none;">`
            }
        }
        htmlStr += `
            <div onclick="openChiTiet('${x.Type}','${x.ItemId}','${x.Lat}','${x.Lng}','${x.TenDoiTuong}', 
            ${latStart}, ${lngStart}, ${latEnd}, ${lngEnd})" class="box_result selectItem">
            <div class="show-infor">
            <p class="title-heading">${x.TenDoiTuong}</p>
        <p class="subItem">
            <span class='italic-text'>Địa chỉ: ${x.DiaChi != null ? `<span>${x.DiaChi}</span>` : `<span class="notValue">Chưa có thông tin</span>`}</span>
        </p>`;

        switch (x.Type) {
            case objType.TuyenDuongThu:
                // Thêm thông tin tuyến đường thư
                if (detail.ParentId == null) {
                    htmlStr += `<p class="subItem">
                        <span class='italic-text'>Tuyến đường: ${x.Diachi != null ? `<span>${x.Diachi}</span>` : `<span class="notValue">Chưa có thông tin</span>`}</span>
                </p>`
                };
                break;
            case objType.QLLoaPhatThanh:
                // Handle the second slide
                if (x.AnhDaiDien == null || x.AnhDaiDien == "") {
                    x.AnhDaiDien = "loaphatthanh.jpg";
                }
                htmlStr += `
                     <div class="subinfor">
                         <div>
                             <span class="subItem">
                              Phương thức lắp đặt :
                                ${detail != null || detail != undefined ? detail.PhuongThucLapDat : `<p class='notValue'> Chưa có thông tin</p>`}
                             </span>
                              <p class="subItem">
                                    Công suất :
                                    ${detail != null && detail != undefined ? detail.CongSuat : '<span class="notValue">Chưa có thông tin </span>'}

                            </p>
                              <p class="subItem">
                                    Tổng số cụm loa :
                                    ${detail != null && detail != undefined ? detail.TongSoCumLoa : '<span class="notValue">Chưa có thông tin </span>'}

                                </p>
                         </div>
                     </div>
                `
                break;
            case objType.QLBuuCuc:
                if (x.AnhDaiDien == null || x.AnhDaiDien == "") {
                    x.AnhDaiDien = "avtBuuCuc.jpg"
                }
                htmlStr += `
                     <div class="subinfor">
                         <div>
                            <p class="subItem">
                              Giờ phục vụ:
                                ${detail != null && detail != undefined ? ToTime2(detail.GioPhucVu) : '<span class="notValue">Chưa có thông tin </span>'}
                             </p>
                             <p class="subItem">
                              Loại hình phục vụ:
                                ${detail != null && detail != undefined ? detail.LoaiHinhPhucVuName : '<span class="notValue">Chưa có thông tin </span>'}
                             </p>
                              <p class="subItem">
                                    Các loại dịch vụ cung cấp:
                                    ${detail != null && detail != undefined ? detail.CacLoaiDichVuCungCapName : '<span class="notValue">Chưa có thông tin </span>'}
                            </p>
                             <p class="subItem">
                             Tình trạng hoạt động:
                                ${getHtmlTinhTrangs(detail.TinhTrangHoatDong)}
                            </p>
                         </div>
                     </div>
                `
                break;
            case objType.QLTramBTS:
                if (x.AnhDaiDien == null || x.AnhDaiDien == "") {
                    x.AnhDaiDien = "avtTramBTS.jpeg";
                }
                htmlStr += `
                     <div class="subinfor">
                         <div>
                         
                             <p class="subItem">
                              Công nghệ phát sóng:
                                ${detail != null && detail != undefined ? detail.CongNghePhatSong : '<span class="notValue">Chưa có thông tin </span>'}
                             </p>
                              <p class="subItem">
                                    Bán kính phát sóng:
                                    ${detail != null && detail != undefined ? detail.BanKinhPhatSong : '<span class="notValue">Chưa có thông tin </span>'}
                                    ${detail != null && detail != undefined ? detail.DonViTinhPhuSong : null}
                            </p>
                            <p class="subItem">
                             Tình trạng hoạt động:
                                ${getHtmlTinhTrangs(detail.TrangThaiHoatDong)}
                            </p>
                         </div>
                     </div>
                `
                break;
            case objType.TuyenViba:
                x.AnhDaiDien = "tuyenviba.jpg"
                htmlStr += `
                         <div class="subinfor">
                             <div>
                                 <p class="subItem">
                                  Chiều dài tuyến:
                                    ${detail != null && detail != undefined ? detail.ChieuDaiTuyen : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                             </div>
                         </div>`;
                break;
            case objType.DichVuVienThong:
                x.AnhDaiDien = "avtDvvt.jpg"
                htmlStr += `
                         <div class="subinfor">
                             <div>
                                 <p class="subItem">
                                  Đơn vị quản lý:
                                    ${detail != null && detail != undefined ? detail.TenDonViQuanLy : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                             </div>
                         </div>`;
                break;
            case objType.InternetVaTroChoi:
                x.AnhDaiDien = "avtInternetVaTroChoi.png"
                htmlStr += `
                         <div class="subinfor">
                             <div>
                                 
                             </div>
                         </div>`;
                break;
            case objType.HaTangTuyenHinh:
                x.AnhDaiDien = "avtHtth.png"
                htmlStr += `
                         <div class="subinfor">
                             <div>
                                 <p class="subItem">
                                  Đơn vị quản lý:
                                    ${detail != null && detail != undefined ? detail.TenDonViQuanLy : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                                 <p class="subItem">
                                  Độ cao Anten:
                                    ${detail != null && detail != undefined ? detail.DoCaoAngten : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                                 <p class="subItem">
                                  Số lượng máy phát:
                                    ${detail != null && detail != undefined ? detail.SoLuongMayPhat : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                                 <p class="subItem">
                                  Công suất máy phát:
                                    ${detail != null && detail != undefined ? detail.CongSuatMayPhat : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                                 <p class="subItem">
                                  Tần số:
                                    ${detail != null && detail != undefined ? detail.TanSo : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                             </div>
                         </div>`;
                break;
            case objType.TramVienThong:
                x.AnhDaiDien = "avtTramVienThong.jpg"
                htmlStr += `
                         <div class="subinfor">
                             <div>
                                 <p class="subItem">
                                  Đơn vị sở hữu:
                                    ${detail != null && detail != undefined ? detail.TenDonViSoHuu : '<span class="notValue">Chưa có thông tin </span>'}
                                 </p>
                             </div>
                         </div>`;
                break;
            default:
                // Handle other slides
                break;
        }

        htmlStr +=
            `</div >
                <div class="">
                    <div class="AnhDaiDien">
                        <img src="/Uploads/Avatars/${x.AnhDaiDien}" alt="Ảnh đại diện" />
                    </div>
                </div>
            `
        htmlStr += `</div > `


    })

    callback(htmlStr, rs);
    isLazyLoad = true;
    loadingData = false;

}


// Hàm xử lí khi click vào một nhãn tìm kiếm 
function ClickTypeSearch(target) {

    $('.time-search').click();

    $(".avtiveBox").removeClass("avtiveBox");
    $(target).addClass("avtiveBox");

    $('.box-bando').addClass('left-bando');
    $('.lopBanDo').addClass('left-bando2');

    $(".loader").show();

    $('#searchAll').attr('style', 'background-color:  none;');

    TypeAll = $(target).find("a").data("type");

    TypeName = $(target).find("a").data("name");


    getdata((rs, arr) => {
        if (TypeAll === objType.TuyenDuongThu) {
            getDirectionForMail(false, arr);
        } else
            if (TypeAll === objType.TuyenViba) {
                GetDirection_Viba(false, arr);
            } else
                Getmarker(false, arr);

        $(".loader").hide();

        $(".Grid_resultSearch").show();
        scrollTopContentKetQua();
        //$('#Grid_resultSearch').scrollTop(0);

        $(".map-search").addClass("background_resultSearch");
        $(".map-search").removeClass("hidden-content");

        $(".time-search").show();
        $(".content_ketqua").html(rs);

        totalNumber = arr.length;

        if (TypeAll === objType.QLTramBTS) {
            dataBTS = arr;
        }

        if (TypeAll == objType.TuyenDuongThu) {
            let count = 0;
            arr.forEach((item) => {
                if (item.ParentId == 0) {
                    count++;
                }
            })
            $("#numberRow").html(`        <span class="color_text">
                ${count} ${TypeName} được tìm thấy </span> `)
        } else {
            $("#numberRow").html(`        <span class="color_text">
                ${arr.length} ${TypeName} được tìm thấy </span> `)
        }

    })

}

function closeChiTiet() {
    $(".chitietObj").hide();
    $('.box-bando').show();
}

function findMarker(point, listMarker) {
    let index = null;
    if (listMarker != null && listMarker != undefined && listMarker.length > 0) {
        for (var i = 0; i < listMarker.length; i++) {
            if (listMarker[i].getPosition().lat() == point.lat && listMarker[i].getPosition().lng() == point.lng) {
                index = i;
            }
        }
    }
    return index;
}

function findViba(latStart, lngStart, latEnd, lngEnd, listDirect) {
    let index = null;
    if (!latStart || !latEnd || !lngStart || !lngEnd) return null;
    if (!listDirect) return null;
    for (var i = 0; i < listDirect.length; i++) {
        const latStart1 = listDirect[i].getDirections().request.origin.location.lat();
        const lngStart1 = listDirect[i].getDirections().request.origin.location.lng();
        const latEnd1 = listDirect[i].getDirections().request.destination.location.lat();
        const lngEnd1 = listDirect[i].getDirections().request.destination.location.lng();

        if (latStart1 == latStart && lngStart1 == lngStart && latEnd1 == latEnd && lngEnd1 == lngEnd) {
            return i;
        }
    }

    return index;
}

function openChiTiet(typeItem, ItemID, Lat, Lng, TenDoiTuong, latStart, lngStart, latEnd, lngEnd) {
    let centerPoint = {
        lat: Lat.replace(',', '.') * 1.0,
        lng: Lng.replace(',', '.') * 1.0
    }

    AjaxCall('/QLBanDoArea/QLBanDo/DetailShowSlide', 'GET', { ItemID, typeItem }, (rs) => {
        $(".chitietObj").html(rs);
        map.setCenter(centerPoint);
        map.setZoom(13);
    });
    desPoint = { lat: Lat, lng: Lng };
    desPointName = TenDoiTuong;

    $(".chitietObj").show();
    $('.box-bando').hide();

    $(document).on('click', '.selectItem', function () {
        $('.selectItem').removeClass('selectedItem');
        $(this).addClass('selectedItem');

    })
    // focus marker
    const index = findMarker(centerPoint, ListMarker);
    if (index != null && index >= 0) {
        if (oldMarkerFocus != null && oldMarkerFocus != undefined) {
            oldMarkerFocus.icon.scaledSize = new google.maps.Size(40, 40);
            oldMarkerFocus.icon.size = new google.maps.Size(40, 40);
            oldMarkerFocus.setZIndex(100);
        }

        ListMarker[index].icon.scaledSize = new google.maps.Size(55, 55);
        ListMarker[index].icon.size = new google.maps.Size(55, 55);
        ListMarker[index].setZIndex(120);

        oldMarkerFocus = ListMarker[index];
    }

    // focus viba

    const index_viba = findViba(latStart, lngStart, latEnd, lngEnd, directionsResults_Viba);
    if (index_viba != null && index_viba >= 0) {
        let route = directionsResults_Viba[index_viba].getDirections().routes[0];
        let bounds = new google.maps.LatLngBounds();
        route.overview_path.forEach(function (path) {
            bounds.extend(path);
        });
        google.maps.event.addListenerOnce(map, 'idle', function () {
            map.fitBounds(bounds);
            map.setZoom(map.getZoom() - 0.75);

        });

        // trả trạng thái của direction cũ về ban đầu
        if (oldDirect_vibaOption) {
            oldDirection_viba.setOptions({
                polylineOptions: oldDirect_vibaOption
            })
            oldDirection_viba.setDirections(oldDirection_viba.getDirections());
        }

        oldDirect_vibaOption = directionsResults_Viba[index_viba].polylineOptions;

        // trạng thái mới
        directionsResults_Viba[index_viba].setOptions({
            polylineOptions: {
                strokeColor: 'blue',
                strokeOpacity: 1.0,
                strokeWeight: 5
            }
        });

        directionsResults_Viba[index_viba].setDirections(directionsResults_Viba[index_viba].getDirections());

        oldDirection_viba = directionsResults_Viba[index_viba];

    }
}

$(".time-search").click(function () {

    // Xóa các đối tượng trên bản đồ
    Getmarker(false, [])
    GetDirection_Viba(false, []);
    getDirectionForMail(false, []);

    $('.close-feature').click();

    $(".avtiveBox").removeClass("avtiveBox");
    $("#input-search").val("");
    PageIndex = 1;
    TypeAll = "";
    ValSearch = "";
    $(".Grid_resultSearch").hide();
    $(".map-search").removeClass("background_resultSearch");
    $(".map-search").addClass("hidden-content");
    $(this).hide();
    closeChiTiet();

    $('.box-bando').removeClass('left-bando');
    $('.lopBanDo').removeClass('left-bando2');

    $('#Huyen').val(null);
    $('#Xa').val(null);
    $('#NhaMang').val(null);
    $('#select2-Huyen-container').text('Tất cả huyện');
    $('#select2-Xa-container').text('Tất cả xã');
    $('#select2-NhaMang-container').text('Tất cả nhà mạng');
    $('#chk-2G').prop('checked', true);
    $('#chk-3G').prop('checked', true);
    $('#chk-4G').prop('checked', true);
    $('#chk-5G').prop('checked', true);
    $('#NhaMang').next('.select2').hide();
    $('.chkTech').hide();

    $('.detailObjClick').hide();
});

///Hàm xử lý các lớp bản đồ

function getDataResident(lat, lng, zoomlevel) {
    $.ajax({
        type: "POST",
        url: "/QLBanDoArea/QLBanDo/getResidential",
        data: { lat: lat, lng: lng, zoomLevel: zoomlevel },
        success: function (response) {
            $("#MasterModal").html(response);
            $("#MasterModal").modal("show");
        },
        error: function (error) {
            NotiError(error);
        }
    });
}
function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: centerView,
        zoom: 9.5,
        mapId: "4504f8b37365c3d0",
        mapTypeControl: false,
        styles: StylesLayerMap,
        minZoom: 5,
        maxZoom: 20,
    });

    if (sessionStorage.getItem('mapType') !== null) {
        map.setMapTypeId(sessionStorage.getItem('mapType'));
    }


    //Tạo lớp nền
    wmsLayer = getWMSLayer(map, WMS_UrlMap.layers_BaseMap, '', 0.8);

    //Tạo lớp dân cư
    //wmsLayer_DanCu = getWMSLayer(map, WMS_UrlMap.layers_DanCu, '', 0.8);

    map.overlayMapTypes.push(wmsLayer);
    //map.overlayMapTypes.push(wmsLayer_DanCu);

    indexOfwmsLayer = map.overlayMapTypes.indexOf(wmsLayer);
    //indexOfwmsLayer_DanCu = map.overlayMapTypes.indexOf(wmsLayer_DanCu);

    // thêm element vào bản đồ
    var controlDiv = document.getElementById("alert-feature");
    map.controls[google.maps.ControlPosition.BOTTOM_CENTER].push(controlDiv);

    //Lắng nghe sự kiện khi click vào map
    map.addListener('click', function (event) {

        const lat = event.latLng.lat().toFixed(7); // Lấy vĩ độ của điểm click
        const lng = event.latLng.lng().toFixed(7); // Lấy kinh độ của điểm click
        const zoomLevel = map.getZoom(); // Lấy mức độ zoom hiện tại của bản đồ

        //if (indexOfwmsLayer_DanCu >= 0 && indexOfwmsLayer_DanCu != null && indexOfwmsLayer_DanCu != undefined) {
        //    getDataResident(lat, lng, zoomLevel);

        //}


        // đo khoảng cách giữa hai điểm
        if (twoPointsDistance) {
            const location = { lat: event.latLng.lat(), lng: event.latLng.lng() };
            paths.push(location);
            addMarker(location);
            drawPolyline(paths);

            if (paths != null && paths != undefined && paths.length > 1) {
                var distance = haversineDistance(paths[paths.length - 2], paths[paths.length - 1]);
                const point1 = paths[paths.length - 2];
                const point2 = paths[paths.length - 1];
                const positionAvg = { lat: (point1.lat + point2.lat) / 2, lng: (point1.lng + point2.lng) / 2 };
                addTextMarker(positionAvg, distance);
            }
        } else
            // Đề xuất BTS
            if (reviewBTS) {
                const location = { lat: event.latLng.lat(), lng: event.latLng.lng() };
                let radius = $('.BTS_Radius').val();

                if (radius) {
                    radius = radius * 1000;
                }
                // vẽ marker của bts
                const markerBTS = new google.maps.Marker({
                    map,
                    position: location,
                    title: "Trạm BTS mới",
                    animation: google.maps.Animation.DROP,
                });

                // xóa marker trước nếu có
                if (oldMarkerBTS) {
                    oldMarkerBTS.setMap(null);
                }
                oldMarkerBTS = markerBTS;

                // vẽ vùng phủ sóng 
                const btsCircle = new google.maps.Circle({
                    center: location,
                    radius: radius,
                    map: map,
                    fillOpacity: 0.35,
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#0000FF",
                    strokeColor: "#0000FF",
                    clickable: false,
                })

                // xóa vòng tròn trước nếu có
                if (oldBtsCirCle) {
                    oldBtsCirCle.setMap(null);
                }
                oldBtsCirCle = btsCircle;

                // kiểm tra xem có vi phạm vùng cấm nào không
                const vungCams = getVungCamIntersection(location, lstVungCam, khoangCach);

                // kiểm tra xem bts vừa vẽ giao với những bts nào
                const BTS = { location: location, radius: radius };
                const btss = isIntersectAllCicle(BTS, lstBTS);

                // Tạo popup
                const modal = getModalDeXuatBTS(btss, vungCams, location)
                $('#MasterModal').html(modal);
                $('#MasterModal').modal('show');

            }

    })

    // Lắng nghe sự kiện zoom
    map.addListener('zoom_changed', function () {
        const zoom = map.getZoom();
        if (ListMarker) {
            ListMarker.forEach((item) => {
                if (zoom > 12) {
                    item.setLabel({
                        text: item.getTitle(),
                        className: 'labelMarker',
                        color: 'blue',
                    })

                } else {
                    item.setLabel(null);
                }
            })
        }
        if (ListMarker_QuyHoachTramBTS) {
            ListMarker_QuyHoachTramBTS.forEach((item) => {
                if (zoom > 12) {
                    item.setLabel({
                        text: item.getTitle(),
                        className: 'labelMarker',
                        color: 'blue',
                    })

                } else {
                    item.setLabel(null);
                }
            })
        }
    })


}
window.initMap = initMap;


///Hàm thêm marker vào map
//Start

function Getmarker(IsAdd, arr) {
    if (!IsAdd) {
        // xóa các marker
        removeMarkers(ListMarker);
        //Delete all Circle
        removeCircles(lCircle);
    }
    arr.forEach((x) => {
        if (x.Lat != null && x.Lng != null && x.Type != objType.TuyenViba != objType.TuyenDuongThu) {
            const uluru = { lat: parseFloat(x.Lat.replace(',', '.')), lng: parseFloat(x.Lng.replace(',', '.')) };

            let icon = {
                url: '',
                scaledSize: new google.maps.Size(40, 40),
            };

            // Set url icon cho từng đối tượng
            //if (x.Type == objType.QLAnten) icon.url = urlIcon.Anten;
            if (x.Type == objType.QLTramBTS) icon.url = urlIcon.Anten;
            if (x.Type == objType.QLLoaPhatThanh) icon.url = urlIcon.LoaPhatThanh;
            if (x.Type == objType.QLBuuCuc) icon.url = urlIcon.BuuCuc;
            if (x.Type == objType.QuyHoachTramBTS) icon.url = urlIcon.QuyHoachTramBTS;
            if (x.Type == objType.InternetVaTroChoi) icon.url = urlIcon.InternetVaTroChoi;
            if (x.Type == objType.DichVuVienThong) icon.url = urlIcon.DichVuVienThong;
            if (x.Type == objType.HaTangTuyenHinh) icon.url = urlIcon.HaTangTuyenHinh;
            if (x.Type == objType.TramVienThong) icon.url = urlIcon.TramVienThong;

            const markerObj = new google.maps.Marker({
                map,
                position: uluru,
                title: x.TenDoiTuong,
                animation: google.maps.Animation.DROP,
                icon: icon,

            });


            // Kiểm tra xem có phải trạm bts ko để vẽ bán kính
            if (x.Type != undefined && (x.Type == objType.QLTramBTS || x.Type == objType.QuyHoachTramBTS)) {
                let detl = JSON.parse(x.detailInfor);

                var radiusDefault = 50;
                if (typeof (detl.BanKinhPhatSong) !== 'undefined') {
                    radiusDefault = detl.BanKinhPhatSong > 0 ? detl.BanKinhPhatSong : radiusDefault;
                    if (typeof (detl.DonViTinhPhuSong) !== 'undefined' && detl.DonViTinhPhuSong != null) {
                        if (detl.DonViTinhPhuSong.toLowerCase() === "km") {
                            radiusDefault *= 1000;
                        }
                    }
                }
                var cityCircle = new google.maps.Circle({
                    center: uluru,
                    radius: radiusDefault, // Bán kính (đơn vị là mét)
                    map: map,
                    strokeColor: "#FF0000",
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#FF0000",
                    fillOpacity: 0.35,
                    clickable: false,
                });

                // lưu vào list circle
                if (x.Type == objType.QLTramBTS) {
                    lCircle.push(cityCircle);
                } else if (x.Type == objType.QuyHoachTramBTS) {
                    lCircle_QuyHoach.push(cityCircle);
                }
            }

            markerObj.addListener("click", () => {

                // khi chưa bấm vào nút đo khoảng cách giữa 2 cột
                if (!twoColumnsDistance) {
                    showObjClick(x);

                    $('.box-bando').removeClass('left-bando');
                    $('.lopBanDo').removeClass('left-bando2');
                    $('.box-bando').addClass('left-bando');
                    $('.lopBanDo').addClass('left-bando2');

                } else {
                    // khi đã bấm nút đo khoảng cách giữa hai cột
                    cots.push(markerObj);
                    const latlng = { lat: markerObj.getPosition().lat(), lng: markerObj.getPosition().lng() }
                    paths2.push(latlng);
                    drawPolyline(paths2);
                    if (paths2 != null && paths2 != undefined && paths2.length > 1) {

                        const point1 = paths2[paths2.length - 2];
                        const point2 = paths2[paths2.length - 1];
                        const distance = haversineDistance(point1, point2);
                        const positionAvg = { lat: (point1.lat + point2.lat) / 2, lng: (point1.lng + point2.lng) / 2 };
                        addTextMarker(positionAvg, distance);
                    }
                }
            });

            // lưu lại list marker
            if (x.Type == objType.QuyHoachTramBTS) {
                if ($('#quyhoach_chk').prop('checked')) {
                    ListMarker_QuyHoachTramBTS.push(markerObj);
                }
                else {
                    // ko hiển thị khi ko bật
                    markerObj.setMap(null);
                    cityCircle.setMap(null);
                }
            } else {
                ListMarker.push(markerObj);
            }
        }
    })
    if (markerCluster) {
        markerCluster.clearMarkers();
    }
    markerCluster = new markerClusterer.MarkerClusterer({ map: map, markers: ListMarker });

    if ($('#quyhoach_chk').prop('checked')) {
        // markerCluster của thông tin quy hoạch
        if (markerCluster_QuyHoachTramBTS) {
            markerCluster_QuyHoachTramBTS.clearMarkers();
        }
        markerCluster_QuyHoachTramBTS = new markerClusterer.MarkerClusterer({ map, markers: ListMarker_QuyHoachTramBTS });
    }
}


// Hàm show chi tiết khi click vào marker
function showObjClick(obj) {
    if (obj == null || obj == undefined) return;
    if ($('#Grid_resultSearch').is(':hidden')) {

        if (obj.detailInfor == null || obj.detailInfor == undefined) return null;
        let detailInfor = JSON.parse(obj.detailInfor);
        if (detailInfor == null || detailInfor == '') return;

        let strHtml = '';
        strHtml = `<div class = "box-detailObjClick"> `

        // xem có ảnh ko thì set mặc định
        if (obj.AnhDaiDien != null && obj.AnhDaiDien != undefined && obj.AnhDaiDien != "") {
            strHtml += `<div class="img-detailObjClick" >
                <img src="/Uploads/Avatars/${obj.AnhDaiDien}" alt="Ảnh đại diện" />
                        </div > `
        } else {
            switch (obj.Type) {

                case objType.TramVienThong:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Avatars/avtTramVienThong.jpg" alt="Ảnh trạm viễn thông" />
                        </div>`
                    break;

                case objType.HaTangTuyenHinh:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Avatars/avtHtth.png" alt="Ảnh hạ tầng truyền hình" />
                        </div>`
                        break;

                case objType.DichVuVienThong:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Avatars/avtDvvt.jpg" alt="Ảnh điểm cung cấp DVVT" />
                        </div>`
                    break;
                case objType.InternetVaTroChoi:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Avatars/avtInternetVaTroChoi.png" alt="Ảnh điểm cung cấp internet và trò chơi" />
                        </div>`
                    break;
                case objType.TuyenDuongThu:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Media/tuyenduongthu.jpg" alt="Ảnh tuyến đường thư" />
                        </div>`
                    break;

                case objType.QLLoaPhatThanh:
                    strHtml += `<div class="img-detailObjClick" >
                <img src="/Uploads/Media/loaphatthanh.jpg" alt="Ảnh loa phát thanh" />
                        </div > `
                    break;
                case objType.QLTramBTS:
                    strHtml += `<div class="img-detailObjClick" >
                                <img src="/Uploads/Avatars/avtTramBTS.jpeg" alt="Ảnh trạm BTS" />
                                </div > `
                    break;
                case objType.TuyenViba:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Media/tuyenviba.jpg" alt="Ảnh tuyến viba" />
                        </div>`
                    break;
                case objType.QLBuuCuc:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Media/buucuc.jpg" alt="Ảnh bưu cục" />
                        </div>`
                    break;
                case objType.QuyHoachTramBTS:
                    strHtml += `<div class="img-detailObjClick">
                            <img src="/Uploads/Media/trambts.jpg" alt="Ảnh anten quy hoạch" />
                        </div>`
                    break;
                default:
                    break;

            }
        }

        switch (obj.Type) {
           
            case objType.QLTramBTS:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${detailInfor.MaTram_TenTram}
                                </h3>
                                <p>
                                    ${detailInfor.TrangThaiHoatDongtxt}
                                </p>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i>Địa chỉ: ${detailInfor.DiaChi}</p>
                                <p>Tên cột anten: ${detailInfor.CotBTStxt}</p>
                                <p>Doanh nghiệp sở hữu: ${detailInfor.DoanhNghieptxt}</p>
                                <p>Công nghệ phát sóng: ${detailInfor.CongNghePhatSong}</p>
                                <p>Bán kính phát sóng: ${detailInfor.BanKinhPhatSong} ${detailInfor.DonViTinhPhuSong}</p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.QLLoaPhatThanh:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                                <p>
                                    ${detailInfor.TinhTrangHoatDongName}
                                </p>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Địa chỉ: ${obj.Huyen} </p>
                                <p><i class= ""></i> Tần số: ${detailInfor.TanSo} </p>
                                <p><i class= ""></i> Tổng số cụm loa: ${detailInfor.TongSoCumLoa} </p>
                                <p><i class= ""></i> Công suất: ${detailInfor.CongSuat} </p>
                                <p><i class= ""></i> Địa chỉ lắp đặt: ${detailInfor.DiaChiLapDat} </p>
                                <p><i class= ""></i> Phương thức lắp đặt: ${detailInfor.PhuongThucLapDat} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.QLBuuCuc:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                                <p>
                                    ${detailInfor.TinhTrangHoatDongName}
                                </p>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Mã bưu chính: ${detailInfor.MaBuuChinh} </p>
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.DiaChi} </p>
                                <p><i class= ""></i> Giờ phục vụ: ${ToTime2(detailInfor.GioPhucVu)} </p>
                                <p><i class= ""></i> Các loại dịch vụ cung cấp: ${detailInfor.CacLoaiDichVuCungCapName} </p>
                                <p><i class= ""></i> Loại hình dịch vụ: ${detailInfor.LoaiHinhPhucVuName} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.QuyHoachTramBTS:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                                <p>
                                    Loại cột: ${detailInfor.TenLoaiCot}
                                </p>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.TenXa} ${detailInfor.TenHuyen} </p>
                                <p><i class= ""></i> Chiều cao tối đa: ${detailInfor.ChieuCaoToiDa}</p>
                                <p><i class= ""></i> Khoảng cách đến tim đường: ${detailInfor.KhoangCachDenTimDuong} </p>
                                <p><i class= ""></i> Công nghệ phát sóng: ${detailInfor.CongNghePhatSong} </p>
                                <p><i class= ""></i> Bán kính phát sóng: ${detailInfor.BanKinhPhatSong}${detailInfor.DonViTinhPhuSong} </p>
                                <p><i class= ""></i> Khoảng cách đến tim đường: ${detailInfor.KhoangCachDenTimDuong} </p>
                                <p><i class= ""></i> Thời điểm bắt đầu chuyển đổi: ${ToDate(detailInfor.ThoiDiemBatDauChuyenDoi)} </p>
                                <p><i class= ""></i> Thời điểm hoàn thành chuyển đổi: ${ToDate(detailInfor.ThoiDiemHoanThanhChuyenDoi)} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.DichVuVienThong:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                                <p>
                                    ${detailInfor.TrangThaiHoatDongName}
                                </p>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Đơn vị quản lý: ${detailInfor.TenDonViQuanLy} </p>
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.TenXa}, ${detailInfor.TenHuyen} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.InternetVaTroChoi:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.TenXa}, ${detailInfor.TenHuyen} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.HaTangTuyenHinh:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.TenDonViQuanLy} </p>
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.DiaChiLapDat} </p>
                                <p><i class= ""></i> Độ cao Anten: ${detailInfor.DoCaoAngten} </p>
                                <p><i class= ""></i> Số lượng máy phát: ${detailInfor.SoLuongMayPhat} </p>
                                <p><i class= ""></i> Công suất máy phát: ${detailInfor.CongSuatMayPhat} </p>
                                <p><i class= ""></i> Tần số: ${detailInfor.TanSo} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            case objType.TramVienThong:
                strHtml += `
                            <div class="title-detailObjClick">
                                <h3>
                                    ${obj.TenDoiTuong}
                                </h3>
                            </div>
                            <div class="content-detailObjClick">
                                <p><i class= ""></i> Địa chỉ: ${detailInfor.TenDonViSoHuu} </p>
                            </div>
                            <div class="footer-detailObjClick">
                                <div class="Duongdi useful" id="streetDirection">
                                    Đường đi
                                    <i class="fa-solid fa-diamond-turn-right"></i>
                                </div>
                            </div>`
                break;
            default:
                break;

        }

        strHtml += `</div>`
        $('.detailObjClick').html(strHtml);
        $('.detailObjClick').show();

        // di chuyển viewport
        let center = { lat: parseFloat(obj.Lat.replace(",", ".")), lng: parseFloat(obj.Lng.replace(",", ".")) };
        map.setCenter(center);
        map.setZoom(13);

        $('#input-search').val(obj.TenDoiTuong);
        $('.time-search').show();

        // Đổ dữ liệu vào ô tìm đường đi
        desPoint = { lat: obj.Lat, lng: obj.Lng };
        desPointName = obj.TenDoiTuong;

        // Focus marker
        const index = findMarker(center, ListMarker);
        if (index != null && index > 0) {
            if (oldMarkerFocus) {
                oldMarkerFocus.icon.scaledSize = new google.maps.Size(40, 40);
                oldMarkerFocus.icon.size = new google.maps.Size(40, 40);
                oldMarkerFocus.setZIndex(100);
            }

            ListMarker[index].icon.scaledSize = new google.maps.Size(55, 55);
            ListMarker[index].icon.size = new google.maps.Size(55, 55);
            ListMarker[index].setZIndex(120);

            oldMarkerFocus = ListMarker[index];
        }

    }
    else {
        openChiTiet(obj.Type, obj.ItemId, obj.Lat, obj.Lng, obj.TenDoiTuong);
    }

}
//End


// Các hàm liên quan tới tìm kiếm điểm xuất phát - Start

// Hàm kiểm tra một điểm có nằm trong boundingBox của Việt Nam không
function isWithinVietnam(lat, lng) {
    var bounds = {
        north: 23.393395,
        south: 8.1790665,
        west: 102.14441,
        east: 109.464637
    };
    return lat >= bounds.south && lat <= bounds.north && lng >= bounds.west && lng <= bounds.east;
}

// Hàm xử lý khi onlick vào một item textSearchResult
function textSearchDirect(myLatLng, originName) {

    var latlngArray = myLatLng.split(',');

    var newLatLng = { lat: parseFloat(latlngArray[0]), lng: parseFloat(latlngArray[1]) };

    $('#origin').val(originName);

    let typeDrive = $('.active_drive').data('drive');

    currentPoint = newLatLng;

    if (!myLatLng || !desPoint) return;
    drawDirect(newLatLng, desPoint, typeDrive);

}


// Hàm hiển thị kết quả tìm kiếm từ textSearch
function genResultTextSearch(results, status) {
    if (status == google.maps.places.PlacesServiceStatus.OK) {
        if (status === google.maps.places.PlacesServiceStatus.OK) {
            var rs = results.filter(function (place) {
                return isWithinVietnam(place.geometry.location.lat(), place.geometry.location.lng());
            });

            let strHtml = "";
            for (var i = 0; i < rs.length; i++) {

                let myLatLng = [rs[i].geometry.location.lat(), rs[i].geometry.location.lng()];

                strHtml += `<div onclick="textSearchDirect('${myLatLng}', '${rs[i].name}')" class="box_result row">
                                    <div class="col-sm-12 show-infor">
                                        <p class="title-heading"> <img class="imgIcon" src="${rs[i].icon}" /> ${rs[i].name}</p>
                                        <span class="subItem">
                                        </span>
                                    </div>
                                </div>`

            }
            $('.item-TextSearch').html(strHtml);
            strHtml = "";

        } else {

        }
    }
}

// Hàm tìm các địa điểm xuất phát
function getPlaceByTextSearch(textSearch) {

    var request = {
        query: textSearch,
        region: 'VN'
    };

    service = new google.maps.places.PlacesService(map);
    service.textSearch(request, genResultTextSearch);
}

function getAllWaypoint(lstWay) {
    var waypoints = lstWay[0].map((item) => {
        if (item.TuyenDuong == 0) {
            return {
                localtion: {
                    lat: item.LatEnd,
                    lng: item.LngEnd
                },
                label: "Lượt đi" + " " + item.ThuTu.toString()
            }
        } else {
            return {
                localtion: {
                    lat: item.LatEnd,
                    lng: item.LngEnd
                },
                label: "Lượt về" + " " + item.ThuTu.toString()
            }
        }

    })
    return waypoints;
}


function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function disPlayDirectionMail(start, end, lstw, type, nameMap) {
    var directionsService = new google.maps.DirectionsService();
    var directionsDisplay;

    if (type == "D") {
        directionsDisplay = new google.maps.DirectionsRenderer({
            polylineOptions: {
                strokeColor: "#FFFF00" // Màu vàng tuyến đi
            },
            suppressMarkers: true,
            //preserveViewport: true,
        });
    } else {
        directionsDisplay = new google.maps.DirectionsRenderer({
            polylineOptions: {
                strokeColor: "#008000" // Màu xanh lá cây tuyến về
            },
            suppressMarkers: true,
            //preserveViewport: true,
        });
    }

    let arrLstPoint = lstw.map(function (waypoint) {
        return {
            location: {
                lat: waypoint.localtion.lat,
                lng: waypoint.localtion.lng
            },
            stopover: true,
        };
    });

    directionsDisplay.setMap(map);

    var request = {
        origin: start,
        destination: end,
        waypoints: arrLstPoint,
        travelMode: 'DRIVING',
    }

    // Gửi yêu cầu và xử lý kết quả
    directionsService.route(request, function (response, status) {
        if (status == "OK") {
            directionsDisplay.setDirections(response);
            // Đánh số thứ tự các điểm dừng và điểm đầu, điểm cuối
            const route = response.routes[0];

            // marker điểm đầu
            const marker = new google.maps.Marker({
                position: route.legs[0].start_location,
                label: {
                    text: '1',
                    color: 'white',
                    fontSize: '14px',
                    fontWeight: 'bold'
                },

                map: map,
            });

            markersThuTu.push(marker);

            // marker những điểm còn lại
            route.legs.forEach((leg, legIndex) => {
                const marker = new google.maps.Marker({
                    position: leg.end_location,
                    label: {
                        text: (legIndex + 2).toString(),
                        color: 'white',
                        fontSize: '12px',
                        fontWeight: 'bold'
                    },

                    map: map,
                });

                markersThuTu.push(marker);
            });
            directionsResults_Mail.push(directionsDisplay);

        } else {
            //window.alert('Không thể hiển thị đường đi do lỗi: ' + status);
        }
    });

}

// Hàm để xóa tất cả các marker trên bản đồ có label là thứ tự
function removeAllMarkersThuTu() {
    for (var i = 0; i < markersThuTu.length; i++) {
        markersThuTu[i].setMap(null); // Đặt map của marker thành null để xóa khỏi bản đồ
    }
    markersThuTu = []; // Đặt lại mảng markers về rỗng
}
function getListParentRoute(x, itemId) {
    let startRoute = [];
    let endRoute = [];

    const dataLocaltion = JSON.parse(x.detailInfor);

    if (dataLocaltion.TuyenDuongThuConDis != undefined && dataLocaltion.TuyenDuongThuConVes != undefined) {
        var TuyenDuongConDi = dataLocaltion.TuyenDuongThuConDis;
        TuyenDuongConDi.forEach((item_di, i_di) => {
            startRoute.push(item_di);
        })

        var TuyenDuongConVe = dataLocaltion.TuyenDuongThuConVes;
        TuyenDuongConVe.forEach((item_ve, i_ve) => {
            endRoute.push(item_ve);
        })

        startRoute.sort((a, b) => a.SoThuTu - b.SoThuTu);
        endRoute.sort((a, b) => a.SoThuTu - b.SoThuTu);
    }

    return [startRoute, endRoute];
}

function getListParentRouteItem(arr) {
    let route = [];
    arr.forEach((item, index) => {
        route.push(item);
    })
    route.sort((a, b) => a.ThuTu - b.ThuTu);
    return route;

}


function getWayPoint(lstwp) {
    var wp = [];
    for (let i = 0; i < lstwp.length - 1; i++) {
        wp.push({
            localtion: {
                lat: lstwp[i].Lat,
                lng: lstwp[i].Lng
            },
            label: (lstwp[i].SoThuTu - 1).toString()
        })
    }
    return wp;
}


function getWayPointItem(lstwp) {
    var wp = [];
    for (let i = 1; i < lstwp.length - 1; i++) {
        wp.push({
            localtion: {
                lat: lstwp[i].Lat,
                lng: lstwp[i].Lng
            },
            label: (lstwp[i].SoThuTu - 1).toString()
        })
    }
    return wp;
}

function handleRouteParent(type, obj, tenTuyen) {
    if (type == 'D') {
        getDirectionForMailItem(true, obj, type, tenTuyen);
    } else {
        getDirectionForMailItem(true, obj, type, tenTuyen);
    }
}

function getDirectionForMailItem(isAdd, arr, type, TenDoiTuong) {
    if (!isAdd) {
        removeAllDirections_Mail();
        removeAllMarkersThuTu();
        removeAllDirectionsItemEach();
    } else {
        removeAllDirections_Mail();
        removeAllMarkersThuTu();
        removeAllDirectionsItemEach();

        let lstWay = [];
        if (!arr) return;
        if (type == 'D') {
            let tuyenduong = getListParentRouteItem(arr);
            lstWay.push(...tuyenduong);
            const origin = { lat: parseFloat(lstWay[0]?.Lat), lng: parseFloat(lstWay[0]?.Lng) };
            const destination = { lat: parseFloat(lstWay[[lstWay.length - 1]]?.Lat), lng: parseFloat(lstWay[[lstWay.length - 1]]?.Lng) };
            var startwp = getWayPointItem(lstWay);
            disPlayDirectionMail(origin, destination, startwp, "D", TenDoiTuong);
        } else {
            //////tuyến về
            let tuyenduong = getListParentRouteItem(arr);
            lstWay.push(...tuyenduong);
            const originend = { lat: parseFloat(lstWay[0]?.Lat), lng: parseFloat(lstWay[0]?.Lng) };
            const destinationend = { lat: parseFloat(lstWay[[lstWay.length - 1]]?.Lat), lng: parseFloat(lstWay[[lstWay.length - 1]]?.Lng) };
            var endwp = getWayPointItem(lstWay);
            disPlayDirectionMail(originend, destinationend, endwp, "E", TenDoiTuong);
        }
    }
}

function getDirectionForMail(isAdd, arr) {
    if (!isAdd) {
        removeAllDirections_Mail();
        removeAllMarkersThuTu();
        removeAllDirectionsItemEach();
    }
    if (!arr || arr.length <= 0) return;
    arr.filter(x => x.Type === objType.TuyenDuongThu).forEach((x) => {
        if (x.ParentId == 0) {
            let tuyenduong = getListParentRoute(x, x.ItemId);
            let lstWay = [];

            lstWay.push(...tuyenduong);
            // Vẽ tuyến đi
            const origin = { lat: parseFloat(lstWay[0][0]?.Lat), lng: parseFloat(lstWay[0][0]?.Lng) };
            const destination = { lat: parseFloat(lstWay[0][lstWay[0].length - 1]?.Lat), lng: parseFloat(lstWay[0][lstWay[0].length - 1]?.Lng) };
            const startwp = getWayPoint(lstWay[0]);
            disPlayDirectionMail(origin, destination, startwp, "D", x.TenDoiTuong);

            // Vẽ tuyến về
            const originVe = { lat: parseFloat(lstWay[1][0]?.Lat), lng: parseFloat(lstWay[1][0]?.Lng) };
            const destinationVe = { lat: parseFloat(lstWay[1][lstWay[1].length - 1]?.Lat), lng: parseFloat(lstWay[1][lstWay[1].length - 1]?.Lng) };
            const endwp = getWayPoint(lstWay[1]);
            disPlayDirectionMail(originVe, destinationVe, endwp, "V", x.TenDoiTuong);

        }

    });
}

function handleDrawDirection(latStart, lngStart, latEnd, lngEnd) {

    // xóa các tuyến đã hiển thị trên map
    removeAllDirections_Mail();
    removeAllMarkersThuTu();
    removeAllDirectionsItemEach();

    // vẽ tuyến mới
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer({
        polylineOptions: {
            strokeColor: "#0000FF",
            strokeOpacity: 0.8,
            strokeWeight: 3
        },
        //preserveViewport: true
    });
    directionsRenderer.setMap(map);

    var request = {
        origin: {
            lat: latStart,
            lng: lngStart
        },
        destination: {
            lat: latEnd,
            lng: lngEnd
        },
        travelMode: 'DRIVING'
    };

    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
        }
    });

    directionsResults_MailItem.push(directionsRenderer);
}
function displayDirections_Viba(origin, destination) {
    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer({
        polylineOptions: {
            strokeColor: getRandomColor(),
            strokeOpacity: 0.8,
            strokeWeight: 3
        },
        preserveViewport: true
    });
    directionsRenderer.setMap(map);

    var request = {
        origin: origin,
        destination: destination,
        travelMode: 'DRIVING'
    };

    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
        }
    });

    directionsResults_Viba.push(directionsRenderer);
}
function GetDirection_Viba(IsAdd, arr) {
    if (!IsAdd) {
        removeAllDirections_Viba();
    }
    if (!arr || arr.length <= 0) return;
    arr.forEach((x) => {
        let latStart = x.LatStart != null && x.LatStart != undefined && x.LatStart != "" ? x.LatStart.replace(",", ".") : null;
        let latEnd = x.LatEnd != null && x.LatEnd != undefined && x.LatEnd != "" ? x.LatEnd.replace(",", ".") : null;
        let lngStart = x.LngStart != null && x.LngStart != undefined && x.LngStart != "" ? x.LngStart.replace(",", ".") : null;
        let lngEnd = x.LngEnd != null && x.LngEnd != undefined && x.LngEnd != "" ? x.LngEnd.replace(",", ".") : null;

        if (latStart != null && lngStart != null && latEnd != null && lngEnd != null) {
            const origin = { lat: parseFloat(latStart), lng: parseFloat(lngStart) };
            const destination = { lat: parseFloat(latEnd), lng: parseFloat(lngEnd) };
            displayDirections_Viba(origin, destination);
        }
    });
}

// Hàm xóa tuyến truyền dẫn viba
function removeAllDirections_Viba() {
    if (!directionsResults_Viba) return;
    directionsResults_Viba.forEach(function (direction) {
        direction.setMap(null);
    });

    directionsResults_Viba = [];
}
function removeAllDirectionsItemEach() {
    directionsResults_MailItem.forEach(function (direction) {
        direction.setMap(null);
    });

    directionsResults_MailItem = [];
}



// ham xóa tuyến đường thư
function removeAllDirections_Mail() {
    directionsResults_Mail.forEach(function (direction) {
        direction.setMap(null);
    });

    directionsResults_Mail = [];
}



// Hàm lấy vị trí hiện tại
function getCurrentLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var lat = position.coords.latitude;
            var lng = position.coords.longitude;
            currentPoint = { lat: lat, lng: lng };

            var latlng = new google.maps.LatLng(lat, lng);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        currentPointName = results[0].formatted_address;
                    }
                }
            });

        }, function (error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    window.alert("Người dùng không cho phép truy cập vị trí.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    window.alert("Thông tin vị trí không khả dụng.");
                    break;
                case error.TIMEOUT:
                    window.alert("Yêu cầu lấy vị trí đã hết thời gian.");
                    break;
                case error.UNKNOWN_ERROR:
                    window.alert("Lỗi không xác định.");
                    break;
            }
        });
    } else {
        window.alert("Trình duyệt của bạn không hỗ trợ");
    }
}

// Xóa chỉ đường khi đóng tab
function RemoveDirect() {
    desPoint = null;
    desPointName = null;
    currentPoint = null;
    currentPointName = null;
    if (oldDirect) {
        oldDirect.setMap(null);
    }
    $('#origin').val(null);
    $('#destination').val(null);
}


// Đổ dữ liệu vào ô điểm đến
function fillDestination() {

    if (desPoint != null && desPoint != undefined) {
        desPoint.lat = parseFloat(desPoint.lat.replace(",", "."));
        desPoint.lng = parseFloat(desPoint.lng.replace(",", "."));
        $('#destination').val(desPointName);
    }
}

// Vẽ khi click vào nút lấy vị trí của bạn
function fillCurrentLocation() {
    getCurrentLocation();
    if (currentPoint != null && currentPoint != undefined) {
        $('#origin').val(currentPointName);
    }
    let typeDrive = $('.active_drive').data('drive');

    if (!currentPoint || !desPoint) return;
    drawDirect(currentPoint, desPoint, typeDrive);
}


function ShowBoxChiDuong() {
    $('.time-search').click();
    fillDestination();

    $('.ChiDuong').show();
}

$(".time_drive").click(function () {

    $('.box-bando').removeClass('left-bando');
    $('.lopBanDo').removeClass('left-bando2');
    $(".ChiDuong").hide();

})


$('#quyhoach_chk').on('change', function () {
    if ($(this).prop('checked')) {
        getDataQuyHoach_Anten((rs) => {
            Getmarker(true, rs);
        });
    }
    else {
        removeMarkers(ListMarker_QuyHoachTramBTS);
        if (markerCluster_QuyHoachTramBTS) {
            markerCluster_QuyHoachTramBTS.clearMarkers();
        }
        removeCircles(lCircle_QuyHoach);
    }
})

var hetRoi = false;
$('.content_ketqua').on('scroll', function () {
    let contentKetQua = $("#Grid_resultSearch").find(".content_ketqua");
    let inner = 0
    let scroll = 0;
    let scrollParent= 0;

    if ($(window).width() < 576) {
         inner = $(this).scrollLeft();
         scroll = $(this).innerWidth() + 50;
         scrollParent = $(this)[0].scrollWidth;
    } else {
         inner = contentKetQua.scrollTop();
         scroll = contentKetQua.innerHeight() + 50;
         scrollParent = contentKetQua[0].scrollHeight;
    }

    if (inner + scroll >= scrollParent && !hetRoi) {
        hetRoi = true;
            PageIndex++;
            getdata((rs, arr) => {
                if (TypeAll === objType.TuyenDuongThu) {
                    getDirectionForMail(true, arr);
                } else
                    if (TypeAll === objType.TuyenViba) {
                        GetDirection_Viba(true, arr);
                    } else
                        Getmarker(true, arr);
                if (rs != "") {
                    $(".content_ketqua").append(rs);
                    hetRoi = false;
                } else {
                    hetRoi = true;
                }
                loadingData = false;
                totalNumber += arr.length;
                if (TypeAll === objType.QLTramBTS) {
                    dataBTS.push(...arr);
                }

                // update text kết quả tìm kiếm
                if (!TypeName) {
                    $("#numberRow").html(`        <span class="color_text">
                        ${totalNumber} kết quả được tìm thấy </span>`)
                } else {
                    $("#numberRow").html(`        <span class="color_text">
                        ${totalNumber} ${TypeName} được tìm thấy </span>`)
                }
            });

    }

});

$(document).on('click', '.item-option', function () {
    $('.dropDownSelection').removeClass('show');
    $('.dropDownSelection').addClass('hide');
})
$(document).on('click', '.dropDownObjType', function () {
    if ($('.dropDownSelection').hasClass('hide')) {
        $('.dropDownSelection').removeClass('hide');
        $('.dropDownSelection').addClass('show');
    } else {
        $('.dropDownSelection').addClass('hide');
        $('.dropDownSelection').removeClass('show');
    }
})
$(document).ready(function () {

    $(".txt-number").autoNumeric({ digitGroupSeparator: '', decimalCharacter: '.', vMin: 0, unformatOnSubmit: false, allowDecimalPadding: false });

    var swiper = new Swiper(".mySwiper", {
        slidesOffsetBefore: 50,
        slidesOffsetAfter: 50,
        slidesPerView: 'auto',
        centeredSlides: false,
        centerInsufficientSlides: true,
        spaceBetween: 15,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
    });

    $('#Huyen').select2();
    $('#Xa').select2();
    $('#NhaMang').select2();

    $('.btn-synthetic').hide();
    $('.btn-review').hide();
    $('.btn-twoColumns').hide();
    $('#NhaMang').next('.select2').hide();
    $('.chkTech').hide();

    $(document).on('click', '.avtiveBox', function () {
        const dataType = $(this).find("a").data("type");
        if (dataType == objType.QLTramBTS) {
            $('.btn-synthetic').show();
            $('.btn-review').show();
            $('#NhaMang').next('.select2').show();
            $('.chkTech').show();

        } else {
            $('.btn-synthetic').hide();
            $('.btn-review').hide();
            $('#NhaMang').next('.select2').hide();
            $('.chkTech').hide();
        }

        if (dataType == objType.QLTramBTS) {
            $('.btn-twoColumns').show();
        } else {
            $('.btn-twoColumns').hide();
        }
    })

    // check box công nghệ phát sóng
    $('input[id^="chk-"]').on('click', function () {
        PageIndex = 1;
        getdata((rs, arr) => {
            Getmarker(false, arr);
            $(".content_ketqua").html("");
            $(".content_ketqua").html(rs);
            totalNumber = arr.length;
            dataBTS = arr;
            $("#numberRow").html(`        <span class="color_text">
                ${arr.length} Trạm BTS được tìm thấy </span>`)
        });

        scrollTopContentKetQua();

    })

    $('#alert-feature').hide();

    $(document).on('click', '#showAll', function () {
        hetRoi = true;
        getdataAll((rs, arr) => {
            if (TypeAll === objType.TuyenDuongThu) {
                getDirectionForMail(false, arr);
            } else
                if (TypeAll === objType.TuyenViba) {
                    GetDirection_Viba(false, arr);
                } else
                    Getmarker(false, arr);

            if (TypeAll === objType.QLTramBTS) {
                dataBTS = arr;
            }

            $(".content_ketqua").html(rs);
            loadingData = false;

            if (!TypeAll) {
                $("#numberRow").html(`        <span class="color_text">
                    ${arr.length} kết quả được tìm thấy </span>`)
            } else {
                $("#numberRow").html(`        <span class="color_text">
                    ${arr.length} ${TypeName} được tìm thấy </span>`)
            }
        })

        isLazyLoad = false;

    });

    $('#Huyen').on('change', function () {
        // zoom map in Huyen
        const Huyenid = $(this).val();
        var listproperties = "<option value=''>---Chọn tất cả---</option>";
        $('#Xa').html(listproperties);
        $('#select2-Xa-container').text('---Chọn tất cả---')
        // lấy dữ liệu từ elastic
        PageIndex = 1;
        getdata((rs, arr) => {
            Getmarker(false, arr);
            $(".content_ketqua").html(rs);

            totalNumber = arr.length;
            if (TypeAll === objType.QLTramBTS) {
                dataBTS = arr;
            }

            if (typeof TypeName === 'undefined') {
                $("#numberRow").html(`        <span class="color_text">
                ${arr.length} kết quả được tìm thấy </span>`)

            } else {
                if (TypeName == objType.TuyenDuongThu) {
                    let count = 0;
                    arr.forEach((item) => {
                        if (item.ParentId == 0) {
                            count++;
                        }
                    })
                    $("#numberRow").html(`        <span class="color_text">
                    ${count} ${TypeName} được tìm thấy </span>`)

                } else {
                    $("#numberRow").html(`        <span class="color_text">
                    ${arr.length} ${TypeName} được tìm thấy </span>`)
                }
            }

        })

        if (Huyenid != null && Huyenid != undefined && Huyenid != '') {
            //Lấy thông tin của xã
            AjaxCall("/XAArea/XA/GetDropdownOfXabyHuyen_V2", "POST", { MaHuyen: Huyenid }, (rs) => {
                rs.Xas.map(x => {
                    listproperties += `<option value=${x.Value}>${x.Text}</option>`
                })
                $('#Xa').html(listproperties);

                //Set zoom đến Huyện
                const center = new google.maps.LatLng(rs.ViDo, rs.KinhDo);
                oldHuyenCenter = center;
                map.setCenter(center);
                map.setZoom(11);
            })
        } else {

            map.setCenter(centerView);
            map.setZoom(9.5);
        }

        const HuyenName = $(this).find("option:selected").text();
        const clq_Fillter = getCQL_FILTER(HuyenName);

        if (!Huyenid) {
            map.overlayMapTypes.removeAt(map.overlayMapTypes.indexOf(huyenLayer));
            huyenLayer = null;
        }
        if (huyenLayer) {
            map.overlayMapTypes.removeAt(map.overlayMapTypes.indexOf(huyenLayer));
        }
        huyenLayer = getWMSLayer(map, WMS_UrlMap.layers_District, clq_Fillter, 1.0);
        map.overlayMapTypes.push(huyenLayer);

        //$('#Grid_resultSearch').scrollTop(0);
        scrollTopContentKetQua();
    })
    $('#Xa').on('change', function () {
        // zoom map in Xa
        const XaId = $(this).val();
        if (XaId != null && XaId != undefined && XaId != '') {
            AjaxCall("/XAArea/XA/GetXaByMaXa", "POST", { MaXa: XaId }, (rs) => {
                //Set zoom đến Xã
                const center = new google.maps.LatLng(rs.ViDoGPS, rs.KinhDoGPS);
                oldHuyenCenter = center;
                map.setCenter(center);
                map.setZoom(13);
            })

            PageIndex = 1;


        } else {
            map.setCenter(oldHuyenCenter);
            map.setZoom(11);
        }

        // lấy dữ liệu từ elastic
        getdata((rs, arr) => {

            Getmarker(false, arr);
            $(".content_ketqua").html(rs);
            totalNumber = arr.length;
            if (TypeAll === objType.QLTramBTS) {
                dataBTS = arr;
            }

            if (typeof TypeName === 'undefined') {
                $("#numberRow").html(`        <span class="color_text">
                ${arr.length} kết quả được tìm thấy </span>`)
            } else {
                $("#numberRow").html(`        <span class="color_text">
                ${arr.length} ${TypeName} được tìm thấy </span>`)
            }
        })

        //$('#Grid_resultSearch').scrollTop(0);
        scrollTopContentKetQua();

    })

    $('#NhaMang').on('change', function () {
        PageIndex = 1;
        getdata((rs, arr) => {
            Getmarker(false, arr);
            $(".content_ketqua").html(rs);
            totalNumber = arr.length;
            if (TypeAll === objType.QLTramBTS) {
                dataBTS = arr;
            }
            $("#numberRow").html(`<span class="color_text">${arr.length} Trạm BTS được tìm thấy </span>`)
        })

        //$('#Grid_resultSearch').scrollTop(0);
        scrollTopContentKetQua();

    })

    $('.lopBanDo').hide();
    $('.lopphu, .lopBanDo').hover(
        function () {
            $('.lopBanDo').show();
            $('.lopphu').css('width', '100px');
            $('.bglopbando').css('border', '5px solid white');
        },
        function () {
            if (!$('.lopphu').is(':hover') && !$('.lopBanDo').is(':hover')) {
                $('.lopBanDo').hide();
                $('.lopphu').css('width', '80px');
                $('.bglopbando').css('border', '3px solid white');
            }
        }
    );


    // Đoạn này dùng cho mobile
    var visiable = false;
    $('.lopphu, .lopBanDo').click(function () {
        if (!visiable) {
            $('.lopBanDo').show();
            $('.lopphu').css('width', '100px');
            $('.bglopbando').css('border', '5px solid white');
            visiable = true;
        } else {
            $('.lopBanDo').hide();
            $('.lopphu').css('width', '80px');
            $('.bglopbando').css('border', '3px solid white');
            visiable = false;

        }

    });
    //


    $('#origin').on('keyup', function () {

        let searchString = $(this).val();
        if (searchString != null || searchString != "") {
            setTimeout(() => getPlaceByTextSearch(searchString), 1000);
        }
    })
    //hàm đóng mở filter

    // Click vào chỉ đường
    $(document).on('click', '#streetDirection', function () {
        ShowBoxChiDuong();
        $(".ChiDuong").attr("style", "display: block;");

        $('.box-bando').removeClass('left-bando');
        $('.lopBanDo').removeClass('left-bando2');
        $('.box-bando').addClass('left-bando');
        $('.lopBanDo').addClass('left-bando2');

        $('.chitietObj').hide();
    })

    //click vào các nút tính năng trên map

    //Nút đo khoảng cách giữa hai điểm
    $('.btn-twoPoints').on('click', function () {

        $(".map-search").addClass("hidden-content");

        if ($(this).find('img').hasClass('selectedFeature')) {
            $('.close-feature').click();
        } else {
            twoPointsDistance = true;

            $(this).find('img').addClass('selectedFeature');
            $('.alert-content-text .text-title').text("Đo khoảng cách giữa hai điểm");
            $('.alert-content-text .text-subTitle').text("Kích vào bản đồ để chọn điểm!");
            $('.alert-content-img').find('img').attr('src', '/Uploads/IconBanDo/distance.png');
            $('.BTS_Radius').addClass('hide');
            $('.BTS_Radius').removeClass('show');
            $('#alert-feature').show();

            twoColumnsDistance = false;
            $('.btn-twoColumns').find('img').removeClass('selectedFeature');

            synthetic = false;
            $('.btn-synthetic').find('img').removeClass('selectedFeature');

            reviewBTS = false;
            $('.btn-review').find('img').removeClass('selectedFeature');

            removeMarkers(textMarkers);
            removePolyline();
            removeMarkers(markers_feature);
            removeRectangle();
            removeMarkers(oldMarkerBTS);
            removeCircles(oldBtsCirCle);
            removeVungCam(lstVungCams);
        }


    })

    // Click nút đo khoảng cách giữa hai cột
    $('.btn-twoColumns').on('click', function () {
        if ($(this).find('img').hasClass('selectedFeature')) {
            $('.close-feature').click();
        } else {
            twoColumnsDistance = true;
            $(this).find('img').addClass('selectedFeature');
            $('.alert-content-text .text-title').text("Đo khoảng cách giữa hai cột");
            $('.alert-content-text .text-subTitle').text("Kích vào biểu tượng trên bản đồ để chọn cột!");
            $('.alert-content-img').find('img').attr('src', '/Uploads/IconBanDo/icontwocots.png');
            $('.BTS_Radius').addClass('hide');
            $('.BTS_Radius').removeClass('show');
            $('#alert-feature').show();

            twoPointsDistance = false;
            $('.btn-twoPoints').find('img').removeClass('selectedFeature');

            synthetic = false;
            $('.btn-synthetic').find('img').removeClass('selectedFeature');

            reviewBTS = false;
            $('.btn-review').find('img').removeClass('selectedFeature');

            removeMarkers(textMarkers);
            removePolyline();
            removeMarkers(markers_feature);
            removeRectangle();
            removeMarkers(oldMarkerBTS);
            removeCircles(oldBtsCirCle);
            removeVungCam(lstVungCams);
        }
    })

    // Click nút tổng hợp trạm BTS theo không gian
    $('.btn-synthetic').on('click', function () {

        if ($(this).find('img').hasClass('selectedFeature')) {
            $('.close-feature').click();
        } else {
            synthetic = true;
            $(this).find('img').addClass('selectedFeature');
            $('.alert-content-text .text-title').text("Tổng hợp cột Anten");
            $('.alert-content-text .text-subTitle').text("Kích và rê chuột trên bản đồ để khoanh vùng!");
            $('.alert-content-img').find('img').attr('src', '/Uploads/IconBanDo/selection.png');
            $('.BTS_Radius').addClass('hide');
            $('.BTS_Radius').removeClass('show');
            $('#alert-feature').show();

            drawRectangle();

            twoColumnsDistance = false;
            $('.btn-twoColumns').find('img').removeClass('selectedFeature');

            twoPointsDistance = false;
            $('.btn-twoPoints').find('img').removeClass('selectedFeature');

            reviewBTS = false;
            $('.btn-review').find('img').removeClass('selectedFeature');

            removeMarkers(textMarkers);
            removePolyline();
            removeMarkers(markers_feature);
            removeMarkers(oldMarkerBTS);
            removeCircles(oldBtsCirCle);
            removeVungCam(lstVungCams);
        }
    })

    $('.btn-review').on('click', function () {

        // xử lý giao diện
        if ($(this).find('img').hasClass('selectedFeature')) {
            $('.close-feature').click();
        } else {
            reviewBTS = true;
            $(this).find('img').addClass('selectedFeature');
            $('.alert-content-text .text-title').text("Đề xuất cột Anten");
            $('.alert-content-text .text-subTitle').text("Nhập bán kính phát sóng (km)");
            $('.alert-content-img').find('img').attr('src', '/Uploads/IconBanDo/rasoat.png');
            $('.BTS_Radius').removeClass('hide');
            $('.BTS_Radius').addClass('show');
            $('#alert-feature').show();

            twoColumnsDistance = false;
            $('.btn-twoColumns').find('img').removeClass('selectedFeature');

            twoPointsDistance = false;
            $('.btn-twoPoints').find('img').removeClass('selectedFeature');

            synthetic = false;
            $('.btn-synthetic').find('img').removeClass('selectedFeature');

            removeMarkers(textMarkers);
            removePolyline();
            removeMarkers(markers_feature);
            removeRectangle();

        }

        // vẽ vùng cấm
        getAllVungCam().then(lst => {
            lstVungCam = lst;
            PolygonAll(lst, map);
        });

        // lấy listBTS
        getAllTramBTS().then(lst => {
            lstBTS = [];
            for (var i = 0; i < lst.length; i++) {
                const detailInfor = JSON.parse(lst[i].detailInfor);
                if (!lst[i].Lat || !lst[i].Lng || !detailInfor) continue;

                let lat = parseFloat(lst[i].Lat.replace(",", "."));
                let lng = parseFloat(lst[i].Lng.replace(",", "."));
                if (!detailInfor) continue;

                let radiusUnit = detailInfor.DonViTinhPhuSong;
                let radius = detailInfor.BanKinhPhatSong;
                if (!radiusUnit || !radius) continue;

                if (radiusUnit == "Km") radius *= 1000;
                const circle = {
                    baseInfor: {
                        location: { lat: lat, lng: lng },
                        radius: radius
                    },
                    detailInfor: detailInfor,
                    distance: null,
                };
                lstBTS.push(circle);
            }
        })
    })

    $('.close-feature').on('click', function () {
        twoPointsDistance = false;
        twoColumnsDistance = false;
        synthetic = false;
        reviewBTS = false;

        $('.btn-twoPoints').find('img').removeClass('selectedFeature');
        $('.btn-twoColumns').find('img').removeClass('selectedFeature');
        $('.btn-synthetic').find('img').removeClass('selectedFeature');
        $('.btn-review').find('img').removeClass('selectedFeature');
        $('.BTS_Radius').addClass('hide');
        $('.BTS_Radius').removeClass('show');
        $('#alert-feature').hide();

        removeMarkers(markers_feature);
        removeMarkers(textMarkers);
        removePolyline();
        removeRectangle();
        removeMarkers(oldMarkerBTS);
        removeCircles(oldBtsCirCle);
        removeVungCam(lstVungCams);

    })

    //$('#searchAll').click();
    $('.dropDownSelection').addClass('hide');

    $('.swiper-slide').eq(1).trigger('click');
});

$(window).on('load', function () {
    if ($(window).width() > 576) {
        $('#homeBtnHamburger').click();
    }
})


function scrollTopContentKetQua() {
    hetRoi = false;
    if ($(window).width() < 576) {
        $("#Grid_resultSearch").find('.content_ketqua').scrollLeft(0);
    } else {
        $("#Grid_resultSearch").find('.content_ketqua').scrollTop(0);
    }

}