using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public abstract class BaseOptions
    {
        public static List<string> GetOptions<T>()
        {
            var options = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            .Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(i => i.GetRawConstantValue().ToString()).ToList();
            return options;
        }
    }
}
