using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Candea.Common.Extensions
{

    /// <summary>
    /// Utility for building Delegates from reflected types.
    /// </summary>
    /// <remarks>
    /// Original Source: http://stackoverflow.com/questions/13041674/create-func-or-action-for-any-method-using-reflection-in-c
    /// </remarks>
    public static class DelegateBuilder
    {
        /// <summary>
        /// Converts a <see cref="MethodInfo"/> to a delegate with the provided signature.
        /// </summary>
        /// <typeparam name="T">The desired delegate signature.</typeparam>
        /// <param name="method">The reflected method details to be converted.</param>
        /// <param name="missingParamValues">The provided method parameters to be removed from the resulting delegate.</param>
        /// <returns>A delegate with the provided signature.</returns>
        public static T BuildDelegate<T>(this MethodInfo method, params object[] missingParamValues)
        {
            var queueMissingParams = new Queue<object>(missingParamValues);

            var dgtMi = typeof(T).GetMethod("Invoke");
            //var dgtRet = dgtMi.ReturnType;
            var dgtParams = dgtMi.GetParameters();

            var paramsOfDelegate = dgtParams
                .Select(tp => Expression.Parameter(tp.ParameterType, tp.Name))
                .ToArray();

            var methodParams = method.GetParameters();

            if (method.IsStatic)
            {
                var paramsToPass = methodParams
                    .Select((p, i) => CreateParam(paramsOfDelegate, i, p, queueMissingParams))
                    .ToArray();

                var expr = Expression.Lambda<T>(
                    Expression.Call(method, paramsToPass),
                    paramsOfDelegate);

                return expr.Compile();
            }
            else
            {
                var paramThis = Expression.Convert(paramsOfDelegate[0], method.DeclaringType);

                var paramsToPass = methodParams
                    .Select((p, i) => CreateParam(paramsOfDelegate, i + 1, p, queueMissingParams))
                    .ToArray();

                var expr = Expression.Lambda<T>(
                    Expression.Call(paramThis, method, paramsToPass),
                    paramsOfDelegate);

                return expr.Compile();
            }
        }

        private static Expression CreateParam(IList<ParameterExpression> paramsOfDelegate,
            int i, ParameterInfo callParamType, Queue<object> queueMissingParams)
        {
            if (i < paramsOfDelegate.Count)
                return Expression.Convert(paramsOfDelegate[i], callParamType.ParameterType);

            if (queueMissingParams.Count > 0)
                return Expression.Constant(queueMissingParams.Dequeue());

            return Expression.Constant(callParamType.ParameterType.IsValueType
                                        ? Activator.CreateInstance(callParamType.ParameterType)
                                        : null);
        }
    }
}
