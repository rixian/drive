// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Options for configuring an instance of IDriveClientOptions.
    /// </summary>
    public class DriveClientOptions
    {
        /// <summary>
        /// Logical name for the HttpClient configured to call the Rixian Drive Api.
        /// </summary>
        public const string DriveHttpClientName = "rixian_drive";

        /// <summary>
        /// Logical name for the ITokenClient that provides access tokens for calling the Rixian Drive Api.
        /// </summary>
        public const string DriveTokenClientName = "rixian_drive_token";

        /// <summary>
        /// Logical name for the HttpClient configured for use by the Rixian Drive ITokenClient.
        /// </summary>
        public const string DriveTokenClientHttpClientName = "rixian_oidc";

        /// <summary>
        /// Gets or sets the options for the ITokenClient.
        /// </summary>
        public TokenClientOptions TokenClientOptions { get; set; }

        /// <summary>
        /// Gets or sets the uri of the Drive api endpoint.
        /// </summary>
        public Uri DriveApiUri { get; set; }

        /// <summary>
        /// Gets or sets the api key used for the Drive api.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the header name used for passing the api key. Defaults to 'Subscription-Key'.
        /// </summary>
        public string ApiKeyHeaderName { get; set; } = "Subscription-Key";

        /// <summary>
        /// Gets or sets the version of the Drive api. Defaults to '2019-09-01'.
        /// </summary>
        public string ApiVersion { get; set; } = "2019-09-01";
    }
}
