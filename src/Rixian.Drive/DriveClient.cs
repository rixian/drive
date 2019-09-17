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
        public Task ClearFileMetadataAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ClearFileMetadataAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task CopyAsync(CloudPath source, CloudPath target, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.CopyAsync(source, target, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DriveItemInfo> CreateDriveItemAsync(CloudPath path, bool? overwrite = null, FileParameter body = null, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.CreateDriveItemAsync(path, overwrite, body, cancellationToken);
        }

        /// <inheritdoc/>
        public Task DeleteItemAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.DeleteItemAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileResponse> DownloadContentAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.DownloadContentAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ExistsResponse> ExistsAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ExistsAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DriveItemInfo> GetItemInfoAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.GetItemInfoAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<DriveItemInfo>> ListChildrenAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListChildrenAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IDictionary<string, string>> ListFileMetadataAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListFileMetadataAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ICollection<string>> ListFileStreamsAsync(CloudPath path, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.ListFileStreamsAsync(path, cancellationToken);
        }

        /// <inheritdoc/>
        public Task MoveAsync(CloudPath source, CloudPath target, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.MoveAsync(source, target, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveFileMetadataAsync(CloudPath path, string key, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.RemoveFileMetadataAsync(path, key, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpsertFileMetadataAsync(CloudPath path, IReadOnlyDictionary<string, string> metadata, CancellationToken cancellationToken = default)
        {
            return this.internalDriveClient.UpsertFileMetadataAsync(
                path,
                new UpsertFileMetadataRequest
                {
                    Metadata = metadata,
                },
                cancellationToken);
        }
    }
}
