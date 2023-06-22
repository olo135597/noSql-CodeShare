# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY ./bin/Release/net6.0/publish .

# Expose the port that the API listens on
EXPOSE 5000

# Run the API as the entrypoint
ENTRYPOINT ["dotnet", "CodeShareApi.dll"]
