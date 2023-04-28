using ResisterAndLoginApp.Models;

namespace ResisterAndLoginApp.Services
{
    public interface ICDTableService
    {
        public List<CDModel> GetAllCardsFromAllDecks();
        public List<CDModel> GetAllCardsFromThisDeckId(int id);
        CDModel GetCdById(int id);
        int DeleteAllCardChilderen(CardModel card);
        int DeleteAllDeckChilderen(DeckModel deck);
        int InsertCD(int deckId, int cardId);
        int DeleteCD(int cdId);
    }
}
