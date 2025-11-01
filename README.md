# Hmu.cs
Mobile-API for HMU a simple social network to help you gain popularity and build friendships

## Example
```cs
using HmuApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new Hmu();
            await api.Login("example@gmail.com", "password");
            string accountInfo = await api.GetAccountInfo();
            Console.WriteLine(accountInfo);
        }
    }
}
```
