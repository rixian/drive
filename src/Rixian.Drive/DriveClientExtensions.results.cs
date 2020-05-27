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
    using System.Threading;
    using System.Threading.Tasks;
    using Rixian.Drive.Common;
    using Rixian.Extensions.Errors;
    using Rixian.Extensions.Http.Client;

    /// <summary>
    /// Extensions for the Rixian Drive api client.
    /// </summary>
    public static partial class DriveClientExtensions
    {
        /// <summary>
        /// Clears all file metadata.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task<Result> ClearFileMetadataResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ClearFileMetadataHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return Result.Default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ClearFileMetadataResultAsync)}").ConfigureAwait(false);
                            return error.ToResult();
                        }
                }
            }
        }

        /// <summary>
        /// Copies a file or directory.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="source">The source location.</param>
        /// <param name="target">The target location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task<Result> CopyResultAsync(this IDriveClient driveClient, CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.CopyHttpResponseAsync(source, target, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return Result.Default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(CopyResultAsync)}").ConfigureAwait(false);
                            return error.ToResult();
                        }
                }
            }
        }

        /// <summary>
        /// Creates a new drive.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="request">The request body for creating a new drive.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a drive or an error.</returns>
        public static async Task<Result<Drive>> CreateDriveResultAsync(this IDriveClient driveClient, CreateDriveRequest request, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.CreateDriveHttpResponseAsync(request, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<Drive>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<Drive>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<Drive>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(CreateDriveResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<Drive>();
                        }
                }
            }
        }

        /// <summary>
        /// Creates a new file or directory.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="overwrite">Overwite the existing file contents.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a DriveItemInfo or an error.</returns>
        public static async Task<Result<DriveItemInfo>> CreateDriveItemResultAsync(this IDriveClient driveClient, CloudPath path, bool? overwrite = null, FileParameter fileContents = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.CreateDriveItemHttpResponseAsync(path, overwrite, fileContents, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<DriveItemInfo>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<DriveItemInfo>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<DriveItemInfo>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(CreateDriveItemResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<DriveItemInfo>();
                        }
                }
            }
        }

        /// <summary>
        /// Deletes a file or directory.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task<Result> DeleteItemResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.DeleteItemHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return Result.Default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(DeleteItemResultAsync)}").ConfigureAwait(false);
                            return error.ToResult();
                        }
                }
            }
        }

        /// <summary>
        /// Downloads the file contents or alternate stream data.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a file response or an error.</returns>
        public static async Task<Result<HttpFileResponse>> DownloadContentResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.DownloadContentHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    {
                        Result<HttpFileResponse> fileResponse = await HttpFileResponse.CreateAsync(response).ConfigureAwait(false);
                        return fileResponse;
                    }

                case HttpStatusCode.NoContent:
                    return default;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.InternalServerError:
                    {
                        try
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<HttpFileResponse>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<HttpFileResponse>();
                            }
                        }
                        finally
                        {
                            response.Dispose();
                        }
                    }

                default:
                    {
                        UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(DownloadContentResultAsync)}").ConfigureAwait(false);
                        response.Dispose();
                        return error.ToResult<HttpFileResponse>();
                    }
            }
        }

        /// <summary>
        /// Checks if a file or directory exists.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either an exists reponse or an error.</returns>
        public static async Task<Result<ExistsResponse>> ExistsResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ExistsHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<ExistsResponse>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<ExistsResponse>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<ExistsResponse>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ExistsResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<ExistsResponse>();
                        }
                }
            }
        }

        /// <summary>
        /// Retrieve the metadata about the drive item.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a DriveItemInfo or an error.</returns>
        public static async Task<Result<DriveItemInfo>> GetItemInfoResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.GetItemInfoHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<DriveItemInfo>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<DriveItemInfo>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<DriveItemInfo>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(GetItemInfoResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<DriveItemInfo>();
                        }
                }
            }
        }

        /// <summary>
        /// Imports custom files into Rixian Drive.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="files">The files to import.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of DriveItemFile's or an error.</returns>
        public static async Task<Result<ICollection<DriveFileInfo>>> ImportFilesResultAsync(this IDriveClient driveClient, ICollection<ImportRecord> files, CloudPath path = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ImportFilesHttpResponseAsync(files, path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<ICollection<DriveFileInfo>>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<ICollection<DriveFileInfo>>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<ICollection<DriveFileInfo>>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ImportFilesResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<ICollection<DriveFileInfo>>();
                        }
                }
            }
        }

        /// <summary>
        /// Lists directory children.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of DriveItemInfo's or an error.</returns>
        public static async Task<Result<ICollection<DriveItemInfo>>> ListChildrenResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ListChildrenHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<ICollection<DriveItemInfo>>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<ICollection<DriveItemInfo>>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<ICollection<DriveItemInfo>>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ListChildrenResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<ICollection<DriveItemInfo>>();
                        }
                }
            }
        }

        /// <summary>
        /// Lists all available drives.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of drives or an error.</returns>
        public static async Task<Result<ICollection<Drive>>> ListDrivesResultAsync(this IDriveClient driveClient, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ListDrivesHttpResponseAsync(tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<ICollection<Drive>>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<ICollection<Drive>>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<ICollection<Drive>>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ListDrivesResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<ICollection<Drive>>();
                        }
                }
            }
        }

        /// <summary>
        /// Lists all metadata about a file.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a dictionary of metadata keys and values or an error.</returns>
        public static async Task<Result<IDictionary<string, string>>> ListFileMetadataResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ListFileMetadataHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<IDictionary<string, string>>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<IDictionary<string, string>>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<IDictionary<string, string>>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ListFileMetadataResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<IDictionary<string, string>>();
                        }
                }
            }
        }

        /// <summary>
        /// Lists the streams associated with a file.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of file stream names or an error.</returns>
        public static async Task<Result<ICollection<string>>> ListFileStreamsResultAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ListFileStreamsHttpResponseAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<ICollection<string>>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<ICollection<string>>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<ICollection<string>>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ListFileStreamsResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<ICollection<string>>();
                        }
                }
            }
        }

        /// <summary>
        /// Lists all available partitions.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of partitions or an error.</returns>
        public static async Task<Result<ICollection<Partition>>> ListPartitionsResultAsync(this IDriveClient driveClient, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.ListPartitionsHttpResponseAsync(tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Result.Create(await response.DeserializeJsonContentAsync<ICollection<Partition>>().ConfigureAwait(false));
                    case HttpStatusCode.NoContent:
                        return default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult<ICollection<Partition>>();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult<ICollection<Partition>>();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(ListPartitionsResultAsync)}").ConfigureAwait(false);
                            return error.ToResult<ICollection<Partition>>();
                        }
                }
            }
        }

        /// <summary>
        /// Moves a file or directory.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="source">The source location.</param>
        /// <param name="target">The target location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task<Result> MoveResultAsync(this IDriveClient driveClient, CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.MoveHttpResponseAsync(source, target, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return Result.Default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(MoveResultAsync)}").ConfigureAwait(false);
                            return error.ToResult();
                        }
                }
            }
        }

        /// <summary>
        /// Removes a specific file metadata item.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="key">The metadata key.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task<Result> RemoveFileMetadataResultAsync(this IDriveClient driveClient, CloudPath path, string key, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.RemoveFileMetadataHttpResponseAsync(path, key, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return Result.Default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(RemoveFileMetadataResultAsync)}").ConfigureAwait(false);
                            return error.ToResult();
                        }
                }
            }
        }

        /// <summary>
        /// Update and/or insert metadata onto a file.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="metadata">All metadata items to update.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task<Result> UpsertFileMetadataResultAsync(this IDriveClient driveClient, CloudPath path, IReadOnlyDictionary<string, string> metadata, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            HttpResponseMessage response = await driveClient.UpsertFileMetadataHttpResponseAsync(path, metadata, tenantId, cancellationToken).ConfigureAwait(false);

            using (response)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return Result.Default;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        {
                            if (response.IsContentProblem())
                            {
                                HttpProblem problem = await response.DeserializeJsonContentAsync<HttpProblem>().ConfigureAwait(false);
                                return new HttpProblemError(problem).ToResult();
                            }
                            else
                            {
                                ErrorResponse errorResponse = await response.DeserializeJsonContentAsync<ErrorResponse>().ConfigureAwait(false);
                                return errorResponse.Error.ToResult();
                            }
                        }

                    default:
                        {
                            UnexpectedStatusCodeError error = await UnexpectedStatusCodeError.CreateAsync(response, $"{nameof(IDriveClient)}.{nameof(UpsertFileMetadataResultAsync)}").ConfigureAwait(false);
                            return error.ToResult();
                        }
                }
            }
        }
    }
}
