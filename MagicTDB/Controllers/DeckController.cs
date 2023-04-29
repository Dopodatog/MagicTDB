using Microsoft.AspNetCore.Mvc;
using MagicTDB.Models;
using MagicTDB.Services;

namespace MagicTDB.Controllers
{
    public class DeckController : Controller
    {
        public IActionResult Index()
        {
            CardDAO listOfDecks = new CardDAO();       
            return View(listOfDecks.GetAllDecks());
        }


        public IActionResult InputForm()
        {
            return View();
        }

        public IActionResult ProcessCreate(DeckModel deckToBeMade)
        {
        
            CardDAO listOfDecks = new CardDAO();
            listOfDecks.InsertDeck(deckToBeMade);
            
            return View("Index", listOfDecks.GetAllDecks());

        }
        public IActionResult Edit(int id)
        {
            CardDAO listOfDecks = new CardDAO();
            DeckModel oneDeck = listOfDecks.GetDeckById(id);

            return View("ShowEdit", oneDeck);

        }
        public IActionResult ProcessEdit(DeckModel cardBeingEdited)
        {
            CardDAO listOfDecks = new CardDAO();
            listOfDecks.UpdateDeck(cardBeingEdited);

            return View("Index", listOfDecks.GetAllDecks());

        }

        public IActionResult Delete(int id)
        {
            CardDAO listOfDecks = new CardDAO();
            DeckModel deckIWillDelete = listOfDecks.GetDeckById(id);

            listOfDecks.DeleteAllDeckChilderen(deckIWillDelete);
            listOfDecks.DeleteDeck(deckIWillDelete);
            return View("Index", listOfDecks.GetAllDecks());
        }
        public IActionResult ShowDetails(int id)
        {
            CardDAO listOfDecks = new CardDAO();
            DeckModel foundDeck = listOfDecks.GetDeckById(id);

            return View(foundDeck);

        }

        public IActionResult SearchResults(int id)
        {
            List<CDModel> allCards = new List<CDModel>();
            CardDAO deckOfCards = new CardDAO();

            allCards = deckOfCards.GetAllCardsFromThisDeckId(id);
            return View("ShowCards", allCards);
        }


    }
}
