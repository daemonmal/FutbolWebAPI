using System.Data.SqlClient;

namespace FutbolWebAPI
{
    public class Teams : Players
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamState { get; set; }
        public string TeamLogo { get; set; }
        public string TeamJersey { get; set; }

        SqlConnection connect = new SqlConnection("server=localhost\\TRAINING;database=FutbolDB;Integrated Security=true;");

        public List<Teams> GetAllTeams()
        {
            List<Teams> teamsList = new List<Teams>();
            SqlCommand cmd = new SqlCommand("select * from Teams_tbl order by TeamId", connect);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    teamsList.Add(new Teams()
                    {
                        TeamId = (int)reader[0],
                        TeamName = reader[1].ToString(),
                        TeamState = reader[2].ToString(),
                        TeamLogo = reader[3].ToString(),
                        TeamJersey = reader[4].ToString(),
                    });
                }
                return teamsList;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connect.Close();
            }
        }

        public Teams GetTeam(string teamName)
        {
            Teams team = new Teams();
            SqlCommand cmd = new SqlCommand("select * from Teams_tbl where TeamName=@tName", connect);
            cmd.Parameters.AddWithValue("@tName", teamName);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    team.TeamId = (int)reader[0];
                    team.TeamName = (string)reader[1];
                    team.TeamState = (string)reader[2];
                    team.TeamLogo = (string)reader[3];
                    team.TeamJersey = (string)reader[4];
                }
                return team;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connect.Close();
            }
        }

        public List<Teams> GetTeamByName(string p_teamName)
        {
            List<Teams> playerList = new List<Teams>();

            SqlCommand cmd = new SqlCommand("select Teams_tbl.TeamId,TeamName,TeamState, Players_tbl.PlayerId,PlayerName,PlayerPosition,PlayerImage,JerseyNumber" +
                " from Teams_tbl,Players_tbl where TeamName=@teamName and Teams_tbl.TeamId=Players_tbl.PlayerTeamId", connect);

            cmd.Parameters.AddWithValue("@teamName", p_teamName);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    playerList.Add(new Teams()
                    {
                        TeamId = (int)reader[0],
                        TeamName = reader[1].ToString(),
                        TeamState = reader[2].ToString(),
                        PlayerId = (int)reader[3],
                        PlayerName = reader[4].ToString(),
                        PlayerPosition = reader[5].ToString(),
                        PlayerImage = reader[6].ToString(),
                        JerseyNumber = (int)reader[7]

                    });
                }
                return playerList;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connect.Close();
            }
        }

        public string AddNewTeam(Teams newTeam)
        {
            int result;
            string message;
            SqlCommand cmd = new SqlCommand("insert into Teams_tbl values(@teamName,@teamCountry,@teamFlag,@teamJers)", connect);
            cmd.Parameters.AddWithValue("@teamName", newTeam.TeamName);
            cmd.Parameters.AddWithValue("@teamCountry", newTeam.TeamState);
            cmd.Parameters.AddWithValue("@teamFlag", newTeam.TeamLogo);
            cmd.Parameters.AddWithValue("@teamJers", newTeam.TeamJersey);
            connect.Open();
            result = cmd.ExecuteNonQuery();
            try
            {
                if (result > 0)
                {
                    message = $"{newTeam.TeamName} from {newTeam.TeamState} added succesfully";
                    return message;
                }
                message = $"{newTeam.TeamName} already exists. Team name must be unique.";
                return message;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
