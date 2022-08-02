namespace Publisher.Helpers
{
    public record MessageEnvelop
    {
        public string CustomerType { get; set; }
        public Guid TransactionId { get; set; }
    }
}
