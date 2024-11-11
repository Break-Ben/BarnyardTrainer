using System.Collections.Generic;

namespace Barnyard_Trainer
{
    public static class Addresses
    {
        public class Address
        {
            // Class fields
            protected readonly string address;
            protected readonly float defaultValue = float.NaN;

            // Constructors
            public Address(string address)
            {
                this.address = address;
            }

            public Address(string address, float defaultValue) : this(address)
            {
                this.defaultValue = defaultValue;
            }

            // Mutators
            public void Write(float value, string errorMessage = "Error writing float")
            {
                Memory.WriteFloat(address, value, errorMessage);
            }

            public void Write(string bytes, string errorMessage = "Error writing bytes")
            {
                Memory.WriteBytes(address, bytes, errorMessage);
            }

            public void Revert(string errorMessage = "Error restoring float")
            {
                if (defaultValue != float.NaN)
                    Memory.WriteFloat(address, defaultValue, errorMessage);
                else
                    Messages.DisplayError("Address has no default value");
            }

            // Accessors
            public float ReadFloat()
            {
                return Memory.ReadFloat(address);
            }

            public int ReadByte()
            {
                return Memory.ReadByte(address);
            }
        }

        public class Instruction : Address
        {
            // Class fields
            private readonly string originalValue;
            private readonly string nopString;

            // Constructor
            public Instruction(string address, string originalValue, int length) : base(address)
            {
                this.originalValue = originalValue;
                nopString = "";
                for (int i = 0; i < length; i++)
                    nopString += "90 ";
                nopString = nopString.TrimEnd();
            }

            // Mutators
            public void Nop(string errorMessage = "Error writing nop")
            {
                Memory.WriteBytes(address, nopString, errorMessage);
            }

            public void Restore(string errorMessage = "Error restoring instruction")
            {
                Memory.WriteBytes(address, originalValue, errorMessage);
            }
        }

        public static readonly Dictionary<string, Address> values = new Dictionary<string, Address>
        {
            { "xPos", new Address("Barnyard.exe+37E344,3C,1C,20,18") },
            { "yPos", new Address("Barnyard.exe+37E344,3C,1C,20,1C") },
            { "zPos", new Address("Barnyard.exe+37E344,3C,1C,20,20") },
            { "bikeXPos", new Address("Barnyard.exe+3C52AC") },
            { "bikeYPos", new Address("Barnyard.exe+3C52B0") },
            { "bikeZPos", new Address("Barnyard.exe+3C52B4") },
            { "bikeGroundVelocity", new Address("Barnyard.exe+3C54D0") },

            { "money", new Address("Barnyard.exe+3B493C,20") },
            { "moneyHud", new Address("Barnyard.exe+3B493C,1C") },
            { "itemCount", new Address("Barnyard.exe+37E344,3C,50,530") },
            { "fov", new Address("Barnyard.exe+3822AC", 1.04719758f) },
            { "stamina", new Address("Barnyard.exe+3B493C,28") },
            { "bikeStamina", new Address("Barnyard.exe+37E344,40,1C,60") },
            { "milk", new Address("Barnyard.exe+37E344,3C,70,C4") },
            { "day", new Address("Barnyard.exe+383D3C,30") },
            { "time", new Address("Barnyard.exe+383D3C,38") },

            { "cantSquirt", new Address("Barnyard.exe+3810E4") },
            { "isOnFoot", new Address("Barnyard.exe+38A0EC,20,C") },
            { "isSprintingFoot", new Address("Barnyard.exe+389568,8,4F0") },
            { "isSprintingBike", new Address("Barnyard.exe+38CD2C,0") },

            { "maxPitch", new Address("Barnyard.exe+3822E0,68,AC", -8f) },
            { "minPitch", new Address("Barnyard.exe+3822E0,68,B0", 22f) },
            { "camTargetDistance", new Address("Barnyard.exe+3822E0,68,84") },
            { "camMaxDistance", new Address("Barnyard.exe+3822E0,68,88", 5f) },
            { "bikeCamTargetDistance", new Address("Barnyard.exe+3822E0,2D0", 4f) },
            { "bikeCamTargetHeight", new Address("Barnyard.exe+3822E0,2EC", 0.8f) },
            { "opacity", new Address("Barnyard.exe+3822E0,68,B4") },
        };

        public static readonly Dictionary<string, Address> unitValues = new Dictionary<string, Address>
        {
            { "walkSpeed", new Address("Barnyard.exe+38A890,54,A84,BC,100", 1.5f) },
            { "runSpeed", new Address("Barnyard.exe+38A890,54,A84,BC,104", 4f) },
            { "sprintSpeed", new Address("Barnyard.exe+38A890,54,A84,BC,108", 7f) },
            { "acceleration", new Address("Barnyard.exe+38A890,54,A84,BC,D8", 10f) },
            { "deceleration", new Address("Barnyard.exe+38A890,54,A84,BC,DC", 30f) },
            { "jumpForce", new Address("Barnyard.exe+38A890,54,A84,BC,124", 8.5f) },
            { "radius", new Address("Barnyard.exe+37E35C,38,70,2C", 0.55f) },
            { "minClimbSpeed", new Address("Barnyard.exe+37E35C,38,28,118", 1f) },
            { "maxClimbSpeed", new Address("Barnyard.exe+37E35C,38,28,11C", 1.3f) },
            { "squirtDelay", new Address("Barnyard.exe+37E35C,38,70,80", 0.5f) },
        };

        public static readonly Dictionary<string, Instruction> instructions = new Dictionary<string, Instruction>
        {
            { "gravity", new Instruction("Barnyard.exe+17746F", "D9 59 1C", 3) },
            { "waterCollision", new Instruction("Barnyard.exe+21ABEC", "89 0E", 2) },
            { "noClip", new Instruction("Barnyard.exe+1773F9", "89 11", 2) },
            { "opacityChangeOne", new Instruction("Barnyard.exe+2B660A", "D9 11", 2) },
            { "opacityChangeTwo", new Instruction("Barnyard.exe+2B660A", "89 01", 2) },

            { "camZoom", new Instruction("Barnyard.exe+63DF3", "8B 96 8C 00 00 00 89 96 88 00 00 00", 12) },
            { "camZoomIn", new Instruction("Barnyard.exe+63E50", "89 01", 2) },
            { "camZoomOut", new Instruction("Barnyard.exe+63DEB", "89 8E 88 00 00 00", 6) },
            { "camReset", new Instruction("Barnyard.exe+62B71", "89 96 88 00 00 00", 6) },
            { "bikeCamDistanceReset", new Instruction("Barnyard.exe+5E626", "89 46 60", 3) },
            { "bikeCamHeightReset", new Instruction("Barnyard.exe+5E65B", "89 46 7C", 3) },
            { "maxPitchResetOne", new Instruction("Barnyard.exe+636EE", "D9 96 AC 00 00 00", 6) },
            { "maxPitchResetTwo", new Instruction("Barnyard.exe+63DC6", "D9 96 AC 00 00 00", 6) },
        };

        public static readonly Dictionary<string, Address> inputs = new Dictionary<string, Address>
        {
            { "space", new Address("DINPUT8.dll+312F1") },
            { "leftControl", new Address("DINPUT8.dll+312D5") }
        };
    }
}