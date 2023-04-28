using System.ComponentModel;

namespace ResisterAndLoginApp.Models
{
    public class DeckModel
    {
        [DisplayName("Id number")]
        public int Id { get; set; }


        [DisplayName("Deck name")]
        public string DeckName { get; set; }
    }
}
