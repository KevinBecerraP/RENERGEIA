// Helper para crear gráficos con Chart.js

window.chartInstances = {};

// Destruir gráfico existente si existe
window.destroyChart = function(canvasId) {
    if (window.chartInstances[canvasId]) {
        window.chartInstances[canvasId].destroy();
        delete window.chartInstances[canvasId];
    }
};

// Crear Curva S (líneas)
window.createCurvaSChart = function(canvasId, labels, dataProgramada, dataEjecutada) {
    window.destroyChart(canvasId);

    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Programado',
                    data: dataProgramada,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    backgroundColor: 'rgba(75, 192, 192, 0.1)',
                    borderWidth: 3,
                    tension: 0.4,
                    fill: true
                },
                {
                    label: 'Ejecutado',
                    data: dataEjecutada,
                    borderColor: 'rgba(255, 99, 132, 1)',
                    backgroundColor: 'rgba(255, 99, 132, 0.1)',
                    borderWidth: 3,
                    tension: 0.4,
                    fill: true
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: true,
                    position: 'top'
                },
                tooltip: {
                    mode: 'index',
                    intersect: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100,
                    ticks: {
                        callback: function(value) {
                            return value + '%';
                        }
                    }
                }
            }
        }
    });
};

// Crear gráfico de barras por disciplina
window.createDisciplinaChart = function(canvasId, labels, dataProgramada, dataEjecutada) {
    window.destroyChart(canvasId);

    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Programado',
                    data: dataProgramada,
                    backgroundColor: 'rgba(75, 192, 192, 0.7)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                },
                {
                    label: 'Ejecutado',
                    data: dataEjecutada,
                    backgroundColor: 'rgba(255, 99, 132, 0.7)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: true,
                    position: 'top'
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100,
                    ticks: {
                        callback: function(value) {
                            return value + '%';
                        }
                    }
                }
            }
        }
    });
};

// Crear gráfico de dona (estado de actividades)
window.createDonutChart = function(canvasId, labels, data, colors) {
    window.destroyChart(canvasId);

    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: colors,
                borderWidth: 2,
                borderColor: '#fff'
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom'
                }
            }
        }
    });
};

// Crear gráfico de área apilada (histogramas) - Ultra moderno y colorido
window.createStackedAreaChart = function(canvasId, labels, datasets) {
    window.destroyChart(canvasId);

    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: datasets
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: {
                mode: 'index',
                intersect: false
            },
            plugins: {
                legend: {
                    display: true,
                    position: 'top',
                    align: 'start',
                    labels: {
                        padding: 20,
                        font: {
                            size: 13,
                            family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                            weight: '600'
                        },
                        usePointStyle: true,
                        pointStyle: 'rectRounded',
                        boxWidth: 15,
                        boxHeight: 15
                    }
                },
                tooltip: {
                    mode: 'index',
                    intersect: false,
                    backgroundColor: 'rgba(255, 255, 255, 0.95)',
                    titleColor: '#1e293b',
                    bodyColor: '#475569',
                    borderColor: '#e2e8f0',
                    borderWidth: 2,
                    titleFont: {
                        size: 15,
                        weight: 'bold'
                    },
                    bodyFont: {
                        size: 14
                    },
                    padding: 16,
                    cornerRadius: 12,
                    displayColors: true,
                    boxWidth: 12,
                    boxHeight: 12,
                    boxPadding: 6,
                    callbacks: {
                        labelColor: function(context) {
                            return {
                                borderColor: context.dataset.borderColor,
                                backgroundColor: context.dataset.backgroundColor,
                                borderWidth: 2,
                                borderRadius: 4
                            };
                        },
                        footer: function(tooltipItems) {
                            let sum = 0;
                            tooltipItems.forEach(function(tooltipItem) {
                                sum += tooltipItem.parsed.y;
                            });
                            return '━━━━━━━━━━\nTotal: ' + sum.toFixed(1);
                        }
                    },
                    footerFont: {
                        size: 14,
                        weight: 'bold'
                    },
                    footerColor: '#1e293b'
                }
            },
            scales: {
                x: {
                    stacked: true,
                    grid: {
                        display: false
                    },
                    ticks: {
                        font: {
                            size: 12,
                            weight: '600'
                        },
                        color: '#475569',
                        padding: 8
                    }
                },
                y: {
                    stacked: true,
                    beginAtZero: true,
                    grid: {
                        color: 'rgba(148, 163, 184, 0.15)',
                        drawBorder: false,
                        lineWidth: 2
                    },
                    ticks: {
                        font: {
                            size: 12,
                            weight: '600'
                        },
                        color: '#475569',
                        padding: 8,
                        callback: function(value) {
                            return value;
                        }
                    }
                }
            },
            elements: {
                line: {
                    tension: 0.4,
                    borderWidth: 3
                },
                point: {
                    radius: 0,
                    hitRadius: 12,
                    hoverRadius: 8,
                    hoverBorderWidth: 3
                }
            },
            animation: {
                duration: 1200,
                easing: 'easeInOutCubic'
            }
        }
    });
};
