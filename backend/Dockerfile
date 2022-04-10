#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN mkdir photos
RUN chmod -R 777 photos
COPY /backend/photos* /app/photos
EXPOSE 80
EXPOSE 44

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backend.csproj", "."]
RUN dotnet restore "./backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backend.csproj" -c Release -o /app/publish

#RUN dotnet ef database update
#RUN chmod +x ./entrypoint.sh

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#CMD /bin/bash ./entrypoint.sh
ENTRYPOINT ["dotnet", "backend.dll"]

