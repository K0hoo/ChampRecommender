using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Models
{
    public enum Tier
    {
        None,
        IRON,
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        DIAMOND,
        MASTER,
        GRANDMASTER,
        CHALLENGER
    }
    
    public enum Lane
    {
        TOP,
        jUNGLE,
        MID,
        AD,
        SUPPORT
    }
}
