// Get the map data
mapData = Highcharts.geojson(Highcharts.maps["countries/vn/vn-all"]);
console.log(dataOfMapJS);

var chart = Highcharts.mapChart('map1', {
    chart: {
        map: 'countries/vn/vn-all'
    },

    title: {
        text: ''
    },

    subtitle: {
        text: ''
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
                 from: 0.3000,
                 to: 0.4000,
                 color: '#009bd8',
                 name: 'NHÓM 2 (0.3250 - 0.4000)'
             }, {
                 from: 0.2500,
                 to: 0.3000,
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
                    }
                }
            }
        }
    },
    series: [{
        data: dataOfMapJS,
        name: 'ĐIỂM CHUYỂN ĐỔI SỐ CÁC TỈNH',
        states: {
            //hover: {
            //    borderColor: '#c0392b',
            //    borderWidth: 5000,
            //    brightness: 0.1
            //},
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

//chart.renderer.text('Hoàng Sa', 350, 300).attr({
//    x: '55%',
//    y: '40%'
//})
//.css({
//    fontSize: '13px',
//    color: '#333333'
//})
//.add();
//chart.renderer.text('Trường Sa', 400, 600).attr({
//    x: '70%',
//    y: '75%'
//})
//.css({
//    fontSize: '13px',
//    color: '#333333'
//})
//.add();

chart.renderer.text('Côn Đảo - Vũng Tàu', 400, 600).attr({
    x: '30%',
    y: '90%'
})
.css({
    fontSize: '11px',
    color: '#333333'
})
.add();
//chart.renderer.text('Phú Quốc', 400, 600).attr({
//    x: '10%',
//    y: '82%'
//})
//.css({
//    fontSize: '13px',
//    color: '#333333'
//})
//.add();