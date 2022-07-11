using System.Data.SqlClient;

namespace FutbolWebAPI
{
    public class Players
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public int PlayerTeamId { get; set; }
        public string PlayerImage { get; set; }
        public int JerseyNumber { get; set; }

        SqlConnection connect = new SqlConnection("server=localhost\\TRAINING;database=FutbolDB;Integrated Security=true;");

        public class PlayerBase : Players
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamState { get; set; }
        }

        public PlayerBase GetPlayerByName(string p_playerName)
        {
            PlayerBase player = new PlayerBase();
            SqlCommand cmd = new SqlCommand("select Teams_tbl.TeamId,TeamName,TeamState, Players_tbl.PlayerId,PlayerName,PlayerPosition,PlayerImage,JerseyNumber " +
                "from Teams_tbl,Players_tbl where PlayerName=@name and Teams_tbl.TeamId=Players_tbl.PlayerTeamId", connect);
            cmd.Parameters.AddWithValue("@name", p_playerName);
            try
            {
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    player.TeamId = (int)reader[0];
                    player.TeamName = reader[1].ToString();
                    player.TeamState = reader[2].ToString();
                    player.PlayerId = (int)reader[3];
                    player.PlayerName = reader[4].ToString();
                    player.PlayerPosition = reader[5].ToString();
                    player.PlayerImage = reader[6].ToString();
                    player.JerseyNumber = (int)reader[7];
                }
                return player;
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

        public List<PlayerBase> GetPlayersByPosition(string p_positionName)
        {
            List<PlayerBase> players = new List<PlayerBase>();
            SqlCommand cmd = new SqlCommand("select Teams_tbl.TeamId,TeamName,TeamState, Players_tbl.PlayerId,PlayerName,PlayerPosition,PlayerImage,JerseyNumber " +
                "from Teams_tbl,Players_tbl where PlayerPosition=@posName and Teams_tbl.TeamId=Players_tbl.PlayerTeamId order by Teams_tbl.TeamId", connect);
            cmd.Parameters.AddWithValue("@posName", p_positionName);
            try
            {
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    players.Add(new PlayerBase()
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
                return players;
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

        public string AddPlayer(Players newPlayer)
        {
            int result;
            string message;
            SqlCommand cmd = new SqlCommand("insert into Players_tbl values(@name,@position,@tId,@pImage,@jNum)", connect);
            cmd.Parameters.AddWithValue("@name", newPlayer.PlayerName);
            cmd.Parameters.AddWithValue("@position", newPlayer.PlayerPosition);
            cmd.Parameters.AddWithValue("@tId", newPlayer.PlayerTeamId);
            cmd.Parameters.AddWithValue("@pImage", newPlayer.PlayerImage);
            cmd.Parameters.AddWithValue("@jNum", newPlayer.JerseyNumber);
            try
            {
                connect.Open();
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    message = $"Player {newPlayer.PlayerName} playing position {newPlayer.PlayerPosition} for Team ID: {newPlayer.PlayerTeamId} added Successfully.";
                    return message;
                }
                message = $"Player {newPlayer.PlayerName} has already been added or {newPlayer.PlayerTeamId} does not exist. Please Try again.";
                return message;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                connect.Close();
            }
        }

        public string ChangePlayerTeam(string p_playerName, string p_newTeamName)
        {
            int result;
            string message;
            SqlCommand cmd = new SqlCommand("update Players_tbl set PlayerTeamId=(select TeamId from Teams_tbl where TeamName=@tName) where PlayerName=@pName)", connect);
            cmd.Parameters.AddWithValue("@pName", p_playerName);
            cmd.Parameters.AddWithValue("@tName", p_newTeamName);
            try
            {
                connect.Open();
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    message = $"{p_playerName} succesfully moved to team {p_newTeamName}.";
                    return message;
                }
                message = $"{p_playerName} or {p_newTeamName} not found.";
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

        public string DeletePlayer(string p_playerName)
        {
            int result;
            string message;
            SqlCommand cmd = new SqlCommand("delete from Players_tbl where PlayerName=@name", connect);
            cmd.Parameters.AddWithValue("@name", p_playerName);
            try
            {
                connect.Open();
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    message = $"{p_playerName} Deleted Successfully";
                    return message;
                }
                message = $"{p_playerName} Not Found";
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
