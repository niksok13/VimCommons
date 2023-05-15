
using System.Threading.Tasks;

namespace VimSceneManagement.Runtime.UISplashscreen
{
    public interface ISplashscreen
    {
        Task Show(string label);
        void Hide();
    }
}