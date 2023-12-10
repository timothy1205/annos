FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /build
COPY . ./
RUN mkdir -p /build/obj && chmod -R 777 /build/obj

FROM dev AS prod
RUN dotnet restore
RUN dotnet publish -c Release --no-restore -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /usr/src/app
COPY --from=prod /build/out ./

EXPOSE 8080
ENTRYPOINT ["./annos"]
