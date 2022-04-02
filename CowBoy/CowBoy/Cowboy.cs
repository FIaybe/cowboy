namespace CowBoy
{
    public class Cowboy
    {
        public double Weight { get; set; }
        public double Speed { get; set; }
        public bool HasTheGun { get; set; }

        public Cowboy(double weight, double speed, bool hasTheGun)
        {
            Weight = weight;
            Speed = speed;
            HasTheGun = hasTheGun;
        }
    }
}