
FROM mcr.microsoft.com/dotnet/aspnet:3.1

# Copy app code (.) to /usr/src/app in container image
COPY . /app/demo

# Set working directory context
WORKDIR /app/demo


# Command for container to execute
ENTRYPOINT ["dotnet", "Tech-Journal-ConsoleApp.dll" ]