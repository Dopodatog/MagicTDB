using System.ComponentModel;

namespace ResisterAndLoginApp.Models
{
    public class CDModel
    {
        [DisplayName("CD id")]
        public int Id { get; set; }

        [DisplayName("Card id")]
        public int CardId { get; set; }


        [DisplayName("Card name")]
        public string CardName { get; set; }


        [DisplayName("Picture URL")]
        public string URL { get; set; }


        [DisplayName("Deck id")]
        public int DeckId { get; set; }


        [DisplayName("Deck name")]
        public string DeckName { get; set; }
        
    }
}
