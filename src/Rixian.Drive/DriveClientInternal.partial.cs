﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.0.6.0 (NJsonSchema v10.0.23.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Rixian.Drive
{
    internal partial class DriveClientInternal
    {
        /// <param name="path"></param>
        /// <param name="overwrite"></param>
        /// <param name="body"></param>
        /// <param name="tenantId">Optional tenant ID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Create Drive Item</summary>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<DriveItemInfo> CreateDriveItemAsync(string path, bool? overwrite = null, FileParameter body = null, Guid? tenantId = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            if (path == null)
                throw new System.ArgumentNullException("path");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("cmd/create?");
            urlBuilder_.Append(System.Uri.EscapeDataString("path") + "=").Append(System.Uri.EscapeDataString(ConvertToString(path, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            if (overwrite != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("overwrite") + "=").Append(System.Uri.EscapeDataString(ConvertToString(overwrite, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (tenantId != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("tenantId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(tenantId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    if (body != null)
                    {
                        var content_ = new System.Net.Http.MultipartFormDataContent();
                        var streamContent_ = new System.Net.Http.StreamContent(body.Data);
                        streamContent_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(body.ContentType);
                        content_.Add(streamContent_, "data", body.FileName);
                        request_.Content = content_;
                    }
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<DriveItemInfo>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default(DriveItemInfo);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }
    }
}
