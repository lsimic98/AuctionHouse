#pragma checksum "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\UnbanUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27436a9d65f93883e8d8aec0c5f89500f783f026"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UnbanUser), @"mvc.1.0.view", @"/Views/User/UnbanUser.cshtml")]
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
#line 1 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\_ViewImports.cshtml"
using AuctionHouse;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\_ViewImports.cshtml"
using AuctionHouse.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27436a9d65f93883e8d8aec0c5f89500f783f026", @"/Views/User/UnbanUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e414c7c74de8f002990fed6020e45b16db58031", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UnbanUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AuctionHouse.Models.Database.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"text-danger\" style=\"font-weight: bold;\">\r\n    ");
#nullable restore
#line 4 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\UnbanUser.cshtml"
Write(Model.state);

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n    <button class=\"btn btn-success\"");
            BeginWriteAttribute("onclick", " onclick=\"", 153, "\"", 191, 3);
            WriteAttributeValue("", 163, "unbanUser(\'", 163, 11, true);
#nullable restore
#line 5 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\UnbanUser.cshtml"
WriteAttributeValue("", 174, Model.UserName, 174, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 189, "\')", 189, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Unban</button>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AuctionHouse.Models.Database.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
