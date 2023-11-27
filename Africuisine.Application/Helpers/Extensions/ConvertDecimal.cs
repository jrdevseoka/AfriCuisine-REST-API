using AutoMapper;
using System.Globalization;
public class ConvertDecimal : ITypeConverter<int, decimal>
{
    public decimal Convert(int source, decimal destination, ResolutionContext context)
    {
        destination = decimal.Parse(source.ToString(), NumberStyles.AllowDecimalPoint);
        return destination;
    }
}