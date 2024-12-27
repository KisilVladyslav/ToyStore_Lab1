namespace ToyStore.Api_mapping.Toys.Update
{
    public class UpdateToysRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public Guid CategoryId { get; set; }

        public virtual ToyCategory Category { get; set; }
    }
}
