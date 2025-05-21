namespace TurfBookingProject.Helpers
{
    using System.Runtime.Loader;
    using System.Reflection;

    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public nint LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllPath)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }

}
