using System;
using CSVFile;

namespace database_insert_test
{
    public class MailingAddress
    {
        public string? Line1 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        private static MailingAddress[]? _addresses;
        private static MailingAddress[] GetAll()
        {
            if (_addresses == null)
            {
                var text = File.ReadAllText("starbucks_addresses.csv");
                _addresses = CSV.Deserialize<MailingAddress>(text).ToArray();
                Console.WriteLine($"Read in {_addresses.Length} addresses");
            }
            return _addresses;
        }
    }
}

