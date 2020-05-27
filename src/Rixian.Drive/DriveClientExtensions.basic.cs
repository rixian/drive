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
        public static async Task ClearFileMetadataAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result result = await driveClient.ClearFileMetadataResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsFail)
            {
                throw ApiException.Create(result.Error);
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
        public static async Task CopyAsync(this IDriveClient driveClient, CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result result = await driveClient.CopyResultAsync(source, target, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsFail)
            {
                throw ApiException.Create(result.Error);
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
        public static async Task<Drive> CreateDriveAsync(this IDriveClient driveClient, CreateDriveRequest request, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<Drive> result = await driveClient.CreateDriveResultAsync(request, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
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
        public static async Task<DriveItemInfo> CreateDriveItemAsync(this IDriveClient driveClient, CloudPath path, bool? overwrite = null, FileParameter fileContents = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<DriveItemInfo> result = await driveClient.CreateDriveItemResultAsync(path, overwrite, fileContents, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Deletes a file or directory.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a result or an error.</returns>
        public static async Task DeleteItemAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result result = await driveClient.DeleteItemResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsFail)
            {
                throw ApiException.Create(result.Error);
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
        public static async Task<HttpFileResponse> DownloadContentAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<HttpFileResponse> result = await driveClient.DownloadContentResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Checks if a file or directory exists.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either an exists reponse or an error.</returns>
        public static async Task<ExistsResponse> ExistsAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<ExistsResponse> result = await driveClient.ExistsResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Retrieve the metadata about the drive item.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a DriveItemInfo or an error.</returns>
        public static async Task<DriveItemInfo> GetItemInfoAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<DriveItemInfo> result = await driveClient.GetItemInfoResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
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
        public static async Task<ICollection<DriveFileInfo>> ImportFilesAsync(this IDriveClient driveClient, ICollection<ImportRecord> files, CloudPath path = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<ICollection<DriveFileInfo>> result = await driveClient.ImportFilesResultAsync(files, path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Lists directory children.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of DriveItemInfo's or an error.</returns>
        public static async Task<ICollection<DriveItemInfo>> ListChildrenAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<ICollection<DriveItemInfo>> result = await driveClient.ListChildrenResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Lists all available drives.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of drives or an error.</returns>
        public static async Task<ICollection<Drive>> ListDrivesAsync(this IDriveClient driveClient, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<ICollection<Drive>> result = await driveClient.ListDrivesResultAsync(tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Lists all metadata about a file.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a dictionary of metadata keys and values or an error.</returns>
        public static async Task<IDictionary<string, string>> ListFileMetadataAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<IDictionary<string, string>> result = await driveClient.ListFileMetadataResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Lists the streams associated with a file.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of file stream names or an error.</returns>
        public static async Task<ICollection<string>> ListFileStreamsAsync(this IDriveClient driveClient, CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<ICollection<string>> result = await driveClient.ListFileStreamsResultAsync(path, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
        }

        /// <summary>
        /// Lists all available partitions.
        /// </summary>
        /// <param name="driveClient">The IDriveClient instance.</param>
        /// <param name="tenantId">Optional. Specifies which tenant to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Either a collection of partitions or an error.</returns>
        public static async Task<ICollection<Partition>> ListPartitionsAsync(this IDriveClient driveClient, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result<ICollection<Partition>> result = await driveClient.ListPartitionsResultAsync(tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            throw ApiException.Create(result.Error);
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
        public static async Task MoveAsync(this IDriveClient driveClient, CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result result = await driveClient.MoveResultAsync(source, target, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsFail)
            {
                throw ApiException.Create(result.Error);
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
        public static async Task RemoveFileMetadataAsync(this IDriveClient driveClient, CloudPath path, string key, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result result = await driveClient.RemoveFileMetadataResultAsync(path, key, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsFail)
            {
                throw ApiException.Create(result.Error);
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
        public static async Task UpsertFileMetadataAsync(this IDriveClient driveClient, CloudPath path, IReadOnlyDictionary<string, string> metadata, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (driveClient is null)
            {
                throw new ArgumentNullException(nameof(driveClient));
            }

            Result result = await driveClient.UpsertFileMetadataResultAsync(path, metadata, tenantId, cancellationToken).ConfigureAwait(false);

            if (result.IsFail)
            {
                throw ApiException.Create(result.Error);
            }
        }
    }
}
