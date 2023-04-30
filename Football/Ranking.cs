using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Ranking
    {
        private List<Team> teams = new List<Team>();

        const int WinningPoints = 3;
        const int DrawPoints = 1;

        public Ranking(List<Team> teams)
        {
            this.teams = teams;
        }
        
        public void AddTeam(Team team)
        {
            this.teams.Add(team);
        }

        public Team GetTeam(int index)
        {
            index--;
            if(index < 0 || index >= this.teams.Count)
            {
                return null;
            }

            return this.teams[index];
        }

        public int GetPosition(Team team)
        {
            return this.teams.IndexOf(team) + 1;
        }

        public void UpdateRanking(Game game)
        {
            for(int i = 0; i < this.teams.Count; i++)
            {
                if(game.IsDraw() && (game.IsWinner(teams[i]) || game.IsLoser(teams[i])))
                {
                    this.teams[i].AddPoints(DrawPoints);
                }

                if (game.IsWinner(teams[i]))
                {
                    this.teams[i].AddPoints(WinningPoints);
                }
            }

            GenerateRanking();
        }

        private void GenerateRanking()
        {
            for(int i = 0; i < this.teams.Count - 1; i++)
            {
                for(int j = i + 1; j < this.teams.Count; j++)
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
