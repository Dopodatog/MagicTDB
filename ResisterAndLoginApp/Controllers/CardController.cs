using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ResisterAndLoginApp.Models;
using ResisterAndLoginApp.Services;

namespace ResisterAndLoginApp.Controllers
{
    public class CardController : Controller
    {
        //All of these are called from the user
        public IActionResult Index()
        {
            //JObject deckOfCards = new JObject();
            
            CardDAO deckOfCards = new CardDAO();
            return View(deckOfCards.GetAllCards());
        }



        public IActionResult SearchResults(string searchTerm)
        {
            CardDAO deckOfCards = new CardDAO();
            List<CardModel> cardsList = deckOfCards.SearchCards(searchTerm);
            return View("Index", cardsList);
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
            
            CardDAO deckOfCards = new CardDAO();
            deckOfCards.Insert(cardBeingCreated);

            return View("Index", deckOfCards.GetAllCards());

        }
        public IActionResult ShowDetails(int id)
        {
            CardDAO deckOfCards = new CardDAO();
            CardModel foundCard = deckOfCards.GetCardById(id);

            return View(foundCard);

        }
        public IActionResult Edit(int id)
        {
            CardDAO deckOfCards = new CardDAO();
            CardModel foundCard = deckOfCards.GetCardById(id);

            return View("ShowEdit", foundCard);

        }
        public IActionResult ProcessEdit(CardModel cardBeingEdited)
        {
            CardDAO deckOfCards = new CardDAO();
            deckOfCards.Update(cardBeingEdited);

            return View("Index", deckOfCards.GetAllCards());

        }

        public IActionResult Delete(int id)
        {
            CardDAO deckOfCards = new CardDAO();
            CardModel cardIWillDelete = deckOfCards.GetCardById(id);

            deckOfCards.DeleteAllCardChilderen(cardIWillDelete);
            deckOfCards.Delete(cardIWillDelete);
            return View("Index", deckOfCards.GetAllCards());
        }

        public IActionResult AddCardToDeck(int cardID)
        {
            CardDAO listOfDecks = new CardDAO();
            return View("ShowAddCardToDeck", listOfDecks.GetAllDecks());

        }
        public IActionResult ProcessAddCardToDeck(CDModel deckID)
        {
            Console.WriteLine(deckID.ToString());
            

            return View("Index");
        }
    }
}
