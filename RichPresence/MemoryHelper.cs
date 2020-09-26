using System;
using System.Text;

using static NativeMethods;

public static class MemoryHelper
{
    public static int processId;
    public static int processHandle;
    public static bool readSuccess;
    public static bool writeSuccess;


    public static bool Initialise()
    {
        processId = GetCurrentProcessId();
        return Initialise(processId);
    }

    public static bool Initialise(int processId)
    {
        MemoryHelper.processId = processId;
        processHandle = OpenProcess(0x38, false, processId);
        return processHandle != 0;
    }

    public static void Reset()
    {
        processId = 0;
        processHandle = 0;
    }

    public static byte ReadByte(int address)
    {
        return ReadBytes(address, 1)[0];
    }
    public static byte ReadByte(uint address)
    {
        return ReadBytes(address, 1)[0];
    }
    public static byte ReadByte(IntPtr address)
    {
        return ReadBytes(address, 1)[0];
    }
    public static byte ReadByte(UIntPtr address)
    {
        return ReadBytes(address, 1)[0];
    }


    public static short ReadShort(int address)
    {
        return BitConverter.ToInt16(ReadBytes(address, 2), 0);
    }
    public static short ReadShort(uint address)
    {
        return BitConverter.ToInt16(ReadBytes(address, 2), 0);
    }
    public static short ReadShort(IntPtr address)
    {
        return BitConverter.ToInt16(ReadBytes(address, 2), 0);
    }
    public static short ReadShort(UIntPtr address)
    {
        return BitConverter.ToInt16(ReadBytes(address, 2), 0);
    }


    public static ushort ReadUShort(int address)
    {
        return BitConverter.ToUInt16(ReadBytes(address, 2), 0);
    }
    public static ushort ReadUShort(uint address)
    {
        return BitConverter.ToUInt16(ReadBytes(address, 2), 0);
    }
    public static ushort ReadUShort(IntPtr address)
    {
        return BitConverter.ToUInt16(ReadBytes(address, 2), 0);
    }
    public static ushort ReadUShort(UIntPtr address)
    {
        return BitConverter.ToUInt16(ReadBytes(address, 2), 0);
    }


    public static int ReadInt(int address)
    {
        return BitConverter.ToInt32(ReadBytes(address, 4), 0);
    }
    public static int ReadInt(uint address)
    {
        return BitConverter.ToInt32(ReadBytes(address, 4), 0);
    }
    public static int ReadInt(IntPtr address)
    {
        return BitConverter.ToInt32(ReadBytes(address, 4), 0);
    }
    public static int ReadInt(UIntPtr address)
    {
        return BitConverter.ToInt32(ReadBytes(address, 4), 0);
    }


    public static uint ReadUInt(int address)
    {
        return BitConverter.ToUInt32(ReadBytes(address, 4), 0);
    }
    public static uint ReadUInt(uint address)
    {
        return BitConverter.ToUInt32(ReadBytes(address, 4), 0);
    }
    public static uint ReadUInt(IntPtr address)
    {
        return BitConverter.ToUInt32(ReadBytes(address, 4), 0);
    }
    public static uint ReadUInt(UIntPtr address)
    {
        return BitConverter.ToUInt32(ReadBytes(address, 4), 0);
    }


    public static UIntPtr ReadUIntPtr(int address)
    {
        return (UIntPtr)ReadUInt(address);
    }
    public static UIntPtr ReadUIntPtr(uint address)
    {
        return (UIntPtr)ReadUInt(address);
    }
    public static UIntPtr ReadUIntPtr(IntPtr address)
    {
        return (UIntPtr)ReadUInt(address);
    }
    public static UIntPtr ReadUIntPtr(UIntPtr address)
    {
        return (UIntPtr)ReadUInt(address);
    }


    public static IntPtr ReadIntPtr(int address)
    {
        return (IntPtr)ReadInt(address);
    }
    public static IntPtr ReadIntPtr(uint address)
    {
        return (IntPtr)ReadInt(address);
    }
    public static IntPtr ReadIntPtr(IntPtr address)
    {
        return (IntPtr)ReadInt(address);
    }
    public static IntPtr ReadIntPtr(UIntPtr address)
    {
        return (IntPtr)ReadInt(address);
    }


