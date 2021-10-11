
using System.Threading.Tasks;

namespace myPicoAPI.Data
{
    public interface IGeneralStuff
    {
        Task<string> convertCurrency(float amount, string desiredCurrency, string inhandCurrency);
    }
}