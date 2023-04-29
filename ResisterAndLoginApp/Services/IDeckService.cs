using MagicTDB.Models;

namespace MagicTDB.Services
{
    public interface IDeckService
    {
        List<DeckModel> GetAllDecks();
        DeckModel GetDeckById(int id);

        

        int InsertDeck(DeckModel deck);
        int DeleteDeck(DeckModel deck);
        int UpdateDeck(DeckModel deck);
    }
}
