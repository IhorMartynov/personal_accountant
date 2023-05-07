using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using PersonalAccountant.Db.Contracts.Models;
using PersonalAccountant.Db.Mappers;
using PersonalAccountant.Db.Models;

namespace PersonalAccountant.Db.Tests.Mappers;

[TestFixture]
public sealed class UserSettingsMapperTests
{
    private readonly Fixture _fixture = new();

    [Test]
    public void GIVEN_UserSettingsDtoObject_WHEN_MapCalled_THEN_UserSettingsCreated()
    {
        // Arrange
        const string unprotectedGasPassword = "gas password";
        const string unprotectedWaterPassword = "water password";
        const string unprotectedElectricityPassword = "electricity password";
        const string protectedGasPassword = "Z2FzIHBhc3N3b3Jk";
        const string protectedWaterPassword = "d2F0ZXIgcGFzc3dvcmQ";
        const string protectedElectricityPassword = "ZWxlY3RyaWNpdHkgcGFzc3dvcmQ";
        var userSettings = _fixture.Build<UserSettingsDto>()
            .With(x => x.GasAccount, _fixture.Build<GasAccountSettingsDto>()
                .With(x => x.Password, unprotectedGasPassword)
                .Create())
            .With(x => x.WaterAccount, _fixture.Build<WaterAccountSettingsDto>()
                .With(x => x.Password, unprotectedWaterPassword)
                .Create())
            .With(x => x.ElectricityAccount, _fixture.Build<ElectricityAccountSettingsDto>()
                .With(x => x.Password, unprotectedElectricityPassword)
                .Create())
            .Create();

        var dataProtectorProviderMock = new Mock<IDataProtectionProvider>();
        var dataProtectorMock = new Mock<IDataProtector>();

        dataProtectorProviderMock.Setup(provider => provider.CreateProtector(It.Is<string>(x => x == "UserSettings")))
            .Returns(dataProtectorMock.Object)
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Protect(It.Is<byte[]>(x => GetString(x) == unprotectedGasPassword)))
            .Returns(GetBytes(unprotectedGasPassword))
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Protect(It.Is<byte[]>(x => GetString(x) == unprotectedWaterPassword)))
            .Returns(GetBytes(unprotectedWaterPassword))
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Protect(It.Is<byte[]>(x => GetString(x) == unprotectedElectricityPassword)))
            .Returns(GetBytes(unprotectedElectricityPassword))
            .Verifiable();

        var mapper = new UserSettingsMapper(dataProtectorProviderMock.Object);

        // Act
        var result = mapper.Map(userSettings);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserSettings>();
        result.Id.Should().NotBeNullOrEmpty();
        result.Email.Should().Be(userSettings.Email);
        result.Name.Should().Be(userSettings.Name);
        result.GasAccount.Should().NotBeNull();
        result.GasAccount.Should().BeOfType<GasAccountSettings>();
        result.GasAccount.PublicEnterpriseId.Should().Be(userSettings.GasAccount.PublicEnterpriseId);
        result.GasAccount.AccountNumber.Should().Be(userSettings.GasAccount.AccountNumber);
        result.GasAccount.Login.Should().Be(userSettings.GasAccount.Login);
        result.GasAccount.EncryptedPassword.Should().Be(protectedGasPassword);
        result.WaterAccount.Should().NotBeNull();
        result.WaterAccount.Should().BeOfType<WaterAccountSettings>();
        result.WaterAccount.PublicEnterpriseId.Should().Be(userSettings.WaterAccount.PublicEnterpriseId);
        result.WaterAccount.AccountNumber.Should().Be(userSettings.WaterAccount.AccountNumber);
        result.WaterAccount.Login.Should().Be(userSettings.WaterAccount.Login);
        result.WaterAccount.EncryptedPassword.Should().Be(protectedWaterPassword);
        result.ElectricityAccount.Should().NotBeNull();
        result.ElectricityAccount.Should().BeOfType<ElectricityAccountSettings>();
        result.ElectricityAccount.PublicEnterpriseId.Should().Be(userSettings.ElectricityAccount.PublicEnterpriseId);
        result.ElectricityAccount.AccountNumber.Should().Be(userSettings.ElectricityAccount.AccountNumber);
        result.ElectricityAccount.Login.Should().Be(userSettings.ElectricityAccount.Login);
        result.ElectricityAccount.EncryptedPassword.Should().Be(protectedElectricityPassword);

        dataProtectorProviderMock.Verify();
        dataProtectorMock.Verify();
    }

    [Test]
    public void GIVEN_IdAndUserSettingsDtoObject_WHEN_MapCalled_THEN_UserSettingsCreated()
    {
        // Arrange
        const string id = "some identifier";
        const string unprotectedGasPassword = "gas password";
        const string unprotectedWaterPassword = "water password";
        const string unprotectedElectricityPassword = "electricity password";
        const string protectedGasPassword = "Z2FzIHBhc3N3b3Jk";
        const string protectedWaterPassword = "d2F0ZXIgcGFzc3dvcmQ";
        const string protectedElectricityPassword = "ZWxlY3RyaWNpdHkgcGFzc3dvcmQ";
        var userSettings = _fixture.Build<UserSettingsDto>()
            .With(x => x.GasAccount, _fixture.Build<GasAccountSettingsDto>()
                .With(x => x.Password, unprotectedGasPassword)
                .Create())
            .With(x => x.WaterAccount, _fixture.Build<WaterAccountSettingsDto>()
                .With(x => x.Password, unprotectedWaterPassword)
                .Create())
            .With(x => x.ElectricityAccount, _fixture.Build<ElectricityAccountSettingsDto>()
                .With(x => x.Password, unprotectedElectricityPassword)
                .Create())
            .Create();

        var dataProtectorProviderMock = new Mock<IDataProtectionProvider>();
        var dataProtectorMock = new Mock<IDataProtector>();

        dataProtectorProviderMock.Setup(provider => provider.CreateProtector(It.Is<string>(x => x == "UserSettings")))
            .Returns(dataProtectorMock.Object)
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Protect(It.Is<byte[]>(x => GetString(x) == unprotectedGasPassword)))
            .Returns(GetBytes(unprotectedGasPassword))
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Protect(It.Is<byte[]>(x => GetString(x) == unprotectedWaterPassword)))
            .Returns(GetBytes(unprotectedWaterPassword))
            .Verifiable();
        dataProtectorMock.Setup(protector =>
                protector.Protect(It.Is<byte[]>(x => GetString(x) == unprotectedElectricityPassword)))
            .Returns(GetBytes(unprotectedElectricityPassword))
            .Verifiable();

        var mapper = new UserSettingsMapper(dataProtectorProviderMock.Object);

        // Act
        var result = mapper.Map(id, userSettings);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserSettings>();
        result.Id.Should().Be(id);
        result.Email.Should().Be(userSettings.Email);
        result.Name.Should().Be(userSettings.Name);
        result.GasAccount.Should().NotBeNull();
        result.GasAccount.Should().BeOfType<GasAccountSettings>();
        result.GasAccount.PublicEnterpriseId.Should().Be(userSettings.GasAccount.PublicEnterpriseId);
        result.GasAccount.AccountNumber.Should().Be(userSettings.GasAccount.AccountNumber);
        result.GasAccount.Login.Should().Be(userSettings.GasAccount.Login);
        result.GasAccount.EncryptedPassword.Should().Be(protectedGasPassword);
        result.WaterAccount.Should().NotBeNull();
        result.WaterAccount.Should().BeOfType<WaterAccountSettings>();
        result.WaterAccount.PublicEnterpriseId.Should().Be(userSettings.WaterAccount.PublicEnterpriseId);
        result.WaterAccount.AccountNumber.Should().Be(userSettings.WaterAccount.AccountNumber);
        result.WaterAccount.Login.Should().Be(userSettings.WaterAccount.Login);
        result.WaterAccount.EncryptedPassword.Should().Be(protectedWaterPassword);
        result.ElectricityAccount.Should().NotBeNull();
        result.ElectricityAccount.Should().BeOfType<ElectricityAccountSettings>();
        result.ElectricityAccount.PublicEnterpriseId.Should().Be(userSettings.ElectricityAccount.PublicEnterpriseId);
        result.ElectricityAccount.AccountNumber.Should().Be(userSettings.ElectricityAccount.AccountNumber);
        result.ElectricityAccount.Login.Should().Be(userSettings.ElectricityAccount.Login);
        result.ElectricityAccount.EncryptedPassword.Should().Be(protectedElectricityPassword);

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

        var mapper = new UserSettingsMapper(dataProtectorProviderMock.Object);

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => mapper.Map((UserSettingsDto) null));

        // Assert
        exception!.ParamName.Should().Be("userSettings");
    }

    [Test]
    public void GIVEN_IdAndNullObject_WHEN_MapCalled_THEN_ArgumentNullExceptionThrown()
    {
        // Arrange
        var dataProtectorProviderMock = new Mock<IDataProtectionProvider>();
        var dataProtectorMock = new Mock<IDataProtector>();

        dataProtectorProviderMock.Setup(provider => provider.CreateProtector(It.Is<string>(x => x == "UserSettings")))
            .Returns(dataProtectorMock.Object)
            .Verifiable();

        var mapper = new UserSettingsMapper(dataProtectorProviderMock.Object);

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => mapper.Map("id", (UserSettingsDto) null));

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