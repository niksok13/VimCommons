
using System.Threading.Tasks;

namespace VimCommons.SceneManagement.Runtime.UISplashscreen
{
    public interface ISplashscreen
    {
        Task Show(string label);
        void Hide();
    }
}