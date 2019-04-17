/*
This source file is under MIT License (MIT)
Copyright (c) 2014 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System;

namespace Candea.Common.Caching
{
    /// <summary>
    /// Represents a location for storing and retrieving transient information as Key-Value Pairs.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Retrieves the item with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to interpret the value as.</typeparam>
        /// <param name="key">The unique key used to identify the stored value.</param>
        /// <returns>The value associated with the provided <paramref name="key"/>.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Adds or updates a cached <paramref name="item"/> with the provided <paramref name="key"/> and optional <paramref name="expiration"/> duration.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to interpret the <paramref name="item"/> as. Optional, unless boxing.</typeparam>
        /// <param name="item">The item to add or update.</param>
        /// <param name="key">The unique key used to identify the stored value.</param>
        /// <param name="expiration">An optional duration before the item should be cleared from cache.</param>
        void Put<T>(T item, string key, TimeSpan? expiration = null);

        /// <summary>
        /// Removes and retrieves the item with the provided <paramref name="key"/> from cache.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to interpret the value as.</typeparam>
        /// <param name="key">The unique key used to identify the stored value.</param>
        /// <returns>The removed item.</returns>
        T Remove<T>(string key);

        /// <summary>
        /// Removes and retrieves an <see cref="object"/> with the provided <paramref name="key"/> from cache.
        /// </summary>
        /// <param name="key">The unique key used to identify the stored value.</param>
        /// <returns>The removed <see cref="object"/>.</returns>
        object Remove(string key);
    }
}
