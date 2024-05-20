using MapsterMapper;

namespace Application.Libraries.AppMapper;

public class ModelMapper
{
    private static IMapper _mapper;
    static ModelMapper() => _mapper = new Mapper();
    public static TDestination Map<TSource, TDestination>(TSource source)
        => _mapper.Map<TSource, TDestination>(source);
    public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        => _mapper.Map(source, destination);
    public static TDestination Map<TDestination>(object source)
        => _mapper.Map<TDestination>(source);
}
