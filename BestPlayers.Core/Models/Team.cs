using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPlayers.Core.Models
{
    public class Team
    {
        #region Properties  
        /// <summary>  
        /// get and set the ID  
        /// </summary>  
        public int ID { get; set; }
        /// <summary>  
        /// get and set the Name  
        /// </summary>  
        public string Name { get; set; }
        #endregion
    }

    public class Players
    {
        #region Properties  
        /// <summary>  
        /// get and set the ID  
        /// </summary>  
        public int ID { get; set; }
        /// <summary>  
        /// get and set the Name  
        /// </summary>  
        public string Name { get; set; }
        /// <summary>  
        /// get and set the ODI  
        /// </summary>  
        public int ODI { get; set; }
        /// <summary>  
        /// get and set the Tests  
        /// </summary>  
        public int Tests { get; set; }
        /// <summary>  
        /// get and set the ODIRuns  
        /// </summary>  
        public int ODIRuns { get; set; }
        /// <summary>  
        /// get and set the TestRuns  
        /// </summary>  
        public int TestRuns { get; set; }
        #endregion
    }
}
