#pragma checksum "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "652a9dd384c77906490c24aca72d63921a5a679d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_BanUsers), @"mvc.1.0.view", @"/Views/User/BanUsers.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"652a9dd384c77906490c24aca72d63921a5a679d", @"/Views/User/BanUsers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e414c7c74de8f002990fed6020e45b16db58031", @"/Views/_ViewImports.cshtml")]
    public class Views_User_BanUsers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AuctionHouse.Models.View.UsersOverview>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
  
    ViewData["Title"] = "Ban Users";
    int num = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Ban Users</h1>

<div class=""row"">
    <table class=""table table-responsive-xl"">
    <thead>
        <tr>
         <th scope=""col""style=""font-size: 15px;"">#</th>
         <th scope=""col""style=""font-size: 15px;"">Username</th>
         <th scope=""col""style=""font-size: 15px;"">First Name</th>
         <th scope=""col""style=""font-size: 15px;"">Last Name</th>
         <th scope=""col""style=""font-size: 15px;"">Email</th>
         <th scope=""col""style=""font-size: 15px;"">Tokens</th>
         <th scope=""col""style=""font-size: 15px;"">Status/Command</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 25 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
         foreach (var item in @Model.users)
        {
            
            num++;

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <th scope=\"row\">");
#nullable restore
#line 30 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
                           Write(num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                <td>");
#nullable restore
#line 31 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
               Write(item.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 32 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
               Write(item.firstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 33 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
               Write(item.lastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 34 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
               Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 35 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
               Write(item.tokens);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 36 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
                 if(item.state.Equals("Active"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td");
            BeginWriteAttribute("id", " id=\"", 1210, "\"", 1229, 1);
#nullable restore
#line 38 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
WriteAttributeValue("", 1215, item.UserName, 1215, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <div class=\"text-success\" style=\"font-weight: bold;\">");
#nullable restore
#line 39 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
                                                                        Write(item.state);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <button class=\"btn btn-danger\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1352, "\"", 1387, 3);
            WriteAttributeValue("", 1362, "banUser(\'", 1362, 9, true);
#nullable restore
#line 39 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
WriteAttributeValue("", 1371, item.UserName, 1371, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1385, "\')", 1385, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Ban</button></div>\r\n                    </td>\r\n");
#nullable restore
#line 41 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td");
            BeginWriteAttribute("id", " id=\"", 1519, "\"", 1538, 1);
#nullable restore
#line 44 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
WriteAttributeValue("", 1524, item.UserName, 1524, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <div class=\"text-danger\" style=\"font-weight: bold;\">");
#nullable restore
#line 45 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
                                                                       Write(item.state);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <button class=\"btn btn-success\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1661, "\"", 1698, 3);
            WriteAttributeValue("", 1671, "unbanUser(\'", 1671, 11, true);
#nullable restore
#line 45 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
WriteAttributeValue("", 1682, item.UserName, 1682, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1696, "\')", 1696, 2, true);
            EndWriteAttribute();
            WriteLiteral(">Unban</button></div>\r\n                        \r\n                    </td>                \r\n");
#nullable restore
#line 48 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tr>\r\n");
#nullable restore
#line 50 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </tbody>\r\n    </table>\r\n</div>\r\n\r\n\r\n<div class=\"row justify-content-center\">\r\n");
#nullable restore
#line 58 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
Write(Html.PagedListPager(Model.users, page => Url.Action("BanUsers", new { page = page }),
    new X.PagedList.Web.Common.PagedListRenderOptionsBase
    {
        DisplayPageCountAndCurrentLocation = true,
        ContainerDivClasses = new[] { "navigation" },
        LiElementClasses = new[] { "page-item" },
        PageClasses = new[] { "page-link" }
    }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 2325, "\"", 2365, 1);
#nullable restore
#line 68 "C:\Users\Kalu\Desktop\IEP\AuctionHouse\Views\User\BanUsers.cshtml"
WriteAttributeValue("", 2331, Url.Content("~/Scripts/admin.js"), 2331, 34, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@"></script>
    <script>
        // Workaround to fix style of text for showing items .. through ..
        // Problem related to Boostrap 4 according to issue in link below
        // https://github.com/dncuug/X.PagedList/issues/127
        $(document).ready(function () {
            $('ul.pagination > li.disabled > a').addClass('page-link');
        });
    </script>
");
            }
            );
            WriteLiteral("</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AuctionHouse.Models.View.UsersOverview> Html { get; private set; }
    }
}
#pragma warning restore 1591
