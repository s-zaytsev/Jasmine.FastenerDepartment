FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Создаём структуру папок и копируем .csproj
COPY Jasmine.FastenerDepartment.WebApi/Jasmine.FastenerDepartment.WebApi.csproj ./Jasmine.FastenerDepartment.WebApi/
COPY Jasmine.FastenerDepartment.Application/Jasmine.FastenerDepartment.Application.csproj ./Jasmine.FastenerDepartment.Application/
COPY Jasmine.FastenerDepartment.EF/Jasmine.FastenerDepartment.EF.csproj ./Jasmine.FastenerDepartment.EF/
COPY Jasmine.FastenerDepartment.DesktopApplication.Domain/Jasmine.FastenerDepartment.Domain.csproj ./Jasmine.FastenerDepartment.Domain/
COPY Jasmine.FastenerDepartment.Infrastructure/Jasmine.FastenerDepartment.Infrastructure.csproj ./Jasmine.FastenerDepartment.Infrastructure/
COPY Jasmine.FastenerDepartment.Documents/Jasmine.FastenerDepartment.Documents.csproj ./Jasmine.FastenerDepartment.Documents/

# 2. Восстанавливаем зависимости
RUN dotnet restore "Jasmine.FastenerDepartment.WebApi/Jasmine.FastenerDepartment.WebApi.csproj"

# 3. Копируем только нужные файлы из папки Wpf
COPY Jasmine.FastenerDepartment.WebApi/ ./Jasmine.FastenerDepartment.WebApi/
COPY Jasmine.FastenerDepartment.Application/ ./Jasmine.FastenerDepartment.Application/
COPY Jasmine.FastenerDepartment.EF/ ./Jasmine.FastenerDepartment.EF/
COPY Jasmine.FastenerDepartment.DesktopApplication.Domain/ ./Jasmine.FastenerDepartment.DesktopApplication.Domain/
COPY Jasmine.FastenerDepartment.Infrastructure/ ./Jasmine.FastenerDepartment.Infrastructure/
COPY Jasmine.FastenerDepartment.Documents/ ./Jasmine.FastenerDepartment.Documents/

# 4. Публикуем проект
RUN dotnet publish "Jasmine.FastenerDepartment.WebApi/Jasmine.FastenerDepartment.WebApi.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Jasmine.FastenerDepartment.WebApi.dll"]