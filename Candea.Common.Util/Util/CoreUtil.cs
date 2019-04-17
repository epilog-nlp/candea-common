using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Candea.Common
{
    using static Util.corefunc;

    public static partial class Utility
    {

        /// <summary>
        /// Parses the supplied <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The target <see cref="Type"/>.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <returns>The equivalent parsed value.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.parse{T}(string)"/>.
        /// </remarks>
        public static T Parse<T>(string value) => parse<T>(value);

        /// <summary>
        /// Parses an enumeration.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="value">The value of the enumeration.</param>
        /// <returns>The equivalent parsed value.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.enum_parse{T}(string)"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static T EnumParse<T>(string value) => enum_parse<T>(value);

        /// <summary>
        /// Forms a union from the supplied elements.
        /// </summary>
        /// <typeparam name="T">The sequence element type.</typeparam>
        /// <param name="a">The first sequence.</param>
        /// <param name="b">The second sequence.</param>
        /// <param name="c">The third sequence.</param>
        /// <returns>A collection representing the union of the supplied elements.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.union{T}(IEnumerable{T}, IEnumerable{T}, IEnumerable{T})"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IEnumerable<T> Union<T>(IEnumerable<T> a, IEnumerable<T> b, IEnumerable<T> c) => union(a, b, c);

        /// <summary>
        /// Forms a union from the supplied elements.
        /// </summary>
        /// <typeparam name="T">The sequence element type.</typeparam>
        /// <param name="a">The first sequence.</param>
        /// <param name="b">The second sequence.</param>
        /// <param name="c">The third sequence.</param>
        /// <param name="d">The fourth sequence.</param>
        /// <returns>A collection representing the union of the supplied elements.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.union{T}(IEnumerable{T}, IEnumerable{T}, IEnumerable{T}, IEnumerable{T})"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IEnumerable<T> Union<T>(IEnumerable<T> a, IEnumerable<T> b, IEnumerable<T> c, IEnumerable<T> d) => union(a, b, c, d);

        /// <summary>
        /// Forms a union from the supplied elements.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="x">An input sequence.</param>
        /// <param name="y">An arbitrary number of elements.</param>
        /// <returns>A collection representing the union of the supplied elements.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.union{T}(IEnumerable{T}, IEnumerable{T}, T[])"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IEnumerable<T> Union<T>(IEnumerable<T> x, params T[] y) => union(x, y);

        /// <summary>
        /// Forms a union from the supplied elements.
        /// </summary>
        /// <typeparam name="T">The sequence element type.</typeparam>
        /// <param name="x">The first sequence.</param>
        /// <param name="y">The second sequence.</param>
        /// <param name="z">An arbitrary number of elements.</param>
        /// <returns>A collection representing the union of the supplied elements.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.union{T}(IEnumerable{T}, IEnumerable{T}, T[])"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IEnumerable<T> Union<T>(IEnumerable<T> x, IEnumerable<T> y, params T[] z) => union(x, y, z);

        /// <summary>
        /// Casts the sequence items to the specified type.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="items">The sequence of items to cast.</param>
        /// <returns>The resulting collection produced by casting each element of <paramref name="items"/> to the specified type.</returns>
        /// <remarks>
        /// Alias for <see cref="Util.corefunc.cast{T}(IEnumerable)"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IEnumerable<T> Cast<T>(IEnumerable items) => cast<T>(items);

        /// <summary>
        /// Negates the input value.
        /// </summary>
        /// <param name="value">The value of the input.</param>
        /// <returns>The negated input <paramref name="value"/>.</returns>
        /// <remarks>
        /// This function is defined to as to remove ambiguity when needed between the C# intrinsic (!)
        /// operator and the optional value (!) operator that yields an encapsulated value from
        /// within an optional value.
        /// Alias for <see cref="not(bool)"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static bool Not(bool value) => not(value);

        /// <summary>
        /// Creates a compiled regular expression from the supplied pattern.
        /// </summary>
        /// <param name="pattern">The pattern to be interpreted as a regular expression.</param>
        /// <returns>A compiled <see cref="System.Text.RegularExpressions.Regex"/>.</returns>
        /// <remarks>
        /// Based on <see cref="regex(string)"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static Regex ToRegex(this string pattern) => new Regex(pattern, RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="data"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static IEnumerable<S> Map<T, S>(this IEnumerable<T> data, Func<T, S> f) => map(data, f);

        /// <summary>
        /// Yields a <see cref="IReadOnlyList{T}"/> realization instance from an input sequence.
        /// </summary>
        /// <typeparam name="T">The input item type.</typeparam>
        /// <param name="items">The input items.</param>
        /// <returns>A <see cref="IReadOnlyList{T}"/> realization instance from the input sequence.</returns>
        /// <remarks>
        /// Alias for <see cref="list{T}(IEnumerable{T})"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> items) => list(items);

        /// <summary>
        /// Yields a <see cref="IReadOnlyList{T}"/> realization instance from provided input sequences.
        /// </summary>
        /// <typeparam name="T">The input item type.</typeparam>
        /// <param name="items">The input items.</param>
        /// <param name="more">Additional input items.</param>
        /// <returns>A <see cref="IReadOnlyList{T}"/> realization instance from the provided input sequences.</returns>
        /// <remarks>
        /// Alias for <see cref="list{T}(IEnumerable{T}, T[])"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> items, params T[] more) => list(items, more);

        /// <summary>
        /// Yields a <see cref="IReadOnlyList{T}"/> instance from an input sequence.
        /// </summary>
        /// <typeparam name="T">The input item type.</typeparam>
        /// <param name="parms">The input items.</param>
        /// <returns>A <see cref="IReadOnlyList{T}"/> realization instance from the provided input sequences.</returns>
        /// <remarks>
        /// Alias for <see cref="list{T}(T[])"/>.
        /// </remarks>
        public static IReadOnlyList<T> ToReadOnlyList<T>(params T[] parms) => list(parms);

        /// <summary>
        /// Creates a <see cref="Tuple{T1, T2}"/> instance.
        /// </summary>
        /// <typeparam name="T1">The type of the first item.</typeparam>
        /// <typeparam name="T2">The type of the second item.</typeparam>
        /// <param name="x1">The first item.</param>
        /// <param name="x2">The second item.</param>
        /// <returns>A <see cref="Tuple{T1, T2}"/> instance.</returns>
        /// <remarks>
        /// Alias for <see cref="tuple{T1, T2}(T1, T2)"/>.
        /// </remarks>
        public static Tuple<T1, T2> ToTuple<T1, T2>(T1 x1, T2 x2) => tuple(x1, x2);

        /// <summary>
        /// Creates tuple with item of Type <see cref="object"/> in the second position.
        /// </summary>
        /// <typeparam name="T">The type of the first item.</typeparam>
        /// <param name="x1">The first item.</param>
        /// <param name="x2">The second item.</param>
        /// <returns>A <see cref="Tuple{T1,Object}"/> instance.</returns>
        /// <remarks>
        /// Alias for <see cref="tupleo{T}(T, object)"/>.
        /// </remarks>
        public static Tuple<T, object> ToTupleO<T>(T x1, object x2) => tupleo(x1, x2);
    }
}
