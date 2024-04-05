using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class Food : SimObject
    {
        double saturationValue;

        public Food(Vector position, double size, double saturationValue) : base(position, size)
        {
            this.saturationValue = saturationValue;
        }
    }
}