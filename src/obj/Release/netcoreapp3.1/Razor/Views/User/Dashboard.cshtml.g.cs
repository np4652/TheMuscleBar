#pragma checksum "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bba9c8a522108679354e5a5673b1c76379e95aff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Dashboard), @"mvc.1.0.view", @"/Views/User/Dashboard.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\_ViewImports.cshtml"
using TheMuscleBar.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bba9c8a522108679354e5a5673b1c76379e95aff", @"/Views/User/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5774e158c1a5a962cb4fe48861f93e529b2c1b4c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/dbstyle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "bba9c8a522108679354e5a5673b1c76379e95aff3677", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<!-- Styles -->
<style>
    #chartdiv {
        width: 100%;
        height: 500px;
        max-width: 100%;
        zoom: 76%
    }
</style>
<div class=""page-header"">
    <h1 class=""page-title"">Dashboard </h1>
    <div>
        <ol class=""breadcrumb"">
            <li class=""breadcrumb-item""><a href=""javascript:void(0)"">Home</a></li>
            <li class=""breadcrumb-item active"" aria-current=""page"">Dashboard </li>
        </ol>
    </div>
</div>
<div class=""row"">
    <div class=""col-lg-6 col-sm-12 col-md-6 col-xl-3 col-sm-mb-2"">
        <div class=""card overflow-hidden"">
            <div class=""card-body p-0 text-center"">
                <div class=""main-va users-count"">
                    <div class=""online-users"">
                        <div class=""col-nw"">
                            <h3 class=""fw-semibold"" id=""Users"">0</h3>
                            <p class=""fs-13 mb-0 text-muted"">Count</p>
                        </div>
                    </div>
                    <");
            WriteLiteral(@"div class=""offline-users"">
                        <div class=""top-icn dash"">
                            <h3 class=""fw-semibold"" id=""UsersAmnt"">0</h3>
                            <p class=""fs-13 mb-0 text-muted""> Amount</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-footer bg-dark"">
                <div class=""row"">
                    <div class=""col"">
                        <p class=""text-white mt-0 mb-0 fs-fot"">
                            <a href=""User/Index"" style=""color:white""> Users</a>
                        </p>
                    </div>
                </div>
            </div>
            <div class=""counter-icon dash ms-auto box-shadow-primary"">
                <i class=""fa fa-bar-chart""></i>
            </div>
        </div>
    </div>
");
            WriteLiteral(@"
    <div class=""col-lg-6 col-sm-12 col-md-6 col-xl-3 col-sm-mb-2"">
        <div class=""card overflow-hidden"">
            <div class=""card-body p-0 text-center"">
                <div class=""main-va users-count"">
                    <div class=""online-users"">
                        <div class=""col-nw"">
                            <h3 class=""fw-semibold"" id=""ActiveSub"">0</h3>
                            <p class=""fs-13 mb-0 text-muted"">Count </p>
                        </div>
                    </div>
                    <div class=""offline-users"">
                        <div class=""top-icn dash"">
                            <h3 class=""fw-semibold"" id=""ActiveSubAmnt"">0</h3>
                            <p class=""fs-13 mb-0 text-muted""> Amount</p>
                        </div>
                    </div>


                </div>
            </div>
            <div class=""card-footer bg-success"">
                <div class=""row"">
                    <div class=""col"">
                  ");
            WriteLiteral(@"      <p class=""text-white mb-0 mt-0 fs-fot"">
                            <a href=""Report/Subscriptions"" style=""color:white"">Active Subscription</a>
                        </p>
                    </div>
                </div>
            </div>
            <div class=""counter-icon dash ms-auto box-shadow-secondary"">
                <i class=""fa fa-cart-plus""></i>
            </div>
        </div>
    </div>
    <div class=""col-lg-6 col-sm-12 col-md-6 col-xl-3 col-sm-mb-2"">
        <div class=""card overflow-hidden"">
            <div class=""card-body p-0 text-center"">
                <div class=""main-va users-count"">
                    <div class=""online-users"">
                        <div class=""col-nw"">
                            <h3 class=""fw-semibold"" id=""ExpiredSub"">0</h3>
                            <p class=""fs-13 mb-0 text-muted"">Count</p>
                        </div>
                    </div>

                    <div class=""offline-users"">
                        <div cl");
            WriteLiteral(@"ass=""top-icn dash"">
                            <h3 class=""fw-semibold"" id=""ExpiredSubAmnt"">0</h3>
                            <p class=""fs-13 mb-0 text-muted""> Amount</p>
                        </div>
                    </div>

                </div>
            </div>
            <div class=""card-footer bg-danger"">
                <div class=""row"">
                    <div class=""col"">
                        <p class=""text-white mb-0 mt-0 fs-fot"">
                            <!--<span class=""icn-box text-danger fw-semibold fs-13 me-1"">
                                <i class=""fa fa-long-arrow-down""></i>
                                12%</span>-->
                            <a href=""Report/Subscriptions"" style=""color:white""> Expired Subscription</a>
                        </p>
                    </div>
                </div>
            </div>
            <div class=""counter-icon dash ms-auto box-shadow-secondary"">
                <i class=""fa fa-cart-plus""></i>
            </d");
            WriteLiteral(@"iv>
        </div>
    </div>
    <div class=""col-lg-6 col-sm-12 col-md-6 col-xl-3  col-sm-mb-2"">
        <div class=""card overflow-hidden"">
            <div class=""card-body p-0 text-center"">
                <div class=""main-va users-count"">
                    <div class=""online-users"">
                        <div class=""col-nw"">
                            <h3 class=""fw-semibold"" id=""aboutToExpired"">0</h3>
                            <p class=""fs-13 mb-0 text-muted"">Count</p>
                        </div>
                    </div>

                    <div class=""offline-users"">
                        <div class=""top-icn dash"">
                            <h3 class=""fw-semibold"" id=""HitsAmnt"">0</h3>
                            <p class=""fs-13 mb-0 text-muted""> Amount    </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-footer bg-warningg"">
                <div class=""row"">
                    <di");
            WriteLiteral(@"v class=""col"">
                        <p class=""text-white mb-0 mt-0 fs-fot"">
                            <a href=""Report/Subscriptions"" style=""color:white"">  About To Expired</a>
</p>
                    </div>
                </div>
            </div>
            <div class=""counter-icon dash ms-auto box-shadow-secondary"">
                <i class=""fa fa-cart-plus""></i>
            </div>
        </div>
    </div>
</div>
<div class=""row"">
    <div class=""col-md-8"">
        <div class=""card overflow-hidden mt-4"">
            <div class=""card-header"">
                <h3 class=""card-title"">Sales History</h3>
                <div class=""ms-auto"">
");
            WriteLiteral(@"                </div>
            </div>
            <div class=""card-body "">
                <div id=""chartdiv""></div>
            </div>
        </div>
    </div>
    <div class=""col-md-4"">
        <div class=""card overflow-hidden mt-4"">
            <div class=""card-header"">
                <h3 class=""card-title"">Sales History</h3>

            </div>
            <div class=""card-body"">
                <canvas class=""chart"" id=""myChart"" width=""300"" height=""300""></canvas>
            </div>
            <div class=""card__footer"">
                <div class=""card__footer-section"">
                    <div class=""footer-section__data"" id=""Total"">0</div>
                    <div class=""footer-section__label"">Total</div>
                </div>
                <div class=""card__footer-section"">
                    <div class=""footer-section__data"" id=""Remaining"">0</div>
                    <div class=""footer-section__label"">Remaining</div>
                </div>
                <div class");
            WriteLiteral(@"=""card__footer-section"">
                    <div class=""footer-section__data"" id=""Expiry""></div>
                    <div class=""footer-section__label"">Consumed</div>
                </div>
            </div>
        </div>
    </div>
</div>




");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://cpwebassets.codepen.io/assets/common/stopExecutionOnTimeout-1b93190375e9ccc259df3a57c1abc0e64599724ae30d7ea4c6877eb615f89387.js""></script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js'></script>

    <script src=""https://cdn.amcharts.com/lib/5/index.js""></script>
    <script src=""https://cdn.amcharts.com/lib/5/xy.js""></script>
    <script src=""https://cdn.amcharts.com/lib/5/themes/Animated.js""></script>
    <script>
        $(function () {
            $.post('/Report/GetDashboardSummery').done((result) => {
                console.log(result);
                $('#Users').text(result.totalUser);
                $('#totalSubscription').text(result.totalSubscription);
                $('#ActiveSub').text(result.activeSubscription);
                $('#ExpiredSub').text(result.expiredSubscription);
                $('#aboutToExpired').text(result.aboutToExpired);
            });
        })
    </script>
    <script id=""Script1"">
");
                WriteLiteral(@"        $.post('ExpiredActive').done((result) => {
            $('#Total').text(result.total);
            $('#Remaining').text(result.remaining);
            $('#Expiry').text(result.total - result.remaining);
            var ctx = document.getElementById('myChart');
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ['Consumed', 'Remaining'],
                    datasets: [{
                        label: '# of Votes',
                        data: [result.consumed, result.remaining],
                        backgroundColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(75, 192, 192, 1)']
                    }]
                }
            });
        });
        var GetPieChartData = (type) => {
            $.post('PieStatus', { Type: type }).done((result) => {
                var ctx = $('#myPieChart');
                //ctx.empty();
                var myChart =");
                WriteLiteral(@" new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: [
                            'Success',
                            'Pending',
                            'Failed'
                        ],
                        datasets: [{
                            label: 'My First Dataset',
                            data: [result.success, result.pending, result.failed],
                            backgroundColor: [
                                'rgb(0, 200, 81)',
                                'rgb(255, 136, 0)',
                                'rgb(255, 53, 71)'
                            ],
                            hoverOffset: 4
                        }]
                    }
                });
            });
        }
    </script>
    <script>

        am5.ready(async function () {
            // Create root element
            // https://www.amcharts.com/docs/v5/getting-started/#Root_element
            var r");
                WriteLiteral(@"oot = am5.Root.new(""chartdiv"");
            // Set themes
            // https://www.amcharts.com/docs/v5/concepts/themes/
            root.setThemes([
                am5themes_Animated.new(root)
            ]);
            // Create chart
            // https://www.amcharts.com/docs/v5/charts/xy-chart/
            var chart = root.container.children.push(am5xy.XYChart.new(root, {
                panX: true,
                panY: true,
                pinchZoomX: true
            }));
            // Add cursor
            // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
            var cursor = chart.set(""cursor"", am5xy.XYCursor.new(root, {
                behavior: ""none""
            }));
            cursor.lineY.set(""visible"", false);


            var generateDatas = async () => {
                var data = [];
                await $.post('UserDashboardChart').done((result) => {
                    for (let i = 0; i < result.length; ++i) {
                        data.");
                WriteLiteral(@"push({ date: result[i].entryOn, value: result[i].successTransaction });
                    }
                });
                return data;
            }


            // Create axes
            // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
            var xAxis = chart.xAxes.push(am5xy.DateAxis.new(root, {
                maxDeviation: 0.2,
                baseInterval: {
                    timeUnit: ""day"",
                    count: 1
                },
                renderer: am5xy.AxisRendererX.new(root, {}),
                tooltip: am5.Tooltip.new(root, {})
            }));

            var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                renderer: am5xy.AxisRendererY.new(root, {})
            }));


            // Add series
            // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
            var series = chart.series.push(am5xy.LineSeries.new(root, {
                name: ""Series"",
                xAxis: xAxis,
         ");
                WriteLiteral(@"       yAxis: yAxis,
                valueYField: ""value"",
                valueXField: ""date"",
                tooltip: am5.Tooltip.new(root, {
                    labelText: ""{valueY}""
                })
            }));


            // Set data
            var data = await generateDatas();
            series.data.setAll(data);

        });
    </script>
");
            }
            );
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591