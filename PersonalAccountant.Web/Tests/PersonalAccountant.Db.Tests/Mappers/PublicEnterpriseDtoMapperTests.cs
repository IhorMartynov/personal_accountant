using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Tests.Mappers;

[TestFixture]
public sealed class PublicEnterpriseDtoMapperTests
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
        var publicEnterprise = _fixture.Build<PublicEnterprise>()
            .With(x => x.Type, expectedType)
            .Create();

        var mapper = new PublicEnterpriseDtoMapper();

        // Act
        var result = mapper.Map(publicEnterprise);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PublicEnterpriseDto>();
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
        var mapper = new PublicEnterpriseDtoMapper();

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => mapper.Map((PublicEnterprise) null));

        // Assert
        exception!.ParamName.Should().Be("publicEnterprise");
    }

    [Test]
    public void GIVEN_PublicEnterpriseObjects_WHEN_MapCalled_THEN_PublicEnterpriseDtosCreated()
    {
        // Arrange
        var publicEnterprises = _fixture.CreateMany<PublicEnterprise>()
            .ToArray();

        var mapper = new PublicEnterpriseDtoMapper();

        // Act
        var result = mapper.Map(publicEnterprises)
            ?.ToArray();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PublicEnterpriseDto[]>();
        result.Should().HaveCount(publicEnterprises.Length);

        foreach (var dtoItem in result!)
        {
            var expectedItem = publicEnterprises.FirstOrDefault(x => x.Id == dtoItem.Id);

            expectedItem.Should().NotBeNull();
            dtoItem.Id.Should().Be(expectedItem!.Id);
            dtoItem.Type.Should().Be(expectedItem.Type);
            dtoItem.State.Should().Be(expectedItem.State);
            dtoItem.City.Should().Be(expectedItem.City);
            dtoItem.Name.Should().Be(expectedItem.Name);
        }
    }

    [Test]
    public void GIVEN_NullArray_WHEN_MapCalled_THEN_ArgumentNullExceptionThrown()
    {
        // Arrange
        var mapper = new PublicEnterpriseDtoMapper();

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => mapper.Map((IEnumerable<PublicEnterprise>) null));

        // Assert
        exception!.ParamName.Should().Be("publicEnterprises");
    }
}