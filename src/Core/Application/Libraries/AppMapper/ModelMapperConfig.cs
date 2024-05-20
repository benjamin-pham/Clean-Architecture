using Mapster;

namespace Application.Libraries.AppMapper;

public class ModelMapperConfig
{
    public static void MapperRegister()
    {
        //global
        var config = TypeAdapterConfig.GlobalSettings;
        config.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);

        //type pair
        //config.ForType<Product, CreateProductCommand>().TwoWays()
        //    .Map(x => x.Name, y => y.Name);
    }
}
