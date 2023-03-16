namespace DataModel
{
    public class PublisherModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<BoardGameModel>? BoardGames { get; set; }
    }
}
