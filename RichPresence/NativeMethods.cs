using System;
using System.Runtime.InteropServices;

public static class NativeMethods
{
    [DllImport("kernel32", SetLastError = true)]
    internal static extern int OpenProcess(
        int dwDesiredAccess,
        bool bInheritHandle,
        int dwProcessId);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern bool ReadProcessMemory(
        int hProcess,
        UIntPtr lpBaseAddress,
        byte[] lpBuffer,
        int nSize,
        UIntPtr lpNumberOfBytesRead);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern bool WriteProcessMemory(
        int hProcess,
        UIntPtr lpBaseAddress,
        byte[] lpBuffer,
        int nSize,
        UIntPtr lpNumberOfBytesRead);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern int VirtualAllocEx(
        int hProcess,
        UIntPtr lpAddress,
        int dwSize,
        int lAllocationType,
        int flProtect);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern bool VirtualFreeEx(
        int hProcess,
        UIntPtr lpAddress,
        int dwSize,
        int dwFreeType);

    [DllImport("kernel32", SetLastError = true)]
    public static extern int GetCurrentProcessId();
}
