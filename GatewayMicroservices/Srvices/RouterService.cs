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
            var test = settingRepository.GetValueByKey<string>("AuthUri").Result;
            _authenticationService.Uri = test;
        }

        private void LoadRoutes()
        {   
            this._routes = _routeRepository.GetList().ToList();
        }

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            var path = request.Path.ToString().Split('/');
            //string basePath = '/' + path.Split('/')[1];
            var basePath = $"{path[1]}/{path[2]}/{path[3]}";

            Models.DAL.Route route = null;

            try
            {
                route = _routes.First(r => r.Endpoint.Equals(basePath));
                _authenticationService.Uri = route.DestinationUri;
                _authenticationService.RequiresAuthentication = route.RequiresAuthentication;
                _authenticationService.ReplaceMode = route.ReplaceMode;
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
