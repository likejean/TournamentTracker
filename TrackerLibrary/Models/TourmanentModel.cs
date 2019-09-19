using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents the entire Tourmanent
    /// </summary>
    public class TourmanentModel
    {
        /// <summary>
        /// The id of the tournament
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the current tourmanent
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// The set of all team players involved in the tourmanent
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// To enter a fee value
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();
        /// <summary>
        /// The set of prizes 
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        /// <summary>
        /// The set of all matches
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
