using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChampRecommender.Models
{
    public class Champion
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<string>? Lanes { get; set; }

    }
}
