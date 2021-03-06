// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using NSubstitute;
using RichardSzalay.MockHttp;
using Rixian.Drive;
using Rixian.Extensions.Tokens;
using Xunit;
using Xunit.Abstractions;
using static Rixian.Extensions.Errors.Prelude;

public class RequestValidationTests
{
    private readonly ITestOutputHelper logger;

    public RequestValidationTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Fact]
    public async Task ValidateRequest_Default_Success()
    {
        (string accessToken, ITokenClientFactory tokenClientFactory) = MockTokenClientFactory();

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {accessToken}")
            .WithQueryString("api-version", "2019-09-01")
            .With(r => r.Headers.Contains("Subscription-Key") == false)
            .Respond(HttpStatusCode.OK);

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(tokenClientFactory);

        serviceCollection.AddHttpClient(DriveClientOptions.DriveHttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => mockHttp);

        serviceCollection.AddDriveClient(new DriveClientOptions
        {
            TokenClientOptions = new ClientCredentialsTokenClientOptions
            {
                Authority = string.Empty,
                ClientId = string.Empty,
                ClientSecret = string.Empty,
            },
            DriveApiUri = new Uri("http://localhost"),
        });

        ServiceProvider services = serviceCollection.BuildServiceProvider();
        IDriveClient driveClient = services.GetRequiredService<IDriveClient>();

        _ = await driveClient.ListChildrenAsync("c:/").ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task ValidateRequest_CustomVersion_Success()
    {
        (string accessToken, ITokenClientFactory tokenClientFactory) = MockTokenClientFactory();
        string apiVersion = Guid.NewGuid().ToString();

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {accessToken}")
            .WithQueryString("api-version", apiVersion)
            .With(r => r.Headers.Contains("Subscription-Key") == false)
            .Respond(HttpStatusCode.OK);

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(tokenClientFactory);

        serviceCollection.AddHttpClient(DriveClientOptions.DriveHttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => mockHttp);

        serviceCollection.AddDriveClient(new DriveClientOptions
        {
            TokenClientOptions = new ClientCredentialsTokenClientOptions
            {
                Authority = string.Empty,
                ClientId = string.Empty,
                ClientSecret = string.Empty,
            },
            DriveApiUri = new Uri("http://localhost"),
            ApiVersion = apiVersion,
        });

        ServiceProvider services = serviceCollection.BuildServiceProvider();
        IDriveClient driveClient = services.GetRequiredService<IDriveClient>();

        _ = await driveClient.ListChildrenAsync("c:/").ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task ValidateRequest_ApiKey_Success()
    {
        (string accessToken, ITokenClientFactory tokenClientFactory) = MockTokenClientFactory();
        string apiKey = Guid.NewGuid().ToString();

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {accessToken}")
            .WithQueryString("api-version", "2019-09-01")
            .WithHeaders("Subscription-Key", apiKey)
            .Respond(HttpStatusCode.OK);

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(tokenClientFactory);

        serviceCollection.AddHttpClient(DriveClientOptions.DriveHttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => mockHttp);

        serviceCollection.AddDriveClient(new DriveClientOptions
        {
            TokenClientOptions = new ClientCredentialsTokenClientOptions
            {
                Authority = string.Empty,
                ClientId = string.Empty,
                ClientSecret = string.Empty,
            },
            DriveApiUri = new Uri("http://localhost"),
            ApiKey = apiKey,
        });

        ServiceProvider services = serviceCollection.BuildServiceProvider();
        IDriveClient driveClient = services.GetRequiredService<IDriveClient>();

        _ = await driveClient.ListChildrenAsync("c:/").ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task ValidateRequest_TLS12_Success()
    {
        (string accessToken, ITokenClientFactory tokenClientFactory) = MockTokenClientFactory();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(tokenClientFactory);

        serviceCollection.AddDriveClient(new DriveClientOptions
        {
            TokenClientOptions = new ClientCredentialsTokenClientOptions
            {
                Authority = string.Empty,
                ClientId = string.Empty,
                ClientSecret = string.Empty,
            },
            DriveApiUri = new Uri("http://localhost"),
        });

        ServiceProvider services = serviceCollection.BuildServiceProvider();
        HttpClient httpClient = services.GetRequiredService<IHttpClientFactory>().CreateClient(DriveClientOptions.DriveHttpClientName);
        HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://www.howsmyssl.com/a/check")).ConfigureAwait(false);
        response.IsSuccessStatusCode.Should().BeTrue();

        var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var jobj = JObject.Parse(responseJson);
        jobj.Value<string>("tls_version").Should().Be("TLS 1.2");
    }

    private static (string accessToken, ITokenClientFactory tokenClientFactory) MockTokenClientFactory()
    {
        var accessToken = Guid.NewGuid().ToString();
        ITokenInfo tokenInfo = Substitute.For<ITokenInfo>();
        tokenInfo.AccessToken.Returns(accessToken);
        ITokenClient tokenClient = Substitute.For<ITokenClient>();
        tokenClient.GetTokenAsync(Arg.Any<bool>()).Returns(Result(tokenInfo));
        ITokenClientFactory tokenClientFactory = Substitute.For<ITokenClientFactory>();
        tokenClientFactory.GetTokenClient(DriveClientOptions.DriveTokenClientName).Returns(Result(tokenClient));
        return (accessToken, tokenClientFactory);
    }
}
