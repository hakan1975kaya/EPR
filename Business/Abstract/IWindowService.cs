
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IWindowService
    {
        Task<IResult> DoWork();
    }
}
