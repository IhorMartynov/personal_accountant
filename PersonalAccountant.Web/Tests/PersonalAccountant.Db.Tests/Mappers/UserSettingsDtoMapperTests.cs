using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Tests.Mappers;

[TestFixture]
public sealed class UserSettingsDtoMapperTests
{
    private readonly Fixture _fixture = new();

    [Test]
    public void GIVEN_UserSettingsObject_WHEN_MapCalled_THEN_UserSettingsDtoCreated()
    {
        // Arrange
        const string unprotectedGasPassword = "gas password";
        const string unprotectedWaterPassword = "water password";
        const string unprotectedElectricityPassword = "electricity password";
        var userSettings = _fixture.Build<UserSettings>()
            .With(x => x.GasAccount, _fixture.Build<GasAccountSettings>()
                .With(x => x.EncryptedPassword, Convert.ToBase64String(GetBytes(unprotectedGasPassword)))
                .Create())
            .With(x => x.WaterAccount, _fixture.Build<WaterAccountSettings>()
                .With(x => x.EncryptedPassword, Convert.ToBase64String(GetBytes(unprotectedWaterPassword)))
                .Create())
            .With(x => x.ElectricityAccount, _fixture.Build<ElectricityAccountSettings>()
                .With(x => x.EncryptedPassword, Convert.ToBase64String(GetBytes(unprotectedElectricityPassword)))
                .Create())
            .Create();

        var dataProtectorProviderMock = new Mock<IDataProtectionProvider>();
        var dataProtectorMock = new Mock<IDataProtector>();

        dataProtectorProviderMock.Setup(provider => provider.CreateProtector(It.Is<string>(x => x == "UserSettings")))
            .Returns(dataProtectorMock.Object)
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Unprotect(It.Is<byte[]>(x => GetString(x) == unprotectedGasPassword)))
            .Returns(GetBytes(unprotectedGasPassword))
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Unprotect(It.Is<byte[]>(x => GetString(x) == unprotectedWaterPassword)))
            .Returns(GetBytes(unprotectedWaterPassword))
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Unprotect(It.Is<byte[]>(x => GetString(x) == unprotectedElectricityPassword)))
            .Returns(GetBytes(unprotectedElectricityPassword))
            .Verifiable();

        var mapper = new UserSettingsDtoMapper(dataProtectorProviderMock.Object);

        // Act
        var result = mapper.Map(userSettings);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserSettingsDto>();
        result.Email.Should().Be(userSettings.Email);
        result.Name.Should().Be(userSettings.Name);
        result.GasAccount.Should().NotBeNull();
        result.GasAccount.Should().BeOfType<GasAccountSettingsDto>();
        result.GasAccount.PublicEnterpriseId.Should().Be(userSettings.GasAccount.PublicEnterpriseId);
        result.GasAccount.AccountNumber.Should().Be(userSettings.GasAccount.AccountNumber);
        result.GasAccount.Login.Should().Be(userSettings.GasAccount.Login);
        result.GasAccount.Password.Should().Be(unprotectedGasPassword);
        result.WaterAccount.Should().NotBeNull();
        result.WaterAccount.Should().BeOfType<WaterAccountSettingsDto>();
        result.WaterAccount.PublicEnterpriseId.Should().Be(userSettings.WaterAccount.PublicEnterpriseId);
        result.WaterAccount.AccountNumber.Should().Be(userSettings.WaterAccount.AccountNumber);
        result.WaterAccount.Login.Should().Be(userSettings.WaterAccount.Login);
        result.WaterAccount.Password.Should().Be(unprotectedWaterPassword);
        result.ElectricityAccount.Should().NotBeNull();
        result.ElectricityAccount.Should().BeOfType<ElectricityAccountSettingsDto>();
        result.ElectricityAccount.PublicEnterpriseId.Should().Be(userSettings.ElectricityAccount.PublicEnterpriseId);
        result.ElectricityAccount.AccountNumber.Should().Be(userSettings.ElectricityAccount.AccountNumber);
        result.ElectricityAccount.Login.Should().Be(userSettings.ElectricityAccount.Login);
        result.ElectricityAccount.Password.Should().Be(unprotectedElectricityPassword);

        dataProtectorProviderMock.Verify();
        dataProtectorMock.Verify();
    }

    [Test]
    public void GIVEN_Null_WHEN_MapCalled_THEN_ArgumentNullExceptionThrown()
    {
        // Arrange
        var dataProtectorProviderMock = new Mock<IDataProtectionProvider>();
        var dataProtectorMock = new Mock<IDataProtector>();

        dataProtectorProviderMock.Setup(provider => provider.CreateProtector(It.Is<string>(x => x == "UserSettings")))
            .Returns(dataProtectorMock.Object)
            .Verifiable();

        var mapper = new UserSettingsDtoMapper(dataProtectorProviderMock.Object);

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => mapper.Map((UserSettings) null));

        // Assert
        exception!.ParamName.Should().Be("userSettings");
    }

    private static string GetString(byte[] bytes) =>
        Encoding.UTF8.GetString(bytes);

    private static byte[] GetBytes(string text)
    {
        var bytesCount = Encoding.UTF8.GetByteCount(text);
        var bytes = new byte[bytesCount];

        Encoding.UTF8.GetBytes(text, bytes);

        return bytes;
    }
}