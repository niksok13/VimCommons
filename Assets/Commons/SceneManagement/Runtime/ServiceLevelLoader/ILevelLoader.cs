using System.Threading.Tasks;

namespace Commons.SceneManagement.Runtime.ServiceLevelLoader
{
    public interface ILevelLoader
    {
        Task LoadLevel(int level);
    }
}