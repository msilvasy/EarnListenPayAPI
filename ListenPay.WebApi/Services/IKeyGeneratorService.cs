namespace ListenPay.WebApi.Services
{
    public interface IKeyGeneratorService
    {
        string GetBase36(int length = 8);
    }
}