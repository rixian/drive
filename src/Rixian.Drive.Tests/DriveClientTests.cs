// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0 license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RichardSzalay.MockHttp;
using Rixian.Drive;
using Rixian.Extensions.Errors;
using Rixian.Extensions.Tokens;
using Xunit;
using Xunit.Abstractions;
using static Rixian.Extensions.Errors.Prelude;

public class DriveClientTests
{
    private readonly ITestOutputHelper logger;

    public DriveClientTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    /*
    [Fact]
    public async Task ValidateRequest_Default_Success()
    {
        string tenantName = Guid.NewGuid().ToString();
        string tenantId = Guid.NewGuid().ToString();

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When(HttpMethod.Get, "*")
            .Respond("application/json", $"[{{'tenantId': '{tenantId}', 'name': '{tenantName}'}}]");

        IServiceProvider services = ConfigureServices(mockHttp);
        IDriveClient driveClient = services.GetRequiredService<IDriveClient>();

        ICollection<Tenant> tenants = await driveClient.().ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);

        tenants.Should().NotBeNullOrEmpty();
        tenants.Should().HaveCount(1);

        Tenant tenant = tenants.Single();
        tenant.Should().NotBeNull();
        tenant.TenantId.Should().Be(tenantId);
        tenant.Name.Should().Be(tenantName);
    }
    */

    private static IServiceProvider ConfigureServices(MockHttpMessageHandler mockHttp)
    {
        (string accessToken, ITokenClientFactory tokenClientFactory) = MockTokenClientFactory();

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

        return serviceCollection.BuildServiceProvider();
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
