﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using MagicTDB.Models;
using MagicTDB.Services;
using Mysqlx.Crud;
using MagicTDB.Utility;

namespace MagicTDB.Controllers
{
    public class CardController : Controller
    {


        //All of these are called from the user
        
        public IActionResult Index()
        {
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

        public IActionResult AddCardToDeck(CardModel card)
        {
          
            CardDAO listOfDecks = new CardDAO();
    
            HoldMe.SetInt(card.Id);
            

         


            return View("ShowAddCardToDeck", listOfDecks.GetAllDecks());

        }
        public IActionResult ProcessAddCardToDeck(DeckModel deck)
        {
                       
            CardDAO deckOfCards = new CardDAO();
            deckOfCards.InsertCD(deck.Id, HoldMe.GetInt());
            return View("DeckIndex", deckOfCards.GetAllDecks());
        }
    }
}