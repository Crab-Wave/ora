﻿using System;
using System.Collections.Generic;
using ORA.API;
using ORA.API.Http;
using ORA.Core.Managers;
using Xunit;
using Xunit.Abstractions;
using FluentAssertion;

namespace ORA.Core.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper TestOutputHelper;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper;
        }

        [Fact]
        public void HttpTests()
        {
            OraCore.Initialize();
            HttpResponse httpResponse = Ora.GetHttpClient().Get("http://httpbin.org/get");
            Assert.NotNull(httpResponse);
            Assert.NotEmpty(httpResponse.Body);
        }

        [Fact]
        public void ClusterTests()
        {
            OraCluster test = new OraCluster("test","oui");
            Ora.Get().ClusterManager().CreateCluster("test").Should().Be(test);
            Ora.Get().ClusterManager().CreateCluster("test").Should().Throw<Exception>("Cluster already exists");
            Ora.Get().ClusterManager().GetCluster("oui").Should().Be(test);
            Ora.Get().ClusterManager().GetCluster("non").Should().Throw<Exception>("Couldn't find cluster with this non");
            Ora.Get().ClusterManager().DeleteCluster("oui").Should().Be(true);
            Ora.Get().ClusterManager().DeleteCluster("oui").Should().Be(false);
        }
    }
}
