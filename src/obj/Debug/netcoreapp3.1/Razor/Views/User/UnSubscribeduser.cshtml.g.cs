#pragma checksum "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\UnSubscribeduser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f9a9360f62e1e83fb526f5d344be2926d826049"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UnSubscribeduser), @"mvc.1.0.view", @"/Views/User/UnSubscribeduser.cshtml")]
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
#nullable restore
#line 1 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\UnSubscribeduser.cshtml"
using TheMuscleBar.AppCode.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f9a9360f62e1e83fb526f5d344be2926d826049", @"/Views/User/UnSubscribeduser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5774e158c1a5a962cb4fe48861f93e529b2c1b4c", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_UnSubscribeduser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Rishu\Rishabh Software\gym\Musclebar\MuscleBarGit\TheMuscleBar\src\Views\User\UnSubscribeduser.cshtml"
  
    ViewData["Title"] = "Un Subscribed Users";
    Layout = "_LayOut";
   

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""card mt-2"">
    <div class=""card-body"">
        <div class=""table-container"">
            <div class=""table-responsive"">
                <table class=""table table-bordered"">
                    <thead class=""bg-mustered text-white"">
                        <tr>
                            <th>#</th>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Mobile</th>
                            <th>EmailID</th>
                            <th>Adhaar</th>
                            <th>DOB</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
<script>
    $(document).ready(function () {
      
        loadData();
    });
 
    const loadData = (role) => {
        Q.preloader.load();
        $.post('/User/UnSubscribeduserlist').done(result => $('tbody').html(result)).fail(xhr => Q.notify(-1, xhr.responseText)).always(() => Q.preloader.remove());
    }

 




    $(document).on('click', '.btndelete', e => {
        let userId = $(e.currentTarget).closest('tr').data().userId;
        let html = `<input type='hidden' class='form-control' value='${userId}' id='hdnid'></input><input type='text' class='form-control' id='txtkey' placeholder='Enter Security Key Here'></input>
                <a type='submit' class=""btn btn-dark"" id='btnDelete'>SUBMIT</a>`;
        Q.alert({
            title:'Delete Record',
            body: html
        });
        $('#btnDelete').click(e => {
            if ($('#txtkey').val() != '298744') {
                Q.notify(-1, 'Invalid security Key');
                return false;
            }
 ");
                WriteLiteral(@"           $(e.currentTarget).text('Requesting....').attr('disabled', 'disabled');
            $.post('/User/DeleteUserData', { id: $('#hdnid').val() })
                .done(result => {
                    Q.notify(result.statusCode, result.responseText);
                    loadData();
                }).fail(xhr => Q.notify(-1, xhr.responseText)).always(() => $(e.currentTarget).text('SUBMIT').removeAttr('disabled'));
        });
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
