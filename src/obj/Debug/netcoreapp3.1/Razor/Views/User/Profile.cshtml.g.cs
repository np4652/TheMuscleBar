#pragma checksum "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7ced6f32d00941bc03cb1a3acea960187dad3c6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Profile), @"mvc.1.0.view", @"/Views/User/Profile.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7ced6f32d00941bc03cb1a3acea960187dad3c6b", @"/Views/User/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5774e158c1a5a962cb4fe48861f93e529b2c1b4c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TheMuscleBar.Models.ApplicationUser>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<link href=""/css/profile.css"" rel=""stylesheet"" type=""text/css"">

<div class=""row mt-4"">
    <div class=""col-sm-3"">
        <div class=""card clrText"">
            <div class=""valign-wrapper pr12ImgNameDiv"">
                <img class=""circle pr12ImageCircle"" src=""https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ8II-Z-AtZ00u5hZVVhPK7J1tp5I2gVVT9UMOHhCQ&s"" width=""0"" height=""0""");
            BeginWriteAttribute("alt", " alt=\"", 432, "\"", 438, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <div class=\"pr12AuthName fw500 fs18\">");
#nullable restore
#line 9 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
                                                Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n            <div>\r\n                <div>\r\n                    <a");
            BeginWriteAttribute("class", " class=\"", 598, "\"", 606, 0);
            EndWriteAttribute();
            WriteLiteral(@" href=""javascript:void(0)"">
                        <div class=""pr12ProfMenu fs15 cur-po valign-wrapper pr12ProfMenuSelected onMount-appear-done onMount-enter-done"">
                            <div class=""valign-wrapper width100 fsAuto"">Status</div>
                            <div class=""pr12IcBox valign-wrapper"">
                                <input type=""checkbox"" class=""toggle"" ");
#nullable restore
#line 17 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
                                                                 Write(Html.Raw(Model.IsActive?"checked":""));

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n                    </a>\r\n                    <a");
            BeginWriteAttribute("class", " class=\"", 1156, "\"", 1164, 0);
            EndWriteAttribute();
            WriteLiteral(@" href=""javascript:void(0)"">
                        <div class=""pr12ProfMenu fs15 cur-po valign-wrapper onMount-appear-done onMount-enter-done"">
                            <div class=""valign-wrapper width100"">TwoFactorEnabled</div>
                            <div class=""pr12IcBox valign-wrapper"">
                                <input type=""checkbox"" class=""toggle"" ");
#nullable restore
#line 25 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
                                                                 Write(Html.Raw(Model.TwoFactorEnabled?"checked":""));

#line default
#line hidden
#nullable disable
            WriteLiteral(@">
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-sm-9"">
        <div class=""card"">
            <div class=""bd66Box clearfix"">
                <div class=""row"">
                    <div class=""col-md-6"">
                        <div class=""bd66LeftInp"">
                            <div>
                                <div class=""c-fiiZaJ fs14 fw400"">NAME</div>
                                <div class=""c-jCqGRU"">
                                    <div class=""c-UazGY"">
                                        <input class=""c-kDbNPa fw400 fs16"" id=""authorName""");
            BeginWriteAttribute("value", " value=\"", 2301, "\"", 2320, 1);
