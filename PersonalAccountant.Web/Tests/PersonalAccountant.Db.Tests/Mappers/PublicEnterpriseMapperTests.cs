using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Tests.Mappers;

[TestFixture]
public sealed class PublicEnterpriseMapperTests
{
    private readonly Fixture _fixture = new();

    [Test]
    [TestCase(PublicEnterpriseType.Electricity)]
    [TestCase(PublicEnterpriseType.Water)]
    [TestCase(PublicEnterpriseType.Gas)]
    [TestCase(PublicEnterpriseType.GasDelivery)]
    [TestCase(PublicEnterpriseType.Garbage)]
    public void GIVEN_PublicEnterpriseObject_WHEN_MapCalled_THEN_PublicEnterpriseDtoCreated(PublicEnterpriseType expectedType)
    {
        // Arrange
        var publicEnterprise = _fixture.Build<PublicEnterpriseDto>()
            .With(x => x.Type, expectedType)
            .Create();

        var mapper = new PublicEnterpriseMapper();

        // Act
        var result = mapper.Map(publicEnterprise);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PublicEnterprise>();
        result.Id.Should().Be(publicEnterprise.Id);
        result.Type.Should().Be(publicEnterprise.Type);
        result.State.Should().Be(publicEnterprise.State);
        result.City.Should().Be(publicEnterprise.City);
        result.Name.Should().Be(publicEnterprise.Name);
    }

    [Test]
    public void GIVEN_Null_WHEN_MapCalled_THEN_ArgumentNullExceptionThrown()
    {
        // Arrange
        var mapper = new PublicEnterpriseMapper();

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => mapper.Map((PublicEnterpriseDto) null));

        // Assert
        exception!.ParamName.Should().Be("publicEnterpriseDto");
    }

}