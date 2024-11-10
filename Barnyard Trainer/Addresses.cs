using System.Collections.Generic;

namespace Barnyard_Trainer
{
    public static class Addresses
    {
        public class Address
        {
            // Class fields
            protected readonly string address;

            // Constructor
            public Address(string address)
            {
                this.address = address;
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
            private readonly int length;
            private readonly string originalValue;

            // Constructor
            public Instruction(string address, int length, string originalValue) : base(address)
            {
                this.length = length;
                this.originalValue = originalValue;
            }

            // Mutators
            public void Nop(string errorMessage = "Error writing nop")
            {
                string value = "";
                for (int i = 0; i < length; i++)
                    value += "90 ";
                Memory.WriteBytes(address, value.TrimEnd(), errorMessage);
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
            { "fov", new Address("7822ac") },
            { "stamina", new Address("Barnyard.exe+3B493C,28") },
            { "bikeStamina", new Address("Barnyard.exe+37E344,40,1C,60") },
            { "milk", new Address("Barnyard.exe+37E344,3C,70,C4") },
            { "day", new Address("Barnyard.exe+383D3C,30") },

            { "camTargetDistance", new Address("Barnyard.exe+3822E0,68,88") },
            { "bikeCamTargetDistance", new Address("Barnyard.exe+3822E0,2D0") },
            { "bikeCamTargetHeight", new Address("Barnyard.exe+3822E0,2EC") },
            { "opacity", new Address("Barnyard.exe+3822E0,68,B4") },

            { "cantSquirt", new Address("7810e4") },
            { "isOnFoot", new Address("Barnyard.exe+38A0EC,20,C") },
            { "isSprintingFoot", new Address("Barnyard.exe+389568,8,4F0") },
            { "isSprintingBike", new Address("Barnyard.exe+38CD2C,0") },
        };

        public static readonly Dictionary<string, Address> unitValues = new Dictionary<string, Address>
        {
            { "walkSpeed", new Address("Barnyard.exe+CDCA8,20,C,50") },
            { "runSpeed", new Address("Barnyard.exe+CDCA8,20,C,54") },
            { "sprintSpeed", new Address("Barnyard.exe+CDCA8,20,C,58") },
            { "acceleration", new Address("Barnyard.exe+CDCA8,20,C,28") },
            { "deceleration", new Address("Barnyard.exe+CDCA8,20,C,2C") },
            { "jumpForce", new Address("Barnyard.exe+CDCA8,20,C,74") },
            { "radius", new Address("Barnyard.exe+37E35C,38,70,2C") },
            { "minClimbSpeed", new Address("Barnyard.exe+37E35C,38,28,118") },
            { "maxClimbSpeed", new Address("Barnyard.exe+37E35C,38,28,11C") },
            { "squirtDelay", new Address("Barnyard.exe+37E35C,38,70,80") },
        };

        public static readonly Dictionary<string, Instruction> instructions = new Dictionary<string, Instruction>
        {
            { "camZoom", new Instruction("Barnyard.exe+63DF3", 12, "8B 96 8C 00 00 00 89 96 88 00 00 00") },
            { "camZoomIn", new Instruction("Barnyard.exe+63E50", 2, "89 01") },
            { "camZoomOut", new Instruction("Barnyard.exe+63DEB", 6, "89 8E 88 00 00 00") },
            { "camReset", new Instruction("Barnyard.exe+62B71", 6, "89 96 88 00 00 00") },
            { "bikeCamDistanceReset", new Instruction("Barnyard.exe+5E626", 3, "89 46 60") },
            { "bikeCamHeightReset", new Instruction("Barnyard.exe+5E65B", 3, "89 46 7C") },
            { "minPitch", new Instruction("Barnyard.exe+2B6885", 2, "89 01") },
            { "maxPitch", new Instruction("Barnyard.exe+2B689B", 2, "89 11") },
            { "gravity", new Instruction("Barnyard.exe+17746F", 3, "D9 59 1C") },
            { "opacityChangeOne", new Instruction("Barnyard.exe+2B660A", 2, "D9 11") },
            { "opacityChangeTwo", new Instruction("Barnyard.exe+2B660A", 2, "89 01") },
            { "waterCollision", new Instruction("Barnyard.exe+21ABEC", 2, "89 0E") },
            { "noClip", new Instruction("Barnyard.exe+1773F9", 2, "89 11") },
        };

        public static readonly Dictionary<string, Address> inputs = new Dictionary<string, Address>
        {
            { "space", new Address("DINPUT8.dll+312F1") },
            { "leftControl", new Address("DINPUT8.dll+312D5") }
        };
    }
}