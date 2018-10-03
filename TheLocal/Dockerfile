#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 50773
EXPOSE 44356

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["TheLocal/TheLocal.csproj", "TheLocal/"]
RUN dotnet restore "TheLocal/TheLocal.csproj"
COPY . .
WORKDIR "/src/TheLocal"
RUN dotnet build "TheLocal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TheLocal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TheLocal.dll"]