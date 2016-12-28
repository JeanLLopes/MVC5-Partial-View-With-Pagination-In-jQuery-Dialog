using BestPlayers.Core.BL;
using BestPlayers.Core.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC5_PartialViewWithPagination.Web.Controllers
{
    public class PlayersController : Controller
    {
        #region Cricketer          
        /// <summary>    
        /// This method is used to get all cricketer names    
        /// </summary>    
        /// <returns></returns>    
        [HttpGet, ActionName("GetAllTeams")]
        public ActionResult GetAllTeams()
        {
            List<Team> teamList = new List<Team>();

            var response = new PlayersBL().GetAllTeams();
            if (!object.Equals(response, null))
            {
                teamList = response.ToList();
            }
            return View("~/Views/Players/Teams.cshtml", new Cricketer { Teams = teamList });
        }
        #endregion

        /// <summary>  
        /// This method is created to get all players on the basis of team id.  
        /// </summary>  
        /// <param name="page"></param>  
        /// <param name="pageSize"></param>  
        /// <param name="teamId"></param>  
        /// <returns></returns>  
        [HttpGet, ActionName("GetPlayersByTeam")]
        public ActionResult GetPlayersByTeam(int? page, int? pageSize, int teamId)
        {
            List<Players> playersList = new List<Players>();
            if (object.Equals(page, null))
            {
                page = 1;
            }
            if (object.Equals(pageSize, null))
            {
                pageSize = 4;
            }
            ViewBag.TeamId = teamId;
            ViewBag.PageSize = pageSize;
            var response = new PlayersBL().GetPlayersByTeam(teamId, page.ToString(), pageSize.ToString());
            if (!object.Equals(response, null))
            {
                playersList = response.ToList();
            }

            return View("~/Views/Players/_PlayerPartial.cshtml", new StaticPagedList<Players>(playersList, Convert.ToInt32(page), Convert.ToInt32(pageSize), playersList.Count > 0 ? playersList.FirstOrDefault().TotalCount : 0));
        }
    }
}