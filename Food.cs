using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class Food : SimObject
    {
        public double saturationValue;

        public Food(Vector position, double size, double saturationValue) : base(position, size)
        {
            this.saturationValue = saturationValue;
        }
    }
}