using System.IO;
using System.Threading.Tasks;
using jsreport.Client;
using jsreport.Types;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1().Wait();
        }

        public async static Task Test1()
        {
            var bodyTemplate = ReadTemplate(Path.Combine("templates", "hello.html"));
            var footerTemplate = ReadTemplate(Path.Combine("templates", "footer.html"));

            var rs = new ReportingService("http://localhost:5488");

            var report = await rs.RenderAsync(new RenderRequest()
            {
                Template = new Template()
                {
                    Recipe = Recipe.ChromePdf,
                    Engine = Engine.Handlebars,
                    Content = bodyTemplate,
                    Chrome = new Chrome {
                        DisplayHeaderFooter = true,
                        FooterTemplate = footerTemplate,
                        Format = "A4",
                        MarginBottom = "80px",
                        MarginLeft = "20px",
                        MarginRight = "20px",
                        MarginTop = "20px"
                    }
                },
                Data = new
                {
                    message = "world!"
                }
            });
            
            using (var file = File.Create("test.pdf"))
            {
                report.Content.Seek(0, SeekOrigin.Begin);
                report.Content.CopyTo(file);
            }
        }

        static string ReadTemplate(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                return sr.ReadToEnd();
            }
        }
    }
}