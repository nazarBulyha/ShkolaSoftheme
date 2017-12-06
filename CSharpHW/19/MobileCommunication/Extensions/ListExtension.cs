﻿using System.Collections.Generic;

namespace MobileCommunication.Extensions
{
    public static class ListExtenstions
    {
        public static void AddMany<T>(this List<T> list, params T[] elements)
        {
            list.AddRange(elements);
        }
    }
}