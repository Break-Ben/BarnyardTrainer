using Memory;
using System;
using System.Linq;

namespace Barnyard_Trainer
{
    public static class Memory
    {
        static readonly Mem mem = new Mem();

        public static void OpenProcess()
        {
            mem.OpenProcess(GetPID());
        }

        public static void WriteFloat(string address, float value, string errorMessage = "Error writing float")
        {
            if (!mem.WriteMemory(address, "float", value.ToString()))
                Messages.DisplayError(errorMessage);
        }

        public static void WriteBytes(string address, string value, string errorMessage = "Error writing bytes")
        {
            if (!mem.WriteMemory(address, "bytes", value))
                Messages.DisplayError(errorMessage);
        }

        public static float ReadFloat(string address)
        {
            return mem.ReadFloat(address);
        }

        public static int ReadByte(string address)
        {
            return mem.ReadByte(address);
        }

        public static int GetPID()
        {
            return mem.GetProcIdFromName("Barnyard.exe");
        }

        public static bool IsBarnyardOpen()
        {
            return GetPID() != 0;
        }

        // Converts an integer contained in a string into 4 hexadecimal bytes (in a string)
        public static string IntStringTo4Bytes(string intString)
        {
            return string.Join(" ", BitConverter.GetBytes(int.Parse(intString)).Select(b => "0x" + b.ToString("X2")));
        }
    }
}
