using Xunit;

namespace Football.Facts
{
    public class TeamFacts
    {
        [Fact]
        public void AddPoints_ShouldAddPointsToTheTeam()
        {
            Team firstTeam = new Team("Germany");
            Team secondTeam = new Team("England");
            Assert.False(firstTeam.HasLessPoints(secondTeam));
            secondTeam.AddPoints(3);
            Assert.True(firstTeam.HasLessPoints(secondTeam));
        }

        [Fact]
        public void HasLessPoints_FirstTeamHasLessPoints_ShouldReturnTrue()
        {
            Team firstTeam = new Team("Germany");
            Team secondTeam = new Team("England");
            Assert.False(firstTeam.HasLessPoints(secondTeam));
            secondTeam.AddPoints(3);
            Assert.True(firstTeam.HasLessPoints(secondTeam));
            firstTeam.AddPoints(4);
            Assert.False(firstTeam.HasLessPoints(secondTeam));
        }
    }
}
