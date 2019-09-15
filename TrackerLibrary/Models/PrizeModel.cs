using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the Prize
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The place number of the team
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// The name of the placement for the team
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// The $amount of the prize for the current placement
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// Represents the percentage of the prize
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName; //placeName doesn't need to be parsed: must be string anyway...

            int.TryParse(placeNumber, out int placeNumberValue);
            PlaceNumber = placeNumberValue; //must be parsed to an integer type

            decimal.TryParse(prizeAmount, out decimal prizeAmountValue);
            PrizeAmount = prizeAmountValue; //must be parsed to the decimal type

            double.TryParse(prizePercentage, out double prizePercentageValue);
            PrizePercentage = prizePercentageValue; //must be parsed to the double type
        }
    }
}
