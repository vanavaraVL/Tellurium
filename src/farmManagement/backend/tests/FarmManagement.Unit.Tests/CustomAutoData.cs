using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using AutoMapper;
using FarmManagement.Services.Mappings;

namespace FarmManagement.Unit.Tests;

[AttributeUsage(AttributeTargets.Method)]
public class CustomAutoDataAttribute : AutoDataAttribute
{
    public CustomAutoDataAttribute() : base(FixtureHelpers.CreateFixture)
    {
    }
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public CustomInlineAutoDataAttribute(params object[] args) : base(FixtureHelpers.CreateFixture, args)
    {
    }
}

internal static class FixtureHelpers
{
    public static IFixture CreateFixture()
    {
        var fixture = new Fixture();

        fixture.Customize(new AutoMoqCustomization
        {
            ConfigureMembers = true,
            GenerateDelegates = true
        });

        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customizations.Add(new TypeRelay(typeof(IReadOnlySet<>), typeof(HashSet<>)));

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AnimalMappingProfile());
        });
        fixture.Register<IMapper>(() => new Mapper(config));

            
        return fixture;
    }
}