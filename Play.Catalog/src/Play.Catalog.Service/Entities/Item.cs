/**
* Entities - Representation of data to push and pull from MongoDB.  These will be handled by the Item Repository
*
*   Repository - is an abstraction between the data layer and business layer of an applicaiton.
*   (need mongodb nuget package)
*/
using System;

namespace Play.Catalog.Service.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

    }
}