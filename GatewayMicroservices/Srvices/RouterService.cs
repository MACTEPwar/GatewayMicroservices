using GatewayMicroservices.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GatewayMicroservices.Models.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Runtime.CompilerServices;
using GatewayMicroservices.Repositories;

namespace GatewayMicroservices.Srvices
{
    public class RouterService
    {
        private List<Models.DAL.Route> _routes;
        private readonly DestinationService _authenticationService;
        private readonly SettingRepository _settingRepository;
        private readonly RouteRepository _routeRepository;

        public RouterService(DestinationService destinationService, SettingRepository settingRepository, RouteRepository routeRepository)
        {
            _authenticationService = destinationService;
            _settingRepository = settingRepository;
            _routeRepository = routeRepository;

            LoadRoutes();
            _authenticationService.Uri = settingRepository.GetValueByKey<string>("AuthUri").Result;
        }

        private void LoadRoutes()
        {   
            this._routes = _routeRepository.GetList().ToList();
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
