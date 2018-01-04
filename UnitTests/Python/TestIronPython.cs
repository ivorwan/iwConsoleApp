using iwConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Python
{
    [TestClass]
    public class TestIronPython
    {

        [TestMethod]
        public void Test_IronPython()
        {

            string code = "def foo(a):\n    return a*2";

            string code2 = @"
def foo():
    return 'Hello World'";

            string code3 = @"def foo(url):
    return url.split('-')[0]";


            string code4 = "import re\ndef foo(url):\n    return re.search( r'\\d+', url.split('/')[-1].split('-')[-1], re.M|re.I).group()";

            string code5 = @"def foo(jobTitle, location, req):
    return jobTitle + location + req";

            //            str = "Line1-abcdef \nLine2-abc \nLine4-abcd";
            //            print str.split()
            //print str.split(' ', 1)

            iwConsoleApp.PythonInterpreter py = new iwConsoleApp.PythonInterpreter();

            var dynFn5 = py.CompileSourceAndGetFunction(code5, "foo");


            var fn = py.CompileSourceAndGetFunction<int, int>(code, "foo");
            var fn2 = py.CompileSourceAndGetFunction<string>(code2, "foo");
            var fn3 = py.CompileSourceAndGetFunction<string, string>(code3, "foo");
            var fn4 = py.CompileSourceAndGetFunction<string, string>(code4, "foo");
            var fn5 = py.CompileSourceAndGetFunction<string, string, string, string>(code5, "foo");

            string url = "http://asdf.com/asdf/asdf/asdf/234-234555-12566545";


            var splitValue = fn3("asdf-2525-asWERQdf-3vcsdfr");
            Assert.AreEqual(splitValue, "asdf");

            var result = fn(3);
            Assert.AreEqual(result, 6);

            var x = fn(4);
            Assert.AreEqual(x, 8);

            var result2 = fn2();
            Assert.AreEqual(result2, "Hello World");
            //var fn3 = py.CompileSourceAndGetFunction<int, int>(code, "foo");
            var result3 = fn(4);
            Assert.AreEqual(result3, 8);

            var result4 = fn4(url);
            Assert.AreEqual(result4, "12566545");

            //py.CompileSourceAndExecute(code2);


        }
    }
}
