$(function () {

  'use strict';
    
    
    var ts2 = 1484418600000;
    var dates = [];    
    var spikes = [5, -5, 3, -3, 8, -8]
    for (var i = 0; i < 120; i++) {
      ts2 = ts2 + 86400000;
      var innerArr = [ts2, dataSeries[1][i].value];
      dates.push(innerArr)
    }


    var options = {
      chart: {
        type: 'area',
        stacked: false,
        height: 418,
        zoom: {
          type: 'x',
          enabled: true
        },
        toolbar: {
          show: false,
          autoSelected: 'zoom'
        }
      },
      colors: ['#4d79f6'],
      dataLabels: {
        enabled: false
      },
      series: [{
        name: 'Bitcoin',
        data: dates
      }],
      markers: {
        size: 0,
      },
      // title: {
      //   text: 'Stock Price Movement',
      //   align: 'left'
      // },
      fill: {
        type: 'gradient',
        gradient: {
          shadeIntensity: 1,
          inverseColors: true,
          opacityFrom: 0.5,
          opacityTo: 0,
          stops: [0, 90, 100]
        },
      },
      yaxis: {
        min: 20000000,
        max: 250000000,
        labels: {
          formatter: function (val) {
            return "$" + (val / 1000000).toFixed(0);
          },
        },
        title: {
          text: 'Price'
        },
      },
      xaxis: {
        type: 'datetime',
        axisBorder: {
          show: true,
          color: '#bec7e0',
        },  
        axisTicks: {
          show: true,
          color: '#bec7e0',
        },    
      },

      tooltip: {
        shared: false,
        y: {
          formatter: function (val) {
            return "$" + (val / 1000000).toFixed(0)
          }
        }
      }
    }

    var chart = new ApexCharts(
      document.querySelector("#crypto_dash_main"),
      options
    );

    chart.render();






      var options = {
        series: [86],
        chart: {
        height: 160,
         width: 100,
        type: 'radialBar',
        offsetY: 0
      },
      colors:['#f0d218'],
      plotOptions: {
        radialBar: {
          startAngle: -135,
          endAngle: 180,
          dataLabels: {
            name: {
              fontSize: '16px',
              color: undefined,
              offsetY: 30
            },
            value: {
              offsetY: 0,
              fontSize: '22px',
              color: undefined,
              formatter: function (val) {
                return val + "";
              }
            }
          }
        }
      },
      fill: {
        type: 'gradient',
        gradient: {
            shade: 'light',
            shadeIntensity: 0.15,
            inverseColors: false,
            opacityFrom: 1,
            opacityTo: 1,
            stops: [0, 50, 65, 91]
        },
      },
      stroke: {
        dashArray: 0
      },
      labels: ['°F'],
      };

      var chart = new ApexCharts(document.querySelector("#gauge-chart"), options);
      chart.render();
    



function generateData(baseval, count, yrange) {
  var i = 0;
  var series = [];
  while (i < count) {
    //var x =Math.floor(Math.random() * (750 - 1 + 1)) + 1;;
    var y =
      Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;
    var z = Math.floor(Math.random() * (75 - 15 + 1)) + 15;

    series.push([baseval, y, z]);
    baseval += 8640000;
    i++;
  }
  console.log(series);
  return series;
}

var options = {
  series: [
    {
      name: "Power",
      // data: generateData(new Date("11 Feb 2026 GMT").getTime(), 20, {
      //   min: 10,
      //   max: 60
      // })
      data: [
        [1486775200000, 30, 40],
        [1486779840000, 47, 23],
        [1486789490000, 30, 59],
        [1486797120000, 16, 57],
        [1486805760000, 30, 40],
        [1486812300000, 35, 30]
      ]
    },
    {
      name: "Energy",
      // data: generateData(new Date("11 Feb 2026 GMT").getTime(), 20, {
      //   min: 10,
      //   max: 60
      // })
      data: [
        [1486779200000, 55, 30],
        [1486779840000, 15, 23],
        [1486788480000, 10, 59],
        [1486797120000, 55, 57],
        [1486805760000, 35, 40],
        [1486814200000, 55, 30]
      ]
    },
  ],
  chart: {
    height: 255,
    type: "bubble",
    toolbar: {
      show: true,
      tools: {
        download: false,
        zoomin: true,
        zoomout: true,
        pan: false,
        reset: false | '<img src="/static/icons/reset.png" width="20">',
      },
    },
    dropShadow: {
      enabled: false,
      top: 0,
      left: 0,
      blur: 0,
      opacity: 1,
      color: ["#ff9920", "#fec800"]
    }
  },
  dataLabels: {
    enabled: false
  },
  fill: {
    type: "gradient"
  },
  // title: {
  //   text: "3D Bubble Chart"
  // },
  xaxis: {
    tickAmount: 12,
    type: "datetime",
    labels: {
      rotate: 0
    }
  },
  yaxis: {
    forceNiceScale: true
  },
  // theme: {
  //   palette: "palette2"
  // },
  colors: ["#ff9920", "#fec800"],
  grid: {
    show: false,
    xaxis: {
      lines: {
        show: false
      }
    },
    yaxis: {
      lines: {
        show: false
      }
    }
  }
  // plotOptions: {
  //   bubble: {
  //     maxBubbleRadius: 1150
  //   }
  // }
};

var chart = new ApexCharts(document.querySelector("#bubble-chart"), options);
chart.render();



    var options = {
        series: [{
                name: "Energy Produced",
                data: [280, 220, 230, 180, 220, 330, 170]
            },
            {
                name: "Energy Consumption",
                data: [120, 390, 140, 360, 450, 200, 430]
            }
        ],
        chart: {
            height: 360,
            type: 'area',
            foreColor: "#bac0c7",
            dropShadow: {
                enabled: true,
                color: '#000',
                top: 18,
                left: 7,
                blur: 10,
                opacity: 0.2
            },
            toolbar: {
                show: false
            }
        },
        colors: ['#ff9920', '#fec800'],
        dataLabels: {
            enabled: false,
        },
        stroke: {
            width: 2,
            curve: 'smooth'
        },
        grid: {
            borderColor: '#e7e7e7',
        },
        fill: {
            type: 'gradient',
            gradient: {
                shade: 'light',
                type: "vertical",
                shadeIntensity: 0.5,
                gradientToColors: ['#ff9920', '#ff562f'], // optional, if not defined - uses the shades of same color in series
                inverseColors: true,
                opacityFrom: 0.5,
                opacityTo: 0,
                stops: [0, 100],
            }
        },
        xaxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
        },
        legend: {
            show: true,
            position: 'bottom',
        }
    };

    var chart = new ApexCharts(document.querySelector("#recovery_statistics"), options);
    chart.render();






       var options = {
        series: [{
            name: 'Electricity',
            data: [56, 24, 65, 31, 37, 39, 62]
        }],
        chart: {
      foreColor:"#ffffff",
          height: 300,
          type: 'bar',
          zoom: {
            enabled: false
          }
        },
    colors:['#f0d218'],
        dataLabels: {
          enabled: false,
        },
        stroke: {
          show: false,
          curve: 'smooth',
          lineCap: 'butt',
          width: 1,
          dashArray: 0, 
        },    
        markers: {
          size: 0,
          colors: '#f0d218',
          strokeColors: '#f0d218',
          strokeWidth: 3,
          strokeOpacity: 0.9,
          strokeDashArray: 0,
          fillOpacity: 1,
          discrete: [],
          shape: "circle",
          radius: 5,
          offsetX: 0,
          offsetY: 0,
          onClick: undefined,
          onDblClick: undefined,
          hover: {
            size: undefined,
            sizeOffset: 3
          }
        },  
        grid: {
          borderColor: '#f7f7f7', 
          row: {
            colors: ['transparent'], // takes an array which will be repeated on columns
            opacity: 0
          },      
          yaxis: {
            lines: {
              show: false,
            },
          },
        },
        fill: {
          type: "gradient",
          gradient: {
              shade: 'light',
              type: "vertical",
              shadeIntensity: 0.25,
              gradientToColors: undefined,
              inverseColors: true,
              opacityFrom: 1,
              opacityTo: 0.01,
              stops: [0, 100, 100]
            },
          },
        xaxis: {
          categories: ['Jan','Feb','Mar','Apr','May','Jun','Jul'],
          labels: {
            show: true,        
          },
          axisBorder: {
            show: true
          },
          axisTicks: {
            show: true
          },
          tooltip: {
            enabled: false,        
          },
        },
        yaxis: {
          labels: {
            show: true,
            formatter: function (val) {
              return val + " kw";
            }
          }
        },
      };
      var chart = new ApexCharts(document.querySelector("#charts_widget_2_chart"), options);
      chart.render();








       var options = {
        series: [{
            data: [56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27]
        }],
        chart: {
      foreColor:"#bac0c7",
          height: 261,
          type: 'area',
          zoom: {
            enabled: false
          }
        },
    colors:['#FF6C6C'],
        dataLabels: {
          enabled: false,
        },
        stroke: {
            show: true,
      curve: 'smooth',
      lineCap: 'butt',
      width: 1,
      dashArray: 0, 
        },    
    markers: {
      size: 0,
      colors: '#FF6C6C',
      strokeColors: '#ffffff',
      strokeWidth: 3,
      strokeOpacity: 0.9,
      strokeDashArray: 0,
      fillOpacity: 1,
      discrete: [],
      shape: "circle",
      radius: 5,
      offsetX: 0,
      offsetY: 0,
      onClick: undefined,
      onDblClick: undefined,
      hover: {
        size: undefined,
        sizeOffset: 3
      }
    },  
        grid: {
      borderColor: '#f7f7f7', 
          row: {
            colors: ['transparent'], // takes an array which will be repeated on columns
            opacity: 0
          },      
      yaxis: {
      lines: {
        show: true,
      },
      },
        },
    fill: {
      type: "gradient",
      gradient: {
        shadeIntensity: 1,
        opacityFrom: 0.01,
        opacityTo: 1,
        colors: ['transparent'],
        stops: [0, 90, 100]
      }
      },
        xaxis: {
          categories: [
            'Jan',
            'Feb',
            'Mar',
            'Apr',
            'May',
            'Jun',
            'Jul',
            'Aug',
            'Sep',
            'Oct',
            'Nov',
            'Dec',
          ],
      labels: {
      show: true,        
          },
          axisBorder: {
            show: true
          },
          axisTicks: {
            show: true
          },
          tooltip: {
            enabled: true,        
          },
        },
        yaxis: {
          labels: {
            show: true,
            formatter: function (val) {
              return val + "K";
            }
          }
        
        },
      };
      var chart = new ApexCharts(document.querySelector("#charts_widget_3_chart"), options);
      chart.render();





          var options = {
    chart: {
      height: 261,
      type: 'bar',
      toolbar: {
        show: false
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        endingShape: 'rounded',
        columnWidth: '40%',
        distributed: true,
      },
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      show: true,
      colors: ['transparent']
    },
    colors:['#FF6C6C'],
    series: [{
      name: 'Yield/kWh',
      data: [20, 45, 51, 58, 59, 58, 61, 30, 35, 61, 48, 39, 68, 41, 41,20, 45, 51, 58, 59, 58, 61, 30, 35, 61, 48, 39, 68, 41, 41]
    },],
    xaxis: {
      categories: ['1','2','3','4','5','6','7','8','9','10','11','12','13','14','15','16','17','18','19','20','21','22','23','24','25','26','27','28','29','30',],
      axisBorder: {
      show: true,
      color: '#bec7e0',
      },  
      axisTicks: {
      show: true,
      color: '#bec7e0',
      },    
    },
    legend: {
          show: false,
        },
    fill: {
      opacity: 1

    },
    grid: {
      row: {
        colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
        opacity: 0.2
      },
      borderColor: '#f1f3fa'
    },
    tooltip: {
      y: {
        formatter: function (val) {
          return val + ""
        }
      }
    }
  }

  var chart = new ApexCharts(
    document.querySelector("#yearly-comparison"),
    options
  );

  chart.render();




    
    
    
    
}); // End of use strict



