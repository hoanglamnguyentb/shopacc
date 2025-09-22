

var markers_feature = [];
var paths = [];
var paths2 = [];
var textMarkers = [];
var polyline;
var cots = [];
var twoPointsDistance = false;
var twoColumnsDistance = false;
var drawingManager;
var rectangle;
var debounceTimer;
var reviewBTS;
var boundsBTS;
var lstVungCams = [];
var distanceBTS = [];

// Hàm lấy html tình trạng hoạt động
function getHtmlTinhTrangs(code) {
    if (!code) {
        return `<span class="notValue">Chưa có thông tin</span>`;
    }

    // của anten và bts
    if (code === 'HOATDONGTOT') {
        return `<span class="green">Hoạt động tốt</span>`;
    }
    if (code === 'KHONGHOATDONG') {
        return `<span class="grey">Không hoạt động</span>`;
    }
    if (code === 'BAOTRISUACHUA') {
        return `<span class="yellow">Đang bảo trì/sửa chữa</span>`;
    }

    // của bưu cục và tuyến tuyền tải điện
    if (code === 'DangHoatDong') {
        return `<span class="green">Đang hoạt động</span>`;
    }
    if (code === 'DungHoatDong') {
        return `<span class="grey">Dừng hoạt động</span>`;
    }
    if (code === 'TamNgung') {
        return `<span class="yellow">Tạm ngưng</span>`;
    }
}

