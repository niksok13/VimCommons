using System;
using System.Threading.Tasks;

namespace ModuleSceneManagement.ServiceLevelLoader
{
    public interface ILevelLoader
    {
        event Action OnUnload;
        Task LoadLevel(int level);
    }
}