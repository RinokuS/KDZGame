using System.Collections.Generic;
using System.Data;

namespace KDZGame
{
    /// <summary>
    /// Some static parameters
    /// </summary>
    public static class GameSettings
    {
        public static int count = 1;
        public static DataTable myData = null;
        public static List<string> names = new List<string>();

        public static List<Hero> myTeam = new List<Hero>();
        public static List<Hero> enemyTeam = new List<Hero>();
    }
}
