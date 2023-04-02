using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChampRecommender.Models
{
    public static class GetProcessStatus
    { 
        public static bool FindGameStartProcess()
        {
            return 1 <= Process.GetProcessesByName("League of Legend").Length;
        }

        public static bool FindGameOnProcess() 
        {
            return 1 <= Process.GetProcessesByName("LeagueClientUx").Length;
        }
    }
}
