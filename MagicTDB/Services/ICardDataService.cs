using Newtonsoft.Json.Linq;
using MagicTDB.Models;

namespace MagicTDB.Services
{
    public interface ICardDataService
    {
        //if a class starts with I it means to implement
        //This interface will create an outline of what other classes need to implement
        List<CardModel> GetAllCards();
        List<CardModel> SearchCards(string searchTerm);
        CardModel GetCardById(int id);
        int Insert(CardModel card);
        int Delete(CardModel card);
        int Update(CardModel card);

    }
}
