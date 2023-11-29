using AutoMapper;

class ConvertInteger : ITypeConverter<decimal, int>
{
    public int Convert(decimal source, int destination, ResolutionContext context)
    {
        destination = int.Parse(source.ToString());
        return destination;
    }
}