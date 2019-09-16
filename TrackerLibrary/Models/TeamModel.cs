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
        /// The unique identifier for the Prize
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the team
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// Set of players in this team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
       
    }
}
