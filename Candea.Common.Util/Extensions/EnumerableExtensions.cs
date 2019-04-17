﻿/*
This source file is under MIT License (MIT)
Copyright (c) 2014 Mihaela Iridon
https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Candea.Common.Extensions
{
    /// <summary>
    /// Extension methods for 
    /// </summary>
    public static class EnumerableExtensions
    {
        public static int? IndexOf<T>(this IEnumerable<T> items, T item) where T : IEquatable<T>
        {
            var itemsList = items.ToList();
            for (var i = 0; i < itemsList.Count; i++)
            {
                if (itemsList[i].Equals(item))
                    return i;
            }
            return null;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> a)
        {
            if (a != null)
                items?.ToList().ForEach(a);
        }
    }
}
