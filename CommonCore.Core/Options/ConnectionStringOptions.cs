using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Core.Options;

public class ConnectionStringOptions
{
    public const string SectionName = "ConnectionStrings";
    public string DefaultConnection { get; set; } = null!;
}