#nullable restore
#line 43 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
WriteAttributeValue("", 2309, Model.Name, 2309, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                    </div>
                                </div>
                                <div class=""c-loqsEf c-loqsEf-cKbjjX-error-false fs14 fw400""></div>
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-6 bd66RightInp"">
                        <div class=""bd66RightInp"">
                            <div>
                                <div class=""c-fiiZaJ fs14 fw400"">MOBILE NUMBER</div>
                                <div class=""c-jCqGRU"">
                                    <div class=""c-UazGY"">
                                        <input class=""c-kDbNPa  fw400 fs16"" id=""authorMobile""");
            BeginWriteAttribute("value", " value=\"", 3043, "\"", 3069, 1);
#nullable restore
#line 56 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
WriteAttributeValue("", 3051, Model.PhoneNumber, 3051, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                    </div>
                                </div>
                                <div class=""c-loqsEf c-loqsEf-cKbjjX-error-false fs14 fw400""></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""row bd66InpContainer"">
                    <div class=""col-md-6 bd66LeftInp"">
                        <div class=""bd66LeftInp"">
                            <div>
                                <div class=""c-fiiZaJ fs14 fw400"">EMAIL</div>
                                <div class=""c-jCqGRU"">
                                    <div class=""c-UazGY"">
                                        <input class=""c-kDbNPa  fw400 fs16"" id=""authorEmail"" placeholder=""Enter E-Mail"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 3896, "\"", 3916, 1);
#nullable restore
#line 71 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
WriteAttributeValue("", 3904, Model.Email, 3904, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                        <span class=""c-lkBNdM""> </span>
                                    </div>
                                </div>
                                <div class=""c-loqsEf c-loqsEf-cKbjjX-error-false fs14 fw400""></div>
                            </div>
                        </div>
                    </div>
                    <div class=""col-md-6 bd66RightInp"">
                        <div class=""bd66RightInp"">
                            <div>
                                <div class=""c-fiiZaJ fs14 fw400"">Merchant Id</div>
                                <div class=""c-jCqGRU"">
                                    <div class=""c-UazGY"">
                                        <input class=""c-kDbNPa  fw400 fs16"" id=""authorMerchantId""");
            BeginWriteAttribute("value", " value=\"", 4714, "\"", 4731, 1);
#nullable restore
#line 85 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
WriteAttributeValue("", 4722, Model.Id, 4722, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                        <div class=""ctc11Container"">
                                            <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24"" fill=""currentColor"" height=""13"" width=""13"" class=""ctc11CopyLogo"">
                                                <path d=""M0 0h24v24H0z"" fill=""none""></path>
                                                <path d=""M16 1H4c-1.1 0-2 .9-2 2v14h2V3h12V1zm3 4H8c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h11c1.1 0 2-.9 2-2V7c0-1.1-.9-2-2-2zm0 16H8V7h11v14z""></path>
                                            </svg>
                                            <span class=""ctc11ToolTip"">Copy To Clipboard</span>
                                        </div>
                                    </div>
                                </div>
                                <div class=""c-loqsEf c-loqsEf-cKbjjX-error-false fs14 fw400""></div>
                            </div>
                        </div>
                    </div>
              ");
            WriteLiteral(@"  </div>
                <div class=""row bd66InpContainer"">
                    <div class=""col-md-6 bd66LeftInp"">
                        <div class=""bd66LeftInp"">
                            <div>
                                <div class=""c-fiiZaJ fs14 fw400"">Merchant Key</div>
                                <div class=""c-jCqGRU"">
                                    <div class=""c-UazGY"">
                                        <input class=""c-kDbNPa  fw400 fs16"" id=""authorEmail""");
            BeginWriteAttribute("value", " value=\"", 6252, "\"", 6283, 1);
#nullable restore
#line 107 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\Profile.cshtml"
WriteAttributeValue("", 6260, Model.ConcurrencyStamp, 6260, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                        <div class=""ctc11Container"">
                                            <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24"" fill=""currentColor"" height=""13"" width=""13"" class=""ctc11CopyLogo"">
                                                <path d=""M0 0h24v24H0z"" fill=""none""></path>
                                                <path d=""M16 1H4c-1.1 0-2 .9-2 2v14h2V3h12V1zm3 4H8c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h11c1.1 0 2-.9 2-2V7c0-1.1-.9-2-2-2zm0 16H8V7h11v14z""></path>
                                            </svg>
                                            <span class=""ctc11ToolTip"">Copy To Clipboard</span>
                                        </div>
                                    </div>
                                </div>
                                <div class=""c-loqsEf c-loqsEf-cKbjjX-error-false fs14 fw400""></div>
                            </div>
                        </div>
                    </div>
              ");
            WriteLiteral("  </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TheMuscleBar.Models.ApplicationUser> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591