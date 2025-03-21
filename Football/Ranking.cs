﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    public class Ranking
    {
        private Team[] teams;

        public Ranking()
        {
            this.teams = new Team[0];
        }
        
        public void AddTeam(Team team)
        {
            Array.Resize(ref this.teams, this.teams.Length + 1);
            this.teams[this.teams.Length - 1] = team;
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
            return Array.IndexOf(teams, team) + 1;
        }

        public void UpdateRanking(Game game)
        {
            game.AwardPoints();
            GenerateRanking();
        }

        private void GenerateRanking()
        {
            for(int i = 1; i < this.teams.Length; i++)
            {
                for(int p = i; p > 0 && this.teams[p - 1].HasLessPoints(this.teams[p]); p--)
                {
                    (this.teams[p - 1], this.teams[p]) = (this.teams[p], this.teams[p - 1]);
                }
            }
        }
    }
}
