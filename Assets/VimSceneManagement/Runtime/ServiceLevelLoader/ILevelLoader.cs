using System;
using System.Threading.Tasks;

namespace VimSceneManagement.Runtime.ServiceLevelLoader
{
    public interface ILevelLoader
    {
        Task LoadLevel(int level);
    }
}