// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

namespace Rixian.Drive
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Rixian.Drive.Common;

    /// <summary>
    /// Default client for interacting with the Rixian Drive API.
    /// </summary>
    public class DriveClient : IDriveClient
    {
        private readonly DriveClientInternal internalDriveClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveClient"/> class.
        /// </summary>
        /// <param name="httpClient">HttpClient used for communication with the api endpoint.</param>
        public DriveClient(HttpClient httpClient)
        {
            this.internalDriveClient = new DriveClientInternal(httpClient);
        }

        /// <inheritdoc/>
        public Task ClearFileMetadataAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ClearFileMetadataAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CopyAsync(CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.CopyAsync(source, target, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Drive> CreateDriveAsync(CreateDriveRequest request, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.CreateDriveAsync(tenantId, request, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DriveItemInfo> CreateDriveItemAsync(CloudPath path, bool? overwrite = null, FileParameter body = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            return this.internalDriveClient.CreateDriveItemAsync(path, overwrite, body, tenantId, cancellationToken);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        /// <inheritdoc/>
        public Task DeleteItemAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.DeleteItemAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileResponse> DownloadContentAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.DownloadContentAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ExistsResponse> ExistsAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ExistsAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DriveItemInfo> GetItemInfoAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.GetItemInfoAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<DriveFileInfo>> ImportFilesAsync(ICollection<ImportRecord> files, CloudPath path = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ImportFilesAsync(
                new ImportRequest
                {
                    Files = files,
                },
                path,
                tenantId,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<DriveItemInfo>> ListChildrenAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListChildrenAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<Drive>> ListDrivesAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListDrivesAsync(tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IDictionary<string, string>> ListFileMetadataAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListFileMetadataAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<string>> ListFileStreamsAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListFileStreamsAsync(path, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<Partition>> ListPartitionsAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListPartitionsAsync(tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.MoveAsync(source, target, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveFileMetadataAsync(CloudPath path, string key, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.RemoveFileMetadataAsync(path, key, tenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpsertFileMetadataAsync(CloudPath path, IReadOnlyDictionary<string, string> metadata, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.UpsertFileMetadataAsync(
                path,
                new UpsertFileMetadataRequest
                {
                    Metadata = metadata,
                },
                tenantId,
                cancellationToken);
        }
    }
}
