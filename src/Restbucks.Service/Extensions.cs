using System;

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
    }
}