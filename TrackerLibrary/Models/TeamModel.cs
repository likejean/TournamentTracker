using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one Team
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// Set of players in this team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        /// <summary>
        /// The name of the team
        /// </summary>
        public string TeamName { get; set; }

    }
}
