﻿// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

namespace Rixian.Drive
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Rixian.Drive.Common;

    /// <summary>
    /// Interface that defines operations for interacting with the Rixian Drive API.
    /// </summary>
    public partial interface IDriveClient
    {
        /// <summary>
        /// Downloads the file contents or alternate stream data.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<FileResponse> DownloadContentAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the metadata about the drive item.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<DriveItemInfo> GetItemInfoAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all metadata about a file.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<IDictionary<string, string>> ListFileMetadataAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Imports custom files into Rixian Drive.
        /// </summary>
        /// <param name="files">The files to import.</param>
        /// <param name="path">The path to the default import location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<ICollection<DriveFileInfo>> ImportFilesAsync(ICollection<ImportRecord> files, CloudPath path = null, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update and/or insert metadata onto a file.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="metadata">All metadata items to update.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task UpsertFileMetadataAsync(CloudPath path, IReadOnlyDictionary<string, string> metadata, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Clears all file metadata.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task ClearFileMetadataAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a specific file metadata item.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="key">The metadata key.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task RemoveFileMetadataAsync(CloudPath path, string key, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a file or directory.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DeleteItemAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new file or directory.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="overwrite">Overwite the existing file contents.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<DriveItemInfo> CreateDriveItemAsync(CloudPath path, bool? overwrite = null, FileParameter fileContents = null, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists directory children.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ICollection<DriveItemInfo>> ListChildrenAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a file or directory exists.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ExistsResponse> ExistsAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists the streams associated with a file.
        /// </summary>
        /// <param name="path">The path to a location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ICollection<string>> ListFileStreamsAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Copies a file or directory.
        /// </summary>
        /// <param name="source">The source location.</param>
        /// <param name="target">The target location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task CopyAsync(CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves a file or directory.
        /// </summary>
        /// <param name="source">The source location.</param>
        /// <param name="target">The target location.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task MoveAsync(CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new drive.
        /// </summary>
        /// <param name="request">The request body for creating a new drive.</param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Drive> CreateDriveAsync(CreateDriveRequest request, Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all available drives.
        /// </summary>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ICollection<Drive>> ListDrivesAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all available partitions.
        /// </summary>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ICollection<Partition>> ListPartitionsAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    }
}
