/*
This source file is under MIT License (MIT)
Copyright (c) 2016 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Candea.Common.Util
{
    using Logging;

    /// <summary>
    /// Wrapper for action and function execution, to capture and log the processing times.
    /// </summary>
    public static class ExecuteMethodTemplates
    {
        private static readonly ILogger Logger =
            LogResolver.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Executes some action and logs the execution time
        /// </summary>
        /// <param name="a"></param>
        public static void Execute(Action a, [CallerMemberName] string m = "", [CallerFilePath] string f = "")
        {
            BuildLogEntry(m, f).Log(true);
            var s = new Stopwatch();
            s.Start();

            a(); //execute some code

            s.Stop();
            BuildLogEntry(m, f, s.ElapsedMilliseconds).Log(true);
        }

        /// <summary>
        /// Execcutes some function, logs the execution time, and returns the result
        /// </summary>
        /// <typeparam name="TResult">The type of the expected result</typeparam>
        /// <param name="func">The function to be executed</param>
        /// <returns>The result instance returned by the function</returns>
        public static TResult Execute<TResult>(Func<TResult> func, [CallerMemberName] string m = "", [CallerFilePath] string f = "")
        {
            var result = default(TResult);

            Execute(new Action(() => result = func()), m, f); //call the func and save the result

            return result;
        }

        private static void Log(this string m, bool toLog = false)
        {
            if (toLog)
            {
                Logger.Debug(m);
            }
            else
            {
                Debug.WriteLine(m);
            }
        }

        private static string BuildLogEntry(string m, string f, long? duration = null)
        {
            var startOrEnd = duration.HasValue ? "END" : "START";
            var durationStr = duration.HasValue ? $" Duration = {duration.Value} ms." : "";
            return $"\t\t[{f}~{m} {startOrEnd}] {durationStr}";
        }


    }
}
