using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using ChampRecommender.Models;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Net;

namespace ChampRecommender.Dataset
{
    public static class Champions
    {
        private static List<Champion> champions = new List<Champion>();
        private static List<List<int>> champByLane = new List<List<int>>();

        static Champions() 
        {
            champions = new List<Champion>();

            for (int i = 0; i < 5; ++i)
            {
                champByLane.Add(new List<int>());
            }

            List<string> names = ChampionList.names;
            List<int> ids = ChampionList.ids;
            List<List<string>> lanes = ChampionList.lanes;

            int champCnt = names.Count;

            for  (int i = 0;i < champCnt; ++i)
            {
                foreach(string lane in lanes[i])
                {
                    if (lane == Lane.TOP) champByLane[0].Add(ids[i]);
                    else if (lane == Lane.JUNGLE) champByLane[1].Add(ids[i]);
                    else if (lane == Lane.MID) champByLane[2].Add(ids[i]);
                    else if (lane == Lane.BOTTOM) champByLane[3].Add(ids[i]);
                    else if (lane == Lane.SUPPORT) champByLane[4].Add(ids[i]);
                }

                champions.Add(new Champion
                {
                    Name = names[i],
                    Id = ids[i],
                    Lanes = lanes[i],
                });
            }
            /*
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JToken jObject = (JObject)JToken.ReadFrom(reader);
                jObject = jObject["champions"];
                foreach (var o in jObject)
                {
                    List<string> lanes = new List<string>();
                    JArray jArray =  JArray.Parse(o["lane"].ToString());
                    foreach (var lane in  jArray)
                    {
                        string l = lane.ToString();
                        lanes.Add(l.ToString());

                        if (l == Lane.TOP) champByLane[0].Add(Convert.ToInt32(o["id"]));
                        else if (l == Lane.JUNGLE) champByLane[1].Add(Convert.ToInt32(o["id"]));
                        else if (l == Lane.MID) champByLane[2].Add(Convert.ToInt32(o["id"]));
                        else if (l == Lane.BOTTOM) champByLane[3].Add(Convert.ToInt32(o["id"]));
                        else if (l == Lane.SUPPORT) champByLane[4].Add(Convert.ToInt32(o["id"]));
                    }
                    
                    champions.Add(new Champion
                     {
                         Name = o["name"].ToString(),
                         Id = Convert.ToInt32(o["id"]),
                         Lanes = lanes
                     });
                    
                }
            }
            */
        }

        public static void initChampions()
        {
            Champion champ = champions[0];
            return;
        }

        public static Champion? GetChampionById(int id)
        {
            foreach (Champion champion in champions)
            {
                if (champion.Id == id) return champion;
            }
            return null;
        }

        public static Champion? GetChampionByName(string name)
        {
            foreach (Champion champion in champions)
            {
                if (champion.Name == name) return champion;
            }
            return null;
        }

        public static List<int>? GetChampListByLane(string lane)
        {
            if (lane == Lane.TOP) return champByLane[0];
            else if (lane == Lane.JUNGLE) return champByLane[1];
            else if (lane == Lane.MID) return champByLane[2];
            else if (lane == Lane.BOTTOM) return champByLane[3];
            else if (lane == Lane.SUPPORT) return champByLane[4];
            else return null;
        }
    }
}
