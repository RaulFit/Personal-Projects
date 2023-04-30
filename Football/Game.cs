using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Game
    {
        private Team winner;
        private Team loser;
        readonly private bool draw;

        public Game(Team winner, Team loser, bool draw)
        {
            this.winner = winner;
            this.loser = loser;
            this.draw = draw;
        }

        public bool IsDraw()
        {
            if(this.draw)
            {
                return true;
            }

            return false;
        }

        public bool IsWinner(Team team)
        {
            return this.winner.CompareNames(team);
        }

        public bool IsLoser(Team team)
        {
            return this.loser.CompareNames(team);
        }


    }
}
