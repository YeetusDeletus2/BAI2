using System;
using System.Collections.Generic;
using System.Linq;

namespace BAI
{
    public partial class BAI_Afteken2
    {
        public static bool Vooruit(uint b)
        {
            return (b & 0b10000000) != 0;
        }

        public static uint Vermogen(uint b)
        {
            uint bits = (b & 0b01100000);
            return bits + (bits >> 5) + (bits >> 6);
        }

        public static bool Wagon(uint b)
        {
            return (b & 0b00010000) != 0;
        }

        public static bool Licht(uint b)
        {
            return (b & 0b00001000) != 0;
        }

        public static uint ID(uint b)
        {
            return (b & 0b00000111);
        }

        public static HashSet<uint> Alle(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>(inputStroom);
            return set;
        }

        public static HashSet<uint> ZonderLicht(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>(inputStroom.Where(var => !Licht(var)));
            return set;
            
        }

        public static HashSet<uint> MetWagon(List<uint> inputStroom)
        {
            HashSet<uint> set = new HashSet<uint>(inputStroom.Where(var => Wagon(var)));
            return set;
        }

        public static HashSet<uint> SelecteerID(List<uint> inputStroom, uint lower, uint upper)
        {
            HashSet<uint> set = new HashSet<uint>(inputStroom.Where(var => ID(var) >= lower && ID(var) <= upper));
            return set;
        }

        public static HashSet<uint> Opdr3a(List<uint> inputStroom)
        {
            HashSet<uint> set = SelecteerID(inputStroom,0,2);
            set.IntersectWith(ZonderLicht(inputStroom));
            return set;
        }

        public static HashSet<uint> Opdr3b(List<uint> inputStroom)
        {
            HashSet<uint> set = Alle(inputStroom);
            set.ExceptWith(Opdr3a(inputStroom));
            return set;
        }

        public static void ToonInfo(uint b)
        {
            Console.WriteLine(
                $"ID {ID(b)}, Licht {Licht(b)}, Wagon {Wagon(b)}, Vermogen {Vermogen(b)}, Vooruit {Vooruit(b)}");
        }

        public static List<uint> GetInputStroom()
        {
            List<uint> inputStream = new List<uint>();
            for (uint i = 0; i < 256; i++)
            {
                inputStream.Add(i);
            }

            return inputStream;
        }

        public static void PrintSet(HashSet<uint> x)
        {
            Console.Write("{");
            foreach (uint i in x)
                Console.Write($" {i}");
            Console.WriteLine($" }} ({x.Count} elementen)");
        }


        static void Main(string[] args)
        {
            Console.WriteLine("=== Opgave 1 ===");
            ToonInfo(210);
            ToonInfo(0);
            ToonInfo(255);
            Console.WriteLine();

            List<uint> inputStroom = GetInputStroom();

            Console.WriteLine("=== Opgave 2 ===");
            HashSet<uint> alle = Alle(inputStroom);
            PrintSet(alle);
            HashSet<uint> zonderLicht = ZonderLicht(inputStroom);
            PrintSet(zonderLicht);
            HashSet<uint> metWagon = MetWagon(inputStroom);
            PrintSet(metWagon);
            HashSet<uint> groter6 = SelecteerID(inputStroom, 6, 7);
            PrintSet(groter6);
            Console.WriteLine();

            Console.WriteLine("=== Opgave 3a ===");
            HashSet<uint> opg3a = Opdr3a(inputStroom);
            PrintSet(opg3a);
            foreach (uint b in opg3a)
            {
                ToonInfo(b);
            }

            Console.WriteLine();

            Console.WriteLine("=== Opgave 3b ===");
            HashSet<uint> opg3b = Opdr3b(inputStroom);
            PrintSet(opg3b);
            foreach (uint b in opg3b)
            {
                ToonInfo(b);
            }

            Console.WriteLine();
        }
    }
}