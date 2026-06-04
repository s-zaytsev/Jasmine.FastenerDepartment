namespace Jasmine.FastenerDepartment.Templates.Builders.Html;

internal abstract class HtmlTemplateBuilderBase : TemplateBuilderBase
{
    public override string Build()
    {
        return $@"
            <!DOCTYPE html>
            <html class=""wrapper"">
                <head>
                  <meta charset=""utf-8"">
                  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                  <title>{Title}</title>

                  <style type=""text/css"">
                    .wrapper {{
                        padding: 20px;
                        width: 100%;
                        height: 100%
                    }}

                    .body-wrapper {{
                        padding: 20px;
                        background-color: #F5F7FA;
                        height: 100%;
                        max-width: 700px;
                        margin: 0 auto;
                        border-radius: 4px 
                    }}

                    @media only screen and (max-width: 600px) {{
                      .wrapper {{
                        padding: 5px;
                        width: 100%;
                        height: 100%
                       }}

                      .body-wrapper {{
                        padding: 5px;
                        background-color: #F5F7FA;
                        height: 100%;
                        max-width: 100%;
                        margin: 0 auto;
                        border-radius: 4px 
                    }}

                      .mobile-only {{
                        display: block !important;
                        max-height: none !important;
                        overflow: visible !important;
                        visibility: visible !important;
                      }}

                      .desktop-only {{
                        display: none !important;
                      }}
                    }}

                </style>
                </head>

                <body>
                    <div class=""body-wrapper"">
                       {Body}
                    </div>
                </body>
            </html>";
    }
}
