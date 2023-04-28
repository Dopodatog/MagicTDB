using Microsoft.AspNetCore.Mvc;
using ResisterAndLoginApp.Models;
using ResisterAndLoginApp.Services;

namespace ResisterAndLoginApp.Controllers
{
    public class CDController : Controller
    {
        public IActionResult Index()
        {

            List<CDModel> allCards = new List<CDModel>();
            CardDAO deckOfCards = new CardDAO();

            allCards = deckOfCards.GetAllCardsFromAllDecks();
            return View(allCards);
        }
        public IActionResult SearchResults(int deckId)
        {
            List<CDModel> allCards = new List<CDModel>();
            CardDAO deckOfCards = new CardDAO();

            allCards = deckOfCards.GetAllCardsFromThisDeckId(deckId);
            return View("Index", allCards);
        }
        public IActionResult SearchForm()
        {
            return View();
        }
        public IActionResult InputForm()
        {
            return View();
        }
        public IActionResult ProcessCreate(CardModel cardBeingCreated)
        {
            //todo

            CardDAO deckOfCards = new CardDAO();
            deckOfCards.Insert(cardBeingCreated);

            return View("Index", deckOfCards.GetAllCards());

        }

        public IActionResult ShowDetails(int id)
        {

            CardDAO deckOfCards = new CardDAO();
            CDModel foundCard = deckOfCards.GetCdById(id);

            return View(foundCard);

        }
        public IActionResult Delete(int id)
        {
            CardDAO deckOfCards = new CardDAO();
            deckOfCards.DeleteCD(id);

            List<CDModel> allCards = new List<CDModel>();
            allCards = deckOfCards.GetAllCardsFromAllDecks();
           

            return View("Index", allCards);
        }
    }
}
