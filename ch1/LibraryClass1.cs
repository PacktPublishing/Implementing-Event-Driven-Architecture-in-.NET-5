public static class LibraryClass1
{
    // Use multi-targeting to expose platform-specific
    // APIs to .NET Standard.
    public static async Task<string> GetValue()
    {
#if NET461
        return CallNET461Api();
#elif WINDOWS_UWP
        return CallUwpApi();
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
#if NET461 || WINDOWS_UWP
            return true;
#else
            return false;
#endif
        }
    }
}