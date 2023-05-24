
using System.Threading.Tasks;

namespace Commons.SceneManagement.Runtime.UISplashscreen
{
    public interface ISplashscreen
    {
        Task Show(string label);
        void Hide();
    }
}