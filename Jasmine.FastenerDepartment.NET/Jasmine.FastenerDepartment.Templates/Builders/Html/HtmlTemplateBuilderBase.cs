namespace Jasmine.FastenerDepartment.Templates.Builders.Html;

internal abstract class HtmlTemplateBuilderBase : TemplateBuilderBase
{
    public override string Build()
    {
        return $@"
            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
            <html class=""sg-campaigns"" xmlns=""http://www.w3.org/1999/xhtml""
                  style=""padding: 20px; width: 100%; height: 100%"">

                <head>
                  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
                  <meta name=""viewport"" content=""width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1""/>
                  <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge""/>
                  <title>{Title}</title>
                </head>

                <body>
                    <div style=""
                        padding: 20px;
                        background-color: #F5F7FA;
                        height: 100%;
                        max-width: 700px;
                        margin: 0 auto;
                        border-radius: 4px;"">
                            {Body}
                    </div>
                </body>
            </html>";
    }
}
