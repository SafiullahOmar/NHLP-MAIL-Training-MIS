Highcharts.theme = {
    colors: [
        // Main Colours
        '#1E73AA', '#C44441', '#55BF3B', '#7A5992',
        '#009AB2', '#F08736', '#8EAACF', '#CE7057', '#737373'
        /* '#2691d9', '#cf6563', '#79cf63', '#9d81b1', '#33e4ff', '#f39b58', '#a6bcd9', '#d78a75', '#a6a6a6' */
    ],
    legend: {
        itemStyle: {
            font: '9pt Trebuchet MS, Verdana, sans-serif',
            color: 'black'
        },
        itemHoverStyle: {
            color: 'gray'
        }
    },
    lang: {
        decimalPoint: ',',
        thousandsSep: ' '
    }
};

// Apply the theme:
Highcharts.setOptions(Highcharts.theme);