using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Team
    {
        readonly private string name;
        private int points;

        public Team(string name)
        {
            this.name = name;
            this.points = 0;
        }

        public bool CompareNames(Team team)
        {
            return this.name.Equals(team.name);
        }

        public bool HasLessPoints(Team team)
        {
            return this.points < team.points;
        }

        public void AddPoints(int points)
        {
            this.points += points;
        }
    }
}
