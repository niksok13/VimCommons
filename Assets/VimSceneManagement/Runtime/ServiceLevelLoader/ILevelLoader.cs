using System;
using System.Threading.Tasks;

namespace VimSceneManagement.Runtime.ServiceLevelLoader
{
    public interface ILevelLoader
    {
        event Action OnUnload;
        Task LoadLevel(int level);
    }
}