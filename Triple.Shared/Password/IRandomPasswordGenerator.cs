namespace Triple.Shared.Password
{
    public interface IRandomPasswordGenerator
    {
        string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize);
    }
}