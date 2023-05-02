using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Ranking
    {
        private Team[] teams;

        const int MaxTeams = 10;

        public Ranking()
        {
            this.teams = new Team[MaxTeams];
        }
        
        public void AddTeam(Team team)
        {
            int i = 0;
            while (i < this.teams.Length && this.teams[i] != null)
            {
                i++;
            }

            this.teams[i] = team;
        }

        public Team GetTeam(int index)
        {
            index--;
            if(index < 0 || index >= this.teams.Length)
            {
                return null;
            }

            return this.teams[index];
        }

        public int GetPosition(Team team)
        {
            if (this.teams.Contains(team))
            {
                return Array.IndexOf(teams, team) + 1;
            }

            return 0;
        }

        public void UpdateRanking(Game game)
        {
            bool hasWinner = false;
            bool hasLoser = false;
            for(int i = 0; i < this.teams.Length; i++)
            {
                if (game.IsWinner(teams[i]))
                {
                    hasWinner = true;
                }

                if (game.IsLoser(teams[i]))
                {
                    hasLoser = true;
                }
            }

            if(hasWinner && hasLoser)
            {
                game.AwardPoints();
                GenerateRanking();
            }
        }

        private void GenerateRanking()
        {
            for(int i = 0; this.teams[i] != null; i++)
            {
                for(int j = i + 1; this.teams[j] != null; j++)
                {
                    if (teams[i].HasLessPoints(teams[j]))
                    {
                        var aux = teams[i];
                        teams[i] = teams[j];
                        teams[j] = aux;
                    }
                }
            }
        }
    }
}
