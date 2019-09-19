using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        /// <summary>
        /// Generates the full file path being defined in ConfigurationManager
        /// </summary>
        /// <param name="fileName">Passes the file name</param>
        /// <returns></returns>
        public static string FullFilePath(this string fileName) 
        {
            //Passes the file name and joins with the following file path C:\Users\popac\OneDrive\Desktop\Tournament
            return $"{ ConfigurationManager.AppSettings["filePath"]}\\{ fileName } ";
        }


        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }

        /// <summary>
        /// Instantiate the List of 'PrizeModel' type using Generics
        /// and Convert the List of 'string' type into the List of 'PrizeModel'
        /// </summary>
        /// <param name="Lines"></param>
        /// <returns></returns>

        public static List<PrizeModel> ConvertToPrizeModel(this List<string> Lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();
            foreach (string line in Lines)
            {
                string[] cols = line.Split(','); //Comma - Delimiter
                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);

            }

            return output;
        }

        /// <summary>
        /// Instantiates the List of 'PersonModel' type using Generics
        /// and Converts the List of 'string' type into the List of 'PersonModel'
        /// </summary>
        /// <param name="Lines"></param>
        /// <returns></returns>
        public static List<PersonModel> ConvertToPersonModel(this List<string> Lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in Lines)
            {
                string[] cols = line.Split(','); //Comma-Delimiter !No commas should be input
                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.PhoneNumber = cols[4];
                output.Add(p);
            }
            return output;
        }

        /// <summary>
        /// Initiates the List of 'TeamModel' type using Generics
        /// and Converts the List of 'string' type into the List of 'TeamModel'
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<TeamModel> ConvertToTeamModels(this List<string> Lines, string peopleFileName)
        {
            //id, team name, list of ids separated by the pipe
            //
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModel();

            foreach (string line in Lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];
                string[] personIds = cols[2].Split('|');
                
                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }
                output.Add(t);
            }
            return output;
        }


        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.PhoneNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            // Instantiates a new List object of type 'string'
            // in order to save the updated list back into a textfile
            List<string> lines = new List<string>();
            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTournamentFile(this List<TourmanentModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TourmanentModel tm in models)
            {
                lines.Add($@"
                    {tm.Id},
                    {tm.TournamentName},
                    {tm.EntryFee},
                    {ConvertTeamListToString(tm.EnteredTeams)},
                    {ConvertPrizeListToString(tm.Prizes)},
                    {ConvertRoundListToString(tm.Rounds)}"
                );
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        /// <summary>
        /// Helper method for the SaveToTeamFile method
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        public static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";
            if (people.Count == 0) return "";
            foreach (PersonModel p in people)
            {
                output += $"{ p.Id }|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }
        /// <summary>
        /// Helper method for the SaveToTournamentFile method
        /// </summary>
        /// <param name="teams"></param>
        /// <returns></returns>
        public static string ConvertTeamListToString(List<TeamModel> teams)
        {
            string output = "";
            if (teams.Count == 0) return "";
            foreach (TeamModel t in teams)
            {
                output += $"{ t.Id }|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        /// <summary>
        /// Helper method for the SaveToTournamentFile method
        /// </summary>
        /// <param name="prizes"></param>
        /// <returns></returns>
        public static string ConvertPrizeListToString(List<PrizeModel> prizes)
        {
            string output = "";
            if (prizes.Count == 0) return "";
            foreach (PrizeModel p in prizes)
            {
                output += $"{ p.Id }|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        /// <summary>
        /// Helper method for the SaveToTournamentFile method
        /// </summary>
        /// <param name="rounds"></param>
        /// <returns></returns>
        public static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";
            if (rounds.Count == 0) return "";
            foreach (List<MatchupModel> r in rounds)
            {
                output += $"{ConvertMatchupListToString(r)}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        /// <summary>
        /// Helper method for the SaveToTournamentFile method
        /// </summary>
        /// <param name="matchups"></param>
        /// <returns></returns>
        public static string ConvertMatchupListToString(List<MatchupModel> matchups)
        {
            string output = "";
            if (matchups.Count == 0) return "";
            foreach (MatchupModel m in matchups)
            {
                output += $"{ m.Id }^";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }



        public static List<TourmanentModel> ConvertToTournamentModel(this List<string> Lines, string teamFileName, string peopleFileName, string prizeFileName)
        {
            //id, torunament name, entryFee, (id|id|id - Entered Teams), (id|id|id - Prizes), (Rounds - id^id^id|id^id^id|id^id^id)
            List<TourmanentModel> output = new List<TourmanentModel>();
            List<TeamModel> teams = teamFileName.FullFilePath().LoadFile().ConvertToTeamModels(peopleFileName);
            List<PrizeModel> prizes = prizeFileName.FullFilePath().LoadFile().ConvertToPrizeModel();

            foreach (string line in Lines)
            {
                string[] cols = line.Split(',');
                TourmanentModel tm = new TourmanentModel();
                tm.Id = int.Parse(cols[0]);
                tm.TournamentName = cols[1];
                tm.EntryFee = decimal.Parse(cols[2]);

                string[] teamIds = cols[3].Split('|');
                foreach (string id in teamIds)
                {                   
                    tm.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                string[] prizeIds = cols[4].Split('|');
                foreach (string id in prizeIds)
                {
                    tm.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                //TODO - Capture Rounds Information

                output.Add(tm);

            }
            return output;
        }
    }
}
