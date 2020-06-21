using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJs.Blazor.Charts;
using ChartJs.Blazor.ChartJS.Common;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.LineChart;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.Util;

namespace MathDungeonBlazor.Pages.Bases
{
    public class LinearFunctionBase : ComponentBase
    {

        /// <summary>
        /// This is a point for the chart
        /// </summary>
        public class XandFPoint
        {
            public double X { get; set; }
            public double Y { get; set; }

            public XandFPoint(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        /// <summary>
        /// This is a list of points for the graph
        /// </summary>
        protected List<XandFPoint> xandFPoints = new List<XandFPoint>();

        /// <summary>
        /// a from [y = a + bx] function
        /// </summary>
        protected double a;
        /// <summary>
        /// b from [y = a + bx] function
        /// </summary>
        protected double b;
        

        /// <summary>
        /// Range for how many XandFPoint will be generated for given [y = a + bx] function
        /// </summary>
        protected int range = 0;

        protected bool isCalculated = false;

        /// <summary>
        /// This funtion will calculate [y = a + bx] with given data and range
        /// </summary>
        public async void CalculateLinearFunction()
        {
            if(range != 0)
            {
                isCalculated = true;

                for (int i = 0; i <= range; i++)
                {
                    double y = a + (b * i);
                    XandFPoint obj = new XandFPoint(i, y);
                    xandFPoints.Add(obj);
                }

                await UpdateChart();
            }
        }

        /// <summary>
        /// This funtion will reset calculate linear function program
        /// </summary>
        public async void Reset()
        {
            isCalculated = false;
            _XandFx.RemoveRange(0, xandFPoints.Count);
            xandFPoints.Clear();
            a = 0;
            b = 0;
            range = 0;
            await UpdateChart();
        }


        /// <summary>
        /// Declaring configuration for Line Chart
        /// </summary>
        protected LineConfig _lineConfig;
        /// <summary>
        /// Declaring Line Chart
        /// </summary>
        protected ChartJsLineChart _lineChartJs;

        private LineDataset<Point> _XandFx;


        protected override async Task OnInitializedAsync()
        {
            _lineConfig = new LineConfig
            {
                Options = new LineOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = false,
                    },
                    
                    Legend = new Legend
                    {
                        Position = Position.Right,
                        Labels = new LegendLabelConfiguration
                        {
                            UsePointStyle = false,
                        }

                    },
                    Tooltips = new Tooltips
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = false
                    },


                    Scales = new Scales
                    {
                        xAxes = new List<CartesianAxis>
                    {
                        new LinearCartesianAxis
                        {
                            ID = "My ID",
                            
                            ScaleLabel = new ScaleLabel
                            {
                                LabelString = "X",
                                FontColor = ColorUtil.ColorString(255, 255, 255, 1.0)

                            },
                            GridLines = new GridLines
                            {
                                Display = true,
                                ZeroLineWidth = 3,
                                ZeroLineColor = ColorUtil.ColorString(0, 0, 0, 1.0),

                            },
                            Ticks = new LinearCartesianTicks
                            {
                                SuggestedMin = 5,
                                
                            },
                        }
                    },
                        yAxes = new List<CartesianAxis>
                    {
                        new LinearCartesianAxis
                        {
                            ScaleLabel = new ScaleLabel
                            {
                                LabelString = "F(x)",
                                FontColor = ColorUtil.ColorString(255, 255, 255, 1.0)

                            },
                            GridLines = new GridLines
                            {
                                Display = true,
                                ZeroLineWidth = 3,
                                ZeroLineColor = ColorUtil.ColorString(0, 0, 0, 1.0)
                            },
                        }
                    }
                    },
                    Hover = new LineOptionsHover
                    {
                        Intersect = true,
                        Mode = InteractionMode.Y
                    }
                    
                }
            };

            _XandFx = new LineDataset<Point>
            {
                BackgroundColor = ColorUtil.ColorString(0, 255, 0, 1.0),
                BorderColor = ColorUtil.ColorString(0, 0, 255, 1.0),
                Label = "Function F(x)",
                Fill = false,
                PointBackgroundColor = ColorUtil.RandomColorString(),
                BorderWidth = 1,
                PointRadius = 3,
                PointBorderWidth = 1,
                SteppedLine = SteppedLine.False,
                
            };


            _XandFx.AddRange(xandFPoints.Select(p => new Point(p.X, p.Y)));
            _lineConfig.Data.Datasets.Add(_XandFx);
        }

        protected async Task UpdateChart()
        {

            if(xandFPoints.Count != 0)
            {
                for (int i = 0; i < xandFPoints.Count; i++)
                {
                    var obj = xandFPoints[i];

                    _XandFx.Add(new Point(obj.X, obj.Y));
                }
            }
            
            await _lineChartJs.Update();
        }
    }
}
