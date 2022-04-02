//Created by Florian Metz & Dubocage Julien

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CowBoy
{
    class CowBoyBridge
    {
        public static List<Cowboy> Cowboys;
        public static Bridge Bridge;

        static void Main(string[] args)
        {
            Cowboys = new List<Cowboy>();
            Init();
            PrintCowBoy();
            Console.WriteLine($"Il y a {Cowboys.Count} cowboys, ils ont un poids moyen de : {printDouble(Cowboys.Average(x=>x.Weight))} et une vitesse moyenne de {printDouble(Cowboys.Average(x=>x.Speed))}");
            Console.WriteLine($"Le pont peut supporter un poids de {printDouble(Bridge.MaxWeight)}, et il fait une longueur de {printDouble(Bridge.Length)}");

            Console.WriteLine("en prenant les plus rapide en premier : ");
            var speedOrdered = Cowboys.OrderByDescending(x => x.Speed).ToList();
            PassingBridge(speedOrdered);

            Console.WriteLine("en prenant les plus léger en premier : ");
            var wieghtOrdered = Cowboys.OrderByDescending(x => x.Weight).ToList();
            PassingBridge(wieghtOrdered);

        }

        static void Init()
        {
            var rnd = new Random();
            var nbCowBoy = rnd.Next(5,11); //entre 5 et 10 cowboy
            for (var i = 0; i < nbCowBoy; i++)
            {
                var weight = GetRandomDouble(50, 110); // chaque cowboy pese entre 50 et 110kg
                var speed = GetRandomDouble(0.5, 3);// chaque cowboy a une vitesse entre 0.5 et 3m/s
                Cowboys.Add(new Cowboy(weight, speed, false));
            }


            var length = GetRandomDouble(50, 100); //en metre
            var maxWeight = GetRandomDouble(100, 220); // minWeight*2, maxWeight * 2 (au moin deux personne passe ensemble)
            Bridge = new Bridge(length, maxWeight);

        }

        public static double GetRandomDouble(double minimum, double maximum)
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static void PassingBridge(List<Cowboy> orderedCowboys)
        {
            var fastest = orderedCowboys.FirstOrDefault();
            fastest.HasTheGun = true;

            var time = 0.0;
            var nbRotation = 0;
            var j = 1;
            var hasPassed = 0;
            while (hasPassed < orderedCowboys.Count)
            {
                var weigthCount=0.0;
                var group = new List<Cowboy>();
                group.Add(fastest);
                while (weigthCount < Bridge.MaxWeight)
                {
                    if (j==Cowboys.Count || weigthCount + Cowboys[j]?.Weight > Bridge.MaxWeight) break;
                    group.Add(Cowboys[j]);
                    weigthCount = group.Sum(x=> x.Weight);
                    j++;
                }

                var groupMinSpeed = group.Min(x => x.Speed);
                time += Bridge.Length / groupMinSpeed; //distance/speed = time en seconde because m/(m/s)
                time += Bridge.Length / fastest.Speed; //retour du mec rapide avec son pistolet
                nbRotation++;
                hasPassed += group.Count;
            }

            Console.WriteLine($"Ils ont mis  {printDouble(time)}s a traversée");
            Console.WriteLine($"Ca leur a pris {nbRotation} rotations");
        }

        public static void PrintCowBoy()
        {
            foreach (var cowboy in Cowboys)
            {
                Console.WriteLine($"Ce cowboy pese {printDouble(cowboy.Weight)}kg et a une vitesse de {printDouble(cowboy.Speed)}m/s");
            }
        }

        public static string printDouble(double x)
        {
            return String.Format("{0:0.00}", x);
        }
    }
}