    public static long ReadLong(int address)
    {
        return BitConverter.ToInt64(ReadBytes(address, 8), 0);
    }
    public static long ReadLong(uint address)
    {
        return BitConverter.ToInt64(ReadBytes(address, 8), 0);
    }
    public static long ReadLong(IntPtr address)
    {
        return BitConverter.ToInt64(ReadBytes(address, 8), 0);
    }
    public static long ReadLong(UIntPtr address)
    {
        return BitConverter.ToInt64(ReadBytes(address, 8), 0);
    }


    public static ulong ReadULong(int address)
    {
        return BitConverter.ToUInt64(ReadBytes(address, 8), 0);
    }
    public static ulong ReadULong(uint address)
    {
        return BitConverter.ToUInt64(ReadBytes(address, 8), 0);
    }
    public static ulong ReadULong(IntPtr address)
    {
        return BitConverter.ToUInt64(ReadBytes(address, 8), 0);
    }
    public static ulong ReadULong(UIntPtr address)
    {
        return BitConverter.ToUInt64(ReadBytes(address, 8), 0);
    }


    public static float ReadFloat(int address)
    {
        return BitConverter.ToSingle(ReadBytes(address, 4), 0);
    }
    public static float ReadFloat(uint address)
    {
        return BitConverter.ToSingle(ReadBytes(address, 4), 0);
    }
    public static float ReadFloat(IntPtr address)
    {
        return BitConverter.ToSingle(ReadBytes(address, 4), 0);
    }
    public static float ReadFloat(UIntPtr address)
    {
        return BitConverter.ToSingle(ReadBytes(address, 4), 0);
    }


    public static double ReadDouble(int address)
    {
        return BitConverter.ToDouble(ReadBytes(address, 8), 0);
    }
    public static double ReadDouble(uint address)
    {
        return BitConverter.ToDouble(ReadBytes(address, 8), 0);
    }
    public static double ReadDouble(IntPtr address)
    {
        return BitConverter.ToDouble(ReadBytes(address, 8), 0);
    }
    public static double ReadDouble(UIntPtr address)
    {
        return BitConverter.ToDouble(ReadBytes(address, 8), 0);
    }


    public static bool ReadBoolean(int address)
    {
        return BitConverter.ToBoolean(ReadBytes(address, 4), 0);
    }
    public static bool ReadBoolean(uint address)
    {
        return BitConverter.ToBoolean(ReadBytes(address, 4), 0);
    }
    public static bool ReadBoolean(IntPtr address)
    {
        return BitConverter.ToBoolean(ReadBytes(address, 4), 0);
    }
    public static bool ReadBoolean(UIntPtr address)
    {
        return BitConverter.ToBoolean(ReadBytes(address, 4), 0);
    }


    public static byte[] ReadBytes(int address, int length)
    {
        return ReadBytes((uint)address, length);
    }
    public static byte[] ReadBytes(uint address, int length)
    {
        return ReadBytes((UIntPtr)address, length);
    }
    public static byte[] ReadBytes(IntPtr address, int length)
    {
        return ReadBytes((uint)address, length);
    }
    public static byte[] ReadBytes(UIntPtr address, int length)
    {
        byte[] bytes = new byte[length];
        readSuccess = ReadProcessMemory(processHandle, address, bytes, length, UIntPtr.Zero);
        return bytes;
    }


    public static string ReadString(int address)
    {
        return ReadString((UIntPtr)address);
    }
    public static string ReadString(uint address)
    {
        return ReadString((UIntPtr)address);
    }
    public static string ReadString(IntPtr address)
    {
        return ReadString((UIntPtr)(int)address);
    }
    public static string ReadString(UIntPtr address)
    {
        int length = 0;
        while (ReadByte(address + length) != 0)
        {
            length++;
        }
        string s = Encoding.UTF8.GetString(ReadBytes(address, length));
        return s.TrimEnd(char.Parse("�")); // remove corrupted characters at the end
    }


    public static void Write(int address, byte value)
    {
        Write(address, new byte[] { value });
    }
    public static void Write(uint address, byte value)
    {
        Write(address, new byte[] { value });
    }
    public static void Write(IntPtr address, byte value)
    {
        Write(address, new byte[] { value });
    }
    public static void Write(UIntPtr address, byte value)
    {
        Write(address, new byte[] { value });
    }


