using System.Threading.Tasks;

namespace VimCommons.SceneManagement.Runtime.LevelLoader
{
    public interface ILevelLoader
    {
        Task LoadLevel(int level);
    }
}