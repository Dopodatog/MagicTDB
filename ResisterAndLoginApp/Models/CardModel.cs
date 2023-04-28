using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ResisterAndLoginApp.Models
{
    public class CardModel
    {
        [DisplayName("Id number")]
        public int Id { get; set; }


        [DisplayName("Card name")]
        public string CardName { get; set; }


        [DisplayName("Picture URL")]
        public string URL { get; set; }


        [DisplayName("Converted mana cost")]
        public int CMC { get; set; }


        [DisplayName("Colored mana cost")]
        public string Color { get; set; }


        [DisplayName("Creature type")]
        public string Type { get; set; }


        [DisplayName("Creature sub type")]
        public string SubType { get; set; }


        [DisplayName("Rules text")]
        public string RulesText { get; set; }


        [DisplayName("Creature power")]
        public int Power { get; set; }


        [DisplayName("Creature toughness")]
        public int Toughness { get; set; }
    }
}
