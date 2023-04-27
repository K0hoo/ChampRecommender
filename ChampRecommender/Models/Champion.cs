using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChampRecommender.Models
{
    class Champion
    {
        public int Id { get; }

        public string Name { get; }

        // Top: 0 ,Jungle: 1, Mid: 2, AD: 3, Support: 4
        public int Position { get; }

        public Image Image { get; }

    }
}
