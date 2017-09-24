using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class RegexTest
    {
        [TestMethod]
        public void Test_Regex()
        {
            Regex regex = new Regex("[a-z]");
            Assert.IsTrue(regex.IsMatch("abc"));
            Assert.IsFalse(regex.IsMatch("ABC"));
            Assert.IsFalse(regex.IsMatch(""));

            Regex regex2 = new Regex("[a-z]", RegexOptions.IgnoreCase);
            Assert.IsTrue(regex2.IsMatch("abc"));
            Assert.IsTrue(regex2.IsMatch("ABC"));
            Assert.IsFalse(regex2.IsMatch(""));

            Regex regex3 = new Regex("^$|^[a-z]", RegexOptions.IgnorePatternWhitespace);
            Assert.IsTrue(regex3.IsMatch(""));

            Regex regex4 = new Regex("[a-z]*");
            Assert.IsTrue(regex4.IsMatch(""));
        }
    }
}
