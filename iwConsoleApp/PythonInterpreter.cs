using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace iwConsoleApp
{
    class PythonInterpreter
    {
        private ScriptEngine pyEngine = null;
        private ScriptRuntime pyRuntime = null;
        private ScriptScope pyScope = null;
        //private SimpleLogger _logger = new SimpleLogger();

        public PythonInterpreter()
        {
            if (pyEngine == null)
            {
                pyEngine = IronPython.Hosting.Python.CreateEngine();
                pyScope = pyEngine.CreateScope();
                //pyScope.SetVariable("log", _logger);
                //_logger.AddInfo("Python Initialized");
            }
        }

        private void CompileSource(string code)
        {
            ScriptSource script = pyEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            CompiledCode compiledCode = script.Compile();
            compiledCode.Execute(pyScope);
        }

        public Func<TResult> CompileSourceAndGetFunction<TResult>(String code, string functionName)
        {
            //ScriptSource script = pyEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);

            //CompiledCode compiledCode = script.Compile();

            //var result = compiledCode.Execute(pyScope);
            CompileSource(code);

            Func<TResult> fn = pyScope.GetVariable<Func<TResult>>(functionName);

            return fn;
        }

        public Func<TResult, T1, T2, T3> CompileSourceAndGetFunction<TResult, T1, T2, T3>(String code, string functionName)
        {
            //ScriptSource script = pyEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);

            //CompiledCode compiledCode = script.Compile();

            //var result = compiledCode.Execute(pyScope);
            CompileSource(code);

            Func<TResult, T1, T2, T3> fn = pyScope.GetVariable<Func<TResult, T1, T2, T3>>(functionName);

            return fn;
        }


        public Func<TResult, T> CompileSourceAndGetFunction<TResult, T>(String code, string functionName)
        {
            //ScriptSource script = pyEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);

            //CompiledCode compiledCode = script.Compile();

            //var result = compiledCode.Execute(pyScope);
            CompileSource(code);
            Func<TResult, T> fn = pyScope.GetVariable<Func<TResult, T>>(functionName);
            return fn;
        }

        public dynamic CompileSourceAndGetFunction(String code, string functionName)
        {
            CompileSource(code);
            return pyScope.GetVariable(functionName);

        }


        //     public string CompileSourceAndExecuteMethod()
        //     {
        //         using (ScriptEngine engine = new ScriptEngine())
        //         {
        //             engine.Execute(@"
        //def foo(a, b):
        //return a+b*2");

        //             // (1) Retrieve the function
        //             ICallable foo = (IronPython.Runtime.Calls.ICallable)engine.Evaluate("foo");

        //             // (2) Apply function
        //             object result = foo.Call(3, 25);
        //             return Result;
        //         }

        //     }

    }
}
