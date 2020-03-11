namespace EliteChroma.Core.Internal
{
    internal abstract class NativeMethodsAccessor
    {
        protected NativeMethodsAccessor(INativeMethods nativeMethods)
        {
            NativeMethods = nativeMethods;
        }

        protected INativeMethods NativeMethods { get; }
    }
}
