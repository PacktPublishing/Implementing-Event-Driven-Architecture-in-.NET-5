public static class LibraryClass1
{
    // Use multi-targeting to expose platform-specific
    // APIs to .NET Standard using .NET 5.
    public static async Task<string> GetValue()
    {
#if ANDROID
        return CallAndroiodApi();
#elif IOS
        return CalliOSApi ();
#elif WINDOWS
        return CallWindowsApi ();
#else
        throw new PlatformNotSupportedException();
#endif
    }

    // Library friendly enabler callers use to check support
    // DRY OS check or catch PlatformNotSupportedException
    public static bool IsSupported
    {
        get
        {
#if ANDROID || IOS || WINDOWS
            return true;
#else
            return false;
#endif
        }
    }
}