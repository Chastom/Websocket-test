#pragma checksum "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "66831bc4e085fbd165f52dd4d7784c4b60e842fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\TK\source\repos\WsApp\Views\_ViewImports.cshtml"
using WsApp;

#line default
#line hidden
#line 2 "C:\Users\TK\source\repos\WsApp\Views\_ViewImports.cshtml"
using WsApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66831bc4e085fbd165f52dd4d7784c4b60e842fa", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1a938a21b7aace4ffc7b591e22dc34da15c205c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 195, true);
            WriteLiteral("\r\n<div class=\"container top-space\">\r\n    <div class=\"col-xs-6\">\r\n        My fleet:\r\n        <table id=\"grid1\" class=\"table table-bordered\" style=\"background-color:#e6f9ff\">\r\n            <tbody>\r\n");
            EndContext();
#line 10 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                 for (int i = 0; i < 15; i++)
                {

#line default
#line hidden
            BeginContext(306, 23, true);
            WriteLiteral("                    <tr");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 329, "\"", 336, 1);
#line 12 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 334, i, 334, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(337, 3, true);
            WriteLiteral(">\r\n");
            EndContext();
#line 13 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                         for (int j = 0; j < 15; j++)
                        {

#line default
#line hidden
            BeginContext(422, 54, true);
            WriteLiteral("                            <td align=\"center\"></td>\r\n");
            EndContext();
#line 16 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(503, 27, true);
            WriteLiteral("                    </tr>\r\n");
            EndContext();
#line 18 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(549, 216, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"col-xs-6\">\r\n        Enemey fleet:\r\n        <table id=\"grid2\" class=\"table table-bordered\" style=\"background-color:lightgrey\">\r\n            <tbody>\r\n");
            EndContext();
#line 26 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                 for (int i = 0; i < 15; i++)
                {

#line default
#line hidden
            BeginContext(831, 23, true);
            WriteLiteral("                    <tr");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 854, "\"", 861, 1);
#line 28 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 859, i, 859, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(862, 3, true);
            WriteLiteral(">\r\n");
            EndContext();
#line 29 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                         for (int j = 0; j < 15; j++)
                        {

#line default
#line hidden
            BeginContext(947, 54, true);
            WriteLiteral("                            <td align=\"center\"></td>\r\n");
            EndContext();
#line 32 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(1028, 27, true);
            WriteLiteral("                    </tr>\r\n");
            EndContext();
#line 34 "C:\Users\TK\source\repos\WsApp\Views\Home\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(1074, 410, true);
            WriteLiteral(@"            </tbody>
        </table>
    </div>
    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""col-xs-6"">
                <ul id=""selections""></ul>
            </div>
            <div class=""col-xs-6"">
                <ul id=""attacks""></ul>
            </div>
            <input class=""form-control"" id=""selection-content"" />
        </div>
    </div>
</div>


");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(1501, 167, true);
                WriteLiteral("\r\n    <script data-main=\"scripts/main\" src=\"https://rawgit.com/radu-matei/websocket-manager/master/src/WebSocketManager.Client.TS/dist/WebSocketManager.js\"></script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
