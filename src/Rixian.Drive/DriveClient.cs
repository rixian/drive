// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

namespace Rixian.Drive
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Polly;
    using Rixian.Drive.Common;
    using Rixian.Extensions.Http.Client;

    /// <summary>
    /// Client for interacting with the Rixian Drive API.
    /// </summary>
    public class DriveClient : IDriveClient
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient to use for all requests.</param>
        public DriveClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets or sets the policy for the ClearFileMetadata http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ClearFileMetadataPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the Copy http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> CopyPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the CreateDrive http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> CreateDrivePolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the CreateDriveItem http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> CreateDriveItemPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the DeleteItem http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> DeleteItemPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the DownloadContent http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> DownloadContentPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the Exists http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ExistsPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the GetItemInfo http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> GetItemInfoPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the ImportFiles http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ImportFilesPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the ListChildren http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ListChildrenPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the ListDrives http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ListDrivesPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the ListFileMetadata http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ListFileMetadataPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the ListFileStreams http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ListFileStreamsPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the ListPartitions http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> ListPartitionsPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the Move http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> MovePolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the RemoveFileMetadata http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> RemoveFileMetadataPolicy { get; set; }

        /// <summary>
        /// Gets or sets the policy for the UpsertFileMetadata http request.
        /// </summary>
        protected IAsyncPolicy<HttpResponseMessage> UpsertFileMetadataPolicy { get; set; }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ClearFileMetadataHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/clear-metadata")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewClearFileMetadataAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ClearFileMetadataPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> CopyHttpResponseAsync(CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/copy")
                .SetQueryParam("source", source)
                .SetQueryParam("target", target)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewCopyAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.CopyPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> CreateDriveHttpResponseAsync(CreateDriveRequest request, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("drives")
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson()
                .WithContentJson(request);

            requestBuilder = await this.PreviewCreateDriveAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.CreateDrivePolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> CreateDriveItemHttpResponseAsync(CloudPath path, bool? overwrite = null, FileParameter fileContents = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/create")
                .SetQueryParam("path", path)
                .SetQueryParam("overwrite", overwrite, ignoreIfNull: true)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            if (fileContents != null)
            {
                requestBuilder
                    .WithMultipartFormContent()
                    .WithFile("data", fileContents.Data, fileContents.FileName, fileContents.ContentType);
            }

            requestBuilder = await this.PreviewCreateDriveItemAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.CreateDriveItemPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DeleteItemHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/delete")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewDeleteItemAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.DeleteItemPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> DownloadContentHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/download")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationOctet()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewDownloadContentAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.DownloadContentPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ExistsHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/exists")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewExistsAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ExistsPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetItemInfoHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/info")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewGetItemInfoAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.GetItemInfoPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ImportFilesHttpResponseAsync(ICollection<ImportRecord> files, CloudPath path = null, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/import")
                .SetQueryParam("path", path, ignoreIfNull: true)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson()
                .WithContentJson(new
                {
                    files = files,
                });

            requestBuilder = await this.PreviewImportFilesAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ImportFilesPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ListChildrenHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/dir")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewListChildrenAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ListChildrenPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ListDrivesHttpResponseAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("drives")
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewListDrivesAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ListDrivesPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ListFileMetadataHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/list-metadata")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewListFileMetadataAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ListFileMetadataPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ListFileStreamsHttpResponseAsync(CloudPath path, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/streams")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewListFileStreamsAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ListFileStreamsPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> ListPartitionsHttpResponseAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("partitions")
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Get()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewListPartitionsAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ListPartitionsPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> MoveHttpResponseAsync(CloudPath source, CloudPath target, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/move")
                .SetQueryParam("source", source)
                .SetQueryParam("target", target)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewMoveAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.MovePolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> RemoveFileMetadataHttpResponseAsync(CloudPath path, string key, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(Properties.Resources.KeyParameterValueMissingErrorMessage, nameof(key));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/remove-metadata")
                .SetQueryParam("path", path)
                .SetQueryParam("key", key)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            requestBuilder = await this.PreviewRemoveFileMetadataAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.RemoveFileMetadataPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> UpsertFileMetadataHttpResponseAsync(CloudPath path, IReadOnlyDictionary<string, string> metadata, Guid? tenantId = null, CancellationToken cancellationToken = default)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            IHttpRequestMessageBuilder requestBuilder = UrlBuilder
                .Create("cmd/upsert-metadata")
                .SetQueryParam("path", path)
                .SetQueryParam("tenantId", tenantId, ignoreIfNull: true)
                .ToRequest()
                .WithHttpMethod().Post()
                .WithAcceptApplicationJson()
                .WithAcceptApplicationProblemJson();

            IMultipartFormContentBuilder contentBuilder = requestBuilder.WithMultipartFormContent();
            foreach (KeyValuePair<string, string> pair in metadata)
            {
                contentBuilder = contentBuilder.WithString(pair.Key, pair.Value);
            }

            requestBuilder = await this.PreviewListChildrenAsync(requestBuilder, cancellationToken).ConfigureAwait(false);
            HttpResponseMessage response = await this.SendRequestWithPolicy(requestBuilder, this.ListChildrenPolicy, cancellationToken).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ClearFileMetadata.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewClearFileMetadataAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to Copy.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewCopyAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to CreateDrive.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewCreateDriveAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to CreateDriveItem.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewCreateDriveItemAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to DeleteItem.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewDeleteItemAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to DownloadContent.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewDownloadContentAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to Exists.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewExistsAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to GetItemInfo.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewGetItemInfoAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ImportFiles.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewImportFilesAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ListChildren.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewListChildrenAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ListDrives.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewListDrivesAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ListFileMetadata.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewListFileMetadataAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ListFileStreams.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewListFileStreamsAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to ListPartitions.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewListPartitionsAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to Move.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewMoveAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to RemoveFileMetadata.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewRemoveFileMetadataAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        /// <summary>
        /// Optional method for configurings the HttpRequestMessage before sending the call to UpsertFileMetadata.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The updated IHttpRequestMessageBuilder.</returns>
        protected virtual Task<IHttpRequestMessageBuilder> PreviewUpsertFileMetadataAsync(IHttpRequestMessageBuilder httpRequestMessageBuilder, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(httpRequestMessageBuilder);
        }

        private async Task<HttpResponseMessage> SendRequestWithPolicy(IHttpRequestMessageBuilder requestBuilder, IAsyncPolicy<HttpResponseMessage> policy = null, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = requestBuilder.Request;
            using (request)
            {
                Func<CancellationToken, Task<HttpResponseMessage>> sendRequest = (ct) => this.httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct);
                if (policy != null)
                {
                    HttpResponseMessage response = await policy.ExecuteAsync(sendRequest, cancellationToken).ConfigureAwait(false);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = await sendRequest(cancellationToken).ConfigureAwait(false);
                    return response;
                }
            }
        }
    }
}
