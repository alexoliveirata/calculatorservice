#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 55419
EXPOSE 44398

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CalculatorService/CalculatorService.Server.csproj", "CalculatorService/"]
COPY ["Data.Repository/Data.Repository.csproj", "Data.Repository/"]
COPY ["Infrastrcuture/Infrastructure.csproj", "Infrastrcuture/"]
RUN dotnet restore "CalculatorService/CalculatorService.Server.csproj"
COPY . .
WORKDIR "/src/CalculatorService"
RUN dotnet build "CalculatorService.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculatorService.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculatorService.Server.dll"]