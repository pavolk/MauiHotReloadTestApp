namespace FirstMarkupApp.Models;

public class Article
{

    public string? OrderNumber { get; set; }

    public string? Name { get; set; }

    public double PriceEuros { get; set; }

    public string? PackagintUnits { get; set; }

}

public class ListPosition
{

    public int Ordinal { get; set; }

    public Article Article { get; set; }

    public double Amount { get; set; }

}
