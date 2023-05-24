using System.Threading.Tasks;

namespace VimCommons.SceneManagement.Runtime.ServiceLevelLoader
{
    public interface ILevelLoader
    {
        Task LoadLevel(int level);
    }
}