using iwConsoleApp.CacheManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c=iwConsoleApp.CacheManager;


namespace UnitTests.CacheManager
{
    [TestClass]
    public class TestCacheManager
    {
        private IMyService _svc;
        private Mock<IMyService> _mock;
        public TestCacheManager()
        {
            _mock = new Mock<IMyService>();
            _svc = _mock.Object;
        }
        [TestMethod]
        public void Test_MethodWithNoParameters()
        {
            _mock.Setup(foo => foo.GetIds()).Returns(new List<string>() { "a", "b", "c", "d" });
            
            List<string> ret = c.CacheManager.GetFromCache("key1", () => _svc.GetIds());
            Assert.AreEqual(ret.Count(), 4);
            Assert.AreEqual(ret.Contains("a"), true);
            Assert.AreEqual(ret.Contains("b"), true);
            Assert.AreEqual(ret.Contains("c"), true);
            Assert.AreEqual(ret.Contains("d"), true);
        }

        [TestMethod]
        public void Test_MethodWithSingleParameter()
        {
            List<int> ids = new List<int>() { 12, 123, 123, 21, 3 };

            _mock.Setup(foo => foo.GetIds(ids)).Returns(new List<string>() { "abc", "def" });

            
            List<string> ret = c.CacheManager.GetFromCache("key2", () => _svc.GetIds(ids));
            Assert.AreEqual(ret.Count(), 2);
            Assert.AreEqual(ret.Contains("abc"), true);
            Assert.AreEqual(ret.Contains("def"), true);
        }

        [TestMethod]
        public void Test_MethodWithMultipleParameters()
        {
            List<int> ids = new List<int>() { 12, 123, 123, 21, 3 };
            _mock.Setup(foo => foo.GetIds(ids, true)).Returns(54);
            int ret = c.CacheManager.GetFromCache("key3", () => _svc.GetIds(ids, true));

            Assert.AreEqual(ret, 54);
        }
    }
}
