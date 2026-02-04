using System;

namespace Source.Scripts.Utillities
{
    public static class CheckerForNull
    {
        public static void ThrowIfNullArgument(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Аргумент нулевой.", nameof(obj));
            }
        }
    }
}
