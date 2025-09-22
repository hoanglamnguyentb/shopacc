// Get the map data
mapData = Highcharts.geojson(Highcharts.maps["countries/vn/vn-all"]);
console.log(dataOfMapJS);

var chart = Highcharts.mapChart('map1', {
    chart: {
        map: 'countries/vn/vn-all'
    },

    title: {
        text: 'TỔNG THỂ XẾP HẠNG CHỈ SỐ CHUYỂN ĐỔI SỐ CẤP TỈNH'
    },

    subtitle: {
        text: 'Nguồn: <a href="dti.gov.vn">Dti.gov.vn</a>'
    },

    mapNavigation: {
        enabled: false,
        buttonOptions: {
            verticalAlign: 'bottom'
        }
    },

    colorAxis: {
        dataClassColor: 'category',
        dataClasses: [
             {
                 from: 0.4000,
                 color: '#ff9d4d',
                 name: 'NHÓM 1 (> 0.4000)'
             }, {
                 from: 0.3250,
                 to: 0.4000,
                 color: '#009bd8',
                 name: 'NHÓM 2 (0.3250 - 0.4000)'
             }, {
                 from: 0.2500,
                 to: 0.3250,
                 color: '#8dcece',
                 name: 'NHÓM 3 (0.2500 - 0.3250)'
             },
        {
            from: 0,
            to: 0.2500,
            color: '#7288be',
            name: 'NHÓM 4 (< 0.2500)'
        }]
    },

    plotOptions: {
        series: {
            point: {
                events: {
                    click: function () {
                        const key = this["hc-key"];
                        $.ajax('/BAOCAOArea/BaoCaoChiSoChinh/DetailForDepartmentInMap', {
                            type: 'POST',  // http method
                            data: { code: key },  // data to submit
                            success: function (data, status, xhr) {
                                $('#detailSpiderChart #container_spider_home').html(data);
                                $("#detailSpiderChart").modal();
                                $('.modal-backdrop').remove();
                            },
                            error: function (jqXhr, textStatus, errorMessage) {
                                $('#detailSpiderChart #container_spider_home').html(errorMessage);
                                $("#detailSpiderChart").modal();
                            }
                        });
                    }
                }
            }
        }
    },
    series: [{
        data: dataOfMapJS,
        name: 'ĐIỂM CHUYỂN ĐỔI SỐ CÁC TỈNH',
        states: {
            hover: {
                borderColor: '#c0392b',
                borderWidth: 5000,
                brightness: 0.1
            },
            active: {
                borderColor: '#c0392b',
                color: 'red',
                borderWidth: 1,
                brightness: 0.2
            }
        },
        dataLabels: {
            enabled: true,
            format: '{point.name} ',
            color: 'black',
            style: {
               fontWeight: 'normal',
            }
        }
    }]
});

chart.renderer.text('QUẦN ĐẢO HOÀNG SA', 350, 300).attr({
    x: '55%',
    y: '40%'
})
.css({
    fontSize: '13px',
    color: '#333333'
})
.add();
chart.renderer.text('QUẦN ĐẢO TRƯỜNG SA', 400, 600).attr({
    x: '60%',
    y: '75%'
})
.css({
    fontSize: '13px',
    color: '#333333'
})
.add();