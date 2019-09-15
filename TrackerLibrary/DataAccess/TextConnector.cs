using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;


namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";

        //1.****Implement inherited Interface member <CreatePerson>

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModel();
            int currentId = 1;
            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            people.Add(model);
            people.SaveToPeopleFile(PeopleFile);
            return model;
        }



        //2.*****Implement inherited Interface member <CreatePrize>

        //TODO: Wire up the CreatePrize for text files
        //OVERRIDING 'CreatePrize' Method inherited from 'IDataConnection' Interface. 
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Load the text file
            //Convert the test to List<PrizeModel>

            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModel();

            //Find the max Id

            int currentId = 1;
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;


            //Add the new record with the new ID

            prizes.Add(model);

            //Convert the prizes to list<string>
            //Save the list<string> to the text file

            prizes.SaveToPrizeFile(PrizesFile);
            return model;
        }

        public List<PersonModel> GetPerson_All()
        {
            throw new NotImplementedException();
        }
    }

}
