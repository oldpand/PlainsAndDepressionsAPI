namespace PlainsAndDepressions.Contracts.Models
{
    public class PutDepressionsRequest
    {
        public Guid PackId { get; set; }

        public IOrderedEnumerable<Depression> Pack { get; set; } = null!;
    }
}
