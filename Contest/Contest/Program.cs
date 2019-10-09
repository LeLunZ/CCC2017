using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contest
{
    class Program
    {
        static int numberOfWishes;
        static Location[] wFroms;
        static Location[] wTos;
        static int[] times;
        static List<Location> locations;
        static List<Location> stops;
        static List<List<Location>> listOfRoutes;
        static Location Hub;
        static void Main(string[] args)
        {
            rand = new Random();

            int z;
            z = Convert.ToInt32(Console.ReadLine());

            locations = new List<Location>();

            for (int i = 0; i < z; i++)
            {
                string a = Console.ReadLine();
                string[] n = a.Split(' ');
                locations.Add(new Location(n[0], Convert.ToInt32(n[1]), Convert.ToInt32(n[2])));
            }
            //numberOfWishes = Convert.ToInt32(Console.ReadLine());
            //wFroms = new Location[numberOfWishes];
            //wTos = new Location[numberOfWishes];
            //times = new int[numberOfWishes];
            //for (int i = 0; i < numberOfWishes; i++)
            //{
            //    string input1 = Console.ReadLine();
            //    string[] input1a = input1.Split(' ');
            //    int wishTime = Convert.ToInt32(input1a[2]);
            //    wFroms[i] = locations.Find(x => x.name == input1a[0]);
            //    wTos[i] = locations.Find(x => x.name == input1a[1]);
            //    times[i] = wishTime;
            //}



            //string input2 = Console.ReadLine();
            //string[] input2a = input2.Split(' ');
            //string hyperFrom = input2a[0];
            //string hyperTo = input2a[1];
            /*int wishLikers = Convert.ToInt32(Console.ReadLine());


            foreach (Location outer in locations)
            {
                foreach(Location inner in locations)
                {
                    int countedLikers = countLikers(outer, inner);

                    if (countedLikers >= wishLikers)
                    {
                        Console.WriteLine(outer.name + " " + inner.name);
                        return;
                    }
                }
            }


            //Location hyperA = locations.Find(x => x.name == hyperFrom);
            //Location hyperB = locations.Find(x => x.name == hyperTo);
            */

            string input2 = Console.ReadLine();
            string[] input2a = input2.Split(' ');
            string wishFrom = input2a[0];
            string wishTo = input2a[1];

            string loopString = Console.ReadLine();

            string[] loopLocs = loopString.Split(' ');

            int N = Convert.ToInt32(Console.ReadLine());
            int D = Convert.ToInt32(Console.ReadLine());


                for (int a = 0; a < 1000000; a++)
                {
                    List<Location> randpath = getRandomPath(D); //path of max D size

                    stops = randpath;

                if (randpath.Count >= 2 && countLikers(randpath) >= N)
                {
                    Console.Write(randpath.Count + " ");

                    foreach (Location l in randpath)
                    {
                        Console.Write(l.name + " ");
                    }

                    break;
                }
                
            }



            /*stops = new List<Location>();
            for (int i = 1; i < loopLocs.Length; i++)
            {
                Location current = locations.Find(x => x.name == loopLocs[i]);
                current.hasStop = true;
                stops.Add(current);
            }

            Location locWF = locations.Find(x => x.name == wishFrom);
            Location locWT = locations.Find(x => x.name == wishTo);

            double totalTime = journeyTime(locWF, locWT);


            Console.WriteLine(Math.Round(totalTime));*/
            listOfRoutes = new List<List<Location>>();

            string Input = Console.ReadLine();
            Location wishF = locations.Find(x => x.name == Input.Split(' ')[0]);
            Location wishT = locations.Find(x => x.name == Input.Split(' ')[1]);
            string inputHub = Console.ReadLine();
            Hub = locations.Find(x => x.name == inputHub);
            int numberOfHyper = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= numberOfHyper; i++)
            {
                string route = Console.ReadLine();
                string[] oneroute = route.Split(' ');

                List<Location> currentPath = new List<Location>();

                for (int j = 1; j <oneroute.Length; j++)
                {
                    Location current = locations.Find(x => x.name == oneroute[j]);
                    currentPath.Add(current);
                    current.hasStop.Add(i);
                }

                listOfRoutes.Add(currentPath);
            }

            double myhubTime = hubTime(wishF, wishT);

            Console.WriteLine(Math.Round(myhubTime));
        }

        static Random rand;
        
        private static List<Location> getRandomPath(int D)
        {
            locations.ForEach(x => x.hasStop.Remove(x.hasStop[0]));
            List<Location> Path = new List<Location>();
            double currentSize = 0.0;
            int random = rand.Next(0, locations.Count);
            Location first = locations[random];
            first.hasStop = locations[random].hasStop;
            Path.Add(first);
            
            while (currentSize <= D)
            {
                random = rand.Next(0, locations.Count);
                Location second = locations[random];
                if (second.hasStop.Count != 0)
                {
                    continue;
                }
                second.hasStop = locations[random].hasStop;
                Path.Add(second);

                currentSize += first.distanceTo(second);

                first = second;
            }

            Path[Path.Count - 1].hasStop.Remove(Path[Path.Count - 1].hasStop[0]);
            Path.RemoveAt(Path.Count - 1);

            return Path;
        }
 

        public static double hubTime(Location fromw, Location tow)
        {
            myhyperid = getClosestStop(fromw).hasStop[0];
            stops = listOfRoutes[myhyperid - 1];
            double timeDirect = journeyTimeSingleHyper(fromw, tow);


            double timeWithHub = 0.0;

            
            myhyperid = getClosestStop(fromw).hasStop[0];
            stops = listOfRoutes[myhyperid - 1];

            double timeToHub = journeyTimeSingleHyper(fromw, Hub);
            

            myhyperid = getClosestStop(tow).hasStop[0];
            stops = listOfRoutes[myhyperid - 1];

            double timeFromHub = journeyTimeSingleHyperHUB(tow);

            

            timeWithHub = timeFromHub + 300.0 + timeToHub;

            if (Hub == fromw)
            {
                timeWithHub -= 300.0;
            }


            return Math.Min(timeWithHub, timeDirect);
        }

        public static double hyperTime(Location fromstop, Location tostop)
        {
            double time = 0.0;

            int fs = stops.FindIndex(x => x == fromstop);
            int ts = stops.FindIndex(x => x == tostop);
            if (ts < fs)
            {
                int temp = fs;
                fs = ts;
                ts = temp;
            }


            for (int i = fs; i < ts; i++)
            {
                time += timebetweentwostops(stops[i], stops[i + 1]);
            }

            return time;
        }

        public static Location getClosestStop(Location position)
        {
            Location nearest = null;
            double bestdist = Double.MaxValue;
            foreach (Location stop in locations)
            {
                if (stop.hasStop.Count > 0 && stop.distanceTo(position) < bestdist) {
                    nearest = stop;
                    bestdist = stop.distanceTo(position);
                }
            }
            return nearest;
        }

        public static Location getClosestStop(Location position, int hyperid)
        {
            Location nearest = null;
            double bestdist = Double.MaxValue;
            foreach (Location stop in locations)
            {
                if (stop.hasStop.Contains(hyperid) && stop.distanceTo(position) < bestdist)
                {
                    nearest = stop;
                    bestdist = stop.distanceTo(position);
                }
            }
            return nearest;
        }

         public static int countLikers(List<Location> route)
         {
             int counter = 0;
             for (int i = 0; i < numberOfWishes; i++)
             {
                 double hypertime = hubTime(wFroms[i], wTos[i]);
                 if (hypertime < times[i])
                 {
                     counter++;
                 }
             }
             return counter;
         }

        static int myhyperid;

        public static double journeyTimeSingleHyper(Location locWF, Location locWT)
        {
            double totalTime = 0.0;

            totalTime += getClosestStop(locWF).distanceTo(locWF) / 15.0;
            totalTime += getClosestStop(locWT, myhyperid).distanceTo(locWT) / 15.0;
            
            totalTime += hyperTime(getClosestStop(locWF), getClosestStop(locWT, getClosestStop(locWF).hasStop[0]));

            return totalTime;
        }

        public static double journeyTimeSingleHyperHUB(Location locWT)
        {
            double totalTime = 0.0;

            totalTime += getClosestStop(locWT, myhyperid).distanceTo(locWT) / 15.0;

            totalTime += hyperTime(Hub, getClosestStop(locWT));

            return totalTime;
        }

        //public static int countLikers(Location hyperA, Location hyperB)
        //{
        //    int counter = 0;
        //    for (int i = 0; i < numberOfWishes; i++)
        //    {
        //        double hypertime = getHyper(wFroms[i], wTos[i], hyperA, hyperB);
        //        if (hypertime < times[i])
        //        {
        //            counter++;
        //        }
        //    }
        //    return counter;
        //}

        public static double timebetweentwostops(Location stopsA, Location stopB)
        {
            return stopsA.distanceTo(stopB) / 250.0 + 200.0;
        }
        /*
        public static double getHyper(Location wFrom, Location wTo, Location hyperA, Location hyperB)
        {
            double totalTime = 0.0;

            if (wFrom.distanceTo(hyperA) < wFrom.distanceTo(hyperB))
            {
                //A nearest to from
                totalTime += wFrom.distanceTo(hyperA) / 15.0;
                totalTime += wTo.distanceTo(hyperB) / 15.0;

            }
            else
            {
                //B nearest to from
                totalTime += wFrom.distanceTo(hyperB) / 15.0;
                totalTime += wTo.distanceTo(hyperA) / 15.0;
            }

            totalTime += hyperA.distanceTo(hyperB) / 250.0 + 200.0;
            return totalTime;
        }*/
    }
}
