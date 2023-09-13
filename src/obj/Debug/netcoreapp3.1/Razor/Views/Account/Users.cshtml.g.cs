#pragma checksum "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\Account\Users.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0649fb3aeee70472ed0c1269a7d8c78c353a2e13"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Users), @"mvc.1.0.view", @"/Views/Account/Users.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0649fb3aeee70472ed0c1269a7d8c78c353a2e13", @"/Views/Account/Users.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5774e158c1a5a962cb4fe48861f93e529b2c1b4c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Account_Users : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TheMuscleBar.Models.ApplicationUser>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\Account\Users.cshtml"
  
    ViewData["Title"] = "Users";
    Layout = "_LayOut";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""main-container"">
    <!-- Row start -->
    <div class=""row gutters"">
        <div id=""frmcat"" class=""w-100"">
            <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
                <div class=""card card-outline card-outline-red"">
                    <div class=""card-header"">
                        ");
#nullable restore
#line 13 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\Account\Users.cshtml"
                   Write(Model.Role);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 504, "\"", 523, 1);
#nullable restore
#line 14 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\Account\Users.cshtml"
WriteAttributeValue("", 512, Model.Role, 512, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" id=""hdnroleid"" />
                        <div class=""float-right"">  <button id=""btnNew"" class=""btnaddUser btn btn-info"">New</button></div>
                    </div>
                    <div class=""card-body"">
                        <div class=""table-container"">
                            <div class=""table-responsive"">
                                <table class=""table"" id=""example"">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                        
                                            <th>User Name</th>
                                            <th>Name</th>
                                            <th>Mobile</th>
                                            <th>EmailID</th>
                                            <th>Balance</th>
                                            <th>Address</th>
                                            <th>Pincode</th>
    ");
            WriteLiteral(@"                                    </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>

        $(document).ready(function () {
            getUsers();
        })

        $('.btnaddUser').click(function () {
            Q.preloader.load();
            $.ajax({
                type: 'post',
                url: '/Master/UserForm',
                data: { role: $('#hdnroleid').val() },
                success: function (data) {
                    Q.alert({
                        title: ""Add User"",
                        body: data,
                        width: '900px',
                    });
                    Q.preloader.remove();
                },
                error: function (data) {
                    console.log('An error occurred.');
                    console.log(data);
                    Q.notify(-1, 'An error occurred.');
                    Q.preloader.remove();
                },
            });
        });
        function getUsers() {
            Q.preloader.load();
            $.ajax({
                type: 'post',
      ");
                WriteLiteral(@"          url: '/Account/UsersDetails',
                data: { role: $('#hdnroleid').val() },
                success: function (data) {
                    $('tbody').html(data);
                },
                error: function (data) {
                    console.log('An error occurred.');
                    console.log(data);
                    Q.notify(-1, 'An error occurred.');
                },
            });
            Q.preloader.remove();
        }


    </script>
");
            }
            );
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