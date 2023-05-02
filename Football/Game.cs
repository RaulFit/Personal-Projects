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

        const int WinningPoints = 3;
        const int DrawPoints = 1;

        public Game(Team winner, Team loser, bool draw)
        {
            this.winner = winner;
            this.loser = loser;
            this.draw = draw;
        }

        public bool IsWinner(Team team)
        {
            return this.winner == team;
        }

        public bool IsLoser(Team team)
        {
            return this.loser == team;
        }

        public void AwardPoints()
        {
            if(this.draw)
            {
                this.winner.AddPoints(DrawPoints);
                this.loser.AddPoints(DrawPoints);
                return;
            }

            this.winner.AddPoints(WinningPoints);
        }
    }
}
