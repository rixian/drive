// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

namespace Rixian.Drive
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Rixian.Drive.Common;
    using Rixian.Extensions.Errors;
    using Rixian.Extensions.Http.Client;

    /// <summary>
    /// Represents an error from an Api HTTP request.
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The error inner exception.</param>
        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="info">The SerializationInfo for this exception.</param>
        /// <param name="context">The StreamingContext for this exception.</param>
        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates a new instance of an ApiException using an ErrorBase object.
        /// </summary>
        /// <param name="error">The error to use for populating the exception.</param>
        /// <returns>The initialized ApiException.</returns>
        public static ApiException Create(ErrorBase error)
        {
            var errorMessage = JsonConvert.SerializeObject(error, Formatting.Indented);
            return new ApiException(errorMessage);
        }
    }
}
