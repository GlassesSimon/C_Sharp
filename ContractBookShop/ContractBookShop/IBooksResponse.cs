using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ContractBookShop
{
    public interface IBooksResponse
    {
        List<Book> Books { get; set; }
        
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        class Book
        {
            public int Id { get; set; }
        
            public string Title { get; set; }
            
            public double Price { get; set; }
        
            public string Genre { get; set; }

            public bool IsNew { get; set; }

            public DateTime DateDelivery { get; set; }
        }
    }
}