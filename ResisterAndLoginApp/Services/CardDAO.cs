using MySql.Data.MySqlClient;
using MagicTDB.Models;


namespace MagicTDB.Services
{
    public class CardDAO : ICardDataService, IDeckService, ICDTableService
    {
        string connectionString =
            "datasource=localhost;port=3306;username=root;password=root;database=magic;";

        public int Delete(CardModel card)
        {
            int result = -1;
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "DELETE FROM CARDS WHERE CARD_ID = @ID";
                command.Parameters.AddWithValue("@ID", card.Id);
                command.Connection = connection;

                result = command.ExecuteNonQuery();
                
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }

        public List<CardModel> GetAllCards()
        {

            List<CardModel> returnDeck = new List<CardModel>();
            //start with an empty list

            MySqlConnection connection= new MySqlConnection(connectionString);
            //makes a connection to mySQL server
            try
            {
                //need a try/catch with every connection to mysql

                string sqlStatement = "SELECT * FROM CARDS";
                // * means from every column

                connection.Open();
                //log into DB

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);
                //define the sql statement


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    //using() is like a while loop. It tells the program to use the given object then destroy it when it's done
                    while (reader.Read())
                    {
                        CardModel myCard = new CardModel
                        {
                            Id = reader.GetInt32(0),
                            CardName = reader.GetString(1),
                            URL = reader.GetString(2),
                            CMC = reader.GetInt32(3),
                            Color = reader.GetString(4),
                            Type = reader.GetString(5),
                            SubType = reader.GetString(6),
                            RulesText = reader.GetString(7),
                            Power = reader.GetInt32(8),
                            Toughness = reader.GetInt32(9)
                        };
                        returnDeck.Add(myCard);
                    }
                }
                connection.Close();
                //mySQL is smart enought to close on it's own. It is still a good practice to close it manually
            }
            catch (MySqlException e)
            {
                //the error names are helpful for error testing. For example: Incompatible Data Type
                Console.WriteLine(e.Message);
            }
            return returnDeck;
        }

        public CardModel GetCardById(int id)
        {
            CardModel returnCard = new CardModel();

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "SELECT * FROM CARDS WHERE CARD_ID = @ID";
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnCard = new CardModel
                        {
                            Id = reader.GetInt32(0),
                            CardName = reader.GetString(1),
                            URL = reader.GetString(2),
                            CMC = reader.GetInt32(3),
                            Color = reader.GetString(4),
                            Type = reader.GetString(5),
                            SubType = reader.GetString(6),
                            RulesText = reader.GetString(7),
                            Power = reader.GetInt32(8),
                            Toughness = reader.GetInt32(9)
                        };                           
                    }
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return returnCard;
        }

        public List<CDModel> GetAllCardsFromAllDecks()
        {
            List<CDModel> allCards = new List<CDModel>();
           
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "SELECT decksncards.CD_ID, cards.CARD_ID, cards.CARD_NAME, cards.PICTURE_URL, decks.DECK_ID, decks.DECK_NAME " +
                    "FROM decksncards " +
                    "JOIN cards USING (CARD_ID) " +
                    "JOIN decks USING (DECK_ID)";
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CDModel card = new CDModel
                        {
                            Id = reader.GetInt32(0),
                            CardId = reader.GetInt32(1),
                            CardName = reader.GetString(2),
                            URL = reader.GetString(3),
                            DeckId = reader.GetInt32(4),
                            DeckName = reader.GetString(5)

                        };
                        allCards.Add(card);
                    }
                }
                connection.Close();                
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }                  
            return allCards;
        }

        public int Insert(CardModel card)
        {
            int newRows = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();             
                MySqlCommand command = new MySqlCommand();

                command.CommandText = "INSERT INTO CARDS (CARD_NAME, PICTURE_URL, CMC, COLOR, TYPE, SUB_TYPE, RULES_TEXT, POWER, TOUGHNESS) " +
                    "VALUES (@CARD_NAME, @PICTURE_URL, @CMC, @COLOR, @TYPE, @SUB_TYPE, @RULES_TEXT, @POWER, @TOUGHNESS)";
                  
                command.Parameters.AddWithValue("@CARD_NAME", card.CardName);
                command.Parameters.AddWithValue("@PICTURE_URL", card.URL);
                command.Parameters.AddWithValue("@CMC", card.CMC);
                command.Parameters.AddWithValue("@COLOR", card.Color);
                command.Parameters.AddWithValue("@TYPE", card.Type);
                command.Parameters.AddWithValue("@SUB_TYPE", card.SubType);
                command.Parameters.AddWithValue("@RULES_TEXT", card.RulesText);
                command.Parameters.AddWithValue("@POWER", card.Power);
                command.Parameters.AddWithValue("@TOUGHNESS", card.Toughness);
                command.Connection = connection;
                newRows = command.ExecuteNonQuery();
                // close connection after query 
                connection.Close();

                
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return newRows;
        }

        public List<CardModel> SearchCards(string searchTerm)
        {

            List<CardModel> returnDeck = new List<CardModel>();

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                String wildSearchTerm = "%" + searchTerm + "%";
                //the % are wildcard characters

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "SELECT * FROM CARDS WHERE CARD_NAME LIKE @search";
                command.Parameters.AddWithValue("@search", wildSearchTerm);
                command.Connection = connection;
        
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CardModel myCard = new CardModel
                        {
                            Id = reader.GetInt32(0),
                            CardName = reader.GetString(1),
                            URL = reader.GetString(2),
                            CMC = reader.GetInt32(3),
                            Color = reader.GetString(4),
                            Type = reader.GetString(5),
                            SubType = reader.GetString(6),
                            RulesText = reader.GetString(7),
                            Power = reader.GetInt32(8),
                            Toughness = reader.GetInt32(9)
                        };
                        returnDeck.Add(myCard);
                    }
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return returnDeck;
        }

        public int Update(CardModel card)
        {
            //this one took the longest by far
            int newRows = -1;

            //List<CardModel> returnDeck = new List<CardModel>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "UPDATE CARDS SET " +
                    "CARD_NAME = @CARD_NAME," +
                    "PICTURE_URL = @PICTURE_URL," +
                    "CMC = @CMC," +
                    "COLOR = @COLOR," +
                    "TYPE = @TYPE," +
                    "SUB_TYPE = @SUB_TYPE," +
                    "RULES_TEXT = @RULES_TEXT," +
                    "POWER = @POWER," +
                    "TOUGHNESS = @TOUGHNESS " +
                    "WHERE CARD_ID = @ID";

                //this one looks very different from the others because it I was changing everything to find something that worked
                //in the debugger I found out that MySQL statement had syntax errors
                command.Parameters.AddWithValue("@CARD_NAME", card.CardName);
                command.Parameters.AddWithValue("@PICTURE_URL", card.URL);
                command.Parameters.AddWithValue("@CMC", card.CMC);
                command.Parameters.AddWithValue("@COLOR", card.Color);
                command.Parameters.AddWithValue("@TYPE", card.Type);
                command.Parameters.AddWithValue("@SUB_TYPE", card.SubType);
                command.Parameters.AddWithValue("@RULES_TEXT", card.RulesText);
                command.Parameters.AddWithValue("@POWER", card.Power);
                command.Parameters.AddWithValue("@TOUGHNESS", card.Toughness);
                command.Parameters.AddWithValue("@ID", card.Id);
                command.Connection = connection;
                newRows = command.ExecuteNonQuery();              
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);         
            }
            
            return newRows;
        }

        public List<DeckModel> GetAllDecks()
        {
            List<DeckModel> listOfDecks = new List<DeckModel>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
 
                string sqlStatement = "SELECT * FROM DECKS";
 
                connection.Open();

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DeckModel myDeck = new DeckModel
                        {
                            Id = reader.GetInt32(0),
                            DeckName = reader.GetString(1),
                        };
                        listOfDecks.Add(myDeck);
                    }
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return listOfDecks;
        }

        public DeckModel GetDeckById(int id)
        {
            DeckModel returnDeck = new DeckModel();

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "SELECT * FROM DECKS WHERE DECK_ID = @ID";
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDeck = new DeckModel
                        {
                            Id = reader.GetInt32(0),
                            DeckName = reader.GetString(1)
                        };
                    }
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return returnDeck;
        }

        public int InsertDeck(DeckModel deck)
        {
            int newRows = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();

                command.CommandText = "INSERT INTO DECKS (DECK_NAME) " +
                    "VALUES (@DECK_NAME)";

                command.Parameters.AddWithValue("@DECK_NAME", deck.DeckName);
                command.Connection = connection;
                newRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return newRows;
        }
    
        public int DeleteDeck(DeckModel deck)
        {
            int result = -1;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "DELETE FROM DECKS WHERE DECK_ID = @ID";
                command.Parameters.AddWithValue("@ID", deck.Id);
                command.Connection = connection;

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
            //need to delete all decksncards with same deck ID
        }

        public int UpdateDeck(DeckModel deck)
        {
            int newRows = -1;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "UPDATE DECKS SET " +
                    "DECK_NAME = @DECK_NAME " +
                    "WHERE DECK_ID = @ID";

                command.Parameters.AddWithValue("@DECK_NAME", deck.DeckName);
                command.Parameters.AddWithValue("@ID", deck.Id);
                command.Connection = connection;

                newRows = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return newRows;
        }

        public List<CDModel> GetAllCardsFromThisDeckId(int id)
        {
            List<CDModel> allCards = new List<CDModel>();

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();
                command.CommandText = "SELECT decksncards.CD_ID, cards.CARD_ID, cards.CARD_NAME, cards.PICTURE_URL, decks.DECK_ID, decks.DECK_NAME " +
                    "FROM decksncards " +
                    "JOIN cards USING(CARD_ID) " +
                    "JOIN decks USING(DECK_ID) " +
                    "WHERE DECK_ID = @ID";
                           
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CDModel card = new CDModel
                        {
                            Id = reader.GetInt32(0),
                            CardId = reader.GetInt32(1),
                            CardName = reader.GetString(2),
                            URL = reader.GetString(3),
                            DeckId = reader.GetInt32(4),
                            DeckName = reader.GetString(5)

                        };
                        allCards.Add(card);
                    }
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return allCards;
        }

        public int InsertCD(int deckId, int cardId)
        {
            
            int newRows = -1;
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();

                //IGNORE makes it check to see if an identical object exists
                //if true do nothing 
                //if false so the insert
                //"INSERT IGNORE INTO DECKSNCARDS (DECK_ID, CARD_ID) " +
                //"VALUES (@DECK_ID, @CARD_ID)";
                command.CommandText = "INSERT INTO decksncards (CARD_ID, DECK_ID) " +
                    "SELECT @CARD_ID, @DECK_ID " +
                    "FROM dual " +
                    "WHERE NOT EXISTS (" +
                        "SELECT * " +
                        "FROM decksncards " +
                        "WHERE CARD_ID = @CARD_ID " +
                        "AND DECK_ID = @DECK_ID" +
                        ")";


                command.Parameters.AddWithValue("@CARD_ID", cardId);
                command.Parameters.AddWithValue("@DECK_ID", deckId);
                command.Connection = connection;
                newRows = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return newRows;
        }
    
        public int DeleteCD(int cdId)
        {
            int result = -1;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "DELETE FROM decksncards WHERE CD_ID = @CDIDVALUE;";
                command.Parameters.AddWithValue("@CDIDVALUE", cdId);
                command.Connection = connection;

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }

        public int DeleteAllCardChilderen(CardModel card)
        {
            int result = -1;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "DELETE FROM decksncards WHERE CARD_ID = @CARDID;";
                command.Parameters.AddWithValue("@CARDID", card.Id);
                command.Connection = connection;

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public int DeleteAllDeckChilderen(DeckModel deck)
        {
            int result = -1;

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "DELETE FROM decksncards WHERE DECK_ID = @DECKID;";
                command.Parameters.AddWithValue("@DECKID", deck.Id);
                command.Connection = connection;

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public CDModel GetCdById(int id)
        {
            CDModel returnCard = new CDModel();

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand();

                command.CommandText = "SELECT decksncards.CD_ID, cards.CARD_ID, cards.CARD_NAME, cards.PICTURE_URL, decks.DECK_ID, decks.DECK_NAME " +
                    "FROM decksncards " +
                    "JOIN cards USING (CARD_ID) " +
                    "JOIN decks USING (DECK_ID) " +
                    "WHERE CD_ID = @ID";
                command.Parameters.AddWithValue("@ID", id);
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnCard = new CDModel
                        {
                            Id = reader.GetInt32(0),
                            CardId = reader.GetInt32(1),
                            CardName = reader.GetString(2),
                            URL = reader.GetString(3),
                            DeckId = reader.GetInt32(4),
                            DeckName = reader.GetString(5)
                        };
                    }
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return returnCard;
        }

    }
}
