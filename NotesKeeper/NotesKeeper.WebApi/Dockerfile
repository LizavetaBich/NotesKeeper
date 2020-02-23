FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["NotesKeeper.WebApi/NotesKeeper.WebApi.csproj", "NotesKeeper.WebApi/"]
RUN dotnet restore "NotesKeeper.WebApi/NotesKeeper.WebApi.csproj"
COPY . .
WORKDIR "/src/NotesKeeper.WebApi"
RUN dotnet build "NotesKeeper.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesKeeper.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesKeeper.WebApi.dll"]