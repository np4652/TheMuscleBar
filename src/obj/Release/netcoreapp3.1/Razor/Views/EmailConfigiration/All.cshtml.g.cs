#pragma checksum "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d3b9a295fa32e7a39addebfee0f0e33ad455a69"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmailConfigiration_All), @"mvc.1.0.view", @"/Views/EmailConfigiration/All.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d3b9a295fa32e7a39addebfee0f0e33ad455a69", @"/Views/EmailConfigiration/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5774e158c1a5a962cb4fe48861f93e529b2c1b4c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_EmailConfigiration_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<TheMuscleBar.AppCode.Reops.Entities.EmailConfig>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
  
    int sr = 0;
    foreach (var item in Model)
    {
        sr++;

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr data-config-id=\"");
#nullable restore
#line 7 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                       Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n            <td>");
#nullable restore
#line 8 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
           Write(sr);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"text-ellipsis\"><span class=\"btn-edit btn-link cursor-pointer\">");
#nullable restore
#line 9 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                                                                                Write(item.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n            <td class=\"text-ellipsis\"><span class=\"btn-edit btn-link cursor-pointer\">");
#nullable restore
#line 10 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                                                                                Write(item.EmailFrom);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n            <td class=\"text-ellipsis\">");
#nullable restore
#line 11 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                                 Write(item.Password);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"text-ellipsis\">");
#nullable restore
#line 12 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                                 Write(item.HostName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"text-ellipsis\">");
#nullable restore
#line 13 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                                 Write(item.Port);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"text-ellipsis\">");
#nullable restore
#line 14 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
                                 Write(item.EnableSSL);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 16 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\EmailConfigiration\All.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<script>\r\n    $(\'.btn-edit\').click(e => dialog.emailConfig($(e.currentTarget).closest(\'tr\').data().configId));\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<TheMuscleBar.AppCode.Reops.Entities.EmailConfig>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
