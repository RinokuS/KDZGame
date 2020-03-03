using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZGame
{
    public class Hero
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int minDmg { get; set; }
        public int maxDmg { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Growth { get; set; }
        public int AI_Value { get; set; }
        public int Gold { get; set; }
        public bool Alive { get; set; }
        public int RoundsDead { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="name">Name of our character</param>
        /// <param name="atk">Attack points of our character</param>
        /// <param name="dfc">Defence points of our character</param>
        /// <param name="minD">min Damage points of our character</param>
        /// <param name="maxD">max Damage points of our character</param>
        /// <param name="hp">Health points of our character</param>
        /// <param name="spd">Speed of our character</param>
        /// <param name="grwth">Growth of our character</param>
        /// <param name="AI_v">AI Value of our character</param>
        /// <param name="gold">Gold of our character</param>
        public Hero(string name, int atk, int dfc, int minD, int maxD, int hp, int spd, int grwth, int AI_v, int gold)
        {
            Name = name;
            Attack = atk;
            Defence = dfc;
            minDmg = minD;
            maxDmg = maxD;
            Health = hp;
            Speed = spd;
            Growth = grwth;
            AI_Value = AI_v;
            Gold = gold;
            Alive = true;
            RoundsDead = 0;
        }

        public void AttackAction(Hero enemy)
        {
            double atkPoints = Attack + 0.8 * Speed + 0.1 * Growth + 0.2 * AI_Value;
            double dfcPoints = enemy.Defence + 0.7 * enemy.Speed + 0.22 * enemy.Growth + 0.2 * enemy.AI_Value;

            double resPoints = (atkPoints - dfcPoints) / (Health - enemy.Health);

            if (resPoints < 0)
            {

            }
            else
            {
                if (resPoints < 0.4)
                    enemy.DefenceAction(enemy.Health);
                else
                    enemy.DefenceAction((minDmg + maxDmg) / 2);
            }
        }

        public void DefenceAction(int damage)
        {
            Health -= damage;
            IsAlive();
        }

        public void IsAlive()
        {
            if (Health <= 0)
            {
                Alive = false;
            }
        }
    }
}
