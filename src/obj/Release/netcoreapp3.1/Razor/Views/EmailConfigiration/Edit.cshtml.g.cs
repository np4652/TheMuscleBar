#pragma checksum "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd7722a9b3bdf730eb96e95eee05ddef02c2262c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmailConfigiration_Edit), @"mvc.1.0.view", @"/Views/EmailConfigiration/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd7722a9b3bdf730eb96e95eee05ddef02c2262c", @"/Views/EmailConfigiration/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5774e158c1a5a962cb4fe48861f93e529b2c1b4c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_EmailConfigiration_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TheMuscleBar.AppCode.Reops.Entities.EmailConfig>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmproduct"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/EmailConfigiration/Save"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!-- Row start -->\r\n<div class=\"row gutters\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fd7722a9b3bdf730eb96e95eee05ddef02c2262c4277", async() => {
                WriteLiteral("\r\n        <div class=\"col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12\">\r\n            <div class=\"card\">\r\n                <div class=\"card-body\">\r\n                    <div class=\"row gutters\">\r\n                        <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 422, "\"", 439, 1);
#nullable restore
#line 12 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 430, Model.Id, 430, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("name", " name=\"", 440, "\"", 464, 1);
#nullable restore
#line 12 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 447, nameof(Model.Id), 447, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"inputName\">");
#nullable restore
#line 15 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
                                                  Write(nameof(Model.UserId));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                                <input type=\"text\"");
                BeginWriteAttribute("name", " name=\"", 709, "\"", 737, 1);
#nullable restore
#line 16 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 716, nameof(Model.UserId), 716, 21, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 738, "\"", 759, 1);
#nullable restore
#line 16 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 746, Model.UserId, 746, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 781, "\"", 816, 1);
#nullable restore
#line 16 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 795, nameof(Model.UserId), 795, 21, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"inputName\">");
#nullable restore
#line 21 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
                                                  Write(nameof(Model.EmailFrom));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                                <input type=\"text\"");
                BeginWriteAttribute("name", " name=\"", 1132, "\"", 1163, 1);
#nullable restore
#line 22 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1139, nameof(Model.EmailFrom), 1139, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 1164, "\"", 1188, 1);
#nullable restore
#line 22 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1172, Model.EmailFrom, 1172, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 1210, "\"", 1248, 1);
#nullable restore
#line 22 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1224, nameof(Model.EmailFrom), 1224, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"inputName\">");
#nullable restore
#line 27 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
                                                  Write(nameof(Model.Password));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                                <input type=\"text\"");
                BeginWriteAttribute("name", " name=\"", 1563, "\"", 1593, 1);
#nullable restore
#line 28 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1570, nameof(Model.Password), 1570, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 1594, "\"", 1617, 1);
#nullable restore
#line 28 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1602, Model.Password, 1602, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 1639, "\"", 1676, 1);
#nullable restore
#line 28 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1653, nameof(Model.Password), 1653, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"inputName\">");
#nullable restore
#line 33 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
                                                  Write(nameof(Model.HostName));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                                <input type=\"text\"");
                BeginWriteAttribute("name", " name=\"", 1991, "\"", 2021, 1);
#nullable restore
#line 34 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 1998, nameof(Model.HostName), 1998, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 2022, "\"", 2045, 1);
#nullable restore
#line 34 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2030, Model.HostName, 2030, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2067, "\"", 2104, 1);
#nullable restore
#line 34 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2081, nameof(Model.HostName), 2081, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"inputName\">");
#nullable restore
#line 39 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
                                                  Write(nameof(Model.Port));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                                <input type=\"text\"");
                BeginWriteAttribute("name", " name=\"", 2415, "\"", 2441, 1);
#nullable restore
#line 40 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2422, nameof(Model.Port), 2422, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 2442, "\"", 2461, 1);
#nullable restore
#line 40 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2450, Model.Port, 2450, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2483, "\"", 2516, 1);
#nullable restore
#line 40 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2497, nameof(Model.Port), 2497, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col-sm-12\">\r\n                            <div class=\"form-group\">\r\n                                <label for=\"inputName\">");
#nullable restore
#line 45 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
                                                  Write(nameof(Model.EnableSSL));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                                <input type=\"checkbox\"");
                BeginWriteAttribute("name", " name=\"", 2836, "\"", 2867, 1);
#nullable restore
#line 46 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2843, nameof(Model.EnableSSL), 2843, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 2868, "\"", 2892, 1);
#nullable restore
#line 46 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2876, Model.EnableSSL, 2876, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2914, "\"", 2952, 1);
#nullable restore
#line 46 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\Edit.cshtml"
WriteAttributeValue("", 2928, nameof(Model.EnableSSL), 2928, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                            </div>
                        </div>
                        <div class=""col-xl-2 col-lglg-2 col-md-2 col-sm-2 col-12"">
                            <div class=""form-group"">
                                <button class=""btn btn-lg btn-primary"" id=""btnSubmit"" type=""submit"">Add</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TheMuscleBar.AppCode.Reops.Entities.EmailConfig> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591