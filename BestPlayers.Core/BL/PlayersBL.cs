using BestPlayers.Core.DAL;
using BestPlayers.Core.Models;
using System;
using System.Collections.Generic;

namespace BestPlayers.Core.BL
{
    public class PlayersBL
    {
        /// <summary>  
        /// This method is created to get all teams.  
        /// </summary>  
        /// <returns></returns>  
        public List<Team> GetAllTeams()
        {
            List<Team> teams = null;
            try
            {
                teams = new PlayersDAL().GetAllTeams();
            }
            catch (Exception ex)
            {
                bool rethrow = false;
                if (rethrow)
                {
                    throw ex;
                }
                return null;
            }
            return teams;
        }


        /// <summary>  
        /// This method is created to get players list on the basis of team id.  
        /// </summary>  
        /// <param name="teamId"></param>  
        /// <param name="page"></param>  
        /// <param name="pageSize"></param>  
        /// <returns></returns>  
        public List<Players> GetPlayersByTeam(int teamId, string page, string pageSize)
        {
            List<Players> playersList = null;
            try
            {
                playersList = new PlayersDAL().GetPlayersByTeam(teamId, page, pageSize);
            }
            catch (Exception ex)
            {
                bool rethrow = false;
                if (rethrow)
                {
                    throw ex;
                }
                return null;
            }
            return playersList;
        }
    }
}
