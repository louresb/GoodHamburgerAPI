FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish -c Release -p:BlazorWebAssemblyEnableLinking=false -o /app/publish

FROM nginx:alpine
WORKDIR /usr/share/nginx/html

RUN rm -rf ./*
COPY --from=build /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/conf.d/default.conf
