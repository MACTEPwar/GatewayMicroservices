FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

ENV ASPNETCORE_ENVIRONMENT=docker

ADD . /app/build
WORKDIR /app/build
RUN dotnet restore
RUN dotnet publish /app/build/GatewayMicroservices/GatewayMicroservices.csproj -c Release -o ../out

#VOLUME /app/out/appsettings.docker.json
#VOLUME /app/out/logs

WORKDIR /app/out
ENTRYPOINT ["dotnet", "GatewayMicroservices.dll", "http://0.0.0.0:3000"]