    public static void Write(int address, short value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, short value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, short value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, short value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, ushort value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, ushort value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, ushort value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, ushort value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, int value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, int value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, int value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, int value)
    {
        Write(address, BitConverter.GetBytes(value));
    }

    public static void Write(int address, uint value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, uint value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, uint value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, uint value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, IntPtr value)
    {
        Write(address, BitConverter.GetBytes((int)value));
    }
    public static void Write(uint address, IntPtr value)
    {
        Write(address, BitConverter.GetBytes((int)value));
    }
    public static void Write(IntPtr address, IntPtr value)
    {
        Write(address, BitConverter.GetBytes((int)value));
    }
    public static void Write(UIntPtr address, IntPtr value)
    {
        Write(address, BitConverter.GetBytes((int)value));
    }


    public static void Write(int address, UIntPtr value)
    {
        Write(address, BitConverter.GetBytes((uint)value));
    }
    public static void Write(uint address, UIntPtr value)
    {
        Write(address, BitConverter.GetBytes((uint)value));
    }
    public static void Write(IntPtr address, UIntPtr value)
    {
        Write(address, BitConverter.GetBytes((uint)value));
    }
    public static void Write(UIntPtr address, UIntPtr value)
    {
        Write(address, BitConverter.GetBytes((uint)value));
    }


    public static void Write(int address, long value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, long value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, long value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, long value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, ulong value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, ulong value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, ulong value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, ulong value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, float number)
    {
        Write(address, BitConverter.GetBytes(number));
    }
    public static void Write(uint address, float value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, float value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, float value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, double number)
    {
        Write(address, BitConverter.GetBytes(number));
    }
    public static void Write(uint address, double value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, double value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, double value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, bool value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(uint address, bool value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(IntPtr address, bool value)
    {
        Write(address, BitConverter.GetBytes(value));
    }
    public static void Write(UIntPtr address, bool value)
    {
        Write(address, BitConverter.GetBytes(value));
    }


    public static void Write(int address, string value)
    {
        Write(address, Encoding.UTF8.GetBytes(value + (char)0));
    }
    public static void Write(uint address, string value)
    {
        Write(address, Encoding.UTF8.GetBytes(value + (char)0));
    }
    public static void Write(IntPtr address, string value)
    {
        Write(address, Encoding.UTF8.GetBytes(value + (char)0));
    }
    public static void Write(UIntPtr address, string value)
    {
        Write(address, Encoding.UTF8.GetBytes(value + (char)0));
    }


    public static void Write(int address, byte[] bytes)
    {
        Write((UIntPtr)address, bytes);
    }
    public static void Write(uint address, byte[] bytes)
    {
        Write((UIntPtr)address, bytes);
    }
    public static void Write(IntPtr address, byte[] bytes)
    {
        Write((UIntPtr)(int)address, bytes);
    }
    public static void Write(UIntPtr address, byte[] bytes)
    {
        writeSuccess = WriteProcessMemory(processHandle, address, bytes, bytes.Length, UIntPtr.Zero);
    }


    public static int Allocate(int address, int length, int access = 0x40)
    {
        return Allocate((UIntPtr)address, length, access);
    }
    public static int Allocate(uint address, int length, int access = 0x40)
    {
        return Allocate((UIntPtr)address, length, access);
    }
    public static int Allocate(IntPtr address, int length, int access = 0x40)
    {
        return Allocate((UIntPtr)(int)address, length, access);
    }
    public static int Allocate(UIntPtr address, int length, int access = 0x40)
    {
        return VirtualAllocEx(processHandle, address, length, 0x3000, access);
    }

    public static bool Free(int address, int length)
    {
        return Free((UIntPtr)address, length);
    }
    public static bool Free(uint address, int length)
    {
        return Free((UIntPtr)address, length);
    }
    public static bool Free(IntPtr address, int length)
    {
        return Free((UIntPtr)(int)address, length);
    }
    public static bool Free(UIntPtr address, int length)
    {
        return VirtualFreeEx(processHandle, address, length, 0x8000); // 0x8000 = MEM_RELEASE
    }
}