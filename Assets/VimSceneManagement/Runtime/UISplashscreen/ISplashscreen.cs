
using System.Threading.Tasks;

namespace ModuleSceneManagement.UISplashscreen
{
    public interface ISplashscreen
    {
        Task Show(string label);
        void Hide();
    }
}