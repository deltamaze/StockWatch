using System.Threading.Tasks;

namespace StockWatch
{
    interface IApplication
    {
        Task<int> Run();
    }
}
