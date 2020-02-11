using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using jsreport.Shared;
using jsreport.Types;
using Microsoft.AspNetCore.NodeServices;

namespace webapp
{
    public class MPSRenderService : IRenderService
    {
        readonly INodeServices _nodeServices;

        public MPSRenderService(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }  

        public async Task<Report> RenderAsync(RenderRequest request, CancellationToken ct = default)
        {
            var response = await _nodeServices.InvokeAsync<byte[]>("./wwwroot/create-pdf",  request.Template.Content);
            var stream = new MemoryStream();
            stream.Write(response, 0, response.Length);
            stream.Position = 0;
            var meta = new Dictionary<string, string>();

            return new Report { 
                Content = stream, 
                Meta = SerializerHelper.ParseReportMeta(meta) 
            };
        }

        public Task<Report> RenderAsync(string templateShortid, object data, CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Report> RenderAsync(string templateShortid, string jsonData, CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Report> RenderAsync(object request, CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Report> RenderByNameAsync(string templateName, string jsonData, CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Report> RenderByNameAsync(string templateName, object data, CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }
    }
}