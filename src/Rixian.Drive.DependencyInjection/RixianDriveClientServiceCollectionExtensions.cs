﻿// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Security.Authentication;
    using Rixian.Drive;

    /// <summary>
    /// Extensions for adding IDriveClient to the DI container.
    /// </summary>
    public static class RixianDriveClientServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the IDriveClient with the DI container.
        /// </summary>
        /// <param name="serviceCollection">The IServiceCollection.</param>
        /// <param name="options">Configuration options for this client.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddDriveClient(this IServiceCollection serviceCollection, DriveClientOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.TokenClientOptions is null)
            {
                throw new ArgumentOutOfRangeException(nameof(options));
            }

            // Configure the HttpClient for use by the ITokenClient.
            serviceCollection.AddHttpClient(DriveClientOptions.DriveTokenClientHttpClientName)
                .UseSslProtocols(SslProtocols.Tls12);

            // Configure the ITokenClient to use the previous HttpClient.
            serviceCollection
                .AddClientCredentialsTokenClient(DriveClientOptions.DriveTokenClientName, options.TokenClientOptions)
                .UseHttpClientForBackchannel(DriveClientOptions.DriveTokenClientHttpClientName);

            // Configure the HttpClient with the ITokenClient for inserting tokens into the header.
            IHttpClientBuilder httpClientBuilder = serviceCollection
                .AddHttpClient(DriveClientOptions.DriveHttpClientName, c => c.BaseAddress = options.DriveApiUri)
                .UseSslProtocols(SslProtocols.Tls12)
                .UseApiVersion(options.ApiVersion ?? "2019-09-01", null)
                .UseTokenClient(DriveClientOptions.DriveTokenClientName)
                .AddTypedClient<IDriveClient, DriveClient>();

            if (!string.IsNullOrWhiteSpace(options.ApiKey))
            {
                httpClientBuilder.UseHeader(options.ApiKeyHeaderName ?? "Subscription-Key", options.ApiKey);
            }

            return serviceCollection;
        }
    }
}
