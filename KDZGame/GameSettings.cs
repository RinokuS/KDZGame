using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KDZGame
{
    public static class GameSettings
    {
        public static DataTable myData = null;
        public static List<string> names = new List<string>();

        public static List<Hero> myTeam = new List<Hero>();
        public static List<Hero> enemyTeam = new List<Hero>();
    }
}
