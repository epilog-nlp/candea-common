/*
This source file is under MIT License (MIT)
Copyright (c) 2016 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ComponentModel.Composition.Hosting;

namespace Candea.Common.DI
{
    using static Util.auxfunc;
    using static Extensions.ReflectionExtensions;

    /// <summary>
    /// Utility methods for facilitating Service Location using MEF.
    /// </summary>
    public static class Mef
    {
        private static readonly string DefaultPath;
        private static readonly string[] SearchPatterns;

        static Mef()
        {
            var configuredPathToScan = appSetting<string>("MEF.AssembliesPath");
            DefaultPath = configuredPathToScan ?? executingAssemblyDir();
            var searchPatterns = appSetting<string>("MEF.CsvSearchPatterns");
            SearchPatterns = searchPatterns?.Split(',');
        }

        /// <summary>
        /// Returns an instance of type T. 
        /// IMPORANT: if type T or the realization of T implements IDisposable, use the Resolve method that returns the container, instead of this method.
        /// </summary>
        /// <typeparam name="T">The interface type. T should not be implementing IDisposable.</typeparam>
        /// <param name="exportName">The export name (optional)</param>
        /// <param name="path">The path to the realization assembly (optional).</param>
        /// <returns>An instance of a non-disposable type.</returns>
        public static T Resolve<T>(string exportName = null, string path = null) =>
            Resolve<T>(out IDisposable c, exportName, path);



        /// <summary>
        /// Returns an instance of type T - which could be disposable. If it is disposable, instead of calling Dispose on T, Dispose on the output variable container should be called instead.
        /// If T is not disposable, the container will be disposed of before this method returns.
        /// </summary>
        /// <typeparam name="T">The interface type. T may be disposable.</typeparam>
        /// <param name="container">The disposable container that controls the lifecycle of the instance of T. When done, this container should be disposed, instead of disposing T.</param>
        /// <param name="exportName">The export name (optional)</param>
        /// <param name="path">The path to the realization assembly (optional).</param>
        /// <returns>An instance of a potentially disposable type.</returns>
        public static T Resolve<T>(out IDisposable container, string exportName = null, string path = null)
        {
            CompositionContainer c = null;
            var r = Resolve(catalog =>
            {
                var result = GetValueFromCatalog<T>(catalog, out c, exportName);
                if (!result.GetType().ImplementsInterface(typeof(IDisposable)))
                    c.Dispose();
                return result;
            }, exportName, path);
            container = c;
            return r;
        }

        private static T Resolve<T>(Func<AggregateCatalog, T> f, string exportName = null, string path = null)
        {
            Debug.WriteLine($". . . . . . . Calling Resolve for type: {typeof(T).Name}");
            var result = default(T);
            var sw = new Stopwatch();
            sw.Start();

            Debug.WriteLine($"MEF: ExportName={exportName}. Path={path}");

            Debug.WriteLine($"Current Directory: {Environment.CurrentDirectory}");

            try
            {
                //using (var catalog = new DirectoryCatalog(path ?? DefaultPath, "Voxai.*"))
                using (var catalog = new AggregateCatalog())
                {
                    if (SearchPatterns != null && SearchPatterns.Length > 0)
                    {
                        SearchPatterns.ToList().ForEach(x => catalog.Catalogs.Add(new DirectoryCatalog(path ?? DefaultPath, x.EndsWith("dll") ? x : $"{x}dll")));
                    }
                    else
                    {
                        catalog.Catalogs.Add(new DirectoryCatalog(path ?? DefaultPath));
                    }

                    result = f(catalog);
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                Debug.WriteLine($"Bad DLLs. Reflection Type Load exception: {e.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"MEF exception: {e.Message}\n{e.StackTrace}");
                throw new ApplicationException($"MEF exception: {e.Message}", e);
            }

            sw.Stop();
            Debug.WriteLine($"MEF resolve execution time for loading type {typeof(T).Name} = {sw.ElapsedMilliseconds}ms");
            return result;
        }


        private static T GetValueFromCatalog<T>(AggregateCatalog catalog, out CompositionContainer container, string exportName = null)
        {

            container = new CompositionContainer(catalog);

            var export = exportName == null ? container.GetExport<T>() : container.GetExport<T>(exportName);
            if (export == null)
                throw new ApplicationException($"MEF: Implementation for type {typeof(T).Name} not found. " +
                                               $"Full Path(s)={string.Join(",", catalog.Catalogs.Where(x => x is DirectoryCatalog).Cast<DirectoryCatalog>().Select(x => x.FullPath))}; exportName={exportName}");

            return export.Value;
            //the container will be disposed if only if T or the realization of T does not implement IDisposable
        }

    }
}
