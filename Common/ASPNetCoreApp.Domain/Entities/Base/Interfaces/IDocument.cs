using System;
using System.Collections.Generic;


namespace ASPNetCoreApp.Domain.Entities.Base.Interfaces
{
    public interface IDocument
    {
        DateTimeOffset Date { get; set; }

        string Description { get; set; }
    }
}
