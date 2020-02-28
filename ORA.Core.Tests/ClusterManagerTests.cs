using System;
using FluentAssertions;
using ORA.API;
using Xunit;

namespace ORA.Core.Tests
{
    public class ClusterManagerTests : IClassFixture<CoreInitializationFixture>
    {
        private readonly Random _random;

        public ClusterManagerTests()
        {
            this._random = new Random();
        }

        [Fact]
        public void CreateClusterTest()
        {
            string name = "test_create_cluster_" + this._random.Next(1_000_000);

            Cluster cluster = Ora.GetClusterManager().CreateCluster(name);

            cluster.Name.Should().Be(name);
            cluster.Identifier.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetClusterTest()
        {
            Cluster cluster = Ora.GetClusterManager()
                .CreateCluster("test_get_cluster_" + this._random.Next(1_000_000));

            Cluster sameCluster = Ora.GetClusterManager().GetCluster(cluster.Identifier);
            sameCluster.Name.Should().Be(cluster.Name);
            sameCluster.Identifier.Should().Be(cluster.Identifier);
        }

        [Fact]
        public void DeleteClusterTest()
        {
            Cluster cluster = Ora.GetClusterManager()
                .CreateCluster("test_delete_cluster_" + this._random.Next(1_000_000));

            Ora.GetClusterManager().DeleteCluster(cluster.Identifier).Should().Be(true);
        }
    }
}
