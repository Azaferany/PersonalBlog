using System.Threading.Tasks;

namespace PersonalBlog.Application.Contracts
{
    public interface ISmsSender
    {
        #region BaseClass

        Task SendSmsAsync(string number, string message);

        #endregion

        #region CustomMethods

        #endregion
    }
}