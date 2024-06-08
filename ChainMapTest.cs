using System.Collections.Generic;
using ChainMapLib;
using Xunit;

namespace ChainMapTests
{
    public class ChainMapTests
    {
        [Fact]
        public void TestGet()
        {
            var dict1 = new Dictionary<string, string> { { "a", "1" }, { "b", "2" } };
            var dict2 = new Dictionary<string, string> { { "b", "22" }, { "c", "33" } };
            var chainMap = new ChainMap<string, string>(dict1, dict2);
            Assert.Equal("1", chainMap["a"]);
            Assert.Equal("2", chainMap["b"]);
            Assert.Equal("33", chainMap["c"]);
        }

        [Fact]
        public void TestAdd()
        {
            var dict1 = new Dictionary<string, string> { { "a", "1" } };
            var chainMap = new ChainMap<string, string>(dict1);
            chainMap.Add("b", "2");
            Assert.Equal("2", chainMap["b"]);
        }

        [Fact]
        public void TestRemove()
        {
            var dict1 = new Dictionary<string, string> { { "a", "1" } };
            var chainMap = new ChainMap<string, string>(dict1);
            chainMap.Remove("a");
            Assert.False(chainMap.ContainsKey("a"));
        }

        [Fact]
        public void TestMerge()
        {
            var dict1 = new Dictionary<string, string> { { "a", "1" } };
            var dict2 = new Dictionary<string, string> { { "b", "22" }, { "c", "33" } };
            var chainMap = new ChainMap<string, string>(dict1, dict2);
            var merged = chainMap.Merge();
            Assert.Equal("1", merged["a"]);
            Assert.Equal("22", merged["b"]);
            Assert.Equal("33", merged["c"]);
        }
    }
}
