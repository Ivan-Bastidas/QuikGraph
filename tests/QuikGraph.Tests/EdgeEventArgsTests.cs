using JetBrains.Annotations;
using NUnit.Framework;

namespace QuikGraph.Tests
{
    /// <summary>
    /// Tests for <see cref="EdgeEventArgs{TVertex,TEdge}"/>.
    /// </summary>
    [TestFixture]
    internal class EdgeEventArgsTests
    {
        #region Helpers

        public EdgeEventArgs<TVertex, TEdge> Constructor<TVertex, TEdge>([NotNull] TEdge edge)
            where TEdge : IEdge<TVertex>
        {
            // TODO: add assertions to method EdgeEventArgsTVertexTEdgeTest.Constructor(!!1)
            EdgeEventArgs<TVertex, TEdge> target = new EdgeEventArgs<TVertex, TEdge>(edge);
            return target;
        }

        #endregion

        [Test]
        [Ignore("Was already ignored")]
        public void ConstructorThrowsContractException944()
        {
            QuikGraphAssert.ThrowsContractException(
                () =>
                {
                    Constructor<int, Edge<int>>(null);
                });
        }

        [Test]
        public void Constructor571()
        {
            Edge<int> edge = EdgeFactory.Create(0, 0);
            EdgeEventArgs<int, Edge<int>> edgeEventArgs = Constructor<int, Edge<int>>(edge);
            Assert.IsNotNull(edgeEventArgs);
            Assert.IsNotNull(edgeEventArgs.Edge);
        }

        [Test]
        public void Constructor57101()
        {
            EdgeEventArgs<int, SEdge<int>> edgeEventArgs = Constructor<int, SEdge<int>>(default);
            Assert.IsNotNull(edgeEventArgs);
        }
    }
}