Highcharts.chart('container', {

    chart: {
        type: 'gauge',
        plotBackgroundColor: null,
        plotBackgroundImage: null,
        plotBorderWidth: 0,
        plotShadow: false,
        height: '300'
    },

    title: {
        text: ''
    },

    pane: {
        startAngle: -90,
        endAngle: 89.9,
        background: null,
        center: ['50%', '75%'],
        size: '110%'
    },

    // the value axis
    yAxis: {
        min: 0,
        max: 100,
        tickPixelInterval: 72,
        tickPosition: 'inside',
        tickColor: 'var(--highcharts-background-color, #FFFFFF)',
        tickLength: 20,
        tickWidth: 2,
        minorTickInterval: null,
        labels: {
            distance: 20,
            style: {
                fontSize: '14px'
            }
        },
        lineWidth: 0,
        plotBands: [{
            from: 0,
            to: 60,
            color: '#04a08b', // green
            thickness: 20,
            borderRadius: '50%'
        }, {
            from: 60,
            to: 80,
            color: '#ff9920', // yellow
            thickness: 20,
            borderRadius: '50%'
        }, {
            from: 80,
            to: 100,
            color: '#ff562f', // red
            thickness: 20,
            borderRadius: '50%'
        }]
    },

    series: [{
        name: 'Speed',
        data: [80],
        tooltip: {
            valueSuffix: ' KW'
        },
        dataLabels: {
            format: '{y} KW',
            borderWidth: 0,
            color: (
                Highcharts.defaultOptions.title &&
                Highcharts.defaultOptions.title.style &&
                Highcharts.defaultOptions.title.style.color
            ) || '#333333',
            style: {
                fontSize: '16px'
            }
        },
        dial: {
            radius: '80%',
            backgroundColor: 'gray',
            baseWidth: 12,
            baseLength: '0%',
            rearLength: '0%'
        },
        pivot: {
            backgroundColor: 'gray',
            radius: 6
        }

    }]

});

// Add some life
setInterval(() => {
    const chart = Highcharts.charts[0];
    if (chart && !chart.renderer.forExport) {
        const point = chart.series[0].points[0],
            inc = Math.round((Math.random() - 0.5) * 20);

        let newVal = point.y + inc;
        if (newVal < 0 || newVal > 200) {
            newVal = point.y - inc;
        }

        point.update(newVal);
    }

}, 3000);



