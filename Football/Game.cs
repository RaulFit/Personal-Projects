using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Game
    {
        private Team homeTeam;
        private Team awayTeam;
        readonly private int homeTeamScore;
        readonly private int awayTeamScore;

        const int WinningPoints = 3;
        const int DrawPoints = 1;

        public Game(Team homeTeam, Team awayTeam, int homeTeamScore, int awayTeamScore)
        {
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.homeTeamScore = homeTeamScore;
            this.awayTeamScore = awayTeamScore;
        }

        public void AwardPoints()
        {
            if(this.homeTeamScore > this.awayTeamScore)
            {
                this.homeTeam.AddPoints(WinningPoints);
                return;
            }

            if (this.homeTeamScore < this.awayTeamScore)
            {
                this.awayTeam.AddPoints(WinningPoints);
                return;
            }

            this.homeTeam.AddPoints(DrawPoints);
            this.awayTeam.AddPoints(DrawPoints);
        }
    }
}
