using System;
using System.Linq;

namespace Restbucks.Service
{
    public static class Extensions
    {
        public static TProjection UnlessNull<TTarget, TProjection>(this TTarget target, Func<TTarget, TProjection> projection)
            where TTarget : class
        {
            if (target == null)
            {
                return default(TProjection);
            }
            return projection(target);
        }

        public static string GetParentUriString(this Uri uri)
        {
            return uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);
        }
    }
}