namespace CowBoy
{
    public class Bridge
    {
        public double Length { get; set; }
        public double MaxWeight { get; set; }

        public Bridge(double length, double maxWeight)
        {
            Length = length;
            MaxWeight = maxWeight;
        }
    }
}