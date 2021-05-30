namespace EventSourcingDemo.Domain
{
    public sealed class Product : DomainModelBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
