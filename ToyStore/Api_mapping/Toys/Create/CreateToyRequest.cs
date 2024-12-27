namespace ToyStore.Api_mapping.Toys.Create
{
    public class CreateToyRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public Guid CategoryId { get; set; }

        public virtual ToyCategory Category { get; set; }
    }
}
