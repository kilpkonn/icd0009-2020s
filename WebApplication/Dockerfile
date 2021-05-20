FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /source

COPY *.sln .
COPY Directory.Build.props .

# Copy all the project files
# Base projects
COPY BLL.Base/*.csproj ./BLL.Base/
COPY CarApp.BLL.Base/*.csproj ./CarApp.BLL.Base/
COPY CarApp.DAL.Base/*.csproj ./CarApp.DAL.Base/
COPY CarApp.Domain.Base/*.csproj ./CarApp.Domain.Base/
COPY DAL.Base/*.csproj ./DAL.Base/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/
COPY Domain.Base/*.csproj ./Domain.Base/
COPY Extensions.Base/*.csproj ./Extensions.Base/
COPY Resource.Base/*.csproj ./Resource.Base/

# App projects
COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY CarApp.BLL.App/*.csproj ./CarApp.BLL.App/
COPY CarApp.DAL.App/*.csproj ./CarApp.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain.App/*.csproj ./Domain.App/
COPY PublicApi.DTO.v1/*.csproj ./PublicApi.DTO.v1/
COPY Resources/*.csproj ./Resources/
COPY Tests/*.csproj ./Tests/
COPY WebApplication/*.csproj ./WebApplication/

# restore all the nuget packages
RUN dotnet restore

# copy over the source code
# Base projects
COPY BLL.Base/. ./BLL.Base/
COPY CarApp.BLL.Base/. ./CarApp.BLL.Base/
COPY CarApp.DAL.Base/. ./CarApp.DAL.Base/
COPY CarApp.Domain.Base/. ./CarApp.Domain.Base/
COPY DAL.Base/. ./DAL.Base/
COPY DAL.Base.EF/. ./DAL.Base.EF/
COPY Domain.Base/. ./Domain.Base/
COPY Extensions.Base/. ./Extensions.Base/
COPY Resource.Base/. ./Resource.Base/

# App projects
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY CarApp.BLL.App/. ./CarApp.BLL.App/
COPY CarApp.DAL.App/. ./CarApp.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain.App/. ./Domain.App/
COPY PublicApi.DTO.v1/. ./PublicApi.DTO.v1/
COPY Resources/. ./Resources/
COPY Tests/. ./Tests/
COPY WebApplication/. ./WebApplication/

COPY SiteJS/ ./SiteJS

WORKDIR /source/SiteJS

#RUN curl -sL https://deb.nodesource.com/setup_14.x -o nodesource_setup.sh
#RUN bash nodesource_setup.sh

RUN apt-get update
RUN apt-get install nodejs -y

RUN curl https://www.npmjs.com/install.sh | sh

RUN rm package-lock.json
RUN npm install
RUN npm run build

WORKDIR /source/WebApplication

RUN dotnet publish -c Release -o out

# create a new image, from aspnet runtime (no compilers)
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
# RUN apt-get -y update && apt-get -y upgrade

WORKDIR /app
COPY --from=build /source/WebApplication/out ./

ENTRYPOINT ["dotnet", "WebApplication.dll"]
