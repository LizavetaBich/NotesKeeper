FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["NotesKeeper.WebApplication/NotesKeeper.WebApplication.csproj", "NotesKeeper.WebApplication/"]
RUN dotnet restore "NotesKeeper.WebApplication/NotesKeeper.WebApplication.csproj"
COPY . .
WORKDIR "/src/NotesKeeper.WebApplication"
RUN dotnet build "NotesKeeper.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesKeeper.WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesKeeper.WebApplication.dll"]