using System.Collections.Generic;
using ORA.API;

namespace ORA.Core
{
    public class OraCluster : Cluster
    {
        public OraCluster(string name, string identifier) : base(name, identifier)
        {
        }

        public override List<T> GetNodes<T>() => throw new System.NotImplementedException();

        public override T GetNode<T>(string identifier) => throw new System.NotImplementedException();

        public override void AddNode<T>(T node) => throw new System.NotImplementedException();

        public override void RemoveNode(string identifier) => throw new System.NotImplementedException();
    }
}
