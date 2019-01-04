FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY EventClassLibrary ./EventClassLibrary
COPY EventSearchAPI ./EventSearchAPI

WORKDIR /app/EventSearchAPI
RUN dotnet publish --configuration Release --output out

EXPOSE 80

ENTRYPOINT ["dotnet", "/app/EventSearchAPI/out/EventSearchAPI.dll"]

