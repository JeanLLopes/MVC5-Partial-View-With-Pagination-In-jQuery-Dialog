using BestPlayers.Core.BL;
using BestPlayers.Core.Models;
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
    }
}