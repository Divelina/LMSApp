
using AutoMapper;

namespace LMSApp.Services.DataServices.Tests
{
   public class BaseServiceTests
    {
        public BaseServiceTests()
        {
            Initialize();
        }

        public static void Initialize()
        {
            Mapper.Reset();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfle>();
            });
        }
    }
}