// Hàm tính khoảng cách giữa hai điểm trên bản đồ
function haversineDistance(point1, point2) {

    const lat1 = point1.lat;
    const lon1 = point1.lng;
    const lat2 = point2.lat;
    const lon2 = point2.lng;

    const toRadians = angle => angle * (Math.PI / 180);

    const R = 6371; // Bán kính của trái đất tính bằng km
    const dLat = toRadians(lat2 - lat1);
    const dLon = toRadians(lon2 - lon1);
    const a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(toRadians(lat1)) * Math.cos(toRadians(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    const distance = R * c; // Khoảng cách tính bằng km

    return distance.toFixed(2);
}


// Hàm tạo marker
function addMarker(location) {
    const marker = new google.maps.Marker({
        position: location,
        map: map,
        icon: {
            url: '/Uploads/IconMarker/location.png',
            scaledSize: new google.maps.Size(40, 40),
            origin: new google.maps.Point(0, 0),
        }
    });
    markers_feature.push(marker);
}

// Hàm vẽ đường thằng nối hai điểm theo đường chim bay
function drawPolyline(paths) {
    if (polyline) {
        polyline.setMap(null);
    }
    polyline = new google.maps.Polyline({
        path: paths,
        geodesic: true,
        strokeColor: 'blue',
        strokeOpacity: 1.0,
        strokeWeight: 2,
    });

    polyline.setMap(map);
}

// Hàm thêm textMarker
function addTextMarker(location, distance) {
    const marker = new google.maps.Marker({
        position: location,
        map: map,
        label: {
            text: distance + " km",
            color: "#0000FF",
            fontSize: "14px",
            fontWeight: "bold"
        },
        icon: {
            url: '/Uploads/IconMarker/trongsuot.png',
            scaledSize: new google.maps.Size(0, 0),
            anchor: new google.maps.Point(0, 10),
        },
    });
    textMarkers.push(marker);

}

// Hàm xóa các vùng cấm
function removeVungCam(vungCams) {
    if (Array.isArray(vungCams)) {
        vungCams.forEach(item => item.setMap(null));
        vungCams.length = 0;
    } else if (vungCams && typeof vungCams === 'object') {
        vungCams.setMap(null);
    }
}


// Hàm xóa các Polyline
function removePolyline() {
    if (polyline) {
        polyline.setMap(null);
    }

    polyline = null;
    paths = [];
    paths2 = [];
}

// Hàm lấy latlng khi chọn cột
async function getCotLatLng(id) {
    try {
        const response = await fetch(`/QLBanDoArea/QLBanDo/GetCotLatLng?id=${id}`, {
            method: 'GET'
        });
        const rs = await response.json();

        if (rs.success) {
            return rs.data;
        } else {
            throw new Error('Request was not successful');
        }
    } catch (error) {
        NotiError("Đã xảy ra lỗi!");
    }
}

// Hàm kiểm tra 1 điểm có nằm trong 1 bounds hay không
function pointInsideBound(point, bound) {
    if (!point || !bound) return false;

    if (bound.contains(point)) {
        return true;
    }
}


// Hàm vẽ hình chữ nhật
function drawRectangle() {
    drawingManager = new google.maps.drawing.DrawingManager({
        drawingMode: google.maps.drawing.OverlayType.RECTANGLE,
        drawingControl: false,
        drawingControlOptions: {
            position: google.maps.ControlPosition.TOP_CENTER,
            drawingModes: ['rectangle']
        },
        rectangleOptions: {
            editable: false,
            draggable: true,
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#00FF00',
            fillOpacity: 0.35,
            zIndex: 100,
        }
    });
    drawingManager.setMap(map);

    let drawingListener = google.maps.event.addListener(drawingManager, 'overlaycomplete', function (event) {
        if (event.type == 'rectangle') {
            rectangle = event.overlay;
            boundsBTS = rectangle.getBounds();
            PageIndex = 1;
            syntheticBTS((rs, arr) => {
                $(".content_ketqua").html(rs);
                $("#numberRow").html(`        <span class="color_text">
                        ${arr.length} ${TypeName} được tìm thấy </span>`)
            });

            google.maps.event.addListener(rectangle, 'dragend', function () {
                PageIndex = 1;
                boundsBTS = rectangle.getBounds();
                syntheticBTS((rs, arr) => {
                    $(".content_ketqua").html(rs);
                    $("#numberRow").html(`        <span class="color_text">
                        ${arr.length} ${TypeName} được tìm thấy </span>`)
                })
            });

            drawingManager.setMap(null);
        }
    });


}

// Hàm xóa marker
function removeMarkers(ListMarker) {
    if (Array.isArray(ListMarker)) {
        if (ListMarker.length === 0) return;
        ListMarker.forEach(marker => marker.setMap(null));
        ListMarker.length = 0;
    } else if (ListMarker !== null && typeof ListMarker === 'object') {
        ListMarker.setMap(null);
        ListMarker = null;
    }
}

// Hàm xóa vòng tròn
function removeCircles(lCircle) {
    if (Array.isArray(lCircle)) {
        if (lCircle.length === 0) return;
        lCircle.forEach(marker => marker.setMap(null));
        lCircle.length = 0;
    } else if (lCircle !== null && typeof lCircle === 'object') {
        lCircle.setMap(null);
        lCircle = null;
    }
}

// Xóa hình chữ nhật và hủy chế độ vẽ
function removeRectangle() {

    if (drawingManager) drawingManager.setMap(null);
    drawingManager = null;
    if (rectangle) rectangle.setMap(null);
    rectangle = null;
}

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

// hàm lấy thông tin các vùng cấm
async function getAllVungCam() {
    try {
        const response = await $.ajax({
            url: '/VungCamTrienKhaiBTSArea/VungCamTrienKhaiBTS/GetAllVungCam',
            type: 'GET',
            data: null
        });
        return response.listVungCams;
    } catch (error) {
        return null;
    }
}

// hàm lấy thông tin các trạm BTS
async function getAllTramBTS() {
    try {
        const response = await $.ajax({
            url: `/QLBanDoArea/QLBanDo/GetAllBTS`,
            type: 'GET',
            data: null
        });
        return response;
    } catch (error) {
        return null;
    }
}

// Hàm vẽ tất cả vùng cấm
function PolygonAll(listVungCam, map) {
    listVungCam.forEach(item => {
        const vungcamBTS = new google.maps.Polygon({
            paths: item.listToaDo,
            strokeColor: "#FF0000",
            strokeOpacity: 0.8,
            strokeWeight: 3,
            fillColor: "#FF0000",
            fillOpacity: 0.35,
            clickable: false,
        });
        vungcamBTS.setMap(map);
        lstVungCams.push(vungcamBTS);
    })
}

//Hàm để tính khoảng cách từ một điểm đến một đoạn thẳng
function distanceToLineSegment(point, lineStart, lineEnd) {
    var dX = lineEnd.lat - lineStart.lat;
    var dY = lineEnd.lng - lineStart.lng;
    var length = Math.sqrt(Math.pow(dX, 2) + Math.pow(dY, 2));
    var u = ((point.lat - lineStart.lat) * dX + (point.lng - lineStart.lng) * dY) / (length * length);

    var intersectionPoint;
    if (u < 0) {
        intersectionPoint = new google.maps.LatLng(lineStart.lat, lineStart.lng);
    } else if (u > 1) {
        intersectionPoint = new google.maps.LatLng(lineEnd.lat, lineEnd.lng);
    } else {
        intersectionPoint = new google.maps.LatLng(lineStart.lat + u * dX, lineStart.lng + u * dY);
    }

    var distance = google.maps.geometry.spherical.computeDistanceBetween(point, intersectionPoint);
    return distance;
}

// Hàm kiểm tra xem một điểm có nằm trong một vùng hay không
function isCircleIntersectPolygons(center, polygonPath, minDistance) {
    var point = new google.maps.LatLng(center);
    var polygon = new google.maps.Polygon({
        paths: polygonPath,
    })

    if (google.maps.geometry.poly.containsLocation(point, polygon)) {
        return true;
    }
    for (var i = 0; i < polygonPath.length; i++) {
        var lineStart = polygonPath[i];
        var lineEnd = polygonPath[((i + 1) % polygonPath.length)];
        var distance = distanceToLineSegment(center, lineStart, lineEnd);
        if (distance <= minDistance) {
            return true;
        }
    }
    return false;
}


// Hàm kiểm tra xem một điểm có vi phạm vùng cấm hay không
function getVungCamIntersection(point, listVungCam, khoangCach) {
    if (!listVungCam || !point || listVungCam.length <= 0) return;
    let arrVungCam = [];
    listVungCam.forEach(item => {
        if (!item.listToaDo || item.length < 0) return;
        if (isCircleIntersectPolygons(point, item.listToaDo, khoangCach)) {
            arrVungCam.push(item);
        }
    })
    return arrVungCam;
}


//Hàm kiểm tra hai circle có giao nhau không (radius đơn vị là m)
function isIntersectTwoCirCle(circle1, circle2) {
    if (!circle1 || !circle2) return false;
    const totalRadius = circle1.radius + circle2.radius;
    const distance = haversineDistance(circle1.location, circle2.location) * 1000;

    if (distance <= totalRadius) {
        return distance;
    } else {
        return -1;
    }
}

// Hàm kiểm tra 1 circle giao nhau với những cicle nào
function isIntersectAllCicle(circle, lstCircle) {
    if (!circle || !lstCircle || lstCircle.lengh <= 0) return null;
    let lstBTS = [];
    lstCircle.forEach(item => {
        const check = isIntersectTwoCirCle(circle, item.baseInfor);
        if (check && check !== -1) {
            item.distance = check;
            lstBTS.push(item);
        }
    });
    return lstBTS;
}


function getTableVungCam(lstVungCam) {
    if (!lstVungCam || lstVungCam.lengh <= 0) return;

    let strHtml =
        `<table>
        <tr>
            <th>STT</th>
            <th>Mã vùng</th>
            <th>Tên vùng</th>
            <th>Huyện</th>
            <th>Xã</th>
            <th>Địa chỉ</th>
            <th>Ghi chú</th>
        </tr>`
    let stt = 1;
    lstVungCam.forEach(item => {
        strHtml +=
            `<tr>
                <td class="text-center">${stt}</td>
                <td>${item.Ma}</td>
                <td>${item.Name}</td>
                <td>${item.TenHuyen != null ? item.TenHuyen : ""}</td>
                <td>${item.TenXa != null ? item.TenXa : ""}</td>
                <td>${item.DiaChi != null ? item.DiaChi : ""}</td>
                <td>${item.GhiChu != null ? item.GhiChu : ""}</td>
            </tr>
            `
        stt++;
    })
    strHtml += `</table>`

    return strHtml;
}

function getTableTramBTS(lstTramBTS) {
    if (!lstTramBTS || lstTramBTS.lengh <= 0) return;
    let strHtml =
        `<table>
        <tr>
            <th>STT</th>
            <th>Khoảng cách</th>
            <th>Mã trạm/Tên trạm</th>
            <th>Cột thu phát sóng</th>
            <th>Doanh nghiệp sở hữu</th>
            <th>Công nghệ phát sóng</th>
            <th>Bán kính phát sóng</th>
            <th>Tình trạng hoạt động</th>
        </tr>`
    let stt = 1;
    lstTramBTS.forEach(item => {
        if (item.detailInfor) {
            const detail = item.detailInfor;
            strHtml +=
                `<tr>
                    <td class="text-center">${stt}</td>
                    <td class="right">${item.distance}m</td>
                    <td>${detail.MaTram_TenTram}</td>
                    <td>${detail.CotBTStxt != null ? detail.CotBTStxt : ""}</td>
                    <td>${detail.DoanhNghieptxt != null ? detail.DoanhNghieptxt : ""}</td>
                    <td>${detail.CongNghePhatSong != null ? detail.CongNghePhatSong : ""}</td>
                    <td class="right">${detail.BanKinhPhatSong != null ? detail.BanKinhPhatSong : ""}${detail.DonViTinhPhuSong != null ? detail.DonViTinhPhuSong : ""}</td>
                    <td>${detail.TrangThaiHoatDongtxt != null ? detail.TrangThaiHoatDongtxt : ""}</td>
                </tr>`
            stt++;
        }
    })
    strHtml += `</table>`

    return strHtml;
}

function getModalDeXuatBTS(lstBTS, lstVungCam, point) {
    const tableBTS = getTableTramBTS(lstBTS);
    const tableVungCam = getTableVungCam(lstVungCam);

    let strHtml =
        `<div class="modal-dialog modal-dexuat">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <div class="center">
                        <h4 class="modal-title">Thông tin vị trí xây mới cột Anten</h4>
                    </div>
                </div>
                <div class="modal-body">`
                if (lstVungCam && lstVungCam.length > 0) {
                    strHtml += `<h4 class="red text-center"> Điểm xây dựng không hợp lệ <span class="sub-title-dexuat">(${point.lat.toFixed(6)}, ${point.lng.toFixed(6)})</span> </h4>`
                } else {
                    strHtml += `<h4 class="blue text-center"> Điểm xây dựng hợp lệ <span class="sub-title-dexuat">(${point.lat.toFixed(6)}, ${point.lng.toFixed(6) })</span></h4>`
                }

                strHtml += 
                   `<fieldset class="dotted-border">
                    <legend class="customer-lengend">
                        Vùng cấm phạm vào
                    </legend>
                            ${tableVungCam}
                    </fieldset>

                    <br>
                    <br>

                    <fieldset class="dotted-border">
                        <legend class="customer-lengend">
                            Thông tin trạm BTS giao với trạm BTS dựng mới
                        </legend>
                            ${tableBTS}
                    </fieldset>
                </div>
                <div class="modal-footer center">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                </div>
            </div>
        </div>`

    return strHtml;
}

