﻿namespace ItemCatalogue.Api;

public static class ApiKeyConstants
{
    public const string ApiKeySectionName = "Authentication:ApiKey";
    public const string ApiKeyHeaderName = "X-Api-Key";
    public const string ApiKeyNotFound = "No api key found in request headers";
    public const string ApiKeyInvalid = "Invalid api key found";
    public const string ApiKeyError = "Api key error";

}
