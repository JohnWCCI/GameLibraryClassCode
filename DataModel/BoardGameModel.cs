using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class BoardGameModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual PublisherModel? Publishers { get; set; }
        [NotMapped]
        public string? NewPublisher { get; set; }

        [ForeignKey("PublisherModel")]
        //Combination of our virtual Publishers and the Id to the linked table (Id in PublisherModel)
        //naming has to match exactly. You must do this or EF will make this foreign key for you
        //and will ignore this property, not allowing you to add seed data to the field.
        public int PublishersId { get; set; }
        [NotMapped]
        public string Publisher
        {
            get
            {
                if (Publishers is not null)
                {
                    return Publishers.Name;
                }
                else
                {
                    return "";
                }
            }
        }

        public string ImageURL { get; set; }
    }
}
