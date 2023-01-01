using GatewayMicroservices.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GatewayMicroservices.Models.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace GatewayMicroservices.Srvices
{
    public class RouterService
    {
        private List<Models.DAL.Route> _routes;
        private readonly DestinationService _authenticationService;
        private readonly SettingService _settingService;
        private readonly IDbContextFactory<DbAppContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public RouterService(IDbContextFactory<DbAppContext> dbContextFactory, DestinationService destinationService, IMapper mapper, SettingService settingService)
        {
            _dbContextFactory = dbContextFactory;
            _authenticationService = destinationService;
            _mapper = mapper;
            _settingService = settingService;

            LoadRoutes();
            _authenticationService.Uri = _settingService.GetValueByKey<string>("AuthUri").Result;
        }

        private void LoadRoutes()
        {   
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                this._routes = _dbContext.Route.ToList();
            }
        }

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            string basePath = '/' + path.Split('/')[1];

            try
            {
                var route = _routes.First(r => r.Endpoint.Equals(basePath));
                _authenticationService.Uri = route.DestinationUri;
                _authenticationService.RequiresAuthentication = route.RequiresAuthentication;
            }
            catch
            {
                return ConstructErrorMessage("The path could not be found.");
            }

            if (_authenticationService.RequiresAuthentication)
            {
                string token = request.Headers["token"];
                request.Query.Append(new KeyValuePair<string, StringValues>("token", new StringValues(token)));
                HttpResponseMessage authResponse = await _authenticationService.SendRequest(request);
                if (!authResponse.IsSuccessStatusCode) return ConstructErrorMessage("Authentication failed.");
            }

            return await _authenticationService.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }
    }
}
