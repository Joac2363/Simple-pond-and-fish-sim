//14159265358979323846264338327950288419716939937510
using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class PRandom
    {
        public static bool[] numbers = {false, false, false, true, true, false, true, true, false, true, true, true, true, true, false, false, false, true, false, true, false, true, false, false, false, true, false, false, true, true, true, false, false, true, true, false, false, true, true, false, true, true, false, true, true, false, true, true, false, false};
        static int pointer = 0;
        public PRandom()
        {
        }
        public static bool boolean()
        {
            MovePointer();
            return PRandom.numbers[pointer];
        }
        static void MovePointer()
        {
            if (pointer == 49)
            {
                pointer = 0;
            } else
            {
                pointer++;
            }
        }
    }
}