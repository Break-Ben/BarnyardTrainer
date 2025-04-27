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
            public Instruction(string address, string originalValue) : base(address)
            {
                this.originalValue = originalValue;
                nopString = "";
                for (int i = 0; i < (originalValue.Length + 1) / 3; i++)
                    nopString += "90 ";
                nopString = nopString.TrimEnd();
            }

            // Mutators
            public void Nop(string errorMessage = "Error writing NOP")
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
            { "xPos", new Address("77E344,3C,1C,20,18") },
            { "yPos", new Address("77E344,3C,1C,20,1C") },
            { "zPos", new Address("77E344,3C,1C,20,20") },
            { "bikeXPos", new Address("7C52AC") },
            { "bikeYPos", new Address("7C52B0") },
            { "bikeZPos", new Address("7C52B4") },
            { "bikeGroundVelocity", new Address("7C54D0") },

            { "money", new Address("7B493C,20") },
            { "moneyHud", new Address("7B493C,1C") },
            { "itemCount", new Address("77E344,3C,50,530") },
            { "fov", new Address("7822AC", 1.04719758f) },
            { "stamina", new Address("7B493C,28") },
            { "bikeStamina", new Address("77E344,40,1C,60") },
            { "milk", new Address("77E344,3C,70,C4") },
            { "day", new Address("783D3C,30") },
            { "time", new Address("783D3C,38") },

            { "cantSquirt", new Address("7810E4") },
            { "isOnFoot", new Address("78A0EC,20,C") },
            { "isSprintingFoot", new Address("789568,8,4F0") },
            { "isSprintingBike", new Address("78CD2C,0") },

            { "minPitch", new Address("7822E0,68,B0", -8f) },
            { "maxPitch", new Address("7822E0,68,AC", 22f) },
            { "camTargetDistance", new Address("7822E0,68,84") },
            { "camMaxDistance", new Address("7822E0,68,88", 5f) },
            { "camHeight", new Address("7822E0,68,94", 1.7f) },
            { "bikeCamMaxDistance", new Address("7822E0,2D0", 4f) },
            { "bikeCamHeight", new Address("7822E0,2EC", 0.8f) },
            { "camX", new Address("7822E0,124") },
            { "camY", new Address("7822E0,128") },
            { "camZ", new Address("7822E0,12C") },
            { "opacity", new Address("7822E0,68,B4") },
        };

        public static readonly Dictionary<string, Address> unitValues = new Dictionary<string, Address>
        {
            { "walkSpeed", new Address("78A890,54,A84,BC,100", 1.5f) },
            { "runSpeed", new Address("78A890,54,A84,BC,104", 4f) },
            { "sprintSpeed", new Address("78A890,54,A84,BC,108", 7f) },
            { "acceleration", new Address("78A890,54,A84,BC,D8", 10f) },
            { "deceleration", new Address("78A890,54,A84,BC,DC", 30f) },
            { "jumpForce", new Address("78A890,54,A84,BC,124", 8.5f) },
            { "radius", new Address("77E35C,38,70,2C", 0.55f) },
            { "minClimbSpeed", new Address("77E35C,38,28,118", 1f) },
            { "maxClimbSpeed", new Address("77E35C,38,28,11C", 1.3f) },
            { "squirtDelay", new Address("77E35C,38,70,80", 0.5f) },
        };

        public static readonly Dictionary<string, Instruction> instructions = new Dictionary<string, Instruction>
        {
            { "gravity", new Instruction("57746F", "D9 59 1C") },
            { "waterCollision", new Instruction("61ABEC", "89 0E") },
            { "noClip", new Instruction("5773F9", "89 11") },
            { "opacityChangeOne", new Instruction("6B660A", "D9 11") },
            { "opacityChangeTwo", new Instruction("6B660A", "89 01") },

            { "camZoom", new Instruction("463DF3", "8B 96 8C 00 00 00 89 96 88 00 00 00") },
            { "camZoomIn", new Instruction("463E50", "89 01") },
            { "camZoomOut", new Instruction("463DEB", "89 8E 88 00 00 00") },
            { "camReset", new Instruction("462B71", "89 96 88 00 00 00") },
            { "bikeCamDistanceReset", new Instruction("45E626", "89 46 60") },
            { "bikeCamHeightReset", new Instruction("45E65B", "89 46 7C") },
            { "maxPitchResetOne", new Instruction("4636EE", "D9 96 AC 00 00 00") },
            { "maxPitchResetTwo", new Instruction("463DC6", "D9 96 AC 00 00 00") },

            { "camLockXFoot", new Instruction("45B2A2", "89 07") },
            { "camLockXBike", new Instruction("45F914", "89 02") },
            { "camLockYFoot", new Instruction("45B2A7", "89 4F 04") },
            { "camLockYBike", new Instruction("45F91A", "89 4A 04") },
            { "camLockZFoot", new Instruction("45B2AD", "89 57 08") },
            { "camLockZBike", new Instruction("45F921", "89 42 08") },
        };
    }
